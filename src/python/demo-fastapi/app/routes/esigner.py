from typing_extensions import Annotated
from fastapi import APIRouter, Query, status
from fastapi.responses import JSONResponse, Response
import httpx
from app.dto import CallbackParams, SanadInitRequest, SanadInitResponse
from app.settings import settings

router = APIRouter(prefix="/esigner", tags=["tawqi3i esigner"])

ACCESS_TOKEN = ""  # store access token here for demo purposes only


@router.post("auth")
def auth() -> Response:
    data = {
        "client_id": settings.CLIENT_ID,
        "client_secret": settings.CLIENT_SECRET,
        "redirect_uri": settings.REDIRECT_URI,
        "grant_type": "client_credentials",
    }

    resp = httpx.post(
        settings.TAWQI3I_ESIGNER_API_URL + "oauth20/token",
        data=data,  # application/x-www-form-urlencoded
    )

    if resp.status_code != 200:
        return JSONResponse(
            content={"message": "Authentication failed"},
            status_code=status.HTTP_401_UNAUTHORIZED,
        )

    ACCESS_TOKEN = resp.json().get("access_token")

    return JSONResponse(content=None, status_code=status.HTTP_200_OK)


@router.post("sanad/init")
def sanad_init() -> Response:

    data = SanadInitRequest(
        NationalId="1234567890",
        RedirectUrl=settings.REDIRECT_URI + "/esigner/callback",
        SigningPage=None,
    )

    resp = httpx.post(
        settings.TAWQI3I_ESIGNER_API_URL + "sanad/init", data=data.__dict__
    )

    if resp.status_code != 200:
        return JSONResponse(
            content={"message": "Authentication failed"},
            status_code=status.HTTP_401_UNAUTHORIZED,
        )

    obj = SanadInitResponse(**resp.json())

    return JSONResponse(content=obj, status_code=status.HTTP_200_OK)


@router.post("sign/advanced")
def sign_advanced():
    return {"message": "Sign advanced endpoint"}


@router.get("callback")
def callback(query: Annotated[CallbackParams, Query()]):
    print(query)

    if query.Error != "" and query.Error is not None:
        return JSONResponse(
            content={"message": "An error occured"},
            status_code=status.HTTP_400_BAD_REQUEST,
        )

    if query.PinVerifyUrl != "" and query.PinVerifyUrl is not None:
        # this.Redirect(query.PinVerifyUrl);
        pass

    if query.ReadyToSign != "" and query.SanadStatusUrl is not None:
        # this.Redirect($"{settings.SignPageUrl}/{query.SessionId}");
        pass

    return JSONResponse(
        content={"message": "Authentication failed"},
        status_code=status.HTTP_401_UNAUTHORIZED,
    )
