let gendercount = [0, 0]
let countkelahiran = [0, 0, 0]

$(document).ready(function () {
    $('#Table').DataTable({
        dom: "Blfrtip",
        buttons: [
        {
            extend: 'colvis',
            postfixButtons: ['colvisRestore'],
            collectionLayout: 'fixed two-column',
            className: 'btn btn-primary'
            }
            , 'copy', 'csv', 'excel', 'pdf', 'print'
        ]
    });
});
$(document).ready(function () {
    let no = 1;
    let table = new DataTable('#myTable', {
        
        dom: '<"toolbar">frtip Blfrtip',
        initComplete: function ()  //Adding Custom button in Tools
        {
            $("div.toolbar").html('<button type="button" onclick="addNewEntry()">Add a New Record</button>');
        },
        buttons: [{
            extend: 'colvis',
            postfixButtons: ['colvisRestore'],
            className:'btn btn-primary'
        },
            'copy', 'csv', 'excel', 'pdf', 'print'
        ],
        ajax: {
            url: "https://localhost:7191/api/employee",
            dataSrc: "data",
            dataType: "JSON"
        },
        columns: [
            {
                data: '',
                render: function (data, type, row) {
                    return no++;
                }
            },
            {
                data: '',
                render: function (data, type, row) {
                    return `${row.firstName} ${row.lastName}`;
                }
            },
            { data: 'nik' },
            { data: "phoneNumber" },
            {
                data: 'gender',
                render: function (data, type, row) {
                    return data == 0?"Female":"Male"
                }
            },
            
            { data: "email" },
            {
                data: '',
                render: function (data, type, row) {
                    return `<button onclick="detailData('${row.guid}')" data-bs-toggle="modal" data-bs-target="#detailTable" class="btn btn-primary"><i class="fa-solid fa-circle-info"></i></button>
                            <button onclick="editData('${row.guid}')" class="btn btn-warning" data-bs-toggle="modal" data-bs-target="#editTable"><i class="fa-solid fa-pen-to-square"></i></button>
                            <button onclick="deleteData('${row.guid}')" class="btn btn-danger"><i class="fa-solid fa-trash"></i></button>`;
                }
            }
        ]
    });
});
function tambahData() {
    var employee = new Object();
    employee.firstName      = $("#firstName").val();
    employee.lastName       = $("#lastName").val();
    employee.email          = $("#email").val();
    employee.birthDate      = $("#birthDate").val();
    employee.hiringDate     = $("#hiringDate").val();
    employee.gender         = parseInt($("#gender").val());
    employee.phoneNumber    = $("#phoneNumber").val();
    $.ajax({
        url     : "https://localhost:7191/api/employee",
        type    : "POST",
        data: JSON.stringify(employee),
        contentType: "application/json"
    }).done((result) => {
        alert("Insert Succesfully")
        location.reload()
    }).fail((error) => {
        alert("Insert Fail")
    })
}
function detailData(guid) {
    $.ajax({
        url: "https://localhost:7191/api/employee/"+guid,
        type:"GET",
    }).done((employee) => {
        console.log(employee.data)
        let gender = ""
        if (employee.data.gender === 0)
        {
            gender = "Female"
        }
        else
        {
            gender = "Male"
        }
        let detail = `<div class="card text-center mb-3" style="width: 100%;">
                      <div class="card-body">
                        <h5 class="card-title">${employee.data.firstName} ${employee.data.lastName}</h5>
                        <div class="row align-left">
                            <div class="col">NIK</div>
                            <div class="col">${employee.data.nik}</div>
                        </div>
                        <div class="row align-left">
                            <div class="col">Birth Date</div>
                            <div class="col">${employee.data.birthDate}</div>
                        </div>
                        <div class="row align-left">
                            <div class="col">Hiring Date</div>
                            <div class="col">${employee.data.hiringDate}</div>
                        </div>
                        <div class="row align-left">
                            <div class="col">Gender</div>
                            <div class="col">${gender}</div>
                        </div>
                        <div class="row align-left">
                            <div class="col">Phone Number</div>
                            <div class="col">${employee.data.phoneNumber}</div>
                        </div>
                        <div class="row align-left">
                            <div class="col">Email</div>
                            <div class="col">${employee.data.email}</div>
                        </div>
                        <div class="row align-left">
                            <div class="col"></div>
                            <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                        </div>
                      </div>
                    </div>
                    `
        $("#Detail").html(detail)
    })
    
}
function deleteData(guid) {
    $.ajax({
        url:"https://localhost:7191/api/employee/?guid=" + guid,
        type:"DELETE"
    }).done((result) => {
        alert("Delete Successfully")
        location.reload()
    }).fail((error) => {
        alert("Delete Failed")
    })
}
function editData(guid) {
    $.ajax({
        url: "https://localhost:7191/api/employee/" + guid,
        type:"GET"
    }).done((result) => {
        console.log(result.data)
        $("#guid").val(result.data.guid);
        $("#firstName").val(result.data.firstName);
        $("#lastName").val(result.data.lastName);
        $("#email").val(result.data.email);
        $("#birthDate").val(result.data.birthDate);
        $("#hiringDate").val(result.data.hiringDate);
        $("#gender").val(result.data.gender);
        $("#phoneNumber").val(result.data.phoneNumber);
        $("#editTable").modal("show");
    })
}
$.ajax({
    url: "https://localhost:7191/api/employee/",
    type: "GET"
}).done((result) => {
    $.each(result.data, (key, val) => {
        val.gender === 0 ? gendercount[0]++:gendercount[1]++

    })
    
    const ctx = document.getElementById('myChart');
    new Chart(ctx, {
        type: 'pie',
        data: {
            labels: ['Female','Male'],
            datasets: [{
                label: 'Gender',
                data: [gendercount[0],gendercount[1]],
                borderWidth: 0,
                backgroundColor: [
                    'rgb(255, 99, 132)',
                    'rgb(54, 162, 235)',
                ],
                hoverOffset: 4
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
})
$.ajax({
    url: "https://localhost:7191/api/education",
    type: "GET"
}).done((result) => {
    $.each(result.data, (key, val) => {
        console.log(val)
        if (val.major === "S1") {
            countkelahiran[0]++
        }
        else if (val.major === "S2") {
            countkelahiran[1]++
        }
        else {
            countkelahiran[2]++
        }
        console.log(countkelahiran)
    })
    
    const chartDegree = document.getElementById('chartAge');
    new Chart(chartDegree, {
        type: 'bar',
        data: {
            labels: ['S1','S2','S3'],
            datasets: [{
                label: ['Degree'],
                data: [countkelahiran[0],countkelahiran[1],countkelahiran[2]],
                borderWidth: 0,
                backgroundColor: [
                    'rgb(255, 99, 132)',
                    'rgb(54, 162, 235)',
                    '#9BD0F5',
                ],
                hoverOffset: 4
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
})