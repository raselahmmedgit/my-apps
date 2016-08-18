
var appMessage = {
    Error: 'We are facing some problem while processing the current request. Please try again later.',
    NotFound: 'Requested object not found.',
    SaveSuccess: 'Save successfully.',
    UpdateSuccess: 'Update successfully.',
    DeleteSuccess: 'Delete successfully.'
};

//-----------------------------------------------------
//start Kendo UI Grid Filter Option Methods

var filterOption = {
    extra: false,
    operators: {
        string: {
            eq: "Is Equal To",
            startswith: "Starts With",
            contains: "Contains",
            endswith: "Ends With"
        }
    }
};

//end Kendo UI Grid Filter Option Methods
//-----------------------------------------------------

//-----------------------------------------------------
//start Treeview Data Get Methods

function GetTreeviewDataList(nodes) {

    var treeViewModelList = [];

    for (var i = 0; i < nodes.length; i++) {
        if (nodes[i].checked) {

            var treeViewModelParent = {};

            var id = nodes[i].id;
            var text = nodes[i].text;

            treeViewModelParent.Id = id;
            treeViewModelParent.Text = text;

            treeViewModelList.push(treeViewModelParent);

        }

        if (nodes[i].hasChildren) {

            GetTreeviewDataList(nodes[i].children.view());

        }
    }

    return treeViewModelList;
}

//end Ajax Get Methods
//-----------------------------------------------------

//-----------------------------------------------------
//start Ajax Get Methods

function AjaxJsonGet(getUrl) {

    $.ajax({
        url: getUrl,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        beforeSend: function () {
            OpenAppProgressWindow();
        },
        success: function (result) {
            var messageType = result.messageType;
            var messageText = result.messageText;
            LoadAppMessageWindow(messageType, messageText);
        },
        error: function (objAjaxRequest, strError) {
            var respText = objAjaxRequest.responseText;
            var messageText = respText;
            LoadErrorAppMessageWindowWithText(messageText);
        }

    });

}

function AjaxJsonGetWithParam(getUrl, paramValue) {

    $.ajax({
        url: getUrl,
        type: 'GET',
        dataType: 'json',
        data: paramValue,
        contentType: 'application/json; charset=utf-8',
        beforeSend: function () {
            OpenAppProgressWindow();
        },
        success: function (result) {
            var messageType = result.messageType;
            var messageText = result.messageText;
            LoadAppMessageWindow(messageType, messageText);
        },
        error: function (objAjaxRequest, strError) {
            var respText = objAjaxRequest.responseText;
            var messageText = respText;
            LoadErrorAppMessageWindowWithText(messageText);
        }

    });

}

//end Ajax Get Methods
//-----------------------------------------------------

//-----------------------------------------------------
//start Ajax Post Methods

function AjaxJsonPost(postUrl) {

    $.ajax({
        url: postUrl,
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        beforeSend: function () {
            OpenAppProgressWindow();
        },
        success: function (result) {
            var messageType = result.messageType;
            var messageText = result.messageText;
            LoadAppMessageWindow(messageType, messageText);
        },
        error: function (objAjaxRequest, strError) {
            var respText = objAjaxRequest.responseText;
            var messageText = respText;
            LoadErrorAppMessageWindowWithText(messageText);
        }

    });

}

function AjaxJsonPostForDelete(postUrl) {

    $.ajax({
        url: postUrl,
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        beforeSend: function () {
            OpenAppProgressWindow();
        },
        success: function (result) {
            var messageType = result.messageType;
            var messageText = result.messageText;
            LoadAppMessageWindow(messageType, messageText);
            KendoGridRefreshInIndexPage();
        },
        error: function (objAjaxRequest, strError) {
            var respText = objAjaxRequest.responseText;
            var messageText = respText;
            LoadErrorAppMessageWindowWithText(messageText);
        }

    });

}

function AjaxJsonPostWithParam(postUrl, paramValue) {

    $.ajax({
        url: postUrl,
        type: 'POST',
        dataType: 'json',
        data: paramValue,
        contentType: 'application/json; charset=utf-8',
        beforeSend: function () {
            OpenAppProgressWindow();
        },
        success: function (result) {
            var messageType = result.messageType;
            var messageText = result.messageText;
            LoadAppMessageWindow(messageType, messageText);
        },
        error: function (objAjaxRequest, strError) {
            var respText = objAjaxRequest.responseText;
            var messageText = respText;
            LoadErrorAppMessageWindowWithText(messageText);
        }

    });

}

