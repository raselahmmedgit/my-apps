var frmId = "frmUserProfile";
var UserProfile = function () {

    var actionHandler = function () {

        $(document).on("change", "#image-upload", function () {
            previewProfileImage(this);
            updateUser();
        });

        $(document).on("click", ".change-image", function () {
            var id = $("#UserId").val();
            if (id != null && id != "") {
                $('#image-upload').trigger('click');
            }
        });
       
        $(document).on("click", ".teststatus", function () {
            var id = $(this).data("id");
            var status = $(this).data("status");

            var changeStatus = function () {
                RB.sendAjaxRequest('/Admin/ChangeTestStatus', { testId: id, status: status }, true, function (res) {
                    if (res != null) {
                        loadDataInitiale();
                    }
                }, true, true, null);
            };

            if (status == 1) {
                bootbox.confirm("Do you want to publish this test!", function () { changeStatus(); });
            } else {
                bootbox.confirm("Do you want to Unpublish this test!", function () { changeStatus(); });
            }

        });
    };

    var validateProfile = function () {
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
                    UserType: {
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
                    if ($('#btnUpdateProfile').length > 0) {
                        var url = "/Admin/UpdateUser";
                        var formData = new FormData();
                        formData.append("UserId", $("#UserId").val());
                        formData.append("UserType", $("#UserType").val());
                        formData.append("FirstName", $("#FirstName").val());
                        formData.append("LastName", $("#LastName").val());
                        formData.append("Email", $("#Email").val());
                        formData.append("MobileNo", $("#MobileNo").val());
                        formData.append("updateimageOnly", false);

                        $.ajax({
                            method: "POST",
                            url: url,
                            data: formData,
                            contentType: false,
                            processData: false,
                            beforeSend: function (xhr) {
                            },
                            success: function (res) {
                                RB.notifier(res.CurrentMessage, res.MessageType);
                            },
                            complete: function (xhr, status) {
                            },
                            error: function (exception) {
                            }
                        });

                    } 

                }
            });
        }
    };


    var validateChangePassword = function () {
        if ($().validate) {
            var form = $("#frmChangePassword");
            var error = $('.alert-danger', form);
            var success = $('.alert-success', form);

            form.validate({
                doNotHideMessage: true,
                errorElement: 'span',
                errorClass: 'help-block help-block-error',
                focusInvalid: false,
                rules: {
                    CurrentPassword: {
                        required: true,
                        minlength: 6,
                        maxlength: 32
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
                    CurrentPassword: {
                        required: "Current Password is required!",
                        minlength: "Minimum 6 digits is required!",
                        maxlength: "No more than 32 characters"
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
                    if ($('#btnChangePassword').length > 0) {
                        var url = "/Admin/ChangePassword";
                        var formData = new FormData();
                        formData.append("UserId", $("#UserId").val());
                        formData.append("CurrentPassword", $("#CurrentPassword").val());
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
                                RB.notifier(res.CurrentMessage, res.MessageType);
                            },
                            complete: function (xhr, status) {
                            },
                            error: function (exception) {
                            }
                        });

                    }

                }
            });
        }
    };




    var previewProfileImage = function (e) {
        if (e.files && e.files[0]) {
            var reader = new FileReader();
            reader.onload = function (res) {
                $('#ProfileImage').show();
                $('#ProfileImage').attr('src', res.target.result);
            };
            reader.readAsDataURL(e.files[0]);
        } 
    };

    var updateUser = function() {
        var url = "/Admin/UpdateUser";
        var formData = new FormData();
        var totalFiles = document.getElementById("image-upload").files.length;
        var browsedFile = document.getElementById("image-upload").files[0];
        var i = 0;
        if (totalFiles != 0) {
            if (browsedFile.type.match('image.*')) {
                formData.append("fileUpload", browsedFile);
            }
        }
        formData.append("UserId", $("#UserId").val());
        
        formData.append("updateimageOnly",true);
       

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
                //RB.notifier(res.CurrentMessage, res.MessageType);
                //clearData();
            },
            complete: function (xhr, status) {
            },
            error: function (exception) {
            }
        });
    };

    var loadData = function (result) {
        var compiled = vash.compile($('#tmpTestList').text());
        var resultText = compiled({ data: result });
        document.getElementById("div-test").innerHTML = resultText;
    };

    var loadDataInitiale = function () {
        var data = {
            iDisplayLength: parseInt($('#ShowPerPageItem').val()),
            keyword: $("#Keyword").val()
        };
        RB.sendAjaxRequest('/Admin/GetAllTest', data, true, function (result) {
            if (result) {
                var compiled = vash.compile($('#tmpTestList').text());
                var resultText = compiled({ data: result });
                document.getElementById("div-test").innerHTML = resultText;
                var total = 0;

                if (result.length > 0) {
                    total = result[0].TotalRecordCount;
                }
                $('#TotalCount').text(total);
                var listPagingSettings = {
                    pagingFor: $('#div-test'),
                    recordPerPage: $('#ShowPerPageItem').val(),
                    currentPage: 0,
                    numOfPagesTobeDisplayed: 5,
                    totalNumberOfRecords: total,
                    sorting: true,
                    url: '/Admin/GetAllTest',
                    paramTobePassed: data,
                    callback: loadData,
                    preLoader: $('#div-test')
                };
                pagination.init(listPagingSettings);
            }
            else {
                $('#TotalCount').text('0');
            }
        }, true, true, null);

    };

    var initializeForm = function () {

    };


    var loadTakenTestData = function (result) {
        var compiled = vash.compile($('#tmpTest').text());
        var resultText = compiled({ data: result });
        document.getElementById("div-test").innerHTML = resultText;
    };

    var loadTakenTestDataInitiale = function () {
        var data = {
            userId:$("#UserId").val(),
            iDisplayLength: parseInt($('#ShowPerPageItem').val()),
        };
        RB.sendAjaxRequest('/Admin/UserTakenTestAjax', data, false, function (result) {
            if (result != null && result.length > 0) {
                $("#no-data-content").hide();
                var compiled = vash.compile($('#tmpTest').text());
                var resultText = compiled({ data: result });
                document.getElementById("div-test").innerHTML = resultText;
                var total = 0;
                if (result.length > 0) {
                    total = result[0].TotalRecordCount;
                }
                $('#TotalCount').text(total);
                var listPagingSettings = {
                    pagingFor: $('#div-test'),
                    recordPerPage: $('#ShowPerPageItem').val(),
                    currentPage: 0,
                    numOfPagesTobeDisplayed: 5,
                    totalNumberOfRecords: total,
                    sorting: true,
                    url: '/Home/MyTakenTestAjax',
                    paramTobePassed: data,
                    callback: loadTakenTestData,
                    preLoader: $('#div-test')
                };
                pagination.init(listPagingSettings);
            }
            else {
                $("#no-data-content").show();
                $('#TotalCount').text('0');
            }
        }, true, true, null);

    };



    var init = function () {
        initializeForm();
        actionHandler();
        loadTakenTestDataInitiale();
        validateProfile();
        validateChangePassword();
    };

    return {
        init: init
    };
}();