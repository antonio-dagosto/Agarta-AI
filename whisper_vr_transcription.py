import os
import sounddevice as sd
import wave
import time
import tempfile
from openai import OpenAI

# IMPORTANT:
# In production, store this securely (e.g., env variable, backend token server).
# For demo/testing only:
os.environ["OPENAI_API_KEY"] = "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"

# Initialize OpenAI Client
client = OpenAI(api_key=os.getenv("OPENAI_API_KEY"))

# Recording config
SAMPLE_RATE = 48000
CHANNELS = 1
recording = False
audio_buffer = []
stream = None

def start_recording():
    """Start capturing audio from the microphone."""
    global recording, audio_buffer, stream
    audio_buffer = []
    recording = True

    def callback(indata, frames, time_info, status):
        if recording:
            audio_buffer.append(indata.copy())
    
    print(sd.query_devices())
    sd.check_input_settings(device=9, samplerate=48000)
    stream = sd.InputStream(
        samplerate=SAMPLE_RATE,
        channels=CHANNELS,
        callback=callback,
        dtype='int16',
        device=9
    )
    stream.start()
    print("Recording started... (press and hold VR button)")

def stop_recording_and_transcribe():
    """Stop recording, save to WAV, and call Whisper API."""
    global recording, stream, audio_buffer
    recording = False
    if stream:
        stream.stop()
        stream.close()
    print("Recording stopped.")

    # Save audio to a temp WAV file
    temp_wav = tempfile.NamedTemporaryFile(delete=False, suffix=".wav")
    with wave.open(temp_wav.name, 'wb') as wf:
        wf.setnchannels(CHANNELS)
        wf.setsampwidth(2)  # 16-bit PCM
        wf.setframerate(SAMPLE_RATE)
        for chunk in audio_buffer:
            wf.writeframes(chunk.tobytes())
    print(f"Audio saved to {temp_wav.name}")

    # API CALL to Whisper
    with open(temp_wav.name, "rb") as audio_file:
        transcription = client.audio.transcriptions.create(
            model="whisper-1",
            file=audio_file
        )

    text = transcription.text
    print(f"Transcription: {text}")
    return text

# VR button hooks (simulated for this demo)
def on_button_press():
    start_recording()

def on_button_release():
    text = stop_recording_and_transcribe()
    # Here you could display captions or run commands in VR
    print(f"VR Caption: {text}")

# Demo run
if __name__ == "__main__":
    print("Simulating VR button press for 5 seconds...")
    on_button_press()
    time.sleep(5)  # simulate holding down button to talk
    on_button_release()
