$(document).ready(function () {
    dataTable = $('#myTable').DataTable({
        "ajax": {
            "url": "/api/Product",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "25%" }, // "name" is case sensitive
            { "data": "description", "width": "15%" }, // "price" is case sensitive
            { "data": "price", "width": "15%" },
            { "data": "estRetailValue", "width": "15%" },
            { "data": "rateCondition.description", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group">
                                <a href="/Admin/Products/upsert?id=${data}" class="btn btn-primary mx-2">
                                    <i class="bi bi-pencil-square"></i>
                                </a>
                                <a onClick=Delete('/api/Products/'+${data}) class="btn btn-danger mx-2">
                                    <i class="bi bi-trash-fill"></i>
                                </a>
                            </div>`
                },
                "width": "15%"
            }
        ],
        "width": "100%"
    });
});

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        //success notification
                        toastr.warning(data.message);
                    }
                    else {
                        //failsure notification
                        toastr.error(data.message);
                    }
                }
            })
        }
    })

}