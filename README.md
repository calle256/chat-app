This is a repository for hosting our chat application project for the TMUK14 Spring course. 

![workflow](https://github.com/calle256/chat-app/actions/workflows/dotnet.yml/badge.svg)  
![Test Code Coverage](https://img.shields.io/badge/Code%20Coverage-10%25-success?style=flat)

# Project description
We want to implement a chat application that communicates between terminals. 
Plans for future builds includes a GUI for the application.

## Detailed description
We are going to use `C#` for the server and client application and a combination of `HTML, CSS and JavaScript` for the eventual frontend. Depending on our time frame we might add more features, such as a user authentication system. 


# Plan 

## What we want to implement 
A chat app between two or more users. We want to implement a client-server model, where several users can communicate with eachother using a TCP socket. 

## Features 
* To be able to see all the users who is online at the moment. 
* Be able to chat with one or more users and add more users to the current chat.

## Languages
* Backend: `C#`
* Frontend: `HTML, CSS, JavaScript` 

## Build system 

# Compilation and running
## Windows/ Mac
Open the solution file in VisualStudio and run.

## Linux
Requirements: Dotnet runtime 8.0.0

To compile the server, run the following command: 
`dotnet build ChatApp/server [target path for artifacts]`  
To just run the server from the command line, run the following command: 
`dotnet run --project ChatApp/server`

To compile the server, run the following command in the Git folder:  
`dotnet build ChatApp/client [target path for artifacts]`  
To instead run the client from the command line, use the following command: 
`dotnet run --project ChatApp/client`

## Unit tests
To run the client tests, run the following command: 
`dotnet test Client.Test`  
To instead run the server tests, run the following command: 
`dotnet test Server.Test`
Run the following command in the Git folder for code coverage:  
`dotnet tool install --global dotnet-coverage`  
Then run:  
`dotnet-coverage collect`  


## Linter
To use the linter, run the following command in the Git folder:  
`dotnet tool install csharpier`  
if csharpier already is installed run:  
`dotnet csharpier .`  
For CI pipeline run:  
`dotnet csharpier  --check`  


## Kanban Link
You can find our Kanban board for the project [here](https://github.com/users/calle256/projects/1)

# Declaration

1. George Saba declare that I am the sole author of the content I add to this repository

2. I, Amanda Nyander declare that I am the sole author of the content I add to this repository

3. I, Calle Ovinder declare that I am the sole author of the content I add to this repository.
   
4. I, Deividas Malaska declare that I am the sole author of the content I add to this repository.

5. Ghadi Khalil declare that I am the sole author of the content I add to this repository.

| Name            | Github Username |
|---              |---              |
| George Saba     | george3235      |
| Calle Ovinder   | calle256        |
| Amanda Nyander  | amandanyander   |
| Deividas Malaska| malaskadeividas |
| Ghadi Khalil    | ksumki          |




