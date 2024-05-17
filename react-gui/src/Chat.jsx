import { useState, useEffect, useRef } from "react";
import reactLogo from "./assets/react.svg";
import { invoke } from "@tauri-apps/api/tauri";
import { listen, emit } from "@tauri-apps/api/event";
import { appWindow } from "@tauri-apps/api/window";
import 'bootstrap/dist/css/bootstrap.min.css';
import "./chat.css";

function App() {
  const [messages, setMessages] = useState([]);
  const [msgCount, setMsgCount] = useState(0);
  const chatEndRef = useRef(null);

  useEffect(() => {
    const unlisten = listen("rcv", (event) => {
      setMsgCount(msgCount + 1);
      setMessages(prevMessages => [
        ...prevMessages,
        {
          msg: event.payload.rs.payload,
          sender: event.payload.rs.sender
        }
      ]);
    });

    return () => {
      unlisten.then(f => f());
    };
  }, []);

  useEffect(() => {
    chatEndRef.current?.scrollIntoView({ behavior: "smooth" });
  }, [messages]);

  const [greetMsg, setGreetMsg] = useState("");
  const [name, setName] = useState("");

  async function greet() {
    setGreetMsg(await invoke("write_stream", { msg: name }));
    setMessages(prevMessages => [
      ...prevMessages,
      {
        msg: name,
        sender: "you"
      }
    ]);
    setName("");
    setMsgCount(msgCount + 1);
  }

  return (
    <div className="d-flex flex-column vh-100 bg-dark">
      <nav className="navbar bg-dark">
        <div className="container-fluid">
          <h1 className="text-warning">ChatApp</h1>
          <form className="d-flex">
            <button className="btn btn-outline-success" type="submit">Log Out</button>
          </form>
        </div>
      </nav>

      <div className="flex-grow-1 overflow-auto p-3 chat-area">
        <ul className="msgList">
          {messages.map((message, index) => {
            if (message.sender === "you") {
              return (
                <li key={index} className="msgYou">
                  <div className="bubble">{message.msg}</div>
                </li>
              );
            } else {
              return (
                <li key={index} className="msgOther">
                  <div className="senderName">{message.sender}</div>
                  <div className="resbubble">{message.msg}</div>
                </li>
              );
            }
          })}
          <div ref={chatEndRef}></div>
        </ul>
      </div>

      <div className="bg-warning p-1 typing-area">
        <form className="d-flex w-100" onSubmit={(e) => {
          e.preventDefault();
          greet();
        }}>
          <input
            type="text"
            className="form-control me-2"
            placeholder="Type your message here..."
            value={name}
            onChange={(e) => setName(e.currentTarget.value)}
            required
          />
          <button className="btn btn-success btn-lg nere" type="submit">Send</button>
        </form>
        <p>{greetMsg}</p>
      </div>
    </div>
  );
}

export default App;
