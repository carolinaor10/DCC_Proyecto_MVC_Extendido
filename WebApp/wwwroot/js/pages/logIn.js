//Clase JS que es el controlador de la vista.
//Cities.cshtml

//Definicion de la clase
function UsersController() {

    this.ViewName = "Users";
    this.ApiService = "UserCRUD";
   

    this.InitView = function () {

        console.log("User view init!!!");

        //Binding del evento del clic al metodo de create del controlador
        $("#btnLogIn").click(function () {
            var vc = new UsersController();
            vc.LogIn();
        })


        $("#btnCancel").click(function () {
            window.location.href = "/Index";
        });

    }

    this.LogIn = function () {


        //Crear un DTO de USER

        var users = {};
        users.email = $("#txtEmail").val();
        users.password = $("#txtPassword").val();
      
        //Llamado al API
        var ctrlActions = new ControlActions();
        var serviceRoute = this.ApiService + "/LogIn";

        var ctrlActions = new ControlActions();

        //Ruta del API para concluir el servicio
        var urlService = ctrlActions.GetUrlApiService(this.ApiService + "/RetrieveByEmail2/" + users.email);


        console.log(urlService);
        
        var response = {};
        try {
            $.ajax({
                type: "GET",
                url: urlService,
                cache: false,
                async: false,
                success: function (data) {
                    
                    response = data;
                }
            });
        } catch (err) {
            console.log(err);
        }
        console.log(JSON.stringify(response));

       if (response != null) {
            sessionStorage.setItem("SESSION_USER", JSON.stringify(response));
           console.log(sessionStorage.getItem("SESSION_USER"));
        };


        ctrlActions.PostToAPI(serviceRoute, users, function () {
            console.log("User logged in ---> " + JSON.stringify(users))
            Swal.fire(
                '¡Éxito!',
                'Inicio de sesión correcto',
                'success'
            ).then(() => {
                window.location.href = "https://localhost:7081/Index"; 
            });

        });

        

        console.log(JSON.stringify(users));

    }


}

//Instanciamiento de la clase
$(document).ready(function () {
    var viewCont = new UsersController();
    viewCont.InitView();
})
