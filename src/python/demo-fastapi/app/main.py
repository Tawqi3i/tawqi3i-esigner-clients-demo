from app.settings import settings
from fastapi import FastAPI
from app.routes import api_router

app = FastAPI(title="ESignerApiDemo", debug=True)


@app.get("/")
async def root():
    return {"message": "Hello from ESignerApiDemo!"}


app.include_router(api_router, prefix=settings.API_V1_STR)
