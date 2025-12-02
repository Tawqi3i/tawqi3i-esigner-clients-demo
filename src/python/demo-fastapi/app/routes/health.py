from fastapi import APIRouter


router = APIRouter(prefix="/health", tags=["tawqi3i esigner"])


@router.get("/")
async def get_all():
    return {"message": "The app is working"}