function AjaxJsonPostForDeleteWithParam(postUrl, paramValue) {

    $.ajax({
        url: postUrl,
        type: 'POST',
        dataType: 'json',
        data: paramValue,
        contentType: 'application/json; charset=utf-8',
        beforeSend: function () {
            OpenAppProgressWindow();
        },
        success: function (result) {
            var messageType = result.messageType;
            var messageText = result.messageText;
            LoadAppMessageWindow(messageType, messageText);
            KendoGridRefreshInIndexPage(); //Have to add index page
        },
        error: function (objAjaxRequest, strError) {
            var respText = objAjaxRequest.responseText;
            var messageText = respText;
            LoadErrorAppMessageWindowWithText(messageText);
        }

    });

}

function AjaxContentPostForAppCommonWindowWithParam(postUrl, paramValue) {

    $.ajax({
        url: postUrl,
        type: 'POST',
        data: paramValue,
        beforeSend: function () {
            OpenAppProgressWindow();
        },
        success: function (result) {

            var updateTargetIdValueList = result.split("|");

            var statusValue = updateTargetIdValueList[0];
            var actionName = updateTargetIdValueList[1];
            var messageTypeValue = updateTargetIdValueList[2];
            var messageTextValue = updateTargetIdValueList[3];

            if (statusValue == "True") {

                //kendo ui progress window close
                CloseAppProgressWindow();

                if (actionName == "Add") {

                    $("#updateTargetId").html("");
                    $('#updateTargetId').removeClass('callout-warning');
                    $('#updateTargetId').addClass('callout-info');
                    $("#updateTargetId").html(messageTextValue);
                    $("#updateTargetId").show();

                }
                else if (actionName == "Edit") {

                    //close kendo ui window
                    CloseAppCommonWindow();

                    LoadAppMessageWindowForAjaxSuccess(messageTypeValue, messageTextValue);
                }

                KendoGridRefreshInIndexPage(); //Have to add index page
            }
            else {


                //kendo ui progress window close
                CloseAppProgressWindow();

                $("#updateTargetId").html("");
                $('#updateTargetId').removeClass('callout-info');
                $('#updateTargetId').addClass('callout-warning');
                $("#updateTargetId").html(messageTextValue);
                $("#updateTargetId").show();
            }

            //end success
        },
        error: function (objAjaxRequest, strError) {
            var respText = objAjaxRequest.responseText;
            var messageText = respText;
            LoadErrorAppMessageWindowWithText(messageText);
        }

    });

}

//end Ajax Post Methods
//-----------------------------------------------------

//-----------------------------------------------------
//start Common
function AppCommonWindowBegin() {

    OpenAppProgressWindow();

}

function AppCommonWindowComplete() {

    CloseAppProgressWindow();

}

//function AppCommonWindowSuccess() {

//    var updateTargetIdValue = $("#updateTargetId").html().trim();

//    var updateTargetIdValueList = updateTargetIdValue.split("|");

//    var statusValue = updateTargetIdValueList[0];
//    var messageTypeValue = updateTargetIdValueList[1];
//    var messageTextValue = updateTargetIdValueList[2];

//    if (statusValue == "True") {

//        //close kendo ui window
//        CloseAppCommonWindow();

//        LoadAppMessageWindowForAjaxSuccess(messageTypeValue, messageTextValue);

//        KendoGridRefreshInIndexPage(); //Have to add index page
//    }
//    else {


//        //kendo ui progress window close
//        CloseAppProgressWindow();

//        $("#updateTargetId").html("");
//        $("#updateTargetId").html(messageTextValue);
//        $("#updateTargetId").show();
//    }
//}

function AppCommonWindowSuccess() {

    var updateTargetIdValue = $("#updateTargetId").html().trim();

    var updateTargetIdValueList = updateTargetIdValue.split("|");

    var statusValue = updateTargetIdValueList[0];
    var actionName = updateTargetIdValueList[1];
    var messageTypeValue = updateTargetIdValueList[2];
    var messageTextValue = updateTargetIdValueList[3];

    if (statusValue == "True") {

        if (actionName == "Add") {

            $("#updateTargetId").html("");
            $('#updateTargetId').removeClass('callout-warning');
            $('#updateTargetId').addClass('callout-info');
            $("#updateTargetId").html(messageTextValue);
            $("#updateTargetId").show();

        }
        else if (actionName == "Edit") {

            //close kendo ui window
            CloseAppCommonWindow();

            LoadAppMessageWindowForAjaxSuccess(messageTypeValue, messageTextValue);
        }

        KendoGridRefreshInIndexPage(); //Have to add index page
    }
    else {


        //kendo ui progress window close
        CloseAppProgressWindow();

        $("#updateTargetId").html("");
        $('#updateTargetId').removeClass('callout-info');
        $('#updateTargetId').addClass('callout-warning');
        $("#updateTargetId").html(messageTextValue);
        $("#updateTargetId").show();
    }
}

