from pydantic import BaseModel


class AuthToken(BaseModel):
    grant_type: str = "client_credentials"
    client_id: str
    client_secret: str

class SanadInitRequest(BaseModel):
    NationalId: str
    RedirectUrl: str
    SigningPage: str | None

class SanadInitResponse(BaseModel):
    SessionId: str
    AuthUrl: str
    SignVerifyUrl: str

class CallbackParams(BaseModel):
    SessionId: str
    NationalId: str | None
    PinVerifyUrl: str| None
    ReadyToSign: bool | None
    Error: str | None