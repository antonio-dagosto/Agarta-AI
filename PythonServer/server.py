import os
import uuid
from fastapi import FastAPI, UploadFile, File
from fastapi.responses import JSONResponse
from fastapi.staticfiles import StaticFiles
from openai import OpenAI
from elevenlabs.client import ElevenLabs

# --------------------
# CONFIG
# --------------------
BASE_DIR = os.path.dirname(__file__)
AUDIO_DIR = os.path.join(BASE_DIR, "audio_out")
os.makedirs(AUDIO_DIR, exist_ok=True)

openai_client = OpenAI(api_key=os.getenv("OPENAI_API_KEY"))
eleven_client = ElevenLabs(api_key=os.getenv("ELEVENLABS_API_KEY"))

app = FastAPI()
app.mount("/audio", StaticFiles(directory=AUDIO_DIR), name="audio")

# --------------------
# ENDPOINT
# --------------------
@app.post("/speech")
async def speech(file: UploadFile = File(...)):
    audio_id = str(uuid.uuid4())
    wav_path = os.path.join(AUDIO_DIR, f"{audio_id}.wav")

    with open(wav_path, "wb") as f:
        f.write(await file.read())

    # Whisper
    with open(wav_path, "rb") as audio_file:
        transcript = openai_client.audio.transcriptions.create(
            model="whisper-1",
            file=audio_file
        )

    user_text = transcript.text.strip()

    # GPT
    chat = openai_client.chat.completions.create(
        model="gpt-4o-mini",
        messages=[
            {"role": "system", "content": "You are a helpful VR assistant."},
            {"role": "user", "content": user_text}
        ]
    )
    reply = chat.choices[0].message.content.strip()

    # ElevenLabs
    mp3_path = os.path.join(AUDIO_DIR, f"{audio_id}.mp3")
    audio_stream = eleven_client.text_to_speech.convert(
        text=reply,
        voice_id="JBFqnCBsd6RMkjVDRZzb",
        model_id="eleven_multilingual_v2",
        output_format="mp3_44100_128"
    )

    with open(mp3_path, "wb") as f:
        for chunk in audio_stream:
            f.write(chunk)

    return JSONResponse({
        "text": reply,
        "audio_url": f"/audio/{audio_id}.mp3"
    })
