import json
from typing_extensions import Annotated
from fastapi import APIRouter, Query, status
from fastapi.responses import JSONResponse, RedirectResponse, Response
import httpx
from app.dto import CallbackParams, SanadInitRequest, SignRequest, SanadSignRequest
from app.settings import settings

router = APIRouter(prefix="/esigner", tags=["esigner"])

ACCESS_TOKEN = ""  # store access token here for demo purposes only


@router.post("/login")
def auth() -> Response:
    """
    Service to service authentication.
    """

    data = {
        "client_id": settings.CLIENT_ID,
        "client_secret": settings.CLIENT_SECRET,
        "redirect_uri": settings.REDIRECT_URI,
        "grant_type": "client_credentials",
    }

    resp = httpx.post(
        "/".join((settings.TAWQI3I_ESIGNER_API_URL_V, "oauth20/token")),
        data=data,  # application/x-www-form-urlencoded
        timeout=10.0,
    )

    # response.raise_for_status()
    if resp.status_code != 200:
        return JSONResponse(
            content={"message": "Authentication failed"},
            status_code=status.HTTP_401_UNAUTHORIZED,
        )

    global ACCESS_TOKEN
    ACCESS_TOKEN = resp.json().get("access_token")

    return JSONResponse(content={}, status_code=status.HTTP_200_OK)


@router.post("/sanad/init")
def sanad_init(req: SanadInitRequest) -> Response:
    """
    Initialise SANAD session for end user.
    """
    print(json.dumps(req.__dict__, indent=4))

    body = SanadInitRequest(
        nationalId=req.nationalId,
        redirectUrl=settings.REDIRECT_URI,
        signingPage=req.signingPage,
    )

    global ACCESS_TOKEN
    if ACCESS_TOKEN == "":
        return JSONResponse(
            content={"message": "Authentication required"},
            status_code=status.HTTP_401_UNAUTHORIZED,
        )

    url = "/".join((settings.TAWQI3I_ESIGNER_API_URL_V, "sanad/init"))
    data = json.dumps(body.__dict__, indent=4)

    resp = httpx.post(
        url,
        data=data,
        headers=get_request_headers(),
        timeout=10.0,
    )

    if resp.status_code != status.HTTP_200_OK:
        return JSONResponse(
            content={"message": "Authentication failed"},
            status_code=resp.status_code,
        )

    return JSONResponse(content=resp.json(), status_code=status.HTTP_200_OK)


@router.post("/sign")
def sign_advanced(req: SanadSignRequest):
    """
    Sign documents using advanced signature with SANAD credentials.

    :param req: Description
    :type req: SanadSignRequest
    """

    global ACCESS_TOKEN
    if ACCESS_TOKEN == "":
        return JSONResponse(
            content={"message": "Authentication required"},
            status_code=status.HTTP_401_UNAUTHORIZED,
        )

    url = "/".join((settings.TAWQI3I_ESIGNER_API_URL_V, "envelopes/sign"))
    data = json.dumps(req.__dict__)

    resp = httpx.post(
        url,
        data=data,
        headers=get_request_headers(),
        timeout=10.0,
    )

    return JSONResponse(content=resp.json(), status_code=status.HTTP_200_OK)


@router.post("/seal")
def seal(req: SignRequest):
    """
    Seal documents with a digital signature.

    :param req: Description
    :type req: SignRequest
    """

    global ACCESS_TOKEN
    if ACCESS_TOKEN == "":
        return JSONResponse(
            content={"message": "Authentication required"},
            status_code=status.HTTP_401_UNAUTHORIZED,
        )

    url = "/".join((settings.TAWQI3I_ESIGNER_API_URL_V, "envelopes/seal"))
    data = json.dumps(req.__dict__)

    resp = httpx.post(
        url,
        data=data,
        headers=get_request_headers(),
        timeout=10.0,
    )

    if resp.status_code != status.HTTP_200_OK:
        return JSONResponse(
            content={"message": "Authentication failed"},
            status_code=resp.status_code,
        )

    return JSONResponse(content=resp.json(), status_code=status.HTTP_200_OK)


@router.get("/callback")
def callback(query: Annotated[CallbackParams, Query()]):
    print(query)

    if query.error != "" and query.error is not None:
        return JSONResponse(
            content={"message": "An error occured"},
            status_code=status.HTTP_400_BAD_REQUEST,
        )

    if query.pinVerifyUrl != "" and query.pinVerifyUrl is not None:
        return RedirectResponse(query.pinVerifyUrl)

    if query.readyToSign != "" and query.readyToSign is not None:
        return RedirectResponse(f"{query.signingPage}/{query.sessionId}")

    return JSONResponse(
        content={"message": "Authentication failed"},
        status_code=status.HTTP_401_UNAUTHORIZED,
    )


def get_request_headers() -> dict[str, str]:
    global ACCESS_TOKEN
    return {
        "Content-Type": "application/json",
        "Authorization": f"Bearer {ACCESS_TOKEN}",
    }