function AppCommonWindowFormValidation(fromId) {

    //validation
    var $form = $("#" + fromId);
    // Unbind existing validation
    $form.unbind();
    $form.data("validator", null);
    // Check document for changes
    $.validator.unobtrusive.parse(document);
    // Re add validation with changes
    $form.validate($form.data("unobtrusiveValidation").options);

}

function LoadAddOrEditAppCommonWindow(viewUrl, windowTitle, windowForm) {

    OpenAppProgressWindow();

    TitleAppCommonWindow(windowTitle);

    $.get(viewUrl, function (data) {

        //kendo ui window content
        ContentAppCommonWindow(data);

        AppCommonWindowFormValidation(windowForm);

        //kendo ui window open
        OpenAppCommonWindow();

        CloseAppProgressWindow();

    });

};

function LoadDetailsAppCommonWindow(viewUrl, windowTitle) {

    OpenAppProgressWindow();

    TitleAppCommonWindow(windowTitle);

    $.get(viewUrl, function (data) {

        //kendo ui window content
        ContentAppCommonWindow(data);

        //kendo ui window open
        OpenAppCommonWindow();

        CloseAppProgressWindow();

    });

};

function LoadAppCommonWindow(viewUrl, windowTitle, windowForm) {

    OpenAppProgressWindow();

    TitleAppCommonWindow(windowTitle);

    $.get(viewUrl, function (data) {

        //kendo ui window content
        ContentAppCommonWindow(data);

        AppCommonWindowFormValidation(windowForm);

        //kendo ui window open
        OpenAppCommonWindow();

        CloseAppProgressWindow();

    });

};

function LoadAppCommonWindowWithoutForm(viewUrl, windowTitle) {

    OpenAppProgressWindow();

    TitleAppCommonWindow(windowTitle);

    $.get(viewUrl, function (data) {

        //kendo ui window content
        ContentAppCommonWindow(data);

        //kendo ui window open
        OpenAppCommonWindow();

        CloseAppProgressWindow();
    });

};

function TitleAppCommonWindow(title) {

    //title kendo ui window
    $("#appCommonWindow").data("kendoWindow").title(title);
}

function ContentAppCommonWindow(content) {

    //content kendo ui window
    $("#appCommonWindow").data("kendoWindow").content(content);

}

function OpenAppCommonWindow() {

    //open kendo ui window
    $("#appCommonWindow").data("kendoWindow").center().open();

}

function CloseAppCommonWindow() {

    //open kendo ui window
    $("#appCommonWindow").data("kendoWindow").close();

}
//end Common
//-----------------------------------------------------

//-----------------------------------------------------
//start Delete
function LoadAppDeleteWindow(viewUrl, windowTitle) {

    TitleAppDeleteWindow(windowTitle);

    //hidden field value
    $("#hdDeleteId").val("");
    $("#hdDeletePostUrl").val("");
    $("#hdDeleteId").val(viewUrl);
    $("#hdDeletePostUrl").val(viewUrl);

    //kendo ui window open
    OpenAppDeleteWindow();

};

function TitleAppDeleteWindow(title) {

    //title kendo ui window
    $("#appDeleteWindow").data("kendoWindow").title(title);
}

function ContentAppDeleteWindow(content) {

    //content kendo ui window
    $("#appDeleteWindow").data("kendoWindow").content(content);

}

function OpenAppDeleteWindow() {

    //open kendo ui window
    $("#appDeleteWindow").data("kendoWindow").center().open();

}

function CloseAppDeleteWindow() {

    //open kendo ui window
    $("#appDeleteWindow").data("kendoWindow").close();

}

function YesDelete() {

    var hdDeleteIdValue = $("#hdDeleteId").val().trim();
    var hdDeletePostUrlValue = $("#hdDeletePostUrl").val().trim();

    //AjaxJsonPost(hdDeletePostUrlValue);
    AjaxJsonPostForDelete(hdDeletePostUrlValue);

    //close kendo ui window
    CloseAppDeleteWindow();

}

