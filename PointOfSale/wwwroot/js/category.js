var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#myTable').DataTable({
        "ajax": {
            "url": "/Category/GetAllData"
        },
        "retrieve": true,
        "columns": [
            { "data": "name", "width": "10%" },
            { "data": "description", "width": "20%" },
            { "data": "noOfProduct", "width": "15%" },
            { "data": "invest", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a onclick=Delete("/Category/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i> 
                                </a>
                                <a href="/Category/Edit/${data}" class="btn btn-warning text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a href="/Category/Details/${data}" class="btn btn-info text-white" style="cursor:pointer">
                                    <i class="fas fa-info-circle"></i> 
                                </a>
                            </div>
                           `;
                }, "width": "20%"
            }
        ],
        "language": {
            "sLengthMenu": "Show _MENU_"
        }
    });
}


function Delete(url) {
    swal({
        title: 'Are you sure you want to Delete?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}