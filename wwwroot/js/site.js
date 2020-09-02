// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const request = new XMLHttpRequest();
const url = 'Product/GetAll';
request.open("GET", url);
request.send();

request.onreadystatechange = (e) => {
    console.log(request.responseText)
}