"use client";

import { useState } from "react";

export default function Main() {
  const [text, setText] = useState(""); // Text for translation & TTS
  const [output, setOutput] = useState(""); // Output text
  const [loading, setLoading] = useState(false); // Loading state
  const [audioFilePath, setAudioFilePath] = useState(""); // File path input for speech-to-text

  // ✅ Translate API Call
  const handleTranslate = async () => {
    setLoading(true);
    try {
      const response = await fetch("http://localhost:5067/api/translation/translate", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ text, targetLanguage: "es" }),
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

  // ✅ Text-to-Speech API Call
  const handleTextToSpeech = async () => {
    setLoading(true);
    try {
      const response = await fetch("http://localhost:5067/api/text/text-to-speech", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ text }),
      });

      const audioBlob = await response.blob();
      const audioUrl = URL.createObjectURL(audioBlob);
      const audio = new Audio(audioUrl);
      audio.play();
      setOutput("Playing audio...");
    } catch (error) {
      console.error("Text-to-Speech error:", error);
      setOutput("Error occurred.");
    } finally {
      setLoading(false);
    }
  };

  // ✅ Speech-to-Text API Call
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

      {/* Audio File Path Input for Speech-to-Text */}
      <input
        type="text"
        placeholder="Enter audio file path..."
        value={audioFilePath}
        onChange={(e) => setAudioFilePath(e.target.value)}
        style={{ width: "80%", padding: "10px", marginBottom: "10px" }}
      />

      <br />
      <button onClick={handleTranslate} disabled={loading}>Translate</button>
      <button onClick={handleTextToSpeech} disabled={loading}>Text to Speech</button>
      <button onClick={handleSpeechToText} disabled={loading}>Speech to Text</button>

      {loading && <p>Loading...</p>}
      
      <h3>Output:</h3>
      <p>{output}</p>
    </div>
  );
}