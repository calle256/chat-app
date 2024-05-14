import { useState, useEffect } from "react";
import reactLogo from "./assets/react.svg";
import { invoke} from "@tauri-apps/api/tauri";
import { listen, emit } from "@tauri-apps/api/event";
import { appWindow } from "@tauri-apps/api/window";
import "./App.css";
import "./styles.css"; 

function App() {
  const [messages, setMessages] = useState([]);
  const [msgCount, setMsgCount] = useState(0); 
  useEffect(() => {
    const unlisten = listen("rcv", (event) => {
      setMsgCount(msgCount+ 1); 
      messages.push(
        { msg: event.payload.message,
          sender: "other"
        }); 
      console.log(event.payload.message); 
      console.log(messages); 
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
    setName(""); 
    setMsgCount(msgCount+1); 
    messages.push({
      msg: name, 
      sender: "you"});
    console.log(messages); 
  }
  return (
    <div className="container">
      <div className="chatCont">
      <ul className="msgList">
        {messages.map((message, index) =>{ 
          if(message.sender=="you"){
            return( 
            <li key={index} className="msgYou"> 
              <div>{message.msg}</div></li>
            )
          }
          else{
            return(
            <li key={index} className="msgOther"> 
              <div>{message.msg}</div>
              </li>
            )
          }
        })}
      </ul>
        <form
          className="row"
          onSubmit={(e) => {
            e.preventDefault()
            e.target.reset();
            greet();
          }}
        >
          <input
            id="messageBox"
            onChange={(e) => setName(e.currentTarget.value)}
            placeholder="Enter message..."
          />
          <button type="submit">Send</button>
        </form>
        <p>{greetMsg}</p>
      </div>
    </div>
  );
}

function createMessage(message) {
  return (
    <li key={msgCount}>
    <div>
      <p>{message}</p> 
    </div>
    </li>
  )
}
export default App; 
