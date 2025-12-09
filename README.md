# Sample Projects
In this repo you can find sample projects to help developers to integrate their apps/services with Tawqi3i ESigner API and the external authorisation providers such SANAD.

Image below shows the stakeholders/roles involved in the authorisation flow with SANAD SSO, and the flow differs based the architecture eg. client side vs server side rendering.

<img width="581" height="78" alt="stakeholders" src="https://github.com/user-attachments/assets/db026e4a-244b-416c-8261-0da911ef90b8" />

# ESigner & SSO Authentication Flow
<img width="1062" height="640" alt="image" src="https://github.com/user-attachments/assets/5a660fb0-674c-4a4a-b5fe-77e4f1995b16" />


## .NET

### Client & Server 
Project: **ESignerDemo.Frontend & ESignerDemo.Backend**

Demonstrates the auth flow when RedirectUrl hits the **Frontend** app first and then with the help of the Backend completes the cycle.

### SSR (MVC)
Project: **EsignerDemo.Mvc**

An ASP.NET Core MVC app that demonstrates the auth flow when RedirectUrl hits the **backend**.


### SSR/HostedApp
Project: **ESignerDemoWasmApp**

An Interactive WebAssembly Blazor app that demonstrates the auth flow when RedirectUrl hits the **host/backend**.

## Python
### FastApi
A backend service demonstrates esigner api integration. Refer to its [Readme](https://github.com/Tawqi3i/tawqi3i-esigner-clients-demo/blob/main/src/python/demo-fastapi/README.md) section.

## ReactJS

### SPA & NodeExpress

Coming soon!
