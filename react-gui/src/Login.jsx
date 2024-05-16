import { useState, useEffect } from "react";
import reactLogo from "./assets/react.svg";
import { invoke} from "@tauri-apps/api/tauri";
import { listen, emit } from "@tauri-apps/api/event";
import { appWindow } from "@tauri-apps/api/window";
import 'bootstrap/dist/css/bootstrap.min.css';
//import "./App.css";
//import "./styles.css"; 
import "./login.css";
function App() {
  useEffect(() => {
    const unlisten = listen("rcv", (event) => {
      console.log(event.message); 
    }); 
    return () => {
      unlisten.then(f => f()); 
    }
  }, [] );
  const [greetMsg, setGreetMsg] = useState("");
  const [name, setName] = useState("");

  async function greet() {
    // Learn more about Tauri commands at https://tauri.app/v1/guides/features/command
    setGreetMsg(await invoke("write_stream", { msg : name }));
    appWindow.emit("login"); 
  }

  return (
    <div className="lgcontainer bg-dark">
      <div className="wrapper">
        <h2 className="logintlt">Welcome to our Chat App!</h2>
        <form
          className="lgform"
          onSubmit={(e) => {
          e.preventDefault();
          greet();
        }}
        >
          <input
            id="greet-input"
            onChange={(e) => setName(e.currentTarget.value)}
            placeholder="Enter your name..."
            required
            className="inputbox"
          />
          <button type="submit">Connect</button>
        </form>
      </div>

      <p>{greetMsg}</p>
    </div>
  );
}

function createMessage(message) {
  return (
    <div>
      <p>message</p> 
    </div>
  )
}
export default App; 
