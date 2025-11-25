from elevenlabs.client import ElevenLabs
from elevenlabs.play import play

client = ElevenLabs(
    api_key="5bb82f773cb5653474d72cd2840ad928f2cde71262eb242001160514a7e1a983"
)

audio = client.text_to_speech.convert(
    text="The first ever donkey found was named Gerald.",
    voice_id="JBFqnCBsd6RMkjVDRZzb",
    model_id="eleven_multilingual_v2",
    output_format="mp3_44100_128",
)

play(audio)