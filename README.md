# Sample Projects
In this repo you can find sample projects to help developers to integrate their apps/services with Tawqi3i ESigner API and the external authorisation providers such SANAD.

Image below shows the stakeholders/roles involved in the authorisation flow with SANAD SSO, and the flow differs based the architecture eg. client side vs server side rendering.

<img width="581" height="78" alt="stakeholders" src="https://github.com/user-attachments/assets/db026e4a-244b-416c-8261-0da911ef90b8" />

# ESigner & SSO Authentication Flow
<img width="1752" height="1056" alt="image" src="https://github.com/user-attachments/assets/b37e1746-0e9b-45e2-94e6-d8422253ef79" />


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


## ReactJS

### SPA & NodeExpress

Coming soon!
