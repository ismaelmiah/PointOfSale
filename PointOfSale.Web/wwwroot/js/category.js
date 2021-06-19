var dataTable;

$(document).ready(function () {
    $("#addcategory").on('click', function () {
        var modal = $("#modal-upsert");
        modal.modal('show');
        $.ajax({
            method: "GET",
            url: "/Category/Upsert"
        }).done(function (response) {
            $("#contentArea").html(response);
            $("#modal-upsert").modal('toggle');
            $("#categorySubmit").click(function (){
                var form = $("#categoryForm");
                form.submit();
            });
        
        }).fail(function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status);
            console.log(thrownError);
        });
    });

    dataTable = $('#myTable').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": "/Category/GetAllData",
        "columnDefs": [
            {
                "orderable": false,
                "targets": 5,
                "render": function (data, type, row) {
                    return `
                            <button type="submit" class="btn btn-warning categoryEdit btn-sm" data-id='${data}' value='${data}'>
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
        Delete(`/Category/delete?id=${id}`);
    });

    $('#myTable').on('click', '.categoryEdit', function (event) {
        var id = $(this).data("id");
        var modal = $("#modal-Upsert");
        modal.modal('show');
        $.ajax({
            method: "GET",
            url: "Category/Upsert?id="+id
        }).done(function (response) {
            $("#contentArea").html(response);
            $("#modal-upsert").modal('toggle');

            $("#categorySubmit").click(function (){
                var form = $("#categoryForm");
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