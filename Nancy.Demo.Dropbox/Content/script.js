getFiles();

document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("uploadFile").onclick = uploadFile;
});

// functions
// get dropbox files from the API
function getFiles() {
    var request = new XMLHttpRequest();
    request.open("GET", "/files", true);
    request.onreadystatechange = function () {
        if (request.readyState != 4 || request.status != 200) {
            return;
        }

        fillTable(JSON.parse(request.response));
    };
    request.send();
}

// fill files table using the response from the API call
function fillTable(files) {
    var tableContent = document.getElementById('content');

    tableContent.innerHTML = "";
    var rows = "";
    for (var i = 0; i < files.length; i++) {
        rows = rows + "<tr><td>" + files[i].name + "</td><td>" + files[i].modified + "</td><td>" + files[i].type + "</td></tr>";
    }
    tableContent.innerHTML = rows;
}

// upload  a new file to dropbox using the API
function uploadFile() {
    var formData = new FormData();

    formData.append("file", document.getElementById("newFile").files[0]);

    var request = new XMLHttpRequest();
    request.open("POST", "/files");

    request.onreadystatechange = function () {
        if (request.readyState == 4 && request.status == 200) {
            alert("File Upload success!");
        }

        if (request.readyState == 4 && request.status != 200) {
            alert("File Upload error!");
        }
    };

    request.send(formData);
}