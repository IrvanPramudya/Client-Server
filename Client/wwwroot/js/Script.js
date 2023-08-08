// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*let admin = document.getElementById("admin")
admin.addEventListener("click", () =>
    alert("Selamat Anda dapat 1 Juta"))*/
let tombol = document.querySelector(".tombol")
let tombol2 = document.querySelector(".tombol2")
let warna = document.getElementById("warna")
tombol.addEventListener('click', () => {
    if (warna.style.backgroundColor == "grey") {
        warna.style.backgroundColor = "white"
    }
    else {
        warna.style.backgroundColor = "grey"
    }
})
let kalimat = document.getElementById("kalimat")
tombol2.addEventListener('click', () => {
    if (kalimat.innerHTML == "Namanya Siapa") {
        kalimat.innerHTML = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Culpa quisquam iusto nemo laborum laudantium eaque ea, facilis id, accusantium sed neque dolorem fugiat illum tenetur ab, aspernatur illo modi aut."
    }
    else {
        kalimat.innerHTML = "Namanya Siapa"
    }
})
let k = document.querySelector("#k")
k.addEventListener("mouseout", () =>
    k.style.backgroundColor = "white")
k.addEventListener("mouseover", () =>
    k.style.backgroundColor = "red")
//asynchronous Javascript
$.ajax({
    url: "https://swapi.dev/api/people/"
}).done((result) => {
    //console.log(result);
    let temp = "";
    let dataSW = "";
    let no = 1;
    $.each(result.results, (key, val) => {
        console.log(result)
        temp += "<li>" + val.name + "</li>";
        dataSW +=
        "<tr>"+
            "<td>" + no             + "</td>"+
            "<td>" + val.name       + "</td>"+
            "<td>" + val.height     + "</td>"+
            "<td>" + val.gender     + "</td>"+
            "<td>" + val.skin_color + "</td>"+
            "<td>" + val.hair_color + "</td>"+
        "</tr>";
        /*dataSW += "<tr>"
        dataSW += "<td>"+no +"</td>";
        dataSW += "<td>"+ val.name+"</td>";
        dataSW += "<td>"+ val.height+"</td>";
        dataSW += "<td>" + val.gender + "</td>";
        dataSW += "<td>" + val.skin_color + "</td>";
        dataSW += "<td>" + val.hair_color + "</td>";
        dataSW += "</tr>";*/
        no++;
    })
    $("#listSW").html(temp);
    $("#tbodySW").html(dataSW);
});

    