from fastapi import APIRouter

from app.routes import esigner, health, esigner2


api_router = APIRouter()

api_router.include_router(esigner.router)
api_router.include_router(esigner2.router)
api_router.include_router(health.router)
