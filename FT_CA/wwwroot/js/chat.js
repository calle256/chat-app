function addUser(name, date) {
    const messagesBox = document.querySelector('.messages-box');
    const listItem = document.createElement('div');
    listItem.className = 'list-group-item list-group-item-action text-white rounded-0';
    listItem.innerHTML = `
        <div class="media">
            <div class="media-body ml-4">
                <div class="d-flex align-items-center justify-content-between mb-1">
                    <div class="d-flex align-items-center">
                        <h4 class="mb-0">${name}</h4>
                        <!--<small class="small font-weight-bold ms-2">${date}</small> -->
                    </div>
                </div>
            </div>
        </div>
    `;
    // Prepend the listItem to the messagesBox so it appears at the top
    messagesBox.prepend(listItem);
}

addUser("George", "26 dec")
addUser("George", "26 dec")
addUser("George", "26 dec")
addUser("George", "26 dec")

addUser("George", "26 dec")
addUser("George", "26 dec")
addUser("George", "26 dec")
addUser("George", "26 dec")

addUser("George", "26 dec")
addUser("George", "26 dec")
addUser("George", "26 dec")
addUser("George", "26 dec")

addUser("George", "26 dec")
addUser("George", "26 dec")
addUser("George", "26 dec")
addUser("George", "26 dec")
document.addEventListener('DOMContentLoaded', function () {
    const sendButton = document.getElementById('sendButton');
    const messageInput = document.getElementById('messageInput');
    const chatMessages = document.getElementById('chatMessages');
    //den tillhör receive funktionen
    //const income       = document.getElementById('incomming');

    // Function to send message
    function sendMessage() {
        const messageText = messageInput.value.trim();  
        if (messageText) {  
            const messageDiv = document.createElement('div');  
            messageDiv.textContent = messageText;  
            
            
            messageDiv.className = 'p-2 bg-white my-2';  
            chatMessages.appendChild(messageDiv);  
            messageInput.value = '';  
            chatMessages.scrollTop = chatMessages.scrollHeight;          }
    }
    /*vet inte riktigt hur jag ska använda den
    function recieveMessage(message){
        const newdiv =document.createElement('div');
        newdiv.textContent=message;
        newdiv.className = "p-2 bg-white my-2";
        income.appendChild(newdiv);
        income.scrollTop= income.scrollHeight;
    }
    */
    // Event listener for the send button
    sendButton.addEventListener('click', sendMessage);

    //Ifall man tycker Enter ist för btn 
    messageInput.addEventListener('keypress', function (event) {
        if (event.key === 'Enter') {
            sendMessage();
            event.preventDefault();  // Prevent the form from submitting which is default action for enter
        }
    });
});


document.addEventListener('DOMContentLoaded', function () {
    const namesListDiv = document.getElementById('namesList');

    function updateNamesList(names) {
        // Check if the array is not empty and is an array
        if (Array.isArray(names) && names.length) {
            // Create a string from the array, separated by commas
            const namesString = names.join(', ');
            // Update the div's content
            namesListDiv.textContent = namesString;
        } else {
            // If the names array is empty or not an array, show default text
            namesListDiv.textContent = 'Names: No names available';
        }
    }

    // Example usage
    const names = ['George', 'Amber', 'Alice', 'Klara', 'Maggi'];
    updateNamesList(names);

    // If you need to update it later, you can call updateNamesList again with a new array of names
});


