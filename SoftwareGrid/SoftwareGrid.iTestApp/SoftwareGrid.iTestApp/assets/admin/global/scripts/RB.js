var RB = function () {

    var _isRTL = false;
    //#region private members start
    var _apiBaseUrl = 'http://localhost:3149/api';//'http://localhost:3149/api','http://mapi.RB.com/api','http://RB-hp:82/api'
    //var _countryDomain = (typeof (localStorage.countryDomain) != 'undefined') ? localStorage.countryDomain : 'us';
    var _baseUrl = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + '/' + localStorage.countryDomain;
    var _resourceBaseUrl = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '');
    

    var _objectContainsValue = function (object, value) {
        var key = -1;
        for (var prop in object) {
            if (object.hasOwnProperty(prop) && object[prop].toLowerCase() === value.toLowerCase()) {
                key = prop;
            }
        }
        return key;
    };

    var _arrayToTree = function (arr, parent) {
        //arr.sort(function (a, b) { return parseInt(b.Level) - parseInt(a.Level) });
        var out = [];
        for (var i in arr) {
            if (arr[i].ParentId == parent) {
                var data = new Object();
                data.text = arr[i].Name;
                if (arr[i].Level == 3) {
                    data.id = arr[i].Id;
                } else {
                    var children = _arrayToTree(arr, arr[i].Id);
                    if (children.length) {
                        data.children = children;
                    }
                }
                out.push(data);
            }
        }
        return out;
    };

    var _favoriteHandler = function (event) {
        event.preventDefault();
        var favoriteItem = $(this);
        var favoriteItemId = parseInt(favoriteItem.data('jobid'));
        var url = '/JobManagement/Job/FavouriteToJobAjax/' + favoriteItemId;

        if ((favoriteItemId > 0) && (url.length > 0)) {
            RB.sendAjaxRequest(url, {}, true, function (result) {
                if (result.success && result.isAuthorized) {
                    (result.IsLiked) ? favoriteItem.removeClass('fa-heart-o').addClass('fa-heart') : favoriteItem.removeClass('fa-heart').addClass('fa-heart-o');

                    RB.notifier(result.data, true);
                }
                else if (!result.success && result.isAuthorized) {
                    RB.notifier(result.data, true);
                }
                else {
                    window.location = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + '/AppAccess/User/LogOn?ReturnUrl=' + encodeURIComponent(location.pathname);
                }
            }, true, true);
        }
    };

    var _modalHandler = function () {
        //$('.RB-bootstrap-modal').unbind('click').on('click', function (event) {
        $('body').undelegate('.RB-bootstrap-modal', 'click').on('click', '.RB-bootstrap-modal', function (event) {
        
            event.preventDefault();
            var url = $(this).attr('href');
            var title = $(this).data('modal-title');
            var icon = $(this).data('modal-icon');
            var modalSize = '';
            var modalDialogSize = '';
            var isPost = false;
            if ($(this).data('ispost')==true) {
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

            $('body').find('.base-modal-RB').each(function () {
                var modal = $(this);
                (modalSize.length > 0) ? modal.addClass(modalSize) : '';
                (modalDialogSize.length > 0) ? modal.find('.RB-modal-dialog').addClass(modalDialogSize) : '';
                modal.find('.RB-modal-title').html('<i class="' + icon + '"></i> ' + title);
                RB.sendAjaxRequest(url, {}, isPost, function (result) {
                    modal.find('.modal-html-container').html(result);
                    modal.modal('show');
                }, true, false);
            });
        });


        //BUtton Click Animate Scroll to Destination
        $('a.scrollonclick[href*=#]:not([href=#])').click(function () {
            if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
                var target = $(this.hash);
                target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
                if (target.length) {
                    $('html,body').animate({
                        scrollTop: target.offset().top
                    }, 1000);
                    return false;
                }
            }
        });
    };

    var _actionHandler = function () {
        
        $('body').on('click', '.dropdown-menu.hold-on-click', function (e) {
            e.stopPropagation();
        });

        
        $('.ad-to-fav .fa-heart-o, .ad-to-fav .fa-heart').unbind('click').on('click', _favoriteHandler);

        $('body').undelegate('#btnSave', 'click').on('click', '#btnSave', function (e) {

            var itemType = $(this).attr('name').replace('btnSave', '');
            var gridId = '#' + $(this).attr('name').replace('btnSave', 'Grid');
            var listviewId = '#' + $(this).attr('name').replace('btnSave', 'ListView');
            var $form = $(this).parents('form:first');
            var url = $form.attr('action');
            if (!$form.valid || $form.valid()) {
                $.post(url, $form.serializeArray(),
                function (res) {
                    if (res.success) {
                        $('.modal').modal('hide');
                        var gridDynamic = $(gridId).data("kendoGrid");
                        if (gridDynamic != null) {
                            gridDynamic.dataSource.read();
                            gridDynamic.refresh();
                        }
                        var listviewDynamic = $(listviewId).data("kendoListView");
                        if (listviewDynamic != null) {
                            listviewDynamic.dataSource.read();
                            listviewDynamic.refresh();
                        }
                        RB.notifier(res.data, true);

                    }
                    else {
                        RB.notifier(res.data, false);
                    }
                    return false;
                }).fail(function () {
                    //loading.hide();
                });
            }
        });
        _modalHandler();
        RB.handleMomentTime();
        RB.bulkChkHandler();
    };
    //#region private members end


    //#region public members start

    var log = function (info) {
        if (typeof console == "undefined") {
            this.console = { log: function (msg) { alert(msg); } };
        }
        console.log(info);
        console.log("=====================================");
    };
    
    var sendCORSAjaxRequest = function (url, data, isPost, callback, isAsync, isJson, target) {
        isJson = typeof (isJson) == 'undefined' ? true : isJson;
        var accept = (isJson) ? "application/json; charset=utf-8" : "text/plain; charset=utf-8";
        var contentType = (isJson) ? "application/json; charset=utf-8" : "text/plain; charset=utf-8";
        var dataType = (isJson) ? "json" : "html";
        if (!isAsync) {
            RB.preloader({
                target: target,
                show: true
            });
        }


        return $.ajax({
            type: isPost ? "POST" : "GET",
            url: url,
            data: isPost ? JSON.stringify(data) : data,
            dataType: dataType,
            headers:{          
                Accept: accept,
                "Content-Type": contentType
            },
            crossDomain: true,
            xhrFields: {
                withCredentials: true
            },
            beforeSend: function (xhr) {
                RB.preloader({
                    target: target,
                    show: true
                });
            },
            success: function (successData) {
                if (!isAsync) {
                    RB.preloader({
                        target: target,
                        show: false
                    });
                }
                return typeof (callback) == 'function' ? callback(successData) : successData;
            },
            complete: function (xhr, status) {
                RB.preloader({
                    target: target,
                    show: false
                });
            },
            error: function (exception) {
                RB.preloader({
                    target: target,
                    show: false
                });
                return false;
            },
            async: isAsync
        });
    };

    var sendAjaxRequest = function (url, data, isPost, callback, isAsync, isJson, target) {
        isJson = typeof (isJson) == 'undefined' ? true : isJson;
        var contentType = (isJson) ? "application/json" : "text/plain";
        var dataType = (isJson) ? "json" : "html";
        if (!isAsync) {
            RB.preloader({
                target: target,
                show: true
            });
        }

        return $.ajax({
            type: isPost ? "POST" : "GET",
            url: url,
            data: isPost ? JSON.stringify(data) : data,
            contentType: contentType,
            dataType: dataType,
            beforeSend: function (xhr) {
                RB.preloader({
                    target: target,
                    show: true
                });
            },
            success: function (successData) {
                if (!isAsync) {
                    RB.preloader({
                        target: target,
                        show: false
                    });
                }
                return typeof (callback) == 'function' ? callback(successData) : successData;
            },
            complete: function (xhr, status) {
                RB.preloader({
                    target: target,
                    show: false
                });
            },
            error: function (exception) {
                return false;
            },
            async: isAsync
        });
    };
    
    var onScrollSendAjaxRequest = function (options) {
        options.target.each(function () {
            options.param.iDisplayLength = ((typeof options.param.iDisplayLength == 'undefined') || (parseInt(options.param.iDisplayLength).toString() == 'NaN') || (options.param.iDisplayLength <= 0)) ? 10 : parseInt(options.param.iDisplayLength);
            options.param.iDisplayStart = ((typeof options.param.iDisplayStart == 'undefined') || (parseInt(options.param.iDisplayStart).toString() == 'NaN') || (options.param.iDisplayStart <= 0)) ? 1 : parseInt(options.param.iDisplayStart);
            options.param.total = ((typeof options.param.total == 'undefined') || (parseInt(options.param.total).toString() == 'NaN') || (options.param.total <= 0)) ? parseInt($('.result-counter').text()) : parseInt(options.param.total);
            options.target.data({
                url : options.url,
                target: options.target,
                param : options.param,
                callback : options.callback
            });

            (options.diposeTarget.length > 0) ? options.diposeTarget.unbind('scroll') : '';
            options.target.unbind('scroll').on('scroll', function () {
                var scrollData = $(this).data();
                if (typeof this.scrollHeight == 'undefined' || typeof this.clientHeight == 'undefined') {
                    if (($(window).scrollTop() + 5) >= $(document).height() - $(window).height()) {
                        if (scrollData.param.total > (scrollData.param.iDisplayStart * options.param.iDisplayLength)) {
                            scrollData.param.iDisplayStart = scrollData.param.iDisplayStart + 1;
                            RB.sendCORSAjaxRequest(scrollData.url, scrollData.param, true, function (result) {
                                if (typeof (scrollData.callback) == 'function') {
                                    scrollData.callback(result)
                                }
                                //else {
                                //    return result;
                                //}
                            }, true, true, $(this));
                        }
                    }
                } else {
                    if ((this.scrollTop + this.clientHeight) >= this.scrollHeight) {
                        if (scrollData.param.total > (scrollData.param.iDisplayStart * options.param.iDisplayLength)) {
                            scrollData.param.iDisplayStart = scrollData.param.iDisplayStart + 1;
                            RB.sendCORSAjaxRequest(scrollData.url, scrollData.param, true, function (result) {
                                if (typeof (scrollData.callback) == 'function') {
                                    scrollData.callback(result);
                                }
                                //else {
                                //    return result;
                                //}
                            }, true, true, $(this));
                        }
                    }
                }
                
            });
        });
        
    };
    var loadClassDropdown=function (targetDropdown, dataSourceUrl, filterByValue, isSelect2, isPlaceholder, isMultiple, isTreeSelect2, selectedValue, isHideSearch) {
        var placeHolder = (typeof (isPlaceholder) == 'undefined' || (typeof (isPlaceholder) == 'boolean')) ? 'Select' : isPlaceholder;
        isPlaceholder = (typeof (isPlaceholder) == 'undefined' || (typeof (isPlaceholder) != 'boolean')) ? true : isPlaceholder;
        isSelect2 = (typeof (isSelect2) == 'undefined') ? true : isSelect2;
        isMultiple = (typeof (isMultiple) == 'undefined') ? false : isMultiple;
        isTreeSelect2 = (typeof (isTreeSelect2) == 'undefined') ? false : isTreeSelect2;
        isHideSearch = (isHideSearch == true) ? -1 : 10;

        RB.sendAjaxRequest(dataSourceUrl, filterByValue, true, function (options) {
            var optionHtml = '';
            if (isSelect2) {
                $('.' + targetDropdown).select2('destroy');

                if ($.isArray(options) && (options.length > 0)) {

                    if ($('.' + targetDropdown).is("select")) {
                        optionHtml = (isPlaceholder) ? '<option></option>' : '';
                        if ($.isArray(options) && (options.length > 0)) {

                            $(options).each(function (index, option) {
                                var dataParam = '';
                                for (var pname in option) {
                                    if (option.hasOwnProperty(pname)) {

                                        if ($.inArray(pname, ['Value', 'Text']) == -1) {
                                            dataParam = dataParam + 'data-' + pname.toLowerCase() + '="' + option[pname] + '" ';
                                        }
                                    }
                                }
                                optionHtml += '<option value="' + option.Value + '" ' + dataParam + ((option.Value == selectedValue) ? " selected='selected'" : "") + '>' + option.Text + '</option>';
                            });
                        }
                        $('.' + targetDropdown).html(optionHtml);
                        $('.' + targetDropdown).select2({
                            placeholder: placeHolder,
                            minimumResultsForSearch: isHideSearch
                        });
                    } else {
                        var select2Options = [];
                        if (isTreeSelect2) {
                            select2Options = _arrayToTree(options, null);
                        } else {
                            $(options).each(function (index, option) {
                                select2Options.push({ id: option.Value, text: option.Text + '' });
                            });
                        }
                        if (isPlaceholder) {
                            $('.' + targetDropdown).select2({
                                placeholder: placeHolder,
                                multiple: isMultiple,
                                data: select2Options,
                                minimumResultsForSearch: isHideSearch
                            });
                        } else {
                            $('.' + targetDropdown).select2({
                                multiple: isMultiple,
                                data: select2Options,
                                minimumResultsForSearch: isHideSearch
                            });
                        }
                    }

                } else {
                    if ($('.' + targetDropdown).is("select")) {
                        if (isPlaceholder) {
                            $('.' + targetDropdown).html('<option></option>');
                        } else {
                            $('.' + targetDropdown).html('');
                        }

                        $('.' + targetDropdown).select2({
                            placeholder: placeHolder,
                            minimumResultsForSearch: isHideSearch
                        });
                    } else {
                        $('.' + targetDropdown).select2({
                            placeholder: placeHolder,
                            multiple: isMultiple,
                            data: [],
                            minimumResultsForSearch: isHideSearch
                        });
                    }
                }
            } else {
                optionHtml = (isPlaceholder) ? '<option></option>' : '';
                if ($.isArray(options) && (options.length > 0)) {

                    $(options).each(function (index, option) {
                        optionHtml += '<option value="' + option.Value + '">' + option.Text + '</option>';
                    });

                }

                $('.' + targetDropdown).html(optionHtml);
            }
        }, true);
    };
    var loadDropdown = function (targetDropdown, dataSourceUrl, filterByValue, isSelect2, isPlaceholder, isMultiple, isTreeSelect2, selectedValue, isHideSearch) {
        var placeHolder = (typeof (isPlaceholder) == 'undefined' || (typeof (isPlaceholder) == 'boolean')) ? 'Select' : isPlaceholder;
        isPlaceholder = (typeof (isPlaceholder) == 'undefined' || (typeof (isPlaceholder) != 'boolean')) ? true : isPlaceholder;
        isSelect2 = (typeof (isSelect2) == 'undefined') ? true : isSelect2;
        isMultiple = (typeof (isMultiple) == 'undefined') ? false : isMultiple;
        isTreeSelect2 = (typeof (isTreeSelect2) == 'undefined') ? false : isTreeSelect2;
        isHideSearch = (isHideSearch == true) ? -1 : 10;
        
        RB.sendAjaxRequest(dataSourceUrl, filterByValue, true, function (options) {
            var optionHtml = '';
            if (isSelect2) {
                $('#' + targetDropdown).select2('destroy');
                if ($.isArray(options) && (options.length > 0)) {

                    if ($('#' + targetDropdown).is("select")) {
                        optionHtml = (isPlaceholder) ? '<option></option>' : '';
                        if ($.isArray(options) && (options.length > 0)) {

                            $(options).each(function (index, option) {
                                var dataParam = '';
                                for (var pname in option) {
                                    if (option.hasOwnProperty(pname)) {
                                        
                                        if ($.inArray(pname,['Value','Text']) == -1) {
                                            dataParam = dataParam + 'data-' + pname.toLowerCase() + '="' + option[pname]+ '" ';
                                        }
                                    }
                                }
                                optionHtml += '<option value="' + option.Value + '" ' + dataParam + ((option.Value == selectedValue) ? " selected='selected'" : "") + '>' + option.Text + '</option>';
                            });
                        }
                        $('#' + targetDropdown).html(optionHtml);
                        $('#' + targetDropdown).select2({
                            placeholder: placeHolder,
                            minimumResultsForSearch: isHideSearch,
                            allowClear: true
                        });
                    } else {
                        var select2Options = [];
                        if (isTreeSelect2) {
                            select2Options = _arrayToTree(options, null);
                        } else {
                            $(options).each(function (index, option) {
                                select2Options.push({ id: option.Value, text: option.Text + '' });
                            });
                        }
                        if (isPlaceholder) {
                            $('#' + targetDropdown).select2({
                                placeholder: placeHolder,
                                multiple: isMultiple,
                                data: select2Options,
                                minimumResultsForSearch: isHideSearch,
                                allowClear: true
                            });
                        } else {
                            $('#' + targetDropdown).select2({
                                multiple: isMultiple,
                                data: select2Options,
                                minimumResultsForSearch: isHideSearch,
                                allowClear: true
                            });
                        }
                    }

                } else {
                    if ($('#' + targetDropdown).is("select")) {
                    if (isPlaceholder) {
                        $('#' + targetDropdown).html('<option></option>');
                    } else {
                        $('#' + targetDropdown).html('');
                    }
                    
                    $('#' + targetDropdown).select2({
                        placeholder: placeHolder,
                        minimumResultsForSearch: isHideSearch,
                        allowClear: true
                    });
                    } else {
                        $('#' + targetDropdown).select2({
                            placeholder: placeHolder,
                            multiple: isMultiple,
                            data: [],
                            minimumResultsForSearch: isHideSearch,
                            allowClear: true
                        });
                }
                }
            } else {
                optionHtml = (isPlaceholder) ? '<option></option>' : '';
                if ($.isArray(options) && (options.length > 0)) {

                    $(options).each(function (index, option) {
                        optionHtml += '<option value="' + option.Value + '">' + option.Text + '</option>';
                    });

                }

                $('#' + targetDropdown).html(optionHtml);
            }
        }, true);
    };
    var loadPickerDropdown = function (targetDropdown, dataSourceUrl, filterByValue, isPlaceholder, selectedValue) {
        $('#' + targetDropdown).selectpicker();
        var placeHolder = (typeof (isPlaceholder) == 'undefined' || (typeof (isPlaceholder) == 'boolean')) ? 'Select' : isPlaceholder;
        isPlaceholder = (typeof (isPlaceholder) == 'undefined' || (typeof (isPlaceholder) != 'boolean')) ? true : isPlaceholder;

        RB.sendAjaxRequest(dataSourceUrl, filterByValue, true, function (options) {
            var optionHtml = '';
            //optionHtml = (isPlaceholder) ? '<option></option>' : '';
            if ($.isArray(options) && (options.length > 0)) {

                $(options).each(function (index, option) {

                        optionHtml += '<option value="' + option.Value + '">' + option.Text + '</option>';
                    });

                }

            $('#' + targetDropdown).html(optionHtml);
            $('#' + targetDropdown).selectpicker();
            $('#' + targetDropdown).selectpicker('refresh');
        }, true);
    };

    var loadAjaxSelect = function (targetDropdown, dataSourceUrl) {

        $('#' + targetDropdown).select2({
            ajax: {
                url: dataSourceUrl,
                dataType: 'json',
                data: function (params) {
                    return {
                        keyword: params.term //search term
                    };
                },
                processResults: function (data) {
                    return {
                        results: data
                    }
                    ;
                },
                cache: true
            },
            escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
            minimumInputLength: 2,
            templateResult:  function (item) {
                //if (item.loading) return item.text;

                //var markup = '<div class="clearfix">' +
                //    '<div class="col-sm-1">' +
                //'<img src="https://avatars.githubusercontent.com/u/1830593?v=3" style="max-width: 100%" />' +
                //'</div>' +
                //'<div clas="col-sm-10">' +
                //'<div class="clearfix">' +
                //'<div class="col-sm-6">' + item.Text + '</div>' +
                //'<div class="col-sm-3"><i class="fa fa-code-fork"></i> ' + item.Value + '</div>' +
                //'</div>';
                //markup += '</div></div>';
                var markup = '<div>' + item.text + '</div>'
                return markup;
            },
            templateSelection: function (item) {

                return item.text;
            }
        });
    };
    
    var loadCheckboxDropdown = function (targetDropdown, dataSourceUrl, filterByValue, placeholder, isTree, selectedValues, lastLevel, chkAllDefault, onCheckCallback) {
        placeholder = typeof (placeholder) == 'undefined' ? 'Any' : placeholder;
        isTree = (typeof (isTree) == 'undefined') ? false : isTree;
        chkAllDefault = (typeof (chkAllDefault) == 'undefined') ? true : chkAllDefault;

        RB.sendAjaxRequest(dataSourceUrl, filterByValue, true, function (options) {
            if ($.isArray(options) && (options.length > 0)) {
                if (isTree) {
                    $('#' + targetDropdown).append('<a href="" class="btn btn-border dropdown-toggle form-control" data-toggle="dropdown" data-close-others="true">' + placeholder + ' <i class="fa fa-angle-down pull-right"></i></a>');
                    $('#' + targetDropdown).append('<ul class="dropdown-menu hold-on-click dropdown-check" style="min-width:200px;max-height:400px;overflow-y:scroll"></ul>');
                    //options.sort(function (a, b) { return parseInt(a.Level) - parseInt(b.Level) });
                    var nodes = options, node;
                    for (var i = 0; i < nodes.length; i += 1) {
                        node = nodes[i];
                        if (node.ParentId !== null) {
                            if (node.Level == lastLevel) {
                                if ($('#' + targetDropdown + ' li[data-hierarchy-level="' + node.ParentId + '"] ul').length > 0) {
                                    $('#' + targetDropdown + ' li[data-hierarchy-level="' + node.ParentId + '"] ul').append('<li><label><input name="' + targetDropdown + '[]" type="checkbox" value="' + node.Id + '" data-chk-label="' + targetDropdown.toLowerCase() + '-' + node.Id + '" data-chk-parent="' + targetDropdown.toLowerCase() + '-' + node.ParentId + '" /> ' + node.Name + ' </label></li>');
                                } else {
                                    $('#' + targetDropdown + ' li[data-hierarchy-level="' + node.ParentId + '"]').append('<ul class="nested-list"><li><label><input name="' + targetDropdown + '[]" type="checkbox" value="' + node.Id + '" data-chk-label="' + targetDropdown.toLowerCase() + '-' + node.Id + '" data-chk-parent="' + targetDropdown.toLowerCase() + '-' + node.ParentId + '" /> ' + node.Name + ' </label></li></ul>');
                                }
                            } else {
                                if ($('#' + targetDropdown + ' li[data-hierarchy-level="' + node.ParentId + '"] ul').length > 0) {
                                    $('#' + targetDropdown + ' li[data-hierarchy-level="' + node.ParentId + '"] ul').append('<li data-hierarchy-level="' + node.Id + '"><label><input type="checkbox" data-chk-label="' + targetDropdown.toLowerCase() + '-' + node.Id + '" data-chk-parent="' + targetDropdown.toLowerCase() + '-' + node.ParentId + '" /> ' + node.Name + ' </label></li>');
                                }
                                else {
                                    $('#' + targetDropdown + ' li[data-hierarchy-level="' + node.ParentId + '"]').append('<ul class="nested-list"><li data-hierarchy-level="' + node.Id + '"><label><input type="checkbox" data-chk-label="' + targetDropdown.toLowerCase() + '-' + node.Id + '" data-chk-parent="' + targetDropdown.toLowerCase() + '-' + node.ParentId + '" /> ' + node.Name + ' </label></li></ul>');
                                }
                            }
                        } else {
                            $('#' + targetDropdown + ' ul').append('<li data-hierarchy-level="' + node.Id + '"><label><input type="checkbox" data-chk-label="' + targetDropdown.toLowerCase() + '-' + node.Id + '" data-chk-parent="" /> ' + node.Name + ' </label></li>');
                        }
                    }
                } else {
                    $('#' + targetDropdown).append('<a href="" class="btn btn-border dropdown-toggle form-control" data-toggle="dropdown" data-close-others="true">' + placeholder + ' <i class="fa fa-angle-down pull-right"></i></a>');
                    $('#' + targetDropdown).append('<ul class="dropdown-menu hold-on-click dropdown-check" style="min-width:200px;max-height:200px;overflow-y:scroll"></ul>');
                    $(options).each(function (index, option) {
                        $('#' + targetDropdown + ' ul').append('<li><label><input name="' + targetDropdown + '[]" type="checkbox" data-chk-label="' + targetDropdown.toLowerCase() + '-' + option.Value + '" data-chk-parent="" value="' + option.Value + '" /> ' + option.Text + ' </label></li>');
                    });
                }

                Metronic.init();
                RB.bulkChkHandler(onCheckCallback);
                if (chkAllDefault) {
                    $('input[Name="' + targetDropdown + '[]"]').trigger('click');
                } else if ((typeof (selectedValues) != 'undefined') && (selectedValues.length > 0)) {
                    $('input[Name="' + targetDropdown + '[]"]').parent('span').removeClass('checked');
                    for (var ci = 0; ci < selectedValues.length; ci++) {
                        $('input[Name="' + targetDropdown + '[]"][value="' + selectedValues[ci] + '"]').trigger('click');
                    }
                }
                
            }
            
        }, false);
    };

    var loadCheckboxTree = function (targetNode, dataSourceUrl, filterByValue, selectedValues, chkAllDefault, onCheckCallback) {
        chkAllDefault = (typeof (chkAllDefault) == 'undefined') ? true : chkAllDefault;
        RB.sendAjaxRequest(dataSourceUrl, filterByValue, true, function (options) {
            $('#' + targetNode).html('');
            if ($.isArray(options) && (options.length > 0)) {
                $('#' + targetNode).append('<ul class="RB-treeview"></ul>');
                $('#' + targetNode).append('<div id="Selected' + targetNode + '"></div>');
                var nodes = options, node;
                for (var i = 0; i < nodes.length; i += 1) {
                    node = nodes[i];
                    if (node.ParentId !== null) {
                        var parentLevel = parseInt(node.Level) - 1;
                        if (node.IsChild) {
                            if ($('#' + targetNode + ' li[data-hierarchy-level="' + node.ParentId + parentLevel + '"] ul').length > 0) {
                                $('#' + targetNode + ' li[data-hierarchy-level="' + node.ParentId + parentLevel + '"] ul').append('<li><label><input name="' + targetNode + '[]" type="checkbox" value="' + node.Id + '" data-chk-label="' + targetNode.toLowerCase() + '-' + node.Id + node.Level + '" data-chk-parent="' + targetNode.toLowerCase() + '-' + node.ParentId + parentLevel + '" /> ' + node.Name + ' </label></li>');
                            } else {
                                $('#' + targetNode + ' li[data-hierarchy-level="' + node.ParentId + parentLevel + '"]').append('<ul class="nested-list"><li><label><input name="' + targetNode + '[]" type="checkbox" value="' + node.Id + '" data-chk-label="' + targetNode.toLowerCase() + '-' + node.Id + node.Level + '" data-chk-parent="' + targetNode.toLowerCase() + '-' + node.ParentId + parentLevel + '" /> ' + node.Name + ' </label></li></ul>');
                            }
                        } else {
                            if ($('#' + targetNode + ' li[data-hierarchy-level="' + node.ParentId + parentLevel + '"] ul').length > 0) {
                                $('#' + targetNode + ' li[data-hierarchy-level="' + node.ParentId + parentLevel + '"] ul').append('<li data-hierarchy-level="' + node.Id + node.Level + '"><span class="navigation-arrow k-icon k-plus"></span><label><input type="checkbox" data-chk-label="' + targetNode.toLowerCase() + '-' + node.Id + node.Level + '" data-chk-parent="' + targetNode.toLowerCase() + '-' + node.ParentId + parentLevel + '" /> ' + node.Name + ' </label></li>');
                            } else {
                                $('#' + targetNode + ' li[data-hierarchy-level="' + node.ParentId + parentLevel + '"]').append('<ul class="nested-list"><li data-hierarchy-level="' + node.Id + node.Level + '"><span class="navigation-arrow k-icon k-plus"></span><label><input type="checkbox" data-chk-label="' + targetNode.toLowerCase() + '-' + node.Id + node.Level + '" data-chk-parent="' + targetNode.toLowerCase() + '-' + node.ParentId + parentLevel + '" /> ' + node.Name + ' </label></li></ul>');
                            }
                        }
                    } else {
                        $('#' + targetNode + ' ul').append('<li data-hierarchy-level="' + node.Id + node.Level + '"><span class="navigation-arrow k-icon k-plus"></span><label><input type="checkbox" data-chk-label="' + targetNode.toLowerCase() + '-' + node.Id + node.Level + '" data-chk-parent="" /> ' + node.Name + ' </label></li>');
                    }
                }

                Metronic.init();
                RB.bulkChkHandler(onCheckCallback);
                for (var o = 0; o < options.length; o += 1) {
                    var option = options[o];
                    if (option.IsChecked) {
                        $('input[data-chk-label="' + targetNode.toLowerCase() + '-' + option.Id + option.Level + '"]').trigger('click');
                    }
                }

                $('.RB-treeview .navigation-arrow').unbind('click').on('click', function() {
                    if ($(this).hasClass('k-minus')) {
                        $(this).removeClass('k-minus').addClass('k-plus');
                        //$(this).closest('ul').slideUp();
                        $(this).siblings("ul").addClass('nested-list');
                    } else {
                        $(this).removeClass('k-plus').addClass('k-minus');
                        //$(this).closest('ul').slideDown();
                        $(this).siblings("ul").removeClass('nested-list');
                    }
                });
                //if (chkAllDefault) {
                //    $('input[Name="' + targetNode + '[]"]').trigger('click');
                //} else if ((typeof (selectedValues) != 'undefined') && (selectedValues.length > 0)) {
                //    $('input[Name="' + targetNode + '[]"]').parent('span').removeClass('checked');
                //    for (var ci = 0; ci < selectedValues.length; ci++) {
                //        $('input[Name="' + targetNode + '[]"][value="' + selectedValues[ci] + '"]').trigger('click');
                //    }
                //}
            } else {
                $('#' + targetNode).html('<div style="font-color:gray">No Item Found</div>');
            }

        }, false);
    };

    var notifier = function (msg, type) {
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
        }
        if (parseInt(type) == 2) {
            toastr['success'](msg, "Success !");
        }
        if (parseInt(type) == 3) {
            toastr['error'](msg, "Error !");
        }
        if (parseInt(type) == 4) {
            toastr['info'](msg, "Information !");
        }
        if (parseInt(type) == 5) {
            toastr['warning'](msg, "Warning !");
        }
       

    };

    var preloader = function (options) {
        options = $.extend(true, {}, options);
        options.animate = true;
        options.overlayColor = '#555';//'none';
        if (options.show) {
            var html = '';
            if (options.animate) {
                html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '">' + '<div class="block-spinner-bar"><div class="bounce1"></div><div class="bounce2"></div><div class="bounce3"></div></div>' + '</div>';
            } //else if (options.iconOnly) {
            //    html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '"><img src="' + this.getGlobalImgPath() + 'loading-spinner-grey.gif" align=""></div>';
            //} else if (options.textOnly) {
            //    html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '"><span>&nbsp;&nbsp;' + (options.message ? options.message : 'LOADING...') + '</span></div>';
            //} else {
            //    html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '"><img src="' + this.getGlobalImgPath() + 'loading-spinner-grey.gif" align=""><span>&nbsp;&nbsp;' + (options.message ? options.message : 'LOADING...') + '</span></div>';
            //}
            if (options.target) { // element blocking
                
                var el = $(options.target).is(document) ? $('body') : $(options.target);
                if (el.height() <= ($(window).height())) {
                    options.cenrerY = true;
                }
                el.block({
                    message: html,
                    baseZ: options.zIndex ? options.zIndex : 9999,
                    centerY: options.cenrerY !== undefined ? options.cenrerY : false,
                    css: {
                        top: '10%',
                        border: '0',
                        padding: '0',
                        backgroundColor: 'none'
                    },
                    overlayCSS: {
                        backgroundColor: options.overlayColor ? options.overlayColor : '#555',
                        opacity: options.boxed ? 0.05 : 0.1,
                        cursor: 'wait'
                    }
                });
            } else { // page blocking
                $.blockUI({
                    message: html,
                    baseZ: options.zIndex ? options.zIndex : 9999,
                    css: {
                        border: '0',
                        padding: '0',
                        backgroundColor: 'none'
                    },
                    overlayCSS: {
                        backgroundColor: options.overlayColor ? options.overlayColor : '#555',
                        opacity: options.boxed ? 0.05 : 0.1,
                        cursor: 'wait'
                    }
                });
            }
        } else {
            if (options.target) {
                var el = $(options.target).is(document) ? $('body') : $(options.target);
                el.unblock({
                    onUnblock: function () {
                        el.css('position', '');
                        el.css('zoom', '');
                    }
                });
            } else {
                $.unblockUI();
            }
        }
    };

    var _singleChkHandler = function (parent) {
        var parent = $(parent).data('chk-parent');
        if (parent != '') {
            var allitem = $('input[data-chk-parent="' + parent + '"]');
            var checkedItem = $('input[data-chk-parent="' + parent + '"]:checkbox:checked');

            if (checkedItem.length == allitem.length) {
                $('input[data-chk-label="' + parent + '"]').prop('checked', true);
                $('input[data-chk-label="' + parent + '"]').parent('span').addClass('checked');
                $('input[data-chk-label="' + parent + '"]').each(function () {
                    _singleChkHandler(this);
                });
            } else {
                $('input[data-chk-label="' + parent + '"]').prop('checked', false);
                $('input[data-chk-label="' + parent + '"]').parent('span').removeClass('checked');
                $('input[data-chk-label="' + parent + '"]').each(function () {
                    _singleChkHandler(this);
                });
            }
        }
    };
    var decimalInput = function (evt, thisObject) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 8 && charCode != 9 && charCode != 45 && (charCode != 46 || $(thisObject).val().indexOf('.') != -1) && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    };

    var integerInput = function (evt, thisObject) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 8 && charCode != 9 && charCode != 45 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    };
    var bulkChkHandler = function (onClickCallback) {

        $('[data-chk-label]').each(function() {
            var callback = typeof (onClickCallback) != 'undefined' ? onClickCallback : function () { };
            if (typeof ($(this).data('clickCallback')) != 'function') {
                $(this).data('clickCallback', callback);
            }
        });

        $('[data-chk-label]').unbind('click').on('click', function (e) {
            var isChecked = false;
            if ($(this).attr('checked') == "checked") {
                isChecked = true;
            }

            var checkUnchekItem = function (item, isChecked) {
                var label = $(item).data('chk-label');
                if (isChecked) {
                    $('[data-chk-parent="' + label + '"]').prop("checked", item.checked);
                    $(item).parent().addClass('checked');

                    if ($('[data-chk-parent="' + label + '"]').length > 0) {
                        $('[data-chk-parent="' + label + '"]').each(function() {
                            checkUnchekItem(this, isChecked);
                        });
                    }
                } else {
                    $('[data-chk-parent="' + label + '"]').removeAttr('checked');
                    $(item).parent().removeClass('checked');

                    if ($('[data-chk-parent="' + label + '"]').length > 0) {
                        $('[data-chk-parent="' + label + '"]').each(function() {
                            checkUnchekItem(this, isChecked);
                        });
                    }
                }
                var itemCallback = $(item).data('clickCallback');
                if (typeof (itemCallback) == 'function') {
                    itemCallback();
                }
            }
            checkUnchekItem(this, isChecked);
            _singleChkHandler(this);
        });
    };

    var passSearchParameter = function (id) {
        var o = {};
        try {
            var frmSearch = $('#' + id);
            if (frmSearch != null && frmSearch != undefined) {

                var data = frmSearch.serializeArray();
                $.each(data, function () {
                    if (o[this.name] !== undefined) {
                        if (!o[this.name].push) {
                            o[this.name] = [o[this.name]];
                        }
                        //o[this.name].push(this.value || '');
                    } else {
                        o[this.name] = this.value || '';
                    }
                });
            }
            var hfParantId = $("#hfParentId");
            if (hfParantId != null) {
                var name = $(hfParantId).attr('name');
                o[name] = hfParantId.val();
            }
            var searchString = window.location.search.substring(1);
            if (searchString != null) {
                var params = searchString.split("&");
                for (var i = 0; i < params.length; i++) {
                    var val = params[i].split("=");
                    o[unescape(val[0])] = unescape(val[1]);
                }
            }
        }
        catch (ex) {
        }
        return o;
    };

    var handleModal = function () {
        _modalHandler();
    };

    var handleMomentTime = function () {
        if (typeof moment == 'function') {
            $(".moment-time:not('.moment-formated')").each(function (i, e) {
                if (!$(this).hasClass('moment-formated')) {
                    var el = this;
                    var time = moment($(el).text());
                    var now = moment();
                    $(el).html(time.from(now));
                    $(el).addClass('moment-formated');
                }
            });
        }
    };

    var handleNumberFormat = function () {
        $('.RB-number').each(function () {
            var number = $(this).text();
            if (Number(number).toString() != 'NaN') {
                $(this).html(Number(number).toLocaleString('en-US'));
            }
        });
    };

    var handleCurrencyFormat = function (amount) {

        if (typeof (amount) != 'undefined') {
            if (Number(amount).toString() != 'NaN') {
                amount = Number(amount).toLocaleString('en-US');
            }
            return amount;
        } else {
            $('.RB-amount').each(function () {
                var amount = $(this).text();
                if (Number(amount).toString() != 'NaN') {
                    $(this).html(Number(amount).toLocaleString('en-US'));
                }
            });
        }
    };

    var handleLocalization = function () {
        var contents = [];
        $('[data-content-key]:not(.translated)').each(function (index) {
            var dynamicContent = {};
            var content = {};
            if ((contentKey = $(this).data('content-key')) != '') {
                var key = contentKey.toLowerCase() + '-locale-' + index;
                $(this).attr('data-translation-key', key);
                content.Original = contentKey;
                dynamicContent.Key = key;
                dynamicContent.Content = content;
                contents.push(dynamicContent);
            }
        });
        if (contents.length > 0) {
            sendAjaxRequest(RB.resourceBaseUrl()+'/ContentManager/GetContentAjax', contents, true, function (data) {
                if (!$.isEmptyObject(data)) {
                    for (var c = 0 ; c < data.content.length; c++) {
                        $('[data-translation-key="' + data.content[c].Key + '"]').contents().filter(function () { return this.nodeType == 3; }).first().replaceWith(data.content[c].Content.Locale);
                        $('[data-translation-key="' + data.content[c].Key + '"]').removeClass('RB-translation').addClass('translated');
                    }
                }
            }, true, true);
        }
    };

    var handleImageLoader = function () {
        $('[RB-img-src]:visible:not(.img-loaded)').each(function () {
            function changesrc(imgsrc, item) {
                item.src = imgsrc;
                $(item).addClass('img-loaded');
            }

            var imgsrc = $(this).attr('RB-img-src');

            if (imgsrc.length > 0) {
                imgsrc = imgsrc.indexOf("http://") > -1 ? imgsrc : RB.resourceBaseUrl() + imgsrc;
                var loadimage = new Image();
                loadimage.src = imgsrc;
                loadimage.onload = changesrc(imgsrc, this);
            }
        });
    };

    var apiBaseUrl = function () {
        return _apiBaseUrl;
    };

    var baseUrl = function () {
        return _baseUrl;
    };

    var resourceBaseUrl = function () {
        return _resourceBaseUrl;
    };

    var getDirectoryUrl = function (specifier) {
        return typeof (_directories[specifier]) == 'undefined' ? '' : _directories[specifier];
    };

    var getSuggestionUrl = function (specifier) {
        return typeof (_locationSuggest[specifier]) == 'undefined' ? '' : _locationSuggest[specifier];
    };

    var getCountryDomain = function () {
        return localStorage.countryDomain;
    }

    var isRTL = function () {
        return _isRTL;
    }

    var decodeHTMLEntities = function (text) {
        var entities = [
			['apos', '\''],
			['amp', '&'],
			['lt', '<'],
			['gt', '>']
        ];

        for (var i = 0, max = entities.length; i < max; ++i)
            text = text.replace(new RegExp('&' + entities[i][0] + ';', 'g'), entities[i][1]);

        return text;
    }

    var initializeRB = function () {
        if ($('body').css('direction') === 'rtl') {
            _isRTL = true;
        }
        
        var device = $(window).width();
        if (device < 412) {
            $('.message-menu, .task-menu, .notify-menu').addClass('abs-xs-dropdown');
        }

        handleCurrencyFormat();
        handleNumberFormat();
        handleImageLoader();
        _actionHandler();
        setLocalTimeZone();
    };
    //#region public members end

    var jqDatePicker = function() {

        $(".jqDatePicker").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd-M-yy',
                beforeShow: function () {
                    setTimeout(function () {
                        $('.ui-datepicker').css('z-index', 99999);
                    }, 0);
                }
            });
    };

    var getTimeZone = function(){
        return /\((.*)\)/.exec(new Date().toString())[1];
    }

    var setLocalTimeZone = function () {
        var expires = new Date();
        if (document.cookie.indexOf("LocalTimeZone") >= 0) {
        } else {
            var timeZone = getTimeZone();
            expires.setTime(expires.getTime() + (24 * 60 * 60 * 1000));
            document.cookie = "LocalTimeZone" + '=' + timeZone + ';expires=' + expires.toUTCString();
        }

        if (document.cookie.indexOf("TimeZoneOffset") >= 0) {
        } else {
            var date = new Date();
            var offset = (date.getTimezoneOffset()) * -1;
            expires.setTime(expires.getTime() + (24 * 60 * 60 * 1000));
            document.cookie = "TimeZoneOffset" + '=' + offset + ';expires=' + expires.toUTCString();
        }

    };

    var makePagination = function (tableId) {
        $('#' + tableId).dataTable({
            "paging": true,
            "ordering": true,
            "info": true
        });
    }



    return {
        log: log,
        init: initializeRB,
        sendAjaxRequest: sendAjaxRequest,
        onScrollSendAjaxRequest: onScrollSendAjaxRequest,
        sendCORSAjaxRequest: sendCORSAjaxRequest,
        apiBaseUrl: apiBaseUrl,
        baseUrl: baseUrl,
        resourceBaseUrl: resourceBaseUrl,
        passSearchParameter: passSearchParameter,
        loadClassDropdown:loadClassDropdown,
        loadDropdown: loadDropdown,
        loadPickerDropdown:loadPickerDropdown,
        loadCheckboxDropdown: loadCheckboxDropdown,
        loadCheckboxTree: loadCheckboxTree,
        loadAjaxSelect:loadAjaxSelect,
        preloader: preloader,
        notifier: notifier,
        getDirectoryUrl: getDirectoryUrl,
        getSuggestionUrl: getSuggestionUrl,
        handleMomentTime: handleMomentTime,
        handleModal: handleModal,
        bulkChkHandler: bulkChkHandler,
        decimalInput: decimalInput,
        integerInput: integerInput,
        getCountryDomain: getCountryDomain,
        jqDatePicker: jqDatePicker,
        decodeHTMLEntities: decodeHTMLEntities,
        handleCurrencyFormat: handleCurrencyFormat,
        handleNumberFormat: handleNumberFormat,
        handleImageLoader: handleImageLoader,
        handleLocalization: handleLocalization,
        setLocalTimeZone: setLocalTimeZone,
        isRTL: isRTL,
        makePagination: makePagination
    };
}();



