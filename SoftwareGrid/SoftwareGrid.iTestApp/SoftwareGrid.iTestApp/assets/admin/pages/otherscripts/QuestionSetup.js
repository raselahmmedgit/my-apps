var frmId = "frmQuestion";
var _questionAnswerOptionList = [];

//Edit, Delete
function editAnswerOption(e) {

    var answerOptionId = parseInt($(e).data('answeroptionid'));

    if (answerOptionId > 0) {
        QuestionSetup.editQuestionAnswerOptionData(answerOptionId);
    }

};

function deleteAnswerOption(e) {

    var answerOptionId = parseInt($(e).data('answeroptionid'));

    if (answerOptionId > 0) {
        bootbox.confirm("Do you want to delete this ?", function (result) {
            if (result) {
                QuestionSetup.removeQuestionAnswerOptionData(answerOptionId);
            }
        });
    }
};

//Edit, Delete

var QuestionSetup = function () {

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
                ignore: "",
                rules: {
                    QuestionCategoryId: {
                        required: true
                    },
                    QuestionText: {
                        required: true
                    },
                    NoOfAnswerOption: {
                        required: true
                    },
                    Marks: {
                        required: true
                    },
                    AnswerOptionList: {
                        required: true
                    }
                },
                errorPlacement: function (error, element) {
                    var errorContainer = element.parents('div.form-group');
                    errorContainer.append(error);
                },
                messages: {
                    QuestionCategoryId: {
                        required: "Question Category is required."
                    },
                    QuestionText: {
                        required: "Question is required."
                    },
                    NoOfAnswerOption: {
                        required: "No Of Answer Option is required."
                    },
                    Marks: {
                        required: "Marks is required."
                    },
                    AnswerOptionList: {
                        required: "Answer Option is required."
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
                    if ($('#BtnSave').length > 0) {
                        saveData();
                    } else {
                        form.submit(function (e) { });
                    }
                }
            });
        }
    };

    var saveData = function () {

        if ($('#' + frmId).valid()) {

            var noOfAnswerOption = $("#NoOfAnswerOption").val();

            if (parseInt(noOfAnswerOption) == parseInt(_questionAnswerOptionList.length)) {

                var url = "/Admin/CreateQuestion";
                var isMultipleAnswer = false;
                if ($('#IsMultipleAnswer').is(":checked")) { isMultipleAnswer = true; }
                var questionAnswerOptionList = JSON.stringify(_questionAnswerOptionList);
                var formData = new FormData();
                formData.append("QuestionId", $("#QuestionId").val());
                formData.append("GlobalId", $("#GlobalId").val());
                formData.append("QuestionImageName", $("#QuestionImageName").val());
                formData.append("QuestionCategoryId", $("#QuestionCategoryId").val());
                formData.append("QuestionText", $("#QuestionText").val());
                formData.append("AnswerExplanation", $("#AnswerExplanation").val());
                formData.append("NoOfAnswerOption", $("#NoOfAnswerOption").val());
                formData.append("Marks", $("#Marks").val());
                formData.append("IsMultipleAnswer", isMultipleAnswer);
                formData.append("Tags", $("#Tags").val());
                formData.append("DifficultyLevel", $("#DifficultyLevel").val());
                formData.append("questionAnswerOptionList", questionAnswerOptionList);
                var totalFiles = document.getElementById("QuestionImagePath").files.length;
                var browsedFile = document.getElementById("QuestionImagePath").files[0];
                if (totalFiles != 0) {
                    if (browsedFile.type.match('image.*')) {
                        formData.append("fileUpload", browsedFile);
                    }
                }

                $.ajax({
                    method: "POST",
                    url: url,
                    data: formData,
                    contentType: false,
                    processData: false,
                    beforeSend: function (xhr) {
                        RB.preloader({ target: "divQuestionForm", show: true });
                    },
                    success: function (res) {
                        if (res != null) {
                            if (res.MessageType == "2") {
                                resetForm();
                                loadDataInitiale();
                            }
                            RB.notifier(res.CurrentMessage, res.MessageType);
                        }
                    },
                    complete: function (xhr, status) {
                        RB.preloader({ target: "divQuestionForm", show: false });
                    },
                    error: function (exception) {
                    },
                });

            } else {
                RB.notifier("Please add all the answer option.", 4);
            }
        } 

    };

    var editData = function (id) {
        RB.sendAjaxRequest('/Admin/GetQuestion', { questionId: id }, true, function (res) {
            if (res != null) {
                $("#QuestionId").val(res.QuestionId);
                $("#GlobalId").val(res.GlobalId);
                $('#QuestionCategoryId').val(res.QuestionCategoryId).trigger("change");
                $("#QuestionText").val(res.QuestionText);

                if (res.QuestionImagePath != null && res.QuestionImagePath != "") {
                    $('#QuestionImage').show();
                    $('#QuestionImage').attr('src', res.QuestionImagePath);
                    $('#QuestionImageName').val(res.QuestionImageName);
                }

                $("#NoOfAnswerOption").val(res.NoOfAnswerOption);
                $("#Marks").val(res.Marks);

                if (res.IsMultipleAnswer) {
                    $('#IsMultipleAnswer').prop('checked', true);
                }
                else {
                    $('#IsMultipleAnswer').prop('checked', false);
                }
                $("#Tags").tagsinput('add', res.Tags);
                $("#DifficultyLevel").val(res.DifficultyLevel).trigger("change");
                $("#AnswerExplanation").val(res.AnswerExplanation);

                _questionAnswerOptionList = res.QuestionAnswerOptionList;
                setQuestionAnswerOptionData();

            }
        }, true, true, null);
    };

    var previewQuestionImage = function (e) {
        $('#QuestionImage').hide();
        if (e.files && e.files[0]) {
            var reader = new FileReader();
            reader.onload = function (res) {
                $('#QuestionImage').show();
                $('#QuestionImage').attr('src', res.target.result);
            };
            reader.readAsDataURL(e.files[0]);
        } else {
            $('#QuestionImage').hide();
        }
    };

    var previewAnswerOptionImage = function (e) {
        $('#AnswerOptionImage').hide();
        if (e.files && e.files[0]) {
            var reader = new FileReader();
            reader.onload = function (res) {
                $('#AnswerOptionImage').show();
                $('#AnswerOptionImage').attr('src', res.target.result);
            };
            reader.readAsDataURL(e.files[0]);
        } else {
            $('#AnswerOptionImage').hide();
        }
    };

    var getAnswerOptionImage = function () {
        var answerOptionImage = $('#AnswerOptionImage').attr('src');
        return answerOptionImage;
    };

    var actionHandler = function () {

        $(document).on("click", "#BtnSave", function () {
            saveData();
        });

        $(document).on("click", "#BtnCancel", function () {
            resetForm();
        });

        $(document).on("click", ".edit", function () {
            var id = $(this).data("id");
            if (id != null && id != "") {
                editData(id);
            }
        });

        $(document).on("click", ".delete", function () {
            var id = $(this).data("id");
            if (id != null && id != "") {
                bootbox.confirm("Do you want to delete this ?", function (result) {
                    if (result) {
                        RB.sendAjaxRequest('/Admin/DeleteQuestion', { questionId: id }, true, function (res) {
                            if (res.MessageType == "2") {
                                reset();
                                getAll();
                            }
                            RB.notifier(res.CurrentMessage, res.MessageType);
                        }, true, true, null);
                    }
                });
            }
        });

        $(document).on("change", "#QuestionImagePath", function () {
            previewQuestionImage(this);
        });

        $(document).on("change", "#AnswerOptionImagePath", function () {
            previewAnswerOptionImage(this);
        });

        $(document).on("click", "#BtnAnswerOptionSave", function () {
            addQuestionAnswerOptionData();
        });

        $(document).on("click", "#BtnAnswerOptionCancel", function () {
            resetAnswerOptionForm();
        });

        $(document).on("change", "#ShowPerPageItem", function () {
            loadDataInitiale();
        });

        $(document).on("click", "#BtnQuestionSearch", function () {
            loadDataInitiale();
        });

        $("#Tags").tagsinput();
    };

    var loadData = function (result) {
        var compiled = vash.compile($('#tmpQuestion').text());
        var resultText = compiled({ data: result });
        document.getElementById("div-question").innerHTML = resultText;
    };

    var loadDataInitiale = function () {

        var data = {
            iDisplayLength: parseInt($('#ShowPerPageItem').val()),
            keyword: $("#Keyword").val(),

        };
        RB.sendAjaxRequest('/Admin/GetAllQuestion', data, true, function (result) {
            if (result) {
                var compiled = vash.compile($('#tmpQuestion').text());
                var resultText = compiled({ data: result });
                document.getElementById("div-question").innerHTML = resultText;
                var total = 0;
                if (result.length > 0) {
                    total = result[0].TotalRecordCount;
                }
                $('#TotalCount').text(total);
                var listPagingSettings = {
                    pagingFor: $('#div-question'),
                    recordPerPage: $('#ShowPerPageItem').val(),
                    currentPage: 0,
                    numOfPagesTobeDisplayed: 5,
                    totalNumberOfRecords: total,
                    sorting: true,
                    url: '/Admin/GetAllQuestion',
                    paramTobePassed: data,
                    callback: loadData,
                    preLoader: $('#div-question')
                };
                pagination.init(listPagingSettings);
            }
            else {
                $('#TotalCount').text('0');
            }
        }, true, true, null);

    };

    var resetForm = function () {
        $("#" + frmId).validate().resetForm();
        resetAnswerOptionForm();
        _questionAnswerOptionList = [];
        setQuestionAnswerOptionData();
        $("#QuestionId").val("");
        $("#GlobalId").val("");
        $("#QuestionCategoryId").val(0).trigger("change");
        $("#QuestionText").val("");
        $("#QuestionImagePath").val("");
        $("#AnswerExplanation").val("");
        $("#NoOfAnswerOption").val("");
        $("#Marks").val("");
        $('#IsMultipleAnswer').prop('checked', false);
        $("#Tags").tagsinput('removeAll');
        $("#DifficultyLevel").val(0).trigger("change");
        $('#QuestionImage').hide();
        $('#QuestionImage').attr('src', "");
        $('#QuestionImageName').val("");
        $("#QuestionCategoryId").focus();
    };

 
    var convertToQuestionAnswerOption = function (id, option, image, isCorrect) {
        var obj = Object();
        obj.AnswerOptionId = id == "" ? 0 : id;
        obj.AnswerOptionText = option;
        obj.AnswerOptionImage = image;
        obj.IsCorrectAnswer = isCorrect;
        return obj;
    };

    var isCorrectAnswerExist = function () {
        var isExist = false;
        if (_questionAnswerOptionList.length > 0) {
            for (var i = 0; i < _questionAnswerOptionList.length; i++) {
                if (_questionAnswerOptionList[i].IsCorrectAnswer) {
                    isExist = true;
                    break;
                }
            };
        }
        return isExist;
    };

    var addQuestionAnswerOptionData = function () {

        if ($('#' + frmId).valid()) {

            var noOfAnswerOption = $("#NoOfAnswerOption").val();
            if (noOfAnswerOption == "") {
                RB.notifier("Please fill the question information first...", 4);
                return;
            }
            var id = $("#AnswerOptionId").val();

            var answerOptionText = $("#AnswerOptionText").val().trim();
            if (answerOptionText == "") {
                RB.notifier("Please enter question answer option properly", 4);
            }
            var answerOptionImage = getAnswerOptionImage();
            var isCorrect = false;
            if ($('#IsCorrectAnswer').is(":checked")) {
                isCorrect = true;
            }

            if (isCorrect) {
                if ($('#IsMultipleAnswer').is(":checked")) {
                    addOrEditQuestionAnswerOptionData(id, noOfAnswerOption, answerOptionText, answerOptionImage, isCorrect);
                } else {
                    if (isCorrectAnswerExist()) {
                        RB.notifier("Please enter correct answer option properly", 4);
                    } else {
                        addOrEditQuestionAnswerOptionData(id, noOfAnswerOption, answerOptionText, answerOptionImage, isCorrect);
                    }
                }
            } else {
                addOrEditQuestionAnswerOptionData(id, noOfAnswerOption, answerOptionText, answerOptionImage, isCorrect);
            }

        } else {
            RB.notifier("Please fill the question information first...", 4);
        }
    };

    var addOrEditQuestionAnswerOptionData = function (id, noOfAnswerOption, answerOptionText, answerOptionImage, isCorrect) {
        if (id == "" || id == 0) {

            if (parseInt(noOfAnswerOption) > parseInt(_questionAnswerOptionList.length)) {
                var addid = parseInt(parseInt(_questionAnswerOptionList.length) + 1);
                _questionAnswerOptionList.push(convertToQuestionAnswerOption(addid, answerOptionText, answerOptionImage, isCorrect));
                setQuestionAnswerOptionData();
                resetAnswerOptionForm();
            } else {
                RB.notifier("You already added all answer option. Please check...", 4);
            }

        } else {
            var editid = parseInt(parseInt(_questionAnswerOptionList.length) + 1);
            removeQuestionAnswerOptionData(id);
            _questionAnswerOptionList.push(convertToQuestionAnswerOption(editid, answerOptionText, answerOptionImage, isCorrect));
            setQuestionAnswerOptionData();
            resetAnswerOptionForm();
            $('#BtnAnswerOptionSave').text('');
            $('#BtnAnswerOptionSave').text('Add');
        }
    };

    var resetAnswerOptionForm = function () {
        $("#AnswerOptionId").val("");
        $("#AnswerOptionText").val("");
        $('#IsCorrectAnswer').prop('checked', false);
        $("#AnswerOptionImagePath").val("");
        $('#AnswerOptionImage').hide();
        $('#AnswerOptionImage').attr('src', "");
        $('#AnswerOptionImageName').val("");
    };

    var setQuestionAnswerOptionData = function () {
        if (_questionAnswerOptionList == null) _questionAnswerOptionList = [];

        var compiled = vash.compile($('#tmpQuestionAnswerOption').text());
        var resultText = compiled({ data: _questionAnswerOptionList });
        document.getElementById("div-questionansweroption").innerHTML = resultText;

    };

    var removeQuestionAnswerOptionData = function (id) {
        if (_questionAnswerOptionList.length > 0) {
            for (var i = 0; i < _questionAnswerOptionList.length; i++) {
                if (id == _questionAnswerOptionList[i].AnswerOptionId) {
                    _questionAnswerOptionList.splice(i, 1);
                    break;
                }
            };
            setQuestionAnswerOptionData();
        }
    };

    var editQuestionAnswerOptionData = function (id) {
        var answerOption = getQuestionAnswerOptionData(id);
        if (answerOption != null) {
            $("#AnswerOptionId").val(answerOption.AnswerOptionId);
            $("#AnswerOptionText").val(answerOption.AnswerOptionText);
            $('#AnswerOptionImage').show();
            $("#AnswerOptionImage").attr('src', answerOption.AnswerOptionImage);

            if (answerOption.IsCorrectAnswer) {
                $('#IsCorrectAnswer').prop('checked', true);
            }
            else {
                $('#IsCorrectAnswer').prop('checked', false);
            }

            $('#BtnAnswerOptionSave').text('');
            $('#BtnAnswerOptionSave').text('Edit');
        }
    };

    var getQuestionAnswerOptionData = function (id) {
        var res = null;
        if (_questionAnswerOptionList.length > 0) {
            for (var i = 0; i < _questionAnswerOptionList.length; i++) {
                if (id == _questionAnswerOptionList[i].AnswerOptionId) {
                    res = _questionAnswerOptionList[i];
                    break;
                }
            };
        }
        return res;
    };

    

    var initializeForm = function () {
        RB.loadDropdown('QuestionCategoryId', '/Admin/LoadQuestionCategoryAjax', {}, false);
        RB.loadDropdown('DifficultyLevel', '/Admin/LoadDifficultyLevelAjax', {}, false);
    };

    var init = function () {
        validateForm();
        initializeForm();
        actionHandler();
        loadDataInitiale();
        setQuestionAnswerOptionData();
    };

    return {
        init: init,
        editQuestionAnswerOptionData: editQuestionAnswerOptionData,
        removeQuestionAnswerOptionData: removeQuestionAnswerOptionData
    };
}();