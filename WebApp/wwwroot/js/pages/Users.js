//clase js controlador de la vista
//no refferimos a controlador partiendo del supuesto que
//esta clase controla el comportamiento de la vista o pagina

//definimos la clase
function UserController() {
    this.ViewName = "Users";
    this.ApiService = "User";

    //metodo a ejecutar al inicio de la vista
    this.InitView = function () {
        console.log("User view init!!")
        //bind del click del boton con la funcion correspondiente.
        $("#btnCreate").click(function () {
            var vc = new UserController();
            vc.Create();
        })

        $("#btnUpdate").click(function () {
            var vc = new UserController();
            vc.Update();
        })

        $("#btnDelete").click(function () {
            var vc = new UserController();
            vc.Delete();
        })

        //carga de la tabla
        this.LoadTable();
    }

    this.Create = function () {
        //Crear un DTO de user
        var user = {};
        user.identity = $("#txtCed").val();
        user.name = $("#txtName").val();
        user.lastName1 = $("#txtLastNameOne").val();
        user.lastName2 = $("#txtLastNameTwo").val();
        user.email = $("#txtEmail").val();
        user.phoneNumber = $("#txtPhone").val();
        user.numDpt = $("#txtNumDptl").val();
        user.hour = $("#txtHour").val();
        user.condominium = $("#txtCondo").val();
        user.entryMethod = $("#txtEntry").val();
        user.role = $("#selectRol").val(),
        user.password = $("#txtPassword").val()

        //invocar al API
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Create";

        ca.PostToAPI(serviceRoute, user, function () {
            console.log("User created -->" + JSON.stringify(user));
        });
    }
    /*******voy aca*****/
    this.Update = function () {
        
        var user = {};
        user.id = $("#txtId").val();
        user.identity = $("#txtCed").val();
        user.name = $("#txtName").val();
        user.lastName1 = $("#txtLastNameOne").val();
        user.lastName2 = $("#txtLastNameTwo").val();
        user.email = $("#txtEmail").val();
        user.phoneNumber = $("#txtPhone").val();
        user.numDpt = $("#txtNumDptl").val();
        user.hour = $("#txtHour").val();
        user.condominium = $("#txtCondo").val(),
        user.entryMethod = $("#txtEntry").val(),
        user.role = $("#selectRol").val(),
        user.password = $("#txtPassword").val()
        

        // Invocar al API
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Update";

        ca.PutToAPI(serviceRoute, user, function () {
            console.log("User updated -->" + JSON.stringify(user));

        });
    }

    this.Delete = function () {
        
        var user = {};
        user.id = $("#txtId").val();
        user.identity = "123";
        user.name = "name";
        user.lastName1 = "lastName1";
        user.lastName2 = "lastName2";
        user.email = "email";
        user.phoneNumber = "phoneNumber";
        user.numDpt = 12;
        user.hour = new Date().toISOString();
        user.condominium = "condominium";
        user.entryMethod = "entryMethod";
        user.role = "role";
        user.password = "password";

        // Invocar al API
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Delete";

        ca.DeleteToAPI(serviceRoute, user, function () {
            console.log("User deleted -->" + JSON.stringify(user));

        });
    }

    //metodo para la carga de las tablas
    this.LoadTable = function () {

        var ca = new ControlActions();

        //ruta API para consumir el servicio
        var urlService = ca.GetUrlApiService(this.ApiService + "/RetrieveAll");

        //Definir las columnas a extraer de json de respuesta
        var columns = [];
        columns[0] = { 'data': 'id' }
        columns[1] = { 'data': 'identity' }
        columns[2] = { 'data': 'name' }
        columns[3] = { 'data': 'lastName1' }
        columns[4] = { 'data': 'lastName2' }
        columns[5] = { 'data': 'email' }
        columns[6] = { 'data': 'phoneNumber' }
        columns[7] = { 'data': 'numDpt' }
        columns[8] = { 'data': 'hour' }
        columns[9] = { 'data': 'condominium' }
        columns[10] = { 'data': 'entryMethod' }
        columns[11] = { 'data': 'role' }


        //inicializar la tabla como un data table
        $("#tblUsers").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });

        //Asignacion de evento al click de la fila de la tabla
        $('#tblUsers tbody').on('click', 'tr', function () {

            //extrae la fila a la que le dio click
            var row = $(this).closest('tr');

            //extraer la data del registro contenido en la fila
            var user = $('#tblUsers').DataTable().row(row).data();

            //Mapeo de los valores del objeto data con el formulario
            $("#txtId").val(user.id);
            $("#txtCed").val(user.identity);
            $("#txtName").val(user.name);
            $("#txtLastNameOne").val(user.lastName1);
            $("#txtLastNameTwo").val(user.lastName2);
            $("#txtEmail").val(user.email);
            $("#txtPhone").val(user.phoneNumber);
            $("#txtNumDptl").val(user.numDpt);
            $("#txtHour").val(user.hour);
            $("#txtCondo").val(user.condominium);
            $("#txtEntry").val(user.entryMethod);
            $("#selectRole").val(user.role);
            $("#txtPassword").val(user.password);

          //  $('btnCreate').prop('disable', true);
        });
    }
}

//instanciamiento de la clase
$(document).ready(function () {
    var vc = new UserController();
    vc.InitView();
})