function show(id) {

    return function (e) {
        RB.preloader({
            target: '#' + id,
            show: true
        });

    };

    //eventhandler for showing loader
}
function destroy(id) {

    return function (e) {
        RB.preloader({
            target: '#' + id,
            show: false
        });

    };
    //eventhandler for destroying loader
}
function OnChangedGrid(gridID) {

    return function () {
        var grid = $("#" + gridID).data("kendoListView");
        var count = grid.dataSource.total();
        var pagerid = "#" + gridID + "_pager";
        var id = pagerid + "  a," + pagerid + "  ul," + pagerid + "  .k-pager-sizes";
        if (count > 0) {
            $(id).show();
        }
        else {
            $(id).hide();
            var nodatacontentid = "#" + gridID + "NoDataContent";
            if (nodatacontentid != null) {
                $(nodatacontentid).show();
            }
        }
    };

}
function EmailTemplateDropDownList_OnChanged(e) {


    var postUrl = '/AppAccessManagement/AppMasterEmailTemplate/GetEmailTemplateAjax?Id=' + e.sender.element.val();
    var propertyID = $('#RelatedToPropertyID').val();
    if (propertyID != '' && propertyID != '0') {
        postUrl = postUrl + '&PropertyId=' + propertyID;
    }

    var SentToContactID = $('#SentToContactID').val();
    if (SentToContactID != '' && SentToContactID != '0') {
        postUrl = postUrl + '&ContactID=' + SentToContactID;
    }
    //alert(e.sender.element.val());

    $.get(postUrl, function (res) {
        $('#EmailSubject').val(res.EmailSubject);
        var editor = $("#EmailMessage").data("kendoEditor");
        editor.value(res.EmailMessage);
        return false;
    });
}
function EmailTemplateDropDownList_OnChanged(e) {

    var postUrl = '/AppAccessManagement/AppMasterEmailTemplate/GetEmailTemplateAjax?Id=' + e.sender.element.val();
    var propertyID = $('#RelatedToPropertyID').val();
    if (propertyID != '' && propertyID != '0') {
        postUrl = postUrl + '&PropertyId=' + propertyID;
    }

    var SentToContactID = $('#SentToContactID').val();
    if (SentToContactID != '' && SentToContactID != '0') {
        postUrl = postUrl + '&ContactID=' + SentToContactID;
    }
    //alert(e.sender.element.val());

    $.get(postUrl, function (res) {
        $('#EmailSubject').val(res.EmailSubject);
        var editor = $("#EmailMessage").data("kendoEditor");
        editor.value(res.EmailMessage);
        return false;
    });
}



