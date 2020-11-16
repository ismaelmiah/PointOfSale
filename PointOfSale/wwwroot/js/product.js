var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#myTable').DataTable({
        "ajax": {
            "url": "/Product/GetAllData"
        },
        "retrieve": true,
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "description", "width": "20%" },
            { "data": "category.name", "width": "10%" },
            { "data": "buyPrice", "width": "10%" },
            { "data": "salePrice", "width": "10%" },
            { "data": "quantity", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a onclick=Delete("/Product/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i> 
                                </a>
                                <a href="/Product/Edit/${data}" class="btn btn-warning text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a href="/Product/Details/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-info-circle"></i> 
                                </a>
                            </div>
                           `;
                }, "width": "15%"
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
        dangerMode:true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        window.toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        window.toastr.error(data.message);
                    }
                }
            });
        }
    });
}