from fastapi import FastAPI, HTTPException
from fastapi.middleware.cors import CORSMiddleware
from pydantic import BaseModel
from typing import List
import sqlite3

from app.db import init_db, seed_data, get_tour_from_db

# =====================================
# APP SETUP
# =====================================

app = FastAPI(
    title="AgriMeta Backend API",
    description="AI-powered virtual tours and agricultural simulation backend.",
    version="2.0.0"
)

# =====================================
# CORS (FRONTEND CONNECTION)
# =====================================

app.add_middleware(
    CORSMiddleware,
    allow_origins=[
        "http://localhost:5500",
        "http://127.0.0.1:5500",
        "http://localhost:3000"
    ],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

DB_PATH = "agri.db"

# =====================================
# RESPONSE MODELS
# =====================================

class Tour(BaseModel):
    id: int
    name: str
    description: str
    video_url: str
    source: str


class UserHistory(BaseModel):
    topics: List[str]


class AIRecommendationResponse(BaseModel):
    status: str
    input_topics: List[str]
    recommended_topics: List[str]
    ai_model: str

# =====================================
# STARTUP (THIS SEEDS THE DATABASE)
# =====================================

@app.on_event("startup")
def startup():
    init_db()
    seed_data()

# =====================================
# API ROUTES
# =====================================

@app.get("/api/tours/{tour_id}", response_model=Tour)
def get_tour(tour_id: int):
    row = get_tour_from_db(tour_id)

    if not row:
        raise HTTPException(status_code=404, detail="Tour not found")

    return {
        "id": row[0],
        "name": row[1],
        "description": row[2],
        "video_url": row[3],
        "source": "database",
    }


@app.get("/api/tours", response_model=List[Tour])
def list_tours():
    conn = sqlite3.connect(DB_PATH)
    cursor = conn.cursor()
    cursor.execute("SELECT id, name, description, video_url FROM tours")
    rows = cursor.fetchall()
    conn.close()

    return [
        {
            "id": row[0],
            "name": row[1],
            "description": row[2],
            "video_url": row[3],
            "source": "database",
        }
        for row in rows
    ]


@app.delete("/api/tours/{tour_id}")
def delete_tour_endpoint(tour_id: int):
    conn = sqlite3.connect(DB_PATH)
    cursor = conn.cursor()
    cursor.execute("DELETE FROM tours WHERE id = ?", (tour_id,))
    conn.commit()
    deleted = cursor.rowcount
    conn.close()

    if deleted == 0:
        raise HTTPException(status_code=404, detail="Tour not found")

    return {"message": "Tour deleted"}


@app.post("/api/ai/recommend", response_model=AIRecommendationResponse)
def ai_recommend(data: UserHistory):
    topics = data.topics
    recommendations = []

    if "irrigation" in topics or "water" in topics:
        recommendations += [
            "Hydroponic Greenhouse Lab",
            "Advanced Irrigation Management"
        ]

    if "soil" in topics or "sustainable" in topics:
        recommendations += [
            "Soil Regeneration 101",
            "Cover Crop VR Experience"
        ]

    if "technology" in topics:
        recommendations += [
            "Drone Farm Mapping Simulation",
            "AI Crop Disease Lab"
        ]

    if not recommendations:
        recommendations = [
            "Climate-Resilient Farming Basics",
            "Organic Farming Introduction",
            "AI in Modern Agriculture"
        ]

    return {
        "status": "success",
        "input_topics": topics,
        "recommended_topics": recommendations,
        "ai_model": "AgriMeta-Recommendation-Engine-v0.3",
    }


@app.get("/health")
def health():
    return {"status": "ok"}


@app.get("/")
def root():
    return {
        "message": "AgriMeta Backend API is running",
        "docs": "/docs",
        "tours": "/api/tours",
        "ai": "/api/ai/recommend"
    }