PassSearchParameter = function () {
    
    var o = {};
    try {
        var frmSearch = $('#frmSearch');
        if (frmSearch != null && frmSearch != undefined) {
            var data = frmSearch.serializeArray();
            $.each(data, function () {
                if (o[this.name] !== undefined) {
                    //if (!o[this.name].push) {
                    //    o[this.name] = [o[this.name]];
                    //}
                    o[this.name] = o[this.name]+','+(this.value || '');
                } else {
                    o[this.name] = this.value || '';
                }
            });
        }
        var hfParantId = $("#hfParentId");
        if (hfParantId != null) {
            var name = $(hfParantId).attr('name');
            o[name] = hfParantId.val();
        }
        var searchString = window.location.search.substring(1);
        if (searchString != null) {
            var params = searchString.split("&");
            for (var i = 0; i < params.length; i++) {
                var val = params[i].split("=");
                o[unescape(val[0])] = unescape(val[1]);
            }
        }
    }
    catch (ex) {
    }
    return o;
};
//For Menu Access
MenuAccessFilter = function () {
    var o = {};
    try {
        o['userRoleId'] = $("#UserRoleID").val();
    }
    catch (ex) {
    }
    return o;
};

//For Rolewise Menu Access
RoleWiseMenuAccessFilter = function () {
    var o = {};
    try {
        o['RoleId'] = $("#RoleID").val();
    }
    catch (ex) {
    }
    return o;
};
//For Contact Menu Access
ContactMenuAccessFilter = function () {
    var o = {};
    try {
        o['contactTypeID'] = $("#ContactTypeID").val();
    }
    catch (ex) {
    }
    return o;
};


