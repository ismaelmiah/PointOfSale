var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#myTable').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": "/MonthDetail/GetAllData",
        "columnDefs": [
            {
                "orderable": false,
                "targets": 5,
            }
        ]
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