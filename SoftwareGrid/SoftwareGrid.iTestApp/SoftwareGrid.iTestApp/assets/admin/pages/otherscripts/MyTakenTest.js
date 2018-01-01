
var MyTakenTest = function () {

    var actionHandler = function () {

    };

   
    var loadTestData = function (result) {
        var compiled = vash.compile($('#tmpTest').text());
        var resultText = compiled({ data: result });
        document.getElementById("div-test").innerHTML = resultText;
    };

    var loadTestDataInitiale = function () {
        var data = {
            iDisplayLength: parseInt($('#ShowPerPageItem').val()),
        };
        RB.sendAjaxRequest('/Home/MyTakenTestAjax', data, false, function (result) {
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
                    callback: loadTestData,
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


    var initializeForm = function () {

    };

    var init = function () {
        initializeForm();
        actionHandler();
        loadTestDataInitiale();
    };

    return {
        init: init,
    };
}();