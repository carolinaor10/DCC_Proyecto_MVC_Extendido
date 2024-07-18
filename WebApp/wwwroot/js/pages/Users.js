// Clase UserController
function UserController() {
    this.ViewName = "Users";
    this.ApiService = "User";

    this.InitView = function () {
        var self = this; // Guardar referencia al contexto actual

        console.log("User view init!!");

        // Asignación de eventos a botones
        $("#btnCreate").click(function () {
            self.Create();
        });

        $("#btnUpdate").click(function () {
            self.Update();
        });

        $("#btnDelete").click(function () {
            self.Delete();
        });

        // Carga inicial de la tabla
        self.LoadTable();
    };

    this.Create = function () {
        var user = {
            identity: $("#txtCed").val(),
            name: $("#txtName").val(),
            lastName1: $("#txtLastNameOne").val(),
            lastName2: $("#txtLastNameTwo").val(),
            email: $("#txtEmail").val(),
            phoneNumber: $("#txtPhone").val(),
            numDpt: $("#txtNumDptl").val(),
            hour: $("#txtHour").val(),
            role: $("#selectRole").val(),
            password: $("#txtPassword").val()
        };

        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Create";

        ca.PostToAPI(serviceRoute, user, function () {
            console.log("User created -->" + JSON.stringify(user));
            // Recargar la tabla después de crear un usuario
            self.LoadTable();
        });
    };

    this.Update = function () {
        var user = {
            id: $("#txtId").val(),
            identity: $("#txtCed").val(),
            name: $("#txtName").val(),
            lastName1: $("#txtLastNameOne").val(),
            lastName2: $("#txtLastNameTwo").val(),
            email: $("#txtEmail").val(),
            phoneNumber: $("#txtPhone").val(),
            numDpt: $("#txtNumDptl").val(),
            hour: $("#txtHour").val(),
            role: $("#selectRole").val(),
            password: $("#txtPassword").val()
        };

        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Update";

        ca.PutToAPI(serviceRoute, user, function () {
            console.log("User updated -->" + JSON.stringify(user));
            // Recargar la tabla después de actualizar un usuario
            self.LoadTable();
        });
    };

    this.Delete = function () {
        var user = {
            id: $("#txtId").val()
        };

        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Delete";

        ca.DeleteToAPI(serviceRoute, user, function () {
            console.log("User deleted -->" + JSON.stringify(user));
            // Recargar la tabla después de eliminar un usuario
            self.LoadTable();
        });
    };

    this.LoadTable = function () {
        var self = this; // Guardar referencia al contexto actual

        var ca = new ControlActions();
        var urlService = ca.GetUrlApiService(this.ApiService + "/RetrieveAll");

        var columns = [
            { data: 'id' },
            { data: 'identity' },
            { data: 'name' },
            { data: 'lastName1' },
            { data: 'lastName2' },
            { data: 'email' },
            { data: 'phoneNumber' },
            { data: 'numDpt' },
            { data: 'hour' },
            { data: 'role' },
            { data: 'password' }
        ];

        // Inicialización de DataTable
        $("#tblUsers").DataTable({
            ajax: {
                url: urlService,
                dataSrc: ""
            },
            columns: columns
        });

        // Asignación de evento al clic de la fila de la tabla
        $('#tblUsers tbody').on('click', 'tr', function () {
            var row = $(this).closest('tr');
            var user = $('#tblUsers').DataTable().row(row).data();

            // Asignar valores a los campos del formulario
            $("#txtId").val(user.id);
            $("#txtCed").val(user.identity);
            $("#txtName").val(user.name);
            $("#txtLastNameOne").val(user.lastName1);
            $("#txtLastNameTwo").val(user.lastName2);
            $("#txtEmail").val(user.email);
            $("#txtPhone").val(user.phoneNumber);
            $("#txtNumDptl").val(user.numDpt);
            $("#txtHour").val(user.hour);
            $("#selectRole").val(user.role);
            $("#txtPassword").val(user.password);
        });
    };
}

// Inicialización de la vista
$(document).ready(function () {
    var vc = new UserController();
    vc.InitView();
});