function onPhotoForContract(e) {
    $.each(e.files, function (index, value) {
        ext = value.extension.toLowerCase();
        if (ext != '.jpg' && ext != '.png'
            && ext != '.gif' && ext != '.jpeg') {
            alert('Only jpg, png, gif are allowed!');
            e.preventDefault();
        }
    });
}

function onSelectContactImportFile(event) {
    var notAllowed = false;
    $.each(event.files, function (index, value) {
        if (value.extension !== '.xls' && value.extension !== '.xlsx') {
            RB.notifier("Only .xls or .xlsx file types are allowed.", false);
            notAllowed = true;
        }
    });
    if (notAllowed == true)
        event.preventDefault();
}

function getIfDocumentIsImage(name) {
    var result = '';
    if (name != null) {
        var rev_str = name.split('').reverse().join('');
        var rev_ext = rev_str.split('.');
        var ext = rev_ext[0].split('').reverse().join('');
        if (ext == 'jpg' || ext == 'png' || ext == 'jpeg' || ext == 'png') {
            return true;
        }
    }
}


function getDocumentTypeWithExtension(name) {
    var result = '';
    if (name != null) {
        var rev_str = name.split('').reverse().join('');
        var rev_ext = rev_str.split('.');
        var ext = rev_ext[0].split('').reverse().join('');
        if (ext == 'pdf') {
            result += "<i class='fa fa-file-pdf-o color-pdf'></i>";
        }
        if (ext == 'xls') {
            result += "<i class='fa fa-file-excel-o color-excel'></i>";
        }
        if (ext == 'xlsx') {
            result += "<i class='fa fa-file-excel-o color-excel'></i>";
        }
        if (ext == 'doc') {
            result += "<i class='fa fa-file-word-o color-word'></i>";
        }
        if (ext == 'docx') {
            result += "<i class='fa fa-file-word-o color-word'></i>";
        }
        if (ext == 'ppt') {
            result += "<i class='fa fa-file-powerpoint-o color-powerpoint'></i>";
        }
        if (ext == 'pptx') {
            result += "<i class='fa fa-file-powerpoint-o color-powerpoint'></i>";
        }
        if (ext == 'jpg' || ext == 'png' || ext == 'jpeg' || ext == 'png') {
            result += "<i class='fa fa-file-image-o'></i>";
        }
        if (ext == 'txt') {
            result += "<i class='fa  fa-file-text-o'></i>";
        }
        if (ext == 'rar' || ext == 'zip') {
            result += "<i class='fa fa-file-archive-o color-zip'></i>";
        }
    }

    return result;
}


