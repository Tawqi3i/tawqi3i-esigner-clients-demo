from pydantic import BaseModel


class AuthToken(BaseModel):
    grant_type: str = "client_credentials"
    client_id: str
    client_secret: str


class SanadInitRequest(BaseModel):
    nationalId: str
    redirectUrl: str | None = None
    signingPage: str | None = None


class SignRequest(BaseModel):
    sessionId: str
    data: str


class CallbackParams(BaseModel):
    sessionId: str
    nationalId: str | None
    pinVerifyUrl: str | None
    readyToSign: bool | None
    error: str | None
