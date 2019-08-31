function GetAll() {
    var ele = document.getElementById("userlist");
    if (ele !== null) {
        while (ele.firstChild) {
            ele.removeChild(ele.firstChild);
        }
    }
    var data = $.ajax(
        {
            url: "api/getall",
            type: "GET",
            async: false
        });
    var obj = JSON.parse(data.responseText);
    obj.forEach(addfunc);
}

function GetAllParam() {
    var list = document.getElementById("listinp");
    if (list.value === "") {
        GetAll();
        return;
    }
    var ele = document.getElementById("userlist");
    if (ele !== null) {
        while (ele.firstChild) {
            ele.removeChild(ele.firstChild);
        }
    }
    var data = $.ajax(
        {
            url: `api/get/search?name=${list.value}`,
            type: "GET",
            async: false
        });
    var obj = JSON.parse(data.responseText);
    obj.forEach(addfunc);
}

function Remove() {
    var num = document.getElementById("userlist").value;
    $.ajax(
        {
            url: `api/remove?id=${num}`,
            method: "GET",
            async: false
        });
    window.location.href = "/";
}

function EditChanged() {
    var num = document.getElementById("userlist").value;
    window.location.href = `/edit?id=${num}`;
}

function addfunc(data) {
    var ele = document.getElementById("userlist");
    var op = document.createElement("option");
    op.appendChild(document.createTextNode(`ID:${data.ID} ${data.Title}.${data.Firstname} ${data.Surname}`));
    op.setAttribute("value", data.ID);
    ele.appendChild(op);
}