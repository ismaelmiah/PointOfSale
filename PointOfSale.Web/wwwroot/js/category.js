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
            LoadFunctionsForUpsert();
        }).fail(function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status);
            console.log(thrownError);
        });
    });


    $('#myTable').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": "/Category/GetAllData",
        "columnDefs": [
            {
                "orderable": false,
                "targets": 6,
                "render": function (data, type, row) {
                    return `
                                    <button type="submit" class="btn btn-warning courseEdit btn-sm" data-id='${data}' value='${data}'>
                                        Edit
                                    </button>
                                    <button type="submit" class="btn btn-danger btn-sm show-bs-modal" href="#" data-id='${data}' value='${data}'>
                                        Delete
                                    </button>`;
                }
            }
        ]
    });
});

function LoadFunctionsForUpsert()
{
    $("#categorySubmit").click(function (){
        var form = $("#categoryForm");
        form.submit();
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