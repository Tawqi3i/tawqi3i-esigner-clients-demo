from fastapi import APIRouter


router = APIRouter(prefix="/esigner", tags=["tawqi3i esigner"])

@router.get("/")
async def get_all():
    return {"message": "ESigner endpoint is working"}

# @router.get("/")
# async def get_all(session: SessionDep) -> list[Album]:
#     statement = select(Album).limit(10)
#     results = session.exec(statement)
#     return results

# @router.post("/")
# async def create(body: AlbumCreate, session: SessionDep) -> Album:
#     a = Album(Title=body.title, ArtistId=body.artist_id)
#     session.add(a)
#     session.commit()
#     session.refresh(a)
#     return a
