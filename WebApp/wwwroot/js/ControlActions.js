function ControlActions() {
    // Ruta base del API
    this.URL_API = "https://localhost:7081/api/";
    this.ApiService = "User";

    this.ApiService = "User"; 

    this.GetUrlApiService = function (service) {
        return this.URL_API + service;
    };

    this.LoadTable = function () {
        var self = this; // Guardar referencia al contexto actual

        var ca = new ControlActions();
        var urlService = ca.GetUrlApiService(self.ApiService + "/RetrieveAll");

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

    this.GetUrlApiService = function (service) {
        return this.URL_API + service;
    };

    this.GetTableColumsDataName = function (tableId) {
        var val = $('#' + tableId).attr("ColumnsDataName");
        return val;
    };

    this.FillTable = function (service, tableId, refresh) {
        if (!refresh) {
            var columns = this.GetTableColumsDataName(tableId).split(',');
            var arrayColumnsData = columns.map(function (value) {
                return { data: value };
            });

            // Inicialización de DataTable
            $('#' + tableId).DataTable({
                "processing": true,
                "ajax": {
                    "url": this.GetUrlApiService(service),
                    "dataSrc": ''
                },
                "columns": arrayColumnsData
            });
        } else {
            // Recargar la tabla
            $('#' + tableId).DataTable().ajax.reload();
        }
    };

    this.GetSelectedRow = function (tableId) {
        var data = sessionStorage.getItem(tableId + '_selected');
        return data;
    };

    this.BindFields = function (formId, data) {
        console.log(data);
        $('#' + formId + ' *').filter(':input').each(function () {
            var columnDataName = $(this).attr("ColumnDataName");
            this.value = data[columnDataName];
        });
    };

    this.GetDataForm = function (formId) {
        var data = {};
        $('#' + formId + ' *').filter(':input').each(function () {
            var columnDataName = $(this).attr("ColumnDataName");
            data[columnDataName] = this.value;
        });
        console.log(data);
        return data;
    };

    /* ACCIONES VIA AJAX, O ACCIONES ASINCRONAS */

    this.PostToAPI = function (service, data, callBackFunction) {
        $.ajax({
            type: "POST",
            url: this.GetUrlApiService(service),
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (callBackFunction) {
                    Swal.fire(
                        'Good job!',
                        'Transaction completed!',
                        'success'
                    );
                    callBackFunction(data);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var responseJson = jqXHR.responseJSON;
                var message = jqXHR.responseText;

                if (responseJson) {
                    var errors = responseJson.errors;
                    var errorMessages = Object.values(errors).flat();
                    message = errorMessages.join("<br/> ");
                }
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    html: message,
                    footer: 'UCenfotec'
                });
            }
        });
    };

    this.PutToAPI = function (service, data, callBackFunction) {
        $.ajax({
            type: "PUT",
            url: this.GetUrlApiService(service),
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                Swal.fire(
                    'Good job!',
                    'Transaction completed!',
                    'success'
                );
                if (callBackFunction) {
                    callBackFunction(response);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var responseJson = jqXHR.responseJSON;
                var message = jqXHR.responseText;

                if (responseJson) {
                    var errors = responseJson.errors;
                    var errorMessages = Object.values(errors).flat();
                    message = errorMessages.join("<br/> ");
                }
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    html: message,
                    footer: 'UCenfotec'
                });
            }
        });
    };

    this.DeleteToAPI = function (service, data, callBackFunction) {
        $.ajax({
            type: "DELETE",
            url: this.GetUrlApiService(service),
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                Swal.fire(
                    'Good job!',
                    'Transaction completed!',
                    'success'
                );
                if (callBackFunction) {
                    callBackFunction(response);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var responseJson = jqXHR.responseJSON;
                var message = jqXHR.responseText;

                if (responseJson) {
                    var errors = responseJson.errors;
                    var errorMessages = Object.values(errors).flat();
                    message = errorMessages.join("<br/> ");
                }
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    html: message,
                    footer: 'UCenfotec'
                });
            }
        });
    };

    this.GetToApi = function (service, callBackFunction) {
        $.ajax({
            type: "GET",
            url: this.GetUrlApiService(service),
            success: function (response) {
                console.log("Response " + response);
                if (callBackFunction) {
                    callBackFunction(response);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var responseJson = jqXHR.responseJSON;
                var message = jqXHR.responseText;

                if (responseJson) {
                    var errors = responseJson.errors;
                    var errorMessages = Object.values(errors).flat();
                    message = errorMessages.join("<br/> ");
                }
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    html: message,
                    footer: 'UCenfotec'
                });
            }
        });
    };
}

// Custom jQuery actions
$.put = function (url, data, callback) {
    if ($.isFunction(data)) {
        callback = data;
        data = {};
    }
    return $.ajax({
        url: url,
        type: 'PUT',
        success: callback,
        data: JSON.stringify(data),
        contentType: 'application/json'
    });
};

$.delete = function (url, data, callback) {
    if ($.isFunction(data)) {
        callback = data;
        data = {};
    }
    return $.ajax({
        url: url,
        type: 'DELETE',
        success: callback,
        data: JSON.stringify(data),
        contentType: 'application/json'
    });
};
