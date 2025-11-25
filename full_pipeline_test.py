import os
import time
import wave
import tempfile
import sounddevice as sd
from openai import OpenAI
from elevenlabs.client import ElevenLabs
from elevenlabs.play import play

# =========================================================
#                 API KEYS (Replace these)
# =========================================================
os.environ["OPENAI_API_KEY"] = "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"

os.environ["ELEVENLABS_API_KEY"] = "5bb82f773cb5653474d72cd2840ad928f2cde71262eb242001160514a7e1a983"

# Initialize clients
openai_client = OpenAI(api_key=os.getenv("OPENAI_API_KEY"))
eleven_client = ElevenLabs(api_key=os.getenv("ELEVENLABS_API_KEY"))

# =========================================================
#                 AUDIO RECORDING CONFIG
# =========================================================
SAMPLE_RATE = 48000
CHANNELS = 1
DEVICE_INDEX = 9  # your working microphone device index

recording = False
audio_buffer = []
stream = None


# =========================================================
#                1. MICROPHONE RECORDING
# =========================================================
def start_recording():
    """Start capturing audio from the microphone."""
    global recording, audio_buffer, stream
    audio_buffer = []
    recording = True

    def callback(indata, frames, time_info, status):
        if recording:
            audio_buffer.append(indata.copy())

    print("\nAvailable audio devices:")
    print(sd.query_devices())

    sd.check_input_settings(device=DEVICE_INDEX, samplerate=SAMPLE_RATE)

    stream = sd.InputStream(
        samplerate=SAMPLE_RATE,
        channels=CHANNELS,
        callback=callback,
        dtype="int16",
        device=DEVICE_INDEX,
    )
    stream.start()
    print(" Recording started... Speak now.")


def stop_recording_and_transcribe():
    """Stop recording, save WAV, send to Whisper for transcription."""
    global recording, stream, audio_buffer
    recording = False

    if stream:
        stream.stop()
        stream.close()

    print(" Recording stopped.")

    # Save audio to temp WAV file
    temp_wav = tempfile.NamedTemporaryFile(delete=False, suffix=".wav")
    with wave.open(temp_wav.name, "wb") as wf:
        wf.setnchannels(CHANNELS)
        wf.setsampwidth(2)
        wf.setframerate(SAMPLE_RATE)
        for chunk in audio_buffer:
            wf.writeframes(chunk.tobytes())

    print(f" Audio saved: {temp_wav.name}")

    # Whisper transcription
    with open(temp_wav.name, "rb") as audio_file:
        transcription = openai_client.audio.transcriptions.create(
            model="whisper-1",
            file=audio_file
        )

    text = transcription.text.strip()
    print(f" Transcription: {text}")
    return text


# =========================================================
#                2. GPT-4o-mini PROCESSING
# =========================================================
def process_text_with_gpt(text, location="Unknown Location"):
    """Send Whisper text → GPT-4o-mini."""
    print("\n Sending text to GPT-4o-mini...")
    print(f"User said: {text}")

    response = openai_client.chat.completions.create(
        model="gpt-4o-mini",
        messages=[
            {
                "role": "system",
                "content": f"You are a VR assistant located in {location}. "
                           f"Respond in clear, simple language. Output will be spoken aloud."
            },
            {"role": "user", "content": text}
        ]
    )

    reply = response.choices[0].message.content.strip()
    print(f"GPT Reply: {reply}")
    return reply


# =========================================================
#                3. ELEVENLABS TTS OUTPUT
# =========================================================
def speak_with_elevenlabs(text, voice_id="JBFqnCBsd6RMkjVDRZzb"):
    """Convert GPT response into speech + playback."""
    print("\nSending text to ElevenLabs TTS...")

    audio = eleven_client.text_to_speech.convert(
        text=text,
        voice_id=voice_id,
        model_id="eleven_multilingual_v2",
        output_format="mp3_44100_128"
    )

    # Play audio directly
    play(audio)
    print("Playback complete.")


# =========================================================
#            VR TRIGGER SIMULATIONS (single pipeline)
# =========================================================
def full_speech_to_speech_pipeline():
    print("\n=== Hold button & speak for 5 seconds ===")
    start_recording()
    time.sleep(5)
    text = stop_recording_and_transcribe()

    reply = process_text_with_gpt(text, "Glacier National Park")
    speak_with_elevenlabs(reply)


# =========================================================
#                      MAIN TEST
# =========================================================
if __name__ == "__main__":
    print("\nStarting Full Speech-to-Speech Test Pipeline...")
    full_speech_to_speech_pipeline()
    print("\nTest complete.")
