var frmId = "frmUser";
var UserSetup = function () {

    var validateForm = function () {
        if ($().validate) {
            var form = $("#" + frmId);
            var error = $('.alert-danger', form);
            var success = $('.alert-success', form);

            form.validate({
                doNotHideMessage: true,
                errorElement: 'span',
                errorClass: 'help-block help-block-error',
                focusInvalid: false,

                rules: {
                    UserName: {
                        required: true,
                        maxlength: 150
                    },
                },
                errorPlacement: function (error, element) {
                    var errorContainer = element.parents('div.form-group div.col-md-9');
                    errorContainer.append(error);
                },
                messages: {
                    UserName: {
                        required: "User Name is required."
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
                    if ($('#BtnSave').length > 0) {
                        var url = "/Admin/CreateUser";
                        $.post(url, $(form).serializeArray(),
                            function (res) {
                                if (res.MessageType == "2") {
                                    resetForm();
                                    loadData();
                                }
                                RB.notifier(res.CurrentMessage, res.MessageType);
                            });
                    } else {
                        form.submit(function (e) { });
                    }

                }
            });
        }
    };

    var actionHandler = function () {

        $(document).on("click", "#BtnCancel", function () {
            resetForm();
        });

        $(document).on("click", ".edit", function () {
            var id = $(this).data("id");
            if (id != null && id != "") {
                RB.sendAjaxRequest('/Admin/GetUser', { roleId: id }, true, function (res) {
                    if (res != null) {
                        $("#UserId").val(res.UserId);
                        $("#UserName").val(res.UserName);
                    }
                }, true, true, null);
            }
        });

        $(document).on("click", ".delete", function () {
            var id = $(this).data("id");
            if (id != null && id != "") {
                bootbox.confirm("Do you want to delete this ?", function (result) {
                    if (result) {
                        RB.sendAjaxRequest('/Admin/DeleteUser', { roleId: id }, true, function (res) {
                            if (res.MessageType == "2") {
                                resetForm();
                                loadData();
                            }
                            RB.notifier(res.CurrentMessage, res.MessageType);
                        }, true, true, null);
                    }
                });
            }
        });

    };

    var loadData = function () {

        RB.sendAjaxRequest('/Admin/GetAllUser', null, true, function (result) {
            if (result) {
                var compiled = vash.compile($('#tmpUser').text());
                var resultText = compiled({ data: result });
                document.getElementById("div-question-category").innerHTML = resultText;
                RB.makePagination("tblUser");
            }
        }, true, true, null);

    };

    var resetForm = function () {
        $(':input', '#' + frmId)
          .removeAttr('checked')
          .removeAttr('selected')
          .not(':button, :submit, :reset, :hidden, :radio, :checkbox')
          .val('');
    };

    var initializeForm = function () {
    };

    var init = function () {
        validateForm();
        initializeForm();
        actionHandler();
        loadData();
    };

    return {
        init: init
    };

}();