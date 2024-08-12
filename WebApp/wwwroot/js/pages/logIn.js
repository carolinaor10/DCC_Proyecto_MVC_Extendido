function UsersController() {
    this.ViewName = "Users";
    this.ApiService = "User";

    // Define el método login en la instancia
    this.login = async function () {
        var username = $("#txtEmail").val();
        var password = $("#txtPassword").val();
        var encryptedPassword = await encryptPassword(password);

        // Enviar el hash al servidor
        $.ajax({
            type: "POST",
            url: "https://localhost:7081/api/user/login",
            data: JSON.stringify({
                Email: username,
                Password: encryptedPassword,
                Identity: "123",
                Name: "John",
                LastName1: "Doe",
                LastName2: "Doe",
                PhoneNumber: "1234",
                NumDpt: 0,
                //Id: 0,
                //Created: new Date().toISOString(),
                Hour: new Date().toISOString(), // Convert Date to ISO string for compatibility,
                Condominium: "Cocos",
                EntryMethod: "A pie",
                Role: ""
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    Swal.fire({
                        icon: 'success',
                        title: '¡Éxito!',
                        text: 'Inicio de sesión correcto',
                        footer: 'UCenfotec'
                    }).then(() => {
                        window.location.href = "/Users";
                    });
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Contraseña incorrecta',
                        footer: 'UCenfotec'
                    });
                }
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Ocurrió un error al intentar iniciar sesión',
                    footer: 'UCenfotec'
                });
            }
        });
    }

    async function encryptPassword(password) {
        const encoder = new TextEncoder();
        const data = encoder.encode(password);

        const hashBuffer = await crypto.subtle.digest('SHA-256', data);

        
        const hashArray = Array.from(new Uint8Array(hashBuffer));
        const hashHex = hashArray.map(byte => byte.toString(16).padStart(2, '0')).join('');

        return hashHex;
    }

    this.InitView = function () {
        console.log("User view init!!!");

        // Binding del evento del clic al metodo de login del controlador
        $("#btnLogIn").click(function () {
            this.login(); // Llama al método login en la instancia actual
        }.bind(this)); // Usa bind(this) para que `this` dentro del manejador de eventos apunte a la instancia actual

        $("#btnCancel").click(function () {
            window.location.href = "/Index";
        });
    }
}

// Instanciamiento de la clase y configuración inicial de la vista
$(document).ready(function () {
    var viewCont = new UsersController();
    viewCont.InitView();
});
