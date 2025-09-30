"use client";

import { useState } from "react";

/**
 * The main component for the Healthcare Translation App.
 * It handles user input, makes API calls for translation, text-to-speech, and speech-to-text,
 * and displays the results.
 */
export default function Main() {
  // State for the input text to be translated or converted to speech.
  const [text, setText] = useState("");
  // State for the output text from the API calls.
  const [output, setOutput] = useState("");
  // State to indicate whether an API call is in progress.
  const [loading, setLoading] = useState(false);
  // State for the path of the audio file for speech-to-text or text-to-speech.
  const [audioFilePath, setAudioFilePath] = useState("");
  // State for the name of the file to be saved in text-to-speech.
  const [fileName, setFileName] = useState("");
  // State for the target language code for translation or text-to-speech.
  const [languageCode, setLanguageCode] = useState("en-US");


  /**
   * Handles the translation of the input text.
   * It calls the translation API endpoint and updates the output state with the translated text.
   */
  const handleTranslate = async () => {
    if (!text) {
      setOutput("Please enter text to translate.");
      return;
    }

    setLoading(true);
    try {
      const targetLanguage = languageCode.trim() || "es"; 

      const response = await fetch("http://localhost:5067/api/translation/translate", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ text, targetLanguage }),
      });

      const data = await response.json();
      setOutput(data.translatedText || "Translation failed.");
    } catch (error) {
      console.error("Translation error:", error);
      setOutput("Error occurred.");
    } finally {
      setLoading(false);
    }
  };


  /**
   * Handles the conversion of the input text to speech.
   * It calls the text-to-speech API endpoint and updates the output state with the path to the saved audio file.
   */
  const handleTextToSpeech = async () => {
    if (!text) {
      setOutput("Please enter text to convert to speech.");
      return;
    }
    if (!audioFilePath) {
      setOutput("Please enter a destination file path.");
      return;
    }
    if (!fileName) {
      setOutput("Please enter a file name.");
      return;
    }
    if (!languageCode) {
      setOutput("Please enter a language code (e.g., en-US).");
      return;
    }

    setLoading(true);
    try {
      const response = await fetch("http://localhost:5067/api/text/text-to-speech", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ text, destinationPath: audioFilePath, fileName, languageCode }),
      });

      const data = await response.json();
      if (response.ok) {
        setOutput(`Audio file saved at: ${data.filePath}`);
      } else {
        setOutput(data.message || "Error occurred.");
      }
    } catch (error) {
      console.error("Text-to-Speech error:", error);
      setOutput("Error occurred.");
    } finally {
      setLoading(false);
    }
  };


  /**
   * Handles the conversion of speech from an audio file to text.
   * It calls the speech-to-text API endpoint and updates the output state with the transcribed text.
   */
  const handleSpeechToText = async () => {
    if (!audioFilePath) {
      setOutput("Please enter an audio file path.");
      return;
    }

    setLoading(true);
    try {
      const response = await fetch("http://localhost:5067/api/speech/speech-to-text", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ audioFilePath }),
      });

      const data = await response.json();
      setOutput(data.transcript || "Speech-to-text failed.");
    } catch (error) {
      console.error("Speech-to-Text error:", error);
      setOutput("Error occurred.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={{ textAlign: "center", marginTop: "50px" }}>
      <h2>Healthcare Translation App</h2>

      {/* Text Input for Translation & TTS */}
      <textarea
        placeholder="Enter text here..."
        value={text}
        onChange={(e) => setText(e.target.value)}
        rows={4}
        style={{ width: "80%", padding: "10px", marginBottom: "10px" }}
      />

      <br />

      {/* Audio File Path Input for Speech-to-Text & TTS */}
      <input
        type="text"
        placeholder="Enter destination file path..."
        value={audioFilePath}
        onChange={(e) => setAudioFilePath(e.target.value)}
        style={{ width: "80%", padding: "10px", marginBottom: "10px" }}
      />

      <br />

      {/* File Name Input for TTS */}
      <input
        type="text"
        placeholder="Enter file name..."
        value={fileName}
        onChange={(e) => setFileName(e.target.value)}
        style={{ width: "80%", padding: "10px", marginBottom: "10px" }}
      />

      <br />
      <button onClick={handleTranslate} disabled={loading}>Translate</button>
      <button onClick={handleTextToSpeech} disabled={loading}>Text to Speech</button>
      <button onClick={handleSpeechToText} disabled={loading}>Speech to Text</button>

      {loading && <p>Loading...</p>}
      
      <h3>Output:</h3>
      <p>{output}</p>

      {/* Language Code Input for Both Translation and TTS (Bottom Right Corner) */}
      <div style={{ position: "absolute", bottom: "20px", right: "20px" }}>
        <input
          type="text"
          placeholder="Language (e.g., en-US, es, fr)"
          value={languageCode}
          onChange={(e) => setLanguageCode(e.target.value)}
          style={{ width: "150px", padding: "5px" }}
        />
      </div>
    </div>
  );
}