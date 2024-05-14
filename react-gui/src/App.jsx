import { useState, useEffect } from "react";
import reactLogo from "./assets/react.svg";
import { invoke} from "@tauri-apps/api/tauri";
import { listen, emit } from "@tauri-apps/api/event";
import Login from  "./Login";
import Chat from "./Chat"; 
import "./App.css";
function App() {

  const [connected, setConnected] = useState(false); 
  useEffect(() => {
    const unlisten = listen("login", (event) => {
      setConnected(true);
      console.log("hello");
      console.log(connected); 
    }); 
    return () => {
      unlisten.then(f => f()); 
    }
  }, [] );
  const [greetMsg, setGreetMsg] = useState("");
  const [name, setName] = useState("");


  if(!connected) return (<Login />
  );
  else return <Chat />; 
}

function createMessage(sender, message) {
  return (
    <div>
      
    </div>
  )
}
export default App; 
