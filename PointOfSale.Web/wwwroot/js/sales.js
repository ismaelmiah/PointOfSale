var dataTable;

$(document).ready(function () {
    loadDataTable();

    $("#Quantity").on("chnage", function()
    {
        console.log(this);
    });
});


function loadDataTable() {
    
    dataTable = $('#myTable').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": "/SaleDetail/GetAllData",
        "columnDefs": [
            {
                "orderable": false,
                "targets": 5,
                "render": function (data, type, row) {
                    return `
                            <button type="submit" class="btn btn-warning saledetailEdit btn-sm" data-id='${data}' value='${data}'>
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
        Delete(`/SaleDetail/delete?id=${id}`);
    });


    $('#myTable').on('click', '.saledetailEdit', function (event) {
        var id = $(this).data("id");
        var modal = $("#modal-Upsert");
        modal.modal('show');
        $.ajax({
            method: "GET",
            url: "SaleDetail/Upsert?id="+id
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
}


function Delete(url) {
    swal({
        title: 'Are you sure you want to Delete?',
        text: "This Product will be added to Product List!",
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