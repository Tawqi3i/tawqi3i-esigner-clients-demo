import json
import httpx
from typing import Optional, Dict
from app.settings import settings


class ESignerClient:
    access_token: Optional[str] = None

    def __init__(
        self,
        base_url: Optional[str] = None,
        timeout: float = 10.0,
        headers: Optional[Dict[str, str]] = None,
        retries: int = 2,
    ):
        self.base_url = base_url
        self.retries = retries
        print("ESignerClient initialized with base_url:", base_url)
        self.client = httpx.Client(
            # base_url=base_url,
            timeout=timeout,
            headers=headers or {},
        )

    def login(self) -> httpx.Response:

        data = {
            "client_id": settings.CLIENT_ID,
            "client_secret": settings.CLIENT_SECRET,
            "redirect_uri": settings.REDIRECT_URI,
            "grant_type": "client_credentials",
        }

        for attempt in range(self.retries + 1):
            try:
                response = self.client.post(
                    "/".join((self.base_url, "oauth20/token")),
                    data=data,  # application/x-www-form-urlencoded
                )

                response.raise_for_status()

                ESignerClient.access_token = response.json().get("access_token")

                return response
            except httpx.HTTPError as e:
                if attempt == self.retries:
                    raise e

    def sanad_init(self, body: Dict) -> httpx.Response:

        if not ESignerClient.access_token:
            raise Exception("Client is not authenticated. Call login() first.")

        url = "/".join((settings.TAWQI3I_ESIGNER_API_URL_V, "sanad/init"))
        data = json.dumps(body, indent=4)
        resp = self.client.post(url, data=data, headers=self.__get_headers())

        return resp

    def seal(self, body: Dict) -> httpx.Response:

        if not ESignerClient.access_token:
            raise Exception("Client is not authenticated. Call login() first.")

        url = "/".join((settings.TAWQI3I_ESIGNER_API_URL_V, "envelopes/seal"))
        data = json.dumps(body, indent=4)
        resp = self.client.post(url, data=data, headers=self.__get_headers())

        return resp

    def sign_advanced(self, body: Dict) -> httpx.Response:

        if not ESignerClient.access_token:
            raise Exception("Client is not authenticated. Call login() first.")

        url = "/".join((settings.TAWQI3I_ESIGNER_API_URL_V, "envelopes/sign"))
        data = json.dumps(body)
        resp = self.client.post(url, data=data, headers=self.__get_headers())

        return resp

    def __get_headers(self) -> Dict[str, str]:
        if not ESignerClient.access_token:
            raise Exception("Client is not authenticated. Call login() first.")

        return {
            "Content-Type": "application/json",
            "Authorization": f"Bearer {ESignerClient.access_token}",
        }


esignerClient = ESignerClient(settings.TAWQI3I_ESIGNER_API_URL_V)
