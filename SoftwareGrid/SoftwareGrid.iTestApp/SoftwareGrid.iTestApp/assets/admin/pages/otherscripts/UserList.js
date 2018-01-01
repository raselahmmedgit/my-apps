var userId = $("#UserId").val();
var UserList = function () {

    var actionHandler = function () {

        $("#ShowPerPageItem").change(getAllUser);

        $("#btnSearchUser").on("click", function () {
            getAllUser();
        });

        

        $(document).on("click", ".user-profile", function () {
            var id = $(this).data("id");
            if (id != null) {
                var url = "/Admin/UserProfile?userId=" + id;
                window.open(url);
            }
        });


        $(document).on("click", ".teststatus", function() {
            var id = $(this).data("id");
            var status = $(this).data("status");

            var changeStatus = function() {
                RB.sendAjaxRequest('/Admin/ChangeUserStatus', { userId: id, status: status }, true, function(res) {
                    if (res != null) {
                        getAllUser();
                    }
                }, true, true, null);
            };

            if (status == 1) {
                bootbox.confirm("Do you want to active this user!", function() { changeStatus(); });
            } else {
                bootbox.confirm("Do you want to inactive this user!", function() { changeStatus(); });
            }
        });
    };

    var userListLoad = function (result)
    {
        var compiled = vash.compile($('#tmpUserList').text());
        var resultText = compiled({ data: result });
        document.getElementById("div-user").innerHTML = resultText;
    }

    var getAllUser = function () {
        var data = {
            iDisplayLength: parseInt($('#ShowPerPageItem').val()),
            keyword: $("#Keyword").val()
        };
        RB.sendAjaxRequest('/Admin/GetAllUser', data, true, function (result) {
            if (result) {
                var compiled = vash.compile($('#tmpUserList').text());
                var resultText = compiled({ data: result });
                document.getElementById("div-user").innerHTML = resultText;
                var total = 0;
                if (result.length > 0) {
                    total = result[0].TotalRecordCount;
                }
                $('#TotalCount').text(total);
                var listPagingSettings = {
                    pagingFor: $('#div-user'),
                    recordPerPage: $('#ShowPerPageItem').val(),
                    currentPage: 0,
                    numOfPagesTobeDisplayed: 5,
                    totalNumberOfRecords: total,
                    sorting: false,
                    url: '/Admin/GetAllUser',
                    paramTobePassed: data,
                    callback: userListLoad,
                    preLoader: $('#div-user')
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
        getAllUser();
    };

    return {
        init: init
    };
}();