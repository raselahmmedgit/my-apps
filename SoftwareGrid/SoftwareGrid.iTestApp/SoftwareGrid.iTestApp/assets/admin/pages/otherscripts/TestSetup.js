var frmId = "frmTestSetup";
var _testWiseQuestionList = [];
var testId = $("#TestId").val();

var TestSetup = function () {
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
                    TestCategoryId: {
                        required: true
                    },
                    TestName: {
                        required: true,
                        maxlength: 150
                    },
                    NoOfQuestion: {
                        required: true,
                        maxlength: 3
                    },
                    TestOrder: {
                        maxlength: 3
                    },
                    Price: {
                        maxlength: 8
                    },
                    Duration: {
                        required: true
                    },
                    Discount: {
                        maxlength: 8
                    },
                    About: {
                        required: true
                    },
                    TestTopic: {
                        required: true
                    },
                    TestDetails: {
                        required: true
                    }
                },
                errorPlacement: function (error, element) {
                    var errorContainer = element.parents('div.form-group');
                    errorContainer.append(error);
                },
                messages: {
                    TestCategoryId: {
                        required: "Test Type is required!"
                    },
                    TestName: {
                        required: "Test Name is required!",
                        maxlength: "No more than 150 charecters!"
                    },
                    NoOfQuestion: {
                        required: "No of Question is required!",
                        maxlength: "No more than 3 digits!"
                    },
                    Duration: {
                        required: "Duration is required!"
                    },
                    Discount: {
                        maxlength: "No more than 8 digits"
                    },
                    About: {
                        required: "About is required!"
                    },
                    TestTopic: {
                        required: "Test Topic is required!"
                    },
                    TestDetails: {
                        required: "Test Details is required!"
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
                        var url = "/Admin/CreateTest";

                        var formData = new FormData();
                        var totalFiles = document.getElementById("TestIconName").files.length;
                        var browsedFile = document.getElementById("TestIconName").files[0];
                        if (totalFiles != 0) {
                            if (browsedFile.type.match('image.*')) {
                                formData.append("file", browsedFile);
                            }
                        }
                        formData.append("TestIconName", $("#testIcon").val());
                        formData.append("TestId", $("#TestId").val());
                        formData.append("TestCategoryId", $("#TestCategoryId").val());
                        formData.append("TestName", $("#TestName").val());
                        formData.append("Tags", $("#Tags").val());
                        formData.append("NoOfQuestion", $("#NoOfQuestion").val());
                        formData.append("TestOrder", $("#TestOrder").val() == "" ? 0 : $("#TestOrder").val());
                        formData.append("Price", $("#Price").val() == "" ? 0 : $("#Price").val());
                        formData.append("Discount", $("#Discount").val());
                        formData.append("Duration", $("#Duration").val());
                        formData.append("IsSingleOrder", $("#IsSingleOrder").is(':checked') ? true : false);
                        formData.append("ValidedFor", $("#ValidedFor").val());
                        formData.append("About", $('#About').summernote("code"));
                        formData.append("TestTopic", $('#TestTopic').summernote("code"));
                        formData.append("TestDetails", $('#TestDetails').summernote("code"));
                        
                        
                        if (_testWiseQuestionList.length != $("#NoOfQuestion").val()) {
                            var message = "Number of selected question does't not match with your input No. of Question! You have select only " + _testWiseQuestionList.length + " questions.";
                            bootbox.alert(message);
                            return;
                        }
                        formData.append("questionIds", JSON.stringify(_testWiseQuestionList));
                        $.ajax({
                            method: "POST",
                            url: url,
                            data: formData,
                            contentType: false,
                            processData: false,
                            beforeSend: function (xhr) {
                            },
                            success: function (res) {
                                if (res.MessageType == 2) {
                                    clearData();
                                    loadQuestionDataInitiale();
                                }
                                RB.notifier(res.CurrentMessage, res.MessageType);
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

    var clearData = function () {
        _testWiseQuestionList = [];
        $("#TestCategoryId").val("").trigger("change");
        $("#TestIconName").val('');
        $("#TestName").val("");
        $("#Tags").tagsinput('removeAll');
        $("#NoOfQuestion").val("");
        $("#TestOrder").val("");
        $("#Price").val("");
        $("#Discount").val("");
        $("#Duration").val("");
        $("#IsSingleOrder").prop("checked", false);
        $("#ValidedFor").val("");
        $('#About').summernote("code", "");
        $('#TestTopic').summernote("code", "");
        $('#TestDetails').summernote("code", "");
        $('.selectQuestion:checkbox:checked').attr("checked", false);
        $("#TestId").val("");
       
    };

    var actionHandler = function () {
        $("#BtnClear").on("click", function () {
            clearData();
        });

        $("#BtnSearchQuestion").on("click", function () {
            loadQuestionDataInitiale();
        });

        $(document).on("click", ".selectQuestion", function () {
            var testName = $("#TestName").val();
            var noOfQuestion = $("#NoOfQuestion").val() == "" ? 0 : $("#NoOfQuestion").val();
            var clickedQuestionId = $(this).data("questionid");

            if (testName == "" || parseFloat(noOfQuestion) == 0) {
                bootbox.alert("Please fill test information properly.");
                return false;
            }
            if (_testWiseQuestionList == null) _testWiseQuestionList = [];
            var alreadySelected = _testWiseQuestionList.length;

            if ($(this).is(':checked')) {
                if (parseInt(alreadySelected) >= parseInt(noOfQuestion)) {
                    bootbox.alert("You already selected " + noOfQuestion + " questions for this test.");
                    return false;
                }
                _testWiseQuestionList.push(createTestWiseQuestionObject(clickedQuestionId));
            } else {
                deleteTestWiseQuestion(clickedQuestionId);
            }
            return true;
        });

        $("#Tags").tagsinput();


        $(document).on("change", "#ShowPerPageItem", function () {
            loadQuestionDataInitiale(0);
        });

    };

    var createTestWiseQuestionObject = function (questionId) {
        var obj = {};
        obj.QuestionId = questionId;
        return obj;
    };

    var deleteTestWiseQuestion = function (questionId) {
        if (_testWiseQuestionList != null && _testWiseQuestionList.length > 0) {
            for (var i = 0; i < _testWiseQuestionList.length; i++) {
                if (_testWiseQuestionList[i].QuestionId == questionId) {
                    _testWiseQuestionList.splice(i, 1);
                    break;
                }
            }
        }
    };

    var isExists = function (questionId) {
        if (_testWiseQuestionList != null && _testWiseQuestionList.length > 0) {
            for (var i = 0; i < _testWiseQuestionList.length; i++) {
                if (_testWiseQuestionList[i].QuestionId == questionId) {
                    return true;
                }
            }
        }
        return false;
    };


    var loadQuestionData = function (result) {
        if (result != null && result.length > 0) {
            for (var i = 0; i < result.length; i++) {
                if (isExists(result[i].QuestionId)) {
                    result[i].TestId = 0;
                } else {
                    result[i].TestId = null;
                }
            }
        }
        var compiled = vash.compile($('#tmpQuestion').text());
        var resultText = compiled({ data: result });
        document.getElementById("div-question").innerHTML = resultText;
    };

    var loadQuestionDataInitiale = function () {
        var data = {
            iDisplayLength: parseInt($('#ShowPerPageItem').val()),
            keyword: $("#Keyword").val(),
            testId: testId == "" ? 0 : testId
        };
        RB.sendAjaxRequest('/Admin/GetAllQuestionByTestId', data, true, function (result) {
            if (result) {
                loadQuestionData(result);
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
                    url: '/Admin/GetAllQuestionByTestId',
                    paramTobePassed: data,
                    callback: loadQuestionData,
                    preLoader: $('#div-question')
                };
                pagination.init(listPagingSettings);
            }
            else {
                $('#TotalCount').text('0');
            }
        }, true, true, null);

    };



    var initializeForm = function () {

        $('#About').summernote({
                height: 100
            });

        $('#TestTopic').summernote({
              height: 100
          });

        $('#TestDetails').summernote({
              height: 100
          });

        RB.loadDropdown('TestCategoryId', '/Admin/LoadTestCategoryAjax', {}, false);
    };
    
    var loadTestById = function (testId) {
            var data = {
                testId: testId
            };
            RB.sendAjaxRequest('/Admin/GetTest', data, true, function(res) {
                if (res != null) {
                    $("#TestId").val(res.TestId);
                    $("#GlobalId").val(res.GlobalId);
                    $('#TestCategoryId').val(res.TestCategoryId).trigger("change");
                    $("#TestName").val(res.TestName);
                    $("#testIcon").val(res.TestIconName);
                    if (res.TestIconPath != null && res.TestIconPath != "") {
                        $('#TestIconPath').show();
                        $('#TestIconPath').attr('src', res.TestIconPath);
                        $('#TestIconPath').val(res.TestIconPath);
                    }
                    $("#TestOrder").val(res.TestOrder);
                    $("#Marks").val(res.Marks);
                    if (res.IsSingleOrder) {
                        $('#IsSingleOrder').prop('checked', true);
                    } else {
                        $('#IsSingleOrder').prop('checked', false);
                    }
                    $("#Tags").tagsinput('add', res.Tags);
                    $("#Duration").val(res.Duration);
                    $("#ValidedFor").val(res.ValidedFor);
                    $("#Price").val(res.Price);
                    $("#NoOfQuestion").val(res.NoOfQuestion);
                    $('#About').summernote("code", res.About);
                    $('#TestTopic').summernote("code", res.TestTopic);
                    $('#TestDetails').summernote("code", res.TestDetails);
                    _testWiseQuestionList = res.TestWiseQuestions;
                    loadQuestionDataInitiale();

                }
            }, true, true, null);
        };
    
    var init = function () {
        validateForm();
        initializeForm();
        actionHandler();
        if (testId != null && testId !== "") {
            loadTestById(testId);
        }
        else {
            loadQuestionDataInitiale();
        }
    };

    return {
        init: init
    };
}();