class Settings:
    API_V1_STR: str = "/api/v1"
    # TAWQI3I_ESIGNER_API_URL: str = "https://api.tawqi3i.com/api/v1/"
    TAWQI3I_ESIGNER_API_URL_V: str = "http://localhost:5242" + API_V1_STR
    TAWQI3I_ESIGNER_API_URL: str = "http://localhost:5242"
    REDIRECT_URI: str = "http://127.0.0.1:8000/api/v1/esigner/callback"
    CLIENT_ID: str = "60909121386e49f38d13b15bc578ead4"
    CLIENT_SECRET: str = "adabcc5b35484d6c9e7829f554343528"


settings = Settings()
