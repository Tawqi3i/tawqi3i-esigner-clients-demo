from fastapi import APIRouter
import httpx

from app.settings import settings

router = APIRouter(prefix="/health", tags=["tawqi3i esigner"])


@router.get("/")
async def get_all():
    return {"message": "The app is working"}


@router.get("/ping")
async def ping():
    async with httpx.AsyncClient() as client:
        response = await client.get(
            "/".join((settings.TAWQI3I_ESIGNER_API_URL, "health"))
        )
        print(response)
