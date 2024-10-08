// Prevents additional console window on Windows in release, DO NOT REMOVE!!
#![cfg_attr(not(debug_assertions), windows_subsystem = "windows")]
use std::io::*;
use tauri::{Event, Window, State, Manager, AppHandle}; 
use std::net::{TcpStream, Shutdown}; 
use std::ops::Deref;
use std::sync::{Arc, Mutex};
use std::{str, thread}; 
use serde::*; 


#[derive(Clone, Serialize, Deserialize)]
struct Request{
    rqType: String, 
    message: String, 
}
#[derive(Clone, Serialize, Deserialize)]
struct Response{
    rsType: String, 
    sender: String, 
    payload: String, 
}


#[derive(Clone, Serialize, Deserialize)]
struct Payload{
    rs: Response, 
}

// Learn more about Tauri commands at https://tauri.app/v1/guides/features/command
#[tauri::command]
fn greet(name: &str) -> String {
    format!("Hello, {}! You've been greeted from Rust!", name)
}

#[tokio::main]
async fn main() {
    let socket= TcpStream::connect("127.0.0.1:1234").unwrap();
    let read = socket.try_clone().unwrap();
    tauri::Builder::default()
        .setup(|app|{
            let handle = app.handle(); 
            thread::spawn(move || {
                read_stream(handle, &read); 
            });
            Ok(())
        })
        .manage(Mutex::new(socket))
        .invoke_handler(tauri::generate_handler![greet, write_stream, connect_server, leave_server])
        .run(tauri::generate_context!())
        .expect("error while running tauri application");
}

#[tauri::command]
fn write_stream(state:State<'_, Mutex<TcpStream>>,msg: String){
    println!("{}", msg); 
    let mut socket = state.deref(); 
    let _ = socket.lock().unwrap().write(msg.as_bytes()); 
}

#[tauri::command]
fn read_stream(handle: AppHandle, mut stream: &TcpStream) {
    loop{
        let mut buf = [0; 1024]; 
        let _ = stream.read(&mut buf[..]);
        let msg = str::from_utf8(&buf).unwrap().trim_matches(char::from(0));
        let rs: Response = serde_json::from_str(msg).expect("Invalid response"); 
        println!("{}", msg); 
        handle.emit_all("rcv", Payload{rs: rs}).unwrap(); 
    }
}

#[tauri::command]
fn connect_server(state:State<'_, Mutex<TcpStream>>, endpoint: String){
    let mut socket = state.deref(); 
    socket = &Mutex::new(TcpStream::connect(endpoint).unwrap()); 
}
#[tauri::command]
fn leave_server(state:State<'_, Mutex<TcpStream>>){ 
    let mut socket = state.deref(); 
    let _ = socket.lock().unwrap().shutdown(Shutdown::Both).unwrap(); 
}
