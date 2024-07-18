function UsersController() {
    this.ViewName = "Users";
    this.ApiService = "User";

    this.InitView = function () {
        console.log("User view init!!!");

        // Binding del evento del clic al metodo de LogIn del controlador
        $("#btnLogIn").click(function () {
            var vc = new UsersController();
            vc.LogIn();
        });

        $("#btnCancel").click(function () {
            window.location.href = "/Index";
        });
    }

    this.LogIn = function () {
        // Obtener correo electrónico y contraseña ingresados por el usuario
        var email = $("#txtEmail").val();
        var password = $("#txtPassword").val();

        // Llamado al API para obtener el usuario por correo electrónico
        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.ApiService + "/RetrieveByEmail2/" + email);

        $.ajax({
            type: "GET",
            url: urlService,
            cache: false,
            success: function (response) {
                if (response != null) {
                    // Comparar la contraseña ingresada con la contraseña almacenada en la respuesta del servicio
                    if (response.password === password) {
                        // Almacenar la información de la sesión en sessionStorage
                        sessionStorage.setItem("SESSION_USER", JSON.stringify(response));

                        Swal.fire({
                            icon: 'success',
                            title: '¡Éxito!',
                            text: 'Inicio de sesión correcto',
                            footer: 'UCenfotec'
                        }).then(() => {
                            window.location.href = "/Index";
                        });
                    } else {
                        // Contraseña incorrecta
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Contraseña incorrecta',
                            footer: 'UCenfotec'
                        });
                    }
                } else {
                    // Usuario no encontrado
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Usuario no encontrado',
                        footer: 'UCenfotec'
                    });
                }
            },
            error: function () {
                // Error al llamar al servicio
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Ocurrió un error al intentar iniciar sesión',
                    footer: 'UCenfotec'
                });
            }
        });
    }

}

// Instanciamiento de la clase y configuración inicial de la vista
$(document).ready(function () {
    var viewCont = new UsersController();
    viewCont.InitView();
});
