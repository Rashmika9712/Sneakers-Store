var dataTable;
$(document).ready(function () {
    dataTable=$('#DT_load').DataTable({
        "ajax": {
            "url": "/api/ShoeItem",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "price", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "shoeType.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="btn-group w-100">
                                <a href="/Admin/ShoeItems/upsert?id=${data}" class="btn clr text-light">
                                    <i class="bi bi-pencil-square">&nbsp;Edit</i>
                                </a>&nbsp;
                                <a onClick=Delete('/api/ShoeItem/'+${data}) class="btn btn-danger">
                                    <i class="bi bi-trash-fill">&nbsp;Delete</i>
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
                        toastr.success(data.message);
                    }
                    else {
                        //failure notification
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}