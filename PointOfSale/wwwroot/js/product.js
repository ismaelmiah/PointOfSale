var dataTable;

$(document).ready(function () {
    $('#exampleModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var recipient = button.data('whatever');
        var modal = $(this);
        modal.find("#Product_Id").val(recipient);
    });
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
            { "data": "category.name", "width": "10%" },
            { "data": "price", "width": "10%" },
            { "data": "quantity", "width": "15%" },
            {
                "data": "id",
                "render": function (data, type, row) {
                    if (row.quantity === 0){
                        return `
                            <div class="text-center">
                                <a href="/Product/Details/${data}" class="btn btn-info text-white" style="cursor:pointer">
                                    <i class="fas fa-info-circle"></i> DETAIL
                                </a>
                                <button type="button" disabled="disabled" class="btn btn-success" data-toggle="modal"
                                data-target="#exampleModal" data-whatever="${data}" ><i class="fas fa-minus-circle"></i>Stock Out</button>\r\n
                            </div>
                           `;
                    } else {
                        return `
                            <div class="text-center">
                                <a href="/Product/Details/${data}" class="btn btn-info text-white" style="cursor:pointer">
                                    <i class="fas fa-info-circle"></i> DETAIL
                                </a>
                                <button type="button" class="btn btn-success" data-toggle="modal"
                                data-target="#exampleModal" data-whatever="${data}" ><i class="fas fa-minus-circle"></i> SALE</button>\r\n
                            </div>
                           `;
                    }
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