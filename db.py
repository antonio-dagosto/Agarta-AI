import sqlite3
import os

DB_PATH = "agri.db"

# Initialize the database if it doesn't exist
def init_db():
    create_table = """
    CREATE TABLE IF NOT EXISTS tours (
        id INTEGER PRIMARY KEY,
        name TEXT,
        description TEXT,
        video_url TEXT
    );
    """

    conn = sqlite3.connect(DB_PATH)
    cursor = conn.cursor()
    cursor.execute(create_table)
    conn.commit()
    conn.close()

# Insert sample data if empty
def seed_data():
    conn = sqlite3.connect(DB_PATH)
    cursor = conn.cursor()

    cursor.execute("SELECT COUNT(*) FROM tours;")
    count = cursor.fetchone()[0]

    if count == 0:
        cursor.execute("""
            INSERT INTO tours (name, description, video_url)
            VALUES 
            ('Sustainable Wheat Farm – Sunrise Tour',
             'A 360° HDR tour showing irrigation, soil sensors, and crop rotation.',
             'https://example.com/video/wheat_360.mp4');
        """)
        conn.commit()

    conn.close()

# Helper to fetch a tour
def get_tour_from_db(tour_id: int):
    conn = sqlite3.connect(DB_PATH)
    cursor = conn.cursor()

    cursor.execute("SELECT * FROM tours WHERE id = ?", (tour_id,))
    row = cursor.fetchone()

    conn.close()
    return row
