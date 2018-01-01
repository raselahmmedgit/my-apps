var InternalUserRegister = function () {
    var frmId = "frmInternalUserRegister";
    
    var validateForm = function () {
        if ($().validate) {
            var form = $("#" + frmId);
            var error = $('.alert-danger', form);
            var success = $('.alert-success', form);

            $.validator.addMethod("phone", function (value, element) {
                return this.optional(element) || /^[0-9+]+$/.test(value);

            }, "not valid phone number!");

            $.validator.addMethod("emailformat", function (value, element) {
                // allow any non-whitespace characters as the host part
                return this.optional(element) || /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(value);
            }, 'Please enter a valid email address.');

            form.validate({
                doNotHideMessage: true,
                errorElement: 'span',
                errorClass: 'help-block help-block-error',
                focusInvalid: false,
                rules: {
                    UserType:{
                        required: true
                    },
                    FirstName: {
                        required: true,
                        maxlength: 50
                    },
                    LastName: {
                        required: true,
                        maxlength: 50
                    },
                    Email: {
                        required: true,
                        emailformat: true,
                        maxlength: 129
                    },
                    MobileNo: {
                        required: true,
                        phone: true,
                        minlength: 10,
                        maxlength: 15
                    },
                    Password: {
                        required: true,
                        minlength: 6,
                        maxlength: 32
                    },
                    ConfirmPassword: {
                        required: true,
                        minlength: 6,
                        maxlength: 32,
                        equalTo: "#Password"
                    }
                },
                errorPlacement: function (error, element) {
                    var errorContainer = element.parents('div.form-group');
                    errorContainer.append(error);
                },
                messages: {
                    FirstName: {
                        required: "First Name is required!",
                        maxlength: "No more than 50 charecters"
                    },
                    LastName: {
                        required: "Last Name is required!",
                        maxlength: "No more than 50 charecters"
                    },
                    Email: {
                        required: "Email is required!",
                        maxlength: "No more than 128 characters"
                    },
                    MobileNo: {
                        required: "Mobile number required!",
                        minlength: "Minimum 9 digits is required!",
                        maxlength: "No more than 11 digits"
                    },
                    Password: {
                        required: "Password is required!",
                        minlength: "Minimum 6 digits is required!",
                        maxlength: "No more than 32 characters"
                    },
                    ConfirmPassword: {

                        required: "ConfirmPassword is required!",
                        minlength: "Minimum 6 digits is required!",
                        maxlength: "No more than 32 characters",
                        equalTo: "Password and confirm password must be same!"
                    }
                },
                invalidHandler: function (event, validator) {
                    success.hide();
                    error.show();
                },
                highlight: function (element) {
                    $(element)
                        .closest('.form-group').addClass('has-error');
                },
                unhighlight: function (element) {
                    $(element)
                        .closest('.form-group').removeClass('has-error');
                },
                success: function (label) {
                    label.closest('.form-group').removeClass('has-error');
                },
                submitHandler: function (form) {
                    if ($('#btnCreateUser').length > 0) {
                        var url = "/Admin/CreateUser";
                        var formData = new FormData();
                        var totalFiles = document.getElementById("PhotoFileName").files.length;
                        var browsedFile = document.getElementById("PhotoFileName").files[0];
                        var i = 0;
                        if (totalFiles != 0) {
                            if (browsedFile.type.match('image.*')) {
                                formData.append("file", browsedFile);
                            }
                        }
                        formData.append("UserType", $("#UserType").val());
                        formData.append("FirstName", $("#FirstName").val());
                        formData.append("LastName", $("#LastName").val());
                        formData.append("Email", $("#Email").val());
                        formData.append("MobileNo", $("#MobileNo").val());
                        formData.append("Password", $("#Password").val());
                        formData.append("ConfirmPassword", $("#ConfirmPassword").val());
                  
                        $.ajax({
                            method: "POST",
                            url: url,
                            data: formData,
                            contentType: false,
                            processData: false,
                            beforeSend: function (xhr) {
                            },
                            success: function (res) {
                                if (res.MessageType == 1) {
                                }
                                RB.notifier(res.CurrentMessage, res.MessageType);
                                clearData();
                            },
                            complete: function (xhr, status) {
                            },
                            error: function (exception) {
                            }
                        });

                    } else {
                        form.submit(function (e) { });
                    }

                }
            });
        }
    };

    var actionHandler = function () {
        $("#btnClear").on("click", function () {
            clearData();
        });
    };

    var initializeForm = function () {
        RB.loadDropdown('UserType', '/Home/GetAllUserTypeAjax', {}, false);
    };

    var clearData = function () {
        $("#UserType").val("");
        $("#FirstName").val('');
        $("#LastName").val("");
        $("#Email").val("");
        $("#MobileNo").val("");
        $("#Password").val("");
        $("#ConfirmPassword").val("");
        $("#PhotoFileName").val("");
    }

    var init = function () {
        validateForm();
        initializeForm();
        actionHandler();
    };

    return {
        init: init
    };
}();