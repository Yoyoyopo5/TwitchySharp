# Acquire User Access Token

This tool automates the process of generating a user access token for E2E testing by handling the browser redirect and authentication API requests.

## How It Works

1.  **Launch**: The app initializes and generates the Twitch OAuth2 URL using environment variable configuration or user secrets json.
2.  **Authorize**: The URL is copied to your clipboard. Paste this into your browser and approve the permissions.
3.  **Listen**: The console app listens on the configured redirect URL.
4.  **Capture**: Once Twitch redirects back to your local machine, the app extracts the temporary auth code and closes the http listener.
5.  **Exchange**: The app hits the `https://id.twitch.tv/oauth2/token` endpoint.
6.  **Finish**: If the authentication request is successful, the new access token details are copied to the clipboard.

## Prerequisites

* A **Twitch Developer Application** registered at the [Twitch Dev Console](https://dev.twitch.tv/console).
    * Ensure your **Redirect URI** is set to a locally bindable port and uri (e.g. http://localhost:5000).

## Configuration

Configuration is done via environment variables or a user secrets file (debug only).

```
Twitch__ClientId=YOUR_CLIENT_ID
Twitch__ClientSecret=YOUR_CLIENT_SECRET
Twitch__RedirectUrl=http://localhost:5000
```

```json
{
  "Twitch": {
    "ClientId": "YOUR_CLIENT_ID",
    "ClientSecret": "YOUR_CLIENT_SECRET",
    "RedirectUrl": "http://localhost:5000"
  }
}
```