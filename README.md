# Healthcare Translation Web App with Generative AI

A web-based prototype that enables real-time, multilingual translation between patients and healthcare providers. This application converts spoken input into text, provides a live transcript, and offers a translated version with audio playback.

## Core Functionalities

-   **Voice-to-Text with Generative AI:** Converts spoken input into accurate text transcripts, leveraging AI to improve recognition of medical terminology.
-   **Audio Playback:** Provides natural-sounding audio playback of transcripts.
-   **Translation:** Translates transcripts into multiple languages for seamless communication.

## Technology Stack & Integrations

-   **Frontend:** Next.js, React, Tailwind CSS
-   **Backend:** ASP.NET Core Web API
-   **Generative AI:** OpenAI
-   **Speech Recognition:** Google Speech-to-Text
-   **Text-to-Speech:** Google Text-to-Speech

## Project Structure

The project is divided into two main parts:

-   **`TranslationAPI/`**: The backend service that handles translation, speech-to-text, and text-to-speech. For more details, see the [TranslationAPI README](./TranslationAPI/README.md).
-   **`TranslationUI/`**: The frontend Next.js application that provides the user interface. For more details, see the [TranslationUI README](./TranslationUI/my-app/README.md).

## Prerequisites

-   .NET SDK
-   Node.js and npm (or yarn)
-   Google Cloud SDK configured with credentials for the Translation and Speech-to-Text APIs.
-   An OpenAI API key.

## Getting Started

### 1. Configure the Backend

1.  Create a `.env.local` file in the root of the project.
2.  Add your `GOOGLE_APPLICATION_CREDENTIALS` path to the `.env.local` file:
    ```
    GOOGLE_APPLICATION_CREDENTIALS="/path/to/your/google-credentials.json"
    ```
3.  Navigate to the `TranslationAPI` directory and open `appsettings.Development.json`.
4.  Add your OpenAI API key to the `OpenAI` section:
    ```json
    "OpenAI": {
      "ApiKey": "YOUR_OPENAI_API_KEY"
    }
    ```

### 2. Run the Backend

1.  Open a terminal and navigate to the `TranslationAPI` directory.
2.  Run `dotnet run`.
3.  The API will be running at `http://localhost:5000` (or the configured port).

### 3. Configure and Run the Frontend

1.  Open another terminal and navigate to the `TranslationUI/my-app` directory.
2.  Run `npm install` to install the dependencies.
3.  Run `npm run dev` to start the development server.
4.  The application will be available at `http://localhost:3000`.

Now you can open your browser and navigate to `http://localhost:3000` to use the application.