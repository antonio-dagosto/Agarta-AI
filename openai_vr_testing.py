import os
from openai import OpenAI
import time

# IMPORTANT:
# In production, store this securely (e.g., env variable or backend token server).
# For demo/testing only:
os.environ["OPENAI_API_KEY"] = "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"

# Initialize OpenAI client
client = OpenAI(api_key=os.getenv("OPENAI_API_KEY"))

# Simulated VR interaction state
processing = False

def process_text_input(transcribed_text: str, location_text: str):
    """
    Take in a transcribed text string (from Whisper or another source),
    send it to GPT-4o-mini, and return the model's response.
    """
    global processing
    processing = True
    print("\n--- Processing input with GPT-4o-mini ---")
    print(f"User said: {transcribed_text}")
    print(f"You are in: {location_text}")
    # API call to GPT-4o-mini
    response = client.chat.completions.create(
        model="gpt-4o-mini",
        messages=[
            {"role": "system", "content": f"""You are a helpful virtual assistant integrated into a VR environment. 
            You are in {location_text}. Please answer their question concisely in up to 6 sentences if necessary, 
            but less if possible. Use simple language and easy to understand sentence formatting. 
            This will be given to the user as audio later."""},
            {"role": "user", "content": transcribed_text}
        ]
    )

    ai_reply = response.choices[0].message.content.strip()
    processing = False
    return ai_reply


# VR button simulation (simulating when transcribed text is ready)
def on_text_ready(transcribed_text: str, location_text: str):
    """Triggered when Whisper output text is available."""
    ai_reply = process_text_input(transcribed_text, location_text)
    # In VR, this could trigger ElevenLabs to speak the reply
    print(f"GPT-4o-mini Response: {ai_reply}")


# Demo run
if __name__ == "__main__":
    print("Simulating text input from Whisper transcription...")
    time.sleep(1)
    
    # Simulated Whisper output (in production, replace this with actual transcription)
    simulated_whisper_output = "What is the most common animal to see in this park?"
    location_info = "Glacier National Park"
    on_text_ready(simulated_whisper_output,location_info)
