var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable(
        {
            "ajax": { url: '/admin/shrimp/getall' },
            "columns":
                [
                    { data: 'name', "width": "20%" },
                    { data: 'owner', "width": "15%" },
                    { data: 'price', "width": "15%" },
                    { data: 'barCode', "width": "15%" },
                    { data: 'species.name', "width": "15%" },
                    {
                        data: 'id',
                        "render": function (data) {
                            return `<div class="w-75 btn-group" role="group" >
                                    <a href="/admin/shrimp/upsert?id=${data}" class="btn btn-primary" ><i class="bi bi-pencil-square"></i>Edit</a>
                                    <a onClick=Delete('/admin/shrimp/delete/${data}') class="btn btn-danger" ><i class="bi bi-trash-fill"></i>Delete</a>
                                    </div>`
                        },
                        "width": "20%"
                    },
                ]
        });
}

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
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    }) }