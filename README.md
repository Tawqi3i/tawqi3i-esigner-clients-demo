# Sample Projects
In this repo you can find sample projects to help developers to integrate their apps/services with Tawqi3i ESigner API and the external authorisation providers such SANAD.

Image below shows the stakeholders/roles involved in the authorisation flow with SANAD SSO, and the flow differs based the architecture eg. client side vs server side rendering.

<img width="581" height="78" alt="stakeholders" src="https://github.com/user-attachments/assets/db026e4a-244b-416c-8261-0da911ef90b8" />

## .NET

### Client & Server 
Project: **ESignerDemo.Frontend & ESignerDemo.Backend**

Demonstrates the auth flow when RedirectUrl hits the **Frontend** app first and then with the help of the Backend completes the cycle.

### SSR/HostedApp
Project: **ESignerDemoWasmApp**

A Client Side Rendering Blazor app that demonstrates the auth flow when RedirectUrl hits the **host/backend**.
This setup  applies to SSR apps as well.



## ReactJS

### SPA & NodeExpress

Coming soon!