function NoDelete() {

    $("#hdDeleteId").val("");
    $("#hdDeletePostUrl").val("");

    //close kendo ui window
    CloseAppDeleteWindow();

}
//end Delete
//-----------------------------------------------------

//-----------------------------------------------------
//start Message

//Common Message Window with messageType, messageText
function LoadAppMessageWindow(messageType, messageText) {

    var windowTitle = messageType;

    TitleAppMessageWindow(windowTitle);

    var windowContent = GetAppMessageWindowContent(windowTitle, messageText);

    ContentAppMessageWindow(windowContent);

    //kendo ui message window open
    OpenAppMessageWindow();

    //kendo ui progress window close
    CloseAppProgressWindow();

};

//Common Message Window with messageType, messageText for ajax success
function LoadAppMessageWindowForAjaxSuccess(messageType, messageText) {

    var windowTitle = messageType;

    TitleAppMessageWindow(windowTitle);

    var windowContent = GetAppMessageWindowContent(windowTitle, messageText);

    ContentAppMessageWindow(windowContent);

    //kendo ui message window open
    OpenAppMessageWindow();

    //kendo ui progress window close
    CloseAppProgressWindow();

};

//Info Message Window
function LoadInfoAppMessageWindowWithText(messageText) {

    var windowTitle = "Info";

    TitleAppMessageWindow(windowTitle);

    var windowContent = GetAppMessageWindowContent(windowTitle, messageText);

    ContentAppMessageWindow(windowContent);

    //kendo ui message window open
    OpenAppMessageWindow();

    //kendo ui progress window close
    CloseAppProgressWindow();

};

//Warn Message Window
function LoadWarnAppMessageWindowWithText(messageText) {

    var windowTitle = "Warn";

    TitleAppMessageWindow(windowTitle);

    var windowContent = GetAppMessageWindowContent(windowTitle, messageText);

    ContentAppMessageWindow(windowContent);

    //kendo ui message window open
    OpenAppMessageWindow();

    //kendo ui progress window close
    CloseAppProgressWindow();

};

//Success Message Window
function LoadSuccessAppMessageWindowWithText(messageText) {

    var windowTitle = "Success";

    TitleAppMessageWindow(windowTitle);

    var windowContent = GetAppMessageWindowContent(windowTitle, messageText);

    ContentAppMessageWindow(windowContent);

    //kendo ui message window open
    OpenAppMessageWindow();

    //kendo ui progress window close
    CloseAppProgressWindow();

};

//Error Message Window
function LoadErrorAppMessageWindowWithText(messageText) {

    var windowTitle = "Error";

    TitleAppMessageWindow(windowTitle);

    var windowContent = GetAppMessageWindowContent(windowTitle, messageText);

    ContentAppMessageWindow(windowContent);

    //kendo ui message window open
    OpenAppMessageWindow();

    //kendo ui progress window close
    CloseAppProgressWindow();

};

function GetAppMessageWindowContent(messageType, messageText) {

    var content = "<div id='messagePage' class='deletePage form-win col-xs-12'><div class='row'> <div style='margin-bottom: 10px !important;' class='alert alert-" + messageType + "'><h4 style='margin-bottom: 0px !important;'>" + messageText + "</h4></div> </div><div class='row form-hr'><div id='buttonZone' class='pull-right buttonZone'><button type='button' class='btn btn-success btn-sm btn-flat' id='btnOkMessage' name='btnOkMessage' onclick='OkMessage()'><i class='fa fa-check-square'></i>&nbsp;&nbsp;Ok</button></div></div></div>";

    return content;

}

function TitleAppMessageWindow(title) {

    var upperTitle = title.toLowerCase().replace(/\b[a-z]/g, function (letter) {
        return letter.toUpperCase();
    });

    //title kendo ui window
    $("#appMessageWindow").data("kendoWindow").title(upperTitle);
}

function ContentAppMessageWindow(content) {

    //content kendo ui window
    $("#appMessageWindow").data("kendoWindow").content(content);

}

function OpenAppMessageWindow() {

    //open kendo ui window
    $("#appMessageWindow").data("kendoWindow").center().open();

}

function CloseAppMessageWindow() {

    //open kendo ui window
    $("#appMessageWindow").data("kendoWindow").close();

}

function OkMessage() {

    //close kendo ui window
    CloseAppMessageWindow();

}

//end Message
//-----------------------------------------------------

//-----------------------------------------------------
//start Progress

