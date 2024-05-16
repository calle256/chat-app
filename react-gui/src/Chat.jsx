import { useState, useEffect } from "react";
import reactLogo from "./assets/react.svg";
import { invoke} from "@tauri-apps/api/tauri";
import { listen, emit } from "@tauri-apps/api/event";
import { appWindow } from "@tauri-apps/api/window";
//mport "./App.css";
import 'bootstrap/dist/css/bootstrap.min.css';
import "./chat.css";
//import "./styles.css"; 

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
    <div className="bg-dark d-flex flex-column vh-100">
      <nav className="navbar bg-body-tertiary bg-dark">
        <div className="container-fluid">
            <h1 className="text-warning">ChatApp</h1>
            <form className="d-flex">
                <button className="btn btn-outline-success" type="submit">Log Out</button>
            </form>
        </div>
      </nav>
  

    
    <div className="flex-grow-1 overflow-auto">       
    <ul className="msgList">
        {messages.map((message, index) =>{ 
          if(message.sender=="you"){
            return( 
            <li key={index} className="msgYou"> 
              <div className="bubble">{message.msg}</div></li>
            )
          }
          else{
            return(
            <li key={index} className="msgOther"> 
              <div className="resbubble">{message.msg}</div>
              </li>
            )
          }
        })}
      </ul>


    </div>

    
    <div className="bg-warning p-1 fixed-bottom nere">
        <form className="d-flex"
              onSubmit={(e) => {
              e.preventDefault()
              e.target.reset();
              greet();
              }}>
            <input type="text" className="form-control me-2" placeholder="Type your message here..."
              onChange={(e) => setName(e.currentTarget.value)}/>
            <button className="btn btn-success btn-lg" type="submit">Send</button>
        </form>
        <p>{greetMsg}</p>
    </div>
    </div>
  );
}


export default App; 