function getDocumentViewLink(name, docid) {
    var rev_str = name.split('').reverse().join('');
    var rev_ext = rev_str.split('.');
    var ext = rev_ext[0].split('').reverse().join('').toLowerCase();
    if (ext == 'jpg' || ext == 'png' || ext == 'jpeg' || ext == 'png') {
        return '<a href="/CRM/ContactDocument/ShowDocument/' + docid + '?documentFileName=' + name + '" class="attachment-img">View</a>';
    } else {
        return "";
    }
}

function UploadFile_OnSuccess(e) {
        var imgTickId = '#img' + e.sender.element[0].name;
        var img = $(imgTickId);
        if (img != null) {
            if (e.operation == "remove") {
                $(img).attr('src', '');
                $(img).hide();
            }
            else {
                var fileName = e.files[0].name;
                if (getIfDocumentIsImage(fileName)) {
                    $(img).attr('src', '/uploadedfiles/temporary/' + fileName);
                    $(img).show();
                }
            }
        }
}
function ResumeUploadFile_OnSuccess(e) {
    
    $("#frmParseEditors").submit();
}

function ApplicantResumeUploadFile_OnSuccess(e) {
    RB.preloader({
        target: '#frmEditors',
        show: true
    });
    $("#frmParseEditors").submit();
}



