function openRegistrationModalForm()
{
    const registrationModalForm = $('#AccountModal');
    $.ajax({
        url: '/Account/Registration',
        type: 'GET',
        success: function (response) {
            registrationModalForm.find('.modal-body').html(response);
            registrationModalForm.find('#AccountModalLabel').text("Регистрация");
            registrationModalForm.find('.btn-primary').text("Зарегистрироваться");

            registrationModalForm.find('.btn-primary').click(function () {
                Registration(registrationModalForm);
            });
            registrationModalForm.modal('show');
        }
    });
}

function Registration(modalForm) {
    var form = modalForm.find('#registrationForm');
    var formData = new FormData(form[0]);

    $.ajax({
        url: form.attr('action'),
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            if (response.redirectUrl) {
                window.location.href = response.redirectUrl;
            }
            else {
                $('#AccountModal .modal-body').html(response);
            }
        },
        error: function (error) {
            // Обработка ошибок
        }
    });
}

function openLoginModalForm() {
    const loginModalForm = $('#AccountModal');
    $.ajax({
        url: '/Account/Login',
        type: 'GET',
        success: function (response) {
            loginModalForm.find('.modal-body').html(response);
            loginModalForm.find('#AccountModalLabel').text("Вход в аккаунт");
            loginModalForm.find('.btn-primary').text("Войти");

            loginModalForm.find('.btn-primary').click(function () {
                Login(loginModalForm);
            });

            loginModalForm.modal('show');
        }
    });
}

function Login(modalForm) {
    var form = modalForm.find('#registrationForm');
    var formData = new FormData(form[0]);

    $.ajax({
        url: form.attr('action'),
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            if (response.redirectUrl) {
                window.location.href = response.redirectUrl;
            }
            else {
                $('#AccountModal .modal-body').html(response);
            }
        },
        error: function (error) {
            // Обработка ошибок
        }
    });
}