# TranslationUI

This is the frontend for the Healthcare Translation Web App. It's a Next.js application that provides the user interface for real-time translation.

## Structure

The application is built with Next.js and uses React for the user interface.

-   **`src/app/`**: This is the main application directory.
    -   **`layout.tsx`**: The main layout of the application.
    -   **`page.tsx`**: The main page of the application.
    -   **`components/`**: Contains the reusable React components.

## Setup

1.  **Prerequisites:**
    *   Node.js and npm (or yarn) installed.

2.  **Installation:**
    *   Navigate to the `TranslationUI/my-app` directory.
    *   Run `npm install` to install the dependencies.

3.  **Configuration:**
    *   The frontend is configured to connect to the `TranslationAPI` running on `http://localhost:5000`. If your API is running on a different port, you will need to update the API URLs in the components.

4.  **Running the Application:**
    *   Run `npm run dev` to start the development server.
    *   The application will be available at `http://localhost:3000`.

## Components

The application is composed of several key components:

-   **`AudioRecorder`**: Handles audio recording and sends it to the backend for transcription.
-   **`TranslationDisplay`**: Displays the original transcript and the translated text.
-   **`LanguageSelector`**: Allows the user to select the target language for translation.

These components work together to provide a seamless translation experience. The state is managed within the main page component and passed down to the child components as props.