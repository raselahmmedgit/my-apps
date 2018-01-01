var frmId = "frmTestList";
var TestList = function () {

    var actionHandler = function () {

        $(document).on("click", ".edit", function () {
            var id = $(this).data("id");
            if (id != null && id != "") {
                window.location.href = "/Admin/EditTest?testId="+id;
            }
        });

        $("#BtnSearchTest").on("click", function () {
            loadDataInitiale();
        });

        $(document).on("change", "#ShowPerPageItem", function () {
            loadDataInitiale();
        });

        $(document).on("click", ".teststatus", function() {
            var id = $(this).data("id");
            var status = $(this).data("status");

            var changeStatus = function() {
                RB.sendAjaxRequest('/Admin/ChangeTestStatus', { testId: id, status: status }, true, function(res) {
                    if (res != null) {
                        loadDataInitiale();
                    }
                }, true, true, null);
            };

            if (status == 1) {
                bootbox.confirm("Do you want to publish this test!", function() { changeStatus(); });
            } else {
                bootbox.confirm("Do you want to Unpublish this test!", function() { changeStatus(); });
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

    var init = function () {
        initializeForm();
        actionHandler();
        loadDataInitiale();
    };

    return {
        init: init
    };
}();