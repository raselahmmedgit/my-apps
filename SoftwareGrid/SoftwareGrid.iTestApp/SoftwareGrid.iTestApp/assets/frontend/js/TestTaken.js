var answerList = [];
var TestTaken = function () {

    var convertToObject = function(questionId,answerOptionId) {
        var testId = $("#TestId").val();
        var obj = {};
        obj.TestId = testId;
        obj.QuestionId = questionId;
        obj.AnswerOptionId = answerOptionId;
        return obj;
    };

    var deleteAnswerOption = function(answerOptionId) {
        if (answerList != null && answerList.length > 0) {
            for (var i = 0; i < answerList.length; i++) {
                if (answerList[i].AnswerOptionId == answerOptionId) {
                    answerList.splice(i, 1);
                    break;
                }
            }
        }
    }

    var isExistAnswerOption = function (answerOptionId) {
        if (answerList != null && answerList.length > 0) {
            for (var i = 0; i < answerList.length; i++) {
                if (answerList[i].AnswerOptionId == answerOptionId) {
                    return true;
                }
            }
        }
        return false;
    }

    var getAnsweredQuestionCount = function () {
        if (answerList != null && answerList.length > 0) {
            var question = [];
            for (var i = 0; i < answerList.length; i++) {
                question.push(answerList[i].QuestionId);
            }
            var distinct = jQuery.unique(question);
            if (distinct != null) {
                return distinct.length;
            }
        }
        return 0;
    }

    var submitTest = function () {
        var testId = $("#TestId").val();
        var takenId = $("#TakenId").val();
        var accessCode = $("#AccessCode").val();

        RB.sendAjaxRequest('/Home/FinishTest', { TakenId: takenId, TestId: testId, AccessCode: accessCode, answerOption: JSON.stringify(answerList) }, true, function (res) {
            if (res != null) {
                if (res.MessageType == "2") {
                    window.location.href = "/Home/TestResult?takenId=" + takenId;
                }
                
            }
        }, true, true, null);
    };


   


    var deleteAnswerOptionByQuestionId = function (questionId) {
        if (answerList != null && answerList.length > 0) {
            for (var i = 0; i < answerList.length; i++) {
                if (answerList[i].QuestionId == questionId) {
                    answerList.splice(i, 1);
                    break;
                }
            }
        }
    }

    var checkSubmitTest = function () {

        var testId = $("#TestId").val();
        var noOfQuestion = $("#NoOfQuestion").val() == "" ? 0 : $("#NoOfQuestion").val();
        if (testId == "") {
            return false;
        }
        var totalAnswered = getAnsweredQuestionCount();

        if (parseInt(totalAnswered) == 0) {
            bootbox.alert("You don't answer any questions. Please answer the questions first.");
            return false;
        }
        if (parseInt(totalAnswered) < parseInt(noOfQuestion)) {
            var left = parseInt(noOfQuestion) - parseInt(totalAnswered);
            bootbox.confirm(left + " questions need to be answered, Do you want to submit without answering remaining question?", function (result) {
                if (result) {
                    submitTest();
                }
            });
            return false;
        }
        if (parseInt(totalAnswered) == parseInt(noOfQuestion)) {
            submitTest();
        }
        return true;
    };

    var actionHandler = function () {

        $(document).on("click", ".answer_option", function () {
            var questionId = $(this).data("questionid");
            var answerOptionId = $(this).data("answeroptionid");
            var multipleanswer = $(this).data("multipleanswer");
            if ($(this).is(':checked')) {
                if (multipleanswer == false) {
                    deleteAnswerOptionByQuestionId(questionId);
                }
                answerList.push(convertToObject(questionId, answerOptionId));
            } else {
                deleteAnswerOption(answerOptionId);
            }
        });

        $("#BtnStartTest").click(function () {

            var testId = $("#TestId").val();
            var takenId = $("#TakenId").val();
            var accessCode = $("#AccessCode").val();
          
            bootbox.confirm("Do you want to start this test?", function (result) {
                if (result) {
                    RB.sendAjaxRequest('/Home/StartTest', { TakenId: takenId, TestId: testId, AccessCode: accessCode }, true, function (res) {
                        if (res != null) {
                            $("#TakenId").val(res.TakenId);
                            $("#BtnStartTest").addClass("collapse");
                            $("#TestQuestion").removeClass("collapse");
                            $("#TestTimer").removeClass("collapse");
                            $("#BtnFinishTest").removeClass("collapse");
                            loadDataInitiale();
                            setTimer();
                        }
                    }, true, true, null);
                }
            });

           
        });
        
        $("#BtnFinishTest").click(function () {
            checkSubmitTest();
        });
        
    };

    var setTestTakenData = function (result) {
        var compiled = vash.compile($('#tmpTestTaken').text());
        var resultText = compiled({ data: result });
        document.getElementById("div-test-taken").innerHTML = resultText;


        $(".answer_option").each(function (index) {
            var answerOptionId = $(this).data("answeroptionid");
            if (isExistAnswerOption(answerOptionId)) {
                $(this).prop('checked', true);
            }
        });

    };

    var loadDataInitiale = function () {
        var data = {
            iDisplayLength:1,
            testId: $("#TestId").val() == "" ? 0 : $("#TestId").val(),
        };
        RB.sendAjaxRequest('/Home/SearchByTestId', data, true, function (result) {
            if (result) {
                setTestTakenData(result);
                var total = 0;
                if (result.length > 0) {
                    total = result[0].TotalRecordCount;
                }
                var listPagingSettings = {
                    pagingFor: $('#div-test-taken'),
                    recordPerPage: 1,
                    currentPage: 0,
                    numOfPagesTobeDisplayed: 10,
                    totalNumberOfRecords: total,
                    sorting: false,
                    url: '/Home/SearchByTestId',
                    paramTobePassed: data,
                    callback: setTestTakenData,
                    preLoader: $('#div-test-taken')
                };
                pagination.init(listPagingSettings);
            }
        }, true, true, null);

    };

    var initializeForm = function () {
        $('.icheck').iCheck({
            checkboxClass: 'icheckbox_minimal-green',
            radioClass: 'iradio_minimal-green',
            increaseArea: '20%'
        });
    };


    var setTimer = function () {
        var value = $("#Duration").val();
        var d = new Date();
        d.setMinutes(d.getMinutes() + parseInt(value));
        var day = d.getDate();
        var month = d.getMonth() + 1;
        var year = d.getFullYear();
        var thisDay = month + "/" + day + "/" + year + " " + parseInt(d.getHours()) + ":" + d.getMinutes() + ":" + d.getSeconds();
        $('#timer').countdown(thisDay, function (event) {
            $(this).html(event.strftime('%H:%M:%S'));
        }).on('finish.countdown', function (event) {
            submitTest();
        });
    }

    var init = function () {
        initializeForm();
        actionHandler();
    };
    return {
        init: init
    };
}();