var dataTable;

$(document).ready(function () {
    $("#addproduct").on('click', function () {
        var modal = $("#modal-upsert");
        modal.modal('show');
        $.ajax({
            method: "GET",
            url: "/Product/Upsert"
        }).done(function (response) {
            $("#contentArea").html(response);
            $("#modal-upsert").modal('toggle');
            $("#Submit").click(function (){
                var form = $("#productForm");
                if(form.valid()){
                    form.submit();
                }
            });
        }).fail(function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status);
            console.log(thrownError);
        });
    });

    dataTable = $('#myTable').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": "/Product/GetAllData",
        "columnDefs": [
            {
                "orderable": false,
                "targets": 5,
                "render": function (data, type, row) {
                    return `
                            <button type="submit" class="btn btn-success productSale btn-sm" data-id='${data}' value='${data}'>
                                Sale Product
                            </button>
                            <button type="submit" class="btn btn-warning productEdit btn-sm" data-id='${data}' value='${data}'>
                                Edit
                            </button>
                            <button type="submit" class="btn btn-danger btn-sm show-bs-modal" href="#" data-id='${data}' value='${data}'>
                                Delete
                            </button>`;
                }
            }
        ]
    });

    $('#myTable').on('click', '.show-bs-modal', function (event) {
        var id = $(this).data("id");
        Delete(`/Product/delete?id=${id}`);
    });

    $('#myTable').on('click', '.productSale', function (event) {
        var id = $(this).data("id");
        var modal = $("#modal-Upsert");
        modal.modal('show');
        $.ajax({
            method: "GET",
            url: "SaleDetail/Upsert"
        }).done(function (response) {
            $("#contentArea").html(response);
            $("#modal-upsert").modal('toggle');
            $("#Submit").click(function (){
                var form = $("#saledetailForm");
                form.submit();
            });
        }).fail(function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status);
            console.log(thrownError);
        });
    });

    $('#myTable').on('click', '.productEdit', function (event) {
        var id = $(this).data("id");
        var modal = $("#modal-Upsert");
        modal.modal('show');
        $.ajax({
            method: "GET",
            url: "Product/Upsert?id="+id
        }).done(function (response) {
            $("#contentArea").html(response);
            $("#modal-upsert").modal('toggle');
            $("#Submit").click(function (){
                var form = $("#productForm");
                form.submit();
            });
        }).fail(function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status);
            console.log(thrownError);
        });
    });
});


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