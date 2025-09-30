# TranslationAPI

This is the backend API for the Healthcare Translation Web App. It provides services for speech-to-text, text-to-speech, and translation.

## Architecture

The API is built with ASP.NET Core and follows a standard controller-service pattern.

-   **Controllers:** Handle incoming HTTP requests and send responses.
-   **Services:** Contain the business logic for interacting with external services like Google Cloud Translation and Speech-to-Text, and OpenAI.
-   **Models:** Define the data structures for requests and responses.

## Setup

1.  **Prerequisites:**
    *   .NET SDK installed.
    *   Google Cloud SDK configured with credentials for the Translation and Speech-to-Text APIs.
    *   An OpenAI API key.

2.  **Configuration:**
    *   Create a `.env.local` file in the root of the project.
    *   Add your `GOOGLE_APPLICATION_CREDENTIALS` path and `OPENAI_API_KEY` to the `.env.local` file:
        ```
        GOOGLE_APPLICATION_CREDENTIALS="/path/to/your/google-credentials.json"
        ```
    *   The OpenAI key is configured in `appsettings.Development.json`.

3.  **Running the API:**
    *   Navigate to the `TranslationAPI` directory.
    *   Run `dotnet run`.
    *   The API will be available at `http://localhost:5000` (or the configured port).
    *   Swagger documentation is available at `http://localhost:5000/swagger`.

## API Endpoints

### Test

-   **`GET /api/test`**
    -   **Description:** A simple endpoint to check if the API is running.
    -   **Response:**
        -   `200 OK` with the message "API is working!".

### Speech

-   **`POST /api/speech/speech-to-text`**
    -   **Description:** Converts audio to text using Google's Speech-to-Text service.
    -   **Request Body:**
        ```json
        {
          "audioFilePath": "path/to/your/audio.wav"
        }
        ```
    -   **Response:**
        -   `200 OK` with the transcribed text.
        ```json
        {
          "transcript": "This is the transcribed text."
        }
        ```

### Text-to-Speech

-   **`POST /api/text/text-to-speech`**
    -   **Description:** Converts text to speech and saves it as an MP3 file.
    -   **Request Body:**
        ```json
        {
          "text": "This is the text to convert to speech.",
          "destinationPath": "path/to/save/the/file/",
          "fileName": "output"
        }
        ```
    -   **Response:**
        -   `200 OK` with a success message and the file path.
        ```json
        {
          "message": "Conversion successful",
          "filePath": "path/to/save/the/file/output.mp3"
        }
        ```

### Translation

-   **`POST /api/translation/translate`**
    -   **Description:** Translates text to a target language using Google Translate.
    -   **Request Body:**
        ```json
        {
          "text": "The text to be translated.",
          "targetLanguage": "es"
        }
        ```
    -   **Response:**
        -   `200 OK` with the translated text.
        ```json
        {
          "translatedText": "El texto a traducir."
        }
        ```