using System;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChatApp
{
    public class Request
    {
        [JsonInclude]
        public string type; 
        [JsonInclude]
        public string message; 

        public Request(string type, string message)
        {
            this.type = type; 
            this.message = message; 
        }

        public string Serialize()
        {
            var json = JsonSerializer.Serialize(this); 
            return json;  
        }
    }

    public class Response 
    {
        [JsonInclude]
        public string type; 
        [JsonInclude]
        public string sender; 
        [JsonInclude]
        public string payload; 

        public Response(string type, string sender, string payload)
        {
            this.type = type; 
            this.sender = sender; 
            this.payload = payload; 
        }
        public string Serialize()
        { 
            return JsonSerializer.Serialize(this); 
        }
    }
}
