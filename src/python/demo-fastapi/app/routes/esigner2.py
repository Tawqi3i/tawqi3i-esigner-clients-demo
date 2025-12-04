from typing_extensions import Annotated
from fastapi import APIRouter, Query, status
from fastapi.responses import JSONResponse, RedirectResponse, Response
from app.dto import CallbackParams, SanadInitRequest, SignRequest, SanadSignRequest
from app.settings import settings
from app.client import esignerClient

router = APIRouter(prefix="/esigner2", tags=["esigner2"])


@router.post("/login")
def auth() -> Response:
    """
    Service to service authentication.
    """

    esignerClient.login()

    return JSONResponse(content={}, status_code=status.HTTP_200_OK)


@router.post("/sanad/init")
def sanad_init(req: SanadInitRequest) -> Response:
    """
    Initialise SANAD session for end user.
    """

    body = SanadInitRequest(
        nationalId=req.nationalId,
        redirectUrl=settings.REDIRECT_URI,
        signingPage=req.signingPage,
    )

    resp = esignerClient.sanad_init(body.__dict__)

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

    resp = esignerClient.sign_advanced(req.__dict__)

    if resp.status_code != status.HTTP_200_OK:
        return JSONResponse(
            content={"message": "Authentication failed"},
            status_code=resp.status_code,
        )

    return JSONResponse(content=resp.json(), status_code=status.HTTP_200_OK)


@router.post("/seal")
def seal(req: SignRequest):

    resp = esignerClient.seal(req.__dict__)

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
