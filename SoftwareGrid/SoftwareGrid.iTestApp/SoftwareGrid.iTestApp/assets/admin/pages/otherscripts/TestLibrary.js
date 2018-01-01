//Load Test List
function loadTest(e) {
    var testCategoryId = parseInt($(e).data('testcategoryid'));
    var testCategoryName = $(e).data('testcategoryname');
    if (testCategoryId > 0) {
        TestLibrary.loadTestByTestCategoryIdData(testCategoryId);
        $('#TestTitle').text(testCategoryName);
    }
};
//Load Test List

var TestLibrary = function () {

    var actionHandler = function () {
       
    };



    function loadTestCategoryData() {
        RB.sendAjaxRequest('/Home/TestCategoryAjax', null, false, function (result) {
            if (result) {
                var compiled = vash.compile($('#tmpTestCategory').text());
                var resultText = compiled({ data: result });
                document.getElementById("div-testcategory").innerHTML = resultText;
            }
        }, true, true, null);
    };

    var loadTestData = function (result) {
        var compiled = vash.compile($('#tmpTest').text());
        var resultText = compiled({ data: result });
        document.getElementById("div-test").innerHTML = resultText;
    };

    var loadTestDataInitiale = function (testCategoryId) {
        var data = {
            testCategoryId: parseInt(testCategoryId),
            iDisplayLength: parseInt($('#ShowPerPageItem').val()),
        };
        RB.sendAjaxRequest('/Home/TestLibraryAjax', data, false, function (result) {
            if (result) {
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
                    url: '/Home/TestLibraryAjax',
                    paramTobePassed: data,
                    callback: loadTestData,
                    preLoader: $('#div-test')
                };
                pagination.init(listPagingSettings);
            }
            else {
                $('#TotalCount').text('0');
            }
        }, true, true, null);

    };

    var loadTestByTestCategoryIdData = function (testCategoryId) {
        loadTestDataInitiale(testCategoryId);
    };

    var initializeForm = function () {
        
    };

    var init = function () {
        initializeForm();
        actionHandler();
        loadTestCategoryData();
        loadTestDataInitiale(0);
    };

    return {
        init: init,
        loadTestByTestCategoryIdData: loadTestByTestCategoryIdData
    };
}();