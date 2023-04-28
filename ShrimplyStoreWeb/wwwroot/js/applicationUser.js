var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable(
        {
            "ajax": { url: '/admin/applicationuser/getall' },
            "columns":
                [
                    { data: 'name', "width": "15%" },
                    { data: 'email', "width": "15%" },
                    { data: 'company.name', "width": "15%" },
                    { data: 'phoneNumber', "width": "15%" },
                    { data: 'role', "width": "15%" },
                    {
                        data: { id: "id", lockoutEnd: "lockoutEnd" },
                        "render": function (data) {
                            var today = new Date().getTime();
                            var lockout = new Date(data.lockoutEnd).getTime();

                            if (lockout > today) {
                                return `<div class="text-centre" >
                                    <a onclick=LockUnlock('${data.id}') class="btn btn-success" style="cursor:pointer">
                                    <i class="bi bi-unlock-fill"></i>Unlock
                                    </a>
                                    <a href="/admin/ApplicationUser/RoleManagement?userid=${data.id}" class="btn btn-danger" style="cursor:pointer">
                                    <i class="bi bi-pencil-square"></i>Permission
                                    </a>
                                    </div>`
                            }
                            else {
                                return `<div class="text-centre" >
                                    <a onclick=LockUnlock('${data.id}') class="btn btn-danger" style="cursor:pointer">
                                    <i class="bi bi-lock-fill"></i>Lock
                                    </a>
                                    <a href="/admin/ApplicationUser/RoleManagement?userId=${data.id}" class="btn btn-danger" style="cursor:pointer">
                                    <i class="bi bi-pencil-square"></i>Permission
                                    </a>
                                    </div>`
                            }
                            S
                        },
                        "width": "25%"
                    },
                ]
        });
}

function LockUnlock(id) {
    $.ajax({
        type: "POST",
        url: '/Admin/ApplicationUser/LockUnlock',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                if (data.message == "Unlocking successful") {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                    dataTable.ajax.reload();
                }
                
            }
        }
    })
}