function ResumeFilesUpload_OnComplete(e) {
    location.href = '/CRM/Contact/BulkParseImport?UploadSuccess=true';
}

function OnResumeFileSelect(e) {
    var files = e.files;
    if (e.files.length > 0) {
        var ext = e.files[0].extension.toLowerCase();
        if (ext != '.pdf' && ext != '.doc' && ext != '.docx') {
            RB.notifier('Please upload pdf or document file',false);
            e.preventDefault();
        }
    }
}

    function onFileSelect(e) {
        $.each(e.files, function (index, value) {
            ext = value.extension.toLowerCase();
            if (ext != '.jpg' && ext != '.png'
                && ext != '.gif' && ext != '.txt'
                && ext != '.doc' && ext != '.docx'
                && ext != '.pdf' && ext != '.jpeg'
                && ext != '.xls' && ext != '.xlsx') {
                alert('Only jpg, png, gif, txt, doc, docx, pdf, excel are allowed!');
                e.preventDefault();
            }
        });
    }

    function onTaskFileSelect(e) {
        $.each(e.files, function (index, value) {
            ext = value.extension.toLowerCase();
            if (ext != '.jpg' && ext != '.png'
                && ext != '.gif' && ext != '.txt'
                && ext != '.doc' && ext != '.docx'
                && ext != '.pdf' && ext != '.jpeg'
                && ext != '.xls' && ext != '.xlsx'
                && ext != '.rar' && ext != '.zip') {
                alert('Only jpg, png, gif, txt, doc, docx, pdf, excel are allowed!');
                e.preventDefault();
            }
        });
    }
    $('a[name="lnkDeleteVisaFile"]').on("click", function (e) {
        e.preventDefault();
        var divid = $(this).data("id");
        var postUrl = $(this).attr('href');
        $.post(postUrl, null,
            function (res) {
                if (res.success) {
                    $("#VisaDocumentID").val("");
                    $("#VisaDocumentFileName").val("");
                    $('.uploaded-file-container').hide();
                    $("#" + divid).show();
                }
                else {
                    
                }
                return false;
            });
        return false;
    });
    function onNoteDataBound(arg) {
        $("#ListViewNote").removeAttr("class");
        $("#ListViewNote").attr("class", "chats");
    }

    $('#btnSubmitNote').unbind("click").on("click", function (e) {
      
        var $form = $('#frmNoteEditor');
        var url = $form.attr('action');
        if (!$form.valid || $form.valid()) {
            $.post(url, $form.serializeArray(),
            function (res) {
                if (res.success) {
                    
                    var lstView = $('#ListViewNote').data("kendoListView");
                    if (lstView != null) {
                        lstView.dataSource.read();
                        lstView.refresh();
                        $('#modal_view').modal('hide');
                    }
                }

                return false;
            });
        }
    });

    var parseReumeTimout;
    $('#btnResumeParse').on("click", function (e) {
        parseReumeTimout = setInterval(ShowResumeParseProgress, 3000);
        $('#divProgress').removeClass("hide");
        var getUrl = "/CRM/Contact/BulkResumeParseAjax";
        var getData = "";
        $('input:checkbox.contactcategory').each(function () {
            var sThisVal = (this.checked ? $(this).val() : "");
            if (sThisVal != ""){
                if (getData == "") {
                    getData += sThisVal;
                } else {
                    getData += "," + sThisVal;
                }
            }
        });

        $.ajax({
            type: "GET",
            url: getUrl,
            data: { contactCategories: getData },
            async:false,
            contentType: 'application/json; charset=utf-8',
            success: function (res) {
                //$('#divProgress').addClass("hide");
                //RB.notifier(res.Data, true);
                ////clearTimeout(parseReumeTimout);
                //ShowResumeParseProgress();
                //$('#lblTotal').html(res.Total);
            },
            complete: function () {
            },
            error: function (req, status, error) {
                //alert('error code-[' + status + "] and error desc-[" + error + "]");
            }
        });
        return false;
    });

    var ShowResumeParseProgress = function () {
        var getUrl = "/CRM/Contact/GetBulkResumeParseStatusAjax";
        var getData = "";
        $.ajax({
            type: "GET",
            url: getUrl,
            data: getData,
            contentType: 'application/json; charset=utf-8',
            success: function (res) {
                if (res.Processed >= res.Total) {
                    $('#divParserCompletedMessage').html("Parsing is completed. Please check the status of individual resume below.");
                    $('#imgProgress').hide();
                    clearInterval(parseReumeTimout);
                    var gridDynamic = $('#GridContact').data("kendoGrid");
                    if (gridDynamic != null) {
                        gridDynamic.dataSource.read();
                        gridDynamic.refresh();
                    }
                }
                $('#lblTotalProcessed').html(res.Processed);
            },
            complete: function () {
               
            },
            error: function (req, status, error) {
               // alert('error code-[' + status + "] and error desc-[" + error + "]");
            }
        });
    };

    $('a[name="lnkEditNote"]').on("click", function (e) {
        var getUrl = $(this).attr('href');
        var getData = "";
        $.ajax({
            type: "GET",
            url: getUrl,
            data: getData,
            contentType: 'application/json; charset=utf-8',
            success: function (res) {
                
                $("#cTitle").html("Add Note");
                $("#ModalViewRandering").html(res);
                setTimeout(function () { $('#modal_view').modal('show'); }, 1000);
            },
            complete: function () {

            },
            error: function (req, status, error) {
                alert('error code-[' + status + "] and error desc-[" + error + "]");
            }
        });
        return false;
    });

    $('body').undelegate('#btnSave', 'click').on('click', '#btnSave', function (e) {
        var itemType = $(this).attr('name').replace('btnSave', '');
        var gridId = '#' + $(this).attr('name').replace('btnSave', 'Grid');
        var listviewId = '#' + $(this).attr('name').replace('btnSave', 'ListView');
        var $form = $(this).parents('form:first');
        var url = $form.attr('action');
        if (!$form.valid || $form.valid()) {
            $.post(url, $form.serializeArray(),
            function (res) {
                if (res.success) {
                    $('.modal').modal('hide');
                    var gridDynamic = $(gridId).data("kendoGrid");
                    if (gridDynamic != null) {
                        gridDynamic.dataSource.read();
                        gridDynamic.refresh();
                    }
                    var listviewDynamic = $(listviewId).data("kendoListView");
                    if (listviewDynamic != null) {
                        listviewDynamic.dataSource.read();
                        listviewDynamic.refresh();
                    }
                    RB.notifier(res.data, true);
                    
                }
                else {
                    RB.notifier(res.data, false);
                }
                return false;
            }).fail(function () {
                //loading.hide();
            });
        }
    });

    function handlerDeleteAjax(self, elm) {
        
        elm.preventDefault();
        var isConfirm = confirm('Would you like to delete?');
        if (isConfirm) {
            var gridID = '#Grid' + $(self).attr('id').replace('lnkDelete', '');
            var listViewID = '#ListView' + $(self).attr('id').replace('lnkDelete', '');
            var postUrl = $(self).attr('href');
            if ($(self).hasClass('bulk-delete')) {
                var itemsClassPostfix = $(self).data('items').toLowerCase();
                var values = [];
                $.each($('input[class="chk-' + itemsClassPostfix + '"]:checked').serializeArray(), function (i, field) {
                    values.push(values[field.name] = field.value);
                });
                $.ajax({
                    type: 'POST',
                    url: postUrl,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ BulkDeleteItems: values }),
                    cache: false,
                    beforeSend: function () {

                    },
                    success: function (res) {
                        if (res.success) {

                            var grid = $(gridID).data("kendoGrid");
                            if (grid != null) {
                                grid.dataSource.read();
                                grid.refresh();
                            }
                            var lstView = $(listViewID).data("kendoListView");
                            if (lstView != null) {
                                lstView.dataSource.read();
                                lstView.refresh();
                            }
                            RB.notifier(res.data, true);
                        }
                        else {
                            RB.notifier(res.data, false);
                        }
                        return false;
                    },
                    error: function (objAjaxRequest, strError) {
                        
                    }
                });
            } else {
                $.post(postUrl, null,
                function (res) {
                    if (res.success) {

                        var grid = $(gridID).data("kendoGrid");
                        if (grid != null) {
                            grid.dataSource.read();
                            grid.refresh();
                        }
                        var lstView = $(listViewID).data("kendoListView");
                        if (lstView != null) {
                            lstView.dataSource.read();
                            lstView.refresh();
                        }
                        RB.notifier(res.data, true);
                    }
                    else {
                        RB.notifier(res.data, false);
                    }
                    return false;
                });
            }
        }
        return false;
    }
    function NewhandlerDeleteAjax(self, elm) {
        elm.preventDefault();
        var isConfirm = confirm('Would you like to delete?');
        if (isConfirm) {
            var gridID = "#"+$(self).data('grid');
            var listViewID = "#" + $(self).data('list');
            var postUrl = $(self).attr('href');
            $.post(postUrl, null,
            function (res) {
                if (res.success) {

                    var grid = $(gridID).data("kendoGrid");
                    if (grid != null) {
                        grid.dataSource.read();
                        grid.refresh();
                    }
                    var lstView = $(listViewID).data("kendoListView");
                    if (lstView != null) {
                        lstView.dataSource.read();
                        lstView.refresh();
                    }
                    RB.notifier(res.data, true);
                }
                else {
                    RB.notifier(res.data, false);
                }
                return false;
            });
        }
        return false;
    }
    
    $(document).on('keydown', '.numeric', function (e) {
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            return;
        }
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });

    $(document).on("click", ".add-to-test", function () {
        var ths = $(this);
        var id = $(this).data("id");
        if (id != "") {
            var url = '/Home/AddFavoriteTest';
            var data = { TestId: id };
            RB.sendAjaxRequest(url, data, true, function (res) {
                if (res.MessageType == "6") {
                    window.location = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + '/Login?ReturnUrl=' + encodeURIComponent(location.pathname);
                }
                else {
                    if (res.State == "1") {
                        ths.removeClass('done');
                    } else {
                        ths.addClass('done');
                    }
                    RB.notifier(res.CurrentMessage, res.MessageType);
                }
            }, true, true);
        }
    });
  