function OpenAppProgressWindow() {

    //open kendo ui window
    $("#appProgressWindow").data("kendoWindow").center().open();

}

function CloseAppProgressWindow() {

    //open kendo ui window
    $("#appProgressWindow").data("kendoWindow").close();

}

//end Progress
//-----------------------------------------------------

//-----------------------------------------------------
//start Refresh Kendo Grid Funtion
function KendoGridRefresh() {
    //Get Grid
    var kdGrid = $('#grid').data('kendoGrid');
    kdGrid.dataSource.read();
}

function KendoGridRefresh(gridId) {
    var _id = "#" + gridId;
    //Get Grid
    var kdGrid = $(_id).data('kendoGrid');
    kdGrid.dataSource.read();
}
//end Refresh Kendo Grid Funtion
//-----------------------------------------------------

$(document).ready(function () {

    $("#appCommonWindow").kendoWindow({
        //actions: ["Custom", "Minimize", "Maximize", "Close"],
        actions: ["Minimize", "Maximize", "Close"],
        draggable: true,
        modal: true,
        resizable: false,
        minHeight: 100,
        //height: 550,
        minWidth: 350,
        //maxWidth: 800,
        pinned: true,
        position: { top: 100 },
        visible: false
    });

    $("#appDeleteWindow").kendoWindow({
        actions: ["Minimize", "Maximize", "Close"],
        draggable: true,
        modal: true,
        resizable: false,
        minHeight: 100,
        minWidth: 350,
        pinned: true,
        position: { top: 100 },
        visible: false
    });

    $("#appMessageWindow").kendoWindow({
        draggable: false,
        modal: true,
        resizable: false,
        minHeight: 80,
        minWidth: 350,
        visible: false
    });

    $("#appProgressWindow").kendoWindow({
        title: false,
        draggable: false,
        modal: true,
        resizable: false,
        visible: false,
        close: function (e) {

            //            e.preventDefault();
            //            return false;

            //            console.log(e);

        }
    });

    //-----------------------------------------------------
    //add Common
    $('#lnkAddCommon').unbind('click').on('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var viewUrl = linkObj.attr('href');
        var windowTitle = linkObj.attr('title');
        var windowForm = "appCommonWindowForm";

        LoadAppCommonWindow(viewUrl, windowTitle, windowForm);

        return false;

    });
    //-----------------------------------------------------

    //-----------------------------------------------------
    //detail Common
    $('.lnkDetailCommon').unbind('click').on('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var viewUrl = linkObj.attr('href');
        var windowTitle = linkObj.attr('title');

        LoadAppCommonWindowWithoutForm(viewUrl, windowTitle);

        return false;

    });
    //-----------------------------------------------------

    //-----------------------------------------------------
    //edit Common
    $('.lnkEditCommon').unbind('click').on('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var viewUrl = linkObj.attr('href');
        var windowTitle = linkObj.attr('title');
        var windowForm = "appCommonWindowForm";

        LoadAppCommonWindow(viewUrl, windowTitle, windowForm);

        return false;

    });
    //-----------------------------------------------------

    //-----------------------------------------------------
    //delete Common
    $('.lnkDeleteCommon').unbind('click').on('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var viewUrl = linkObj.attr('href');
        var windowTitle = linkObj.attr('title');

        LoadAppDeleteWindow(viewUrl, windowTitle);

        return false;

    });
    //-----------------------------------------------------

});

var App = function () {

    var preloaderShow = function () {

        $('body').find('#appModalLoading').each(function () {
            var modal = $(this);
            modal.modal('show');
        });
    };

    var preloaderHide = function () {

        $('body').find('#appModalLoading').each(function () {
            var modal = $(this);
            modal.modal('hide');
        });
    };

    var modalHandler = function () {
        $('body').undelegate('.lnkAppModal', 'click').on('click', '.lnkAppModal', function (event) {

            event.preventDefault();
            var url = $(this).attr('href');
            var title = $(this).data('modal-title');
            var icon = $(this).data('modal-icon');
            var modalSize = '';
            var modalDialogSize = '';
            var isPost = false;
            if ($(this).data('ispost') == true) {
                isPost = true;
            }
            if ($(this).hasClass('modal-small')) {
                modalSize = 'bs-modal-sm';
                modalDialogSize = 'modal-sm';
            } else if ($(this).hasClass('modal-basic')) {
                modalSize = '';
                modalDialogSize = '';
            } else if ($(this).hasClass('modal-large')) {
                modalSize = 'bs-modal-lg';
                modalDialogSize = 'modal-lg';
            } else if ($(this).hasClass('modal-full')) {
                modalSize = '';
                modalDialogSize = 'modal-full';
            }

            $('body').find('#appModal').each(function () {
                var modal = $(this);
                (modalSize.length > 0) ? modal.addClass(modalSize) : '';
                (modalDialogSize.length > 0) ? modal.find('#appModalDialog').addClass(modalDialogSize) : '';
                modal.find('#appModalDialogTitle').html('<i class="' + icon + '"></i> ' + title);
                App.sendAjaxRequest(url, {}, isPost, function (result) {
                    modal.find('#appModalDialogContainer').html(result);
                    modal.modal('show');
                }, true, false);
            });
        });

    };

    var deleteHandler = function () {

        $('body').undelegate('.lnkAppDelete', 'click').on('click', '.lnkAppDelete', function (event) {

            event.preventDefault();
            var url = $(this).attr('href');
            var gridId = $(this).data('gridid');

            App.sendAjaxRequest(url, {}, true, function (result) {
                console.log(result);
            }, true, false);

        });

        return false;
    };

    var modalShow = function () {

        $('body').find('#appModal').each(function () {
            var modal = $(this);
            modal.modal('show');
        });
    };

    var modalHide = function () {

        $('body').find('#appModal').each(function () {
            var modal = $(this);
            modal.modal('hide');
        });
    };

    var sendAjaxRequest = function (url, data, isPost, callback, isAsync, isJson, target) {
        isJson = typeof (isJson) == 'undefined' ? true : isJson;
        var contentType = (isJson) ? "application/json" : "text/plain";
        var dataType = (isJson) ? "json" : "html";
        if (!isAsync) {
            App.preloaderShow();
        }

        return $.ajax({
            type: isPost ? "POST" : "GET",
            url: url,
            data: isPost ? JSON.stringify(data) : data,
            contentType: contentType,
            dataType: dataType,
            beforeSend: function (xhr) {
                App.preloaderShow();
            },
            success: function (successData) {
                if (!isAsync) {
                    App.preloaderHide();
                }
                return typeof (callback) == 'function' ? callback(successData) : successData;
            },
            complete: function (xhr, status) {
                App.preloaderHide();
            },
            error: function (exception) {
                return false;
            },
            async: isAsync
        });
    };

    var arrayToTree = function (arr, parent) {
        //arr.sort(function (a, b) { return parseInt(b.Level) - parseInt(a.Level) });
        var out = [];
        for (var i in arr) {
            if (arr[i].ParentId == parent) {
                var data = new Object();
                data.text = arr[i].Name;
                if (arr[i].Level == 3) {
                    data.id = arr[i].Id;
                } else {
                    var children = arrayToTree(arr, arr[i].Id);
                    if (children.length) {
                        data.children = children;
                    }
                }
                out.push(data);
            }
        }
        return out;
    };

    var loadDropdown = function (targetDropdown, dataSourceUrl, filterByValue) {

        App.sendAjaxRequest(dataSourceUrl, filterByValue, true, function (options) {
            var optionHtml = '';

            if ($.isArray(options) && (options.length > 0)) {

                $(options).each(function (index, option) {
                    optionHtml += '<option value="' + option.Value + '">' + option.Text + '</option>';
                });

            }

            $('#' + targetDropdown).html(optionHtml);

        }, true);

        $('#' + targetDropdown).val(0);

    };

    var toastrNotifier = function (msg, isSuccess) {
        toastr.clear();
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "positionClass": "toast-top-right",
            "onclick": null,
            "showDuration": "1000",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
        if (isSuccess) {
            toastr['success'](msg, "Success !");
        } else {
            toastr['error'](msg, "Error !");
        }
    };

    var toastrNotifierInfo = function (msg) {
        toastr.clear();
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "positionClass": "toast-top-right",
            "onclick": null,
            "showDuration": "1000",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        toastr['info'](msg, "Info !");
    };

    var actionHandler = function () {

        modalHandler();
        deleteHandler();

    };

    var initializeApp = function () {
        actionHandler();
    };

    return {
        init: initializeApp,
        modalShow: modalShow,
        modalHide: modalHide,
        preloaderShow: preloaderShow,
        preloaderHide: preloaderHide,
        sendAjaxRequest: sendAjaxRequest,
        loadDropdown: loadDropdown,
        toastrNotifier: toastrNotifier,
        toastrNotifierInfo: toastrNotifierInfo
    };
}();