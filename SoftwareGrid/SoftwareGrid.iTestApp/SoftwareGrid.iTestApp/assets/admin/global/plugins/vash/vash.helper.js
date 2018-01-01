// Shak Imran
// May 03, 2016

// Use: @html.FunctionName(model.data);


vash.helpers.GetDate = function (jsonDate) {
    if (jsonDate) {
        var d = new Date(parseInt((jsonDate).slice(6, -2)));
        var newDate = d.getDate() + '/' + (1 + d.getMonth()) + '/' + d.getFullYear();

        return newDate;
    }
    return '';
}

vash.helpers.GetDateTime = function (jsonDate) {
    if (jsonDate) {
        var d = new Date(parseInt((jsonDate).slice(6, -2)));

        var hours = d.getHours() > 12 ? d.getHours() - 12 : d.getHours();
        var am_pm = d.getHours() >= 12 ? "PM" : "AM";
        hours = hours < 10 ? "0" + hours : hours;
        var minutes = d.getMinutes() < 10 ? "0" + d.getMinutes() : d.getMinutes();
        var newDateTime = d.getDate() + '/' + (1 + d.getMonth()) + '/' + d.getFullYear() + ' ' + hours + ':' + minutes + ' ' + am_pm;

        return newDateTime;
    }
    return '';
}

vash.helpers.UserCategoryColorClassName = function (userCategoryId) {

    var className = '';

    switch (userCategoryId) {
        case 1:
            className = "icon-agent";
            break;
        case 2:
            className = "icon-brokerage ";
            break;
        case 3:
            className = "icon-owner";
            break;
        case 4:
            className = "icon-developer";
            break;
        case 6:
            className = "icon-owner";
            break;
    }

    return className;

}

vash.helpers.UserCategoryName = function (userCategoryId) {

    var agentOrAgencyType = '';

    switch (userCategoryId) {
        case 1:
            agentOrAgencyType = 'Agent';
            break;
        case 2:
            agentOrAgencyType = 'Real Estate Brokerage';
            break;
        case 3:
            agentOrAgencyType = 'Buyer';
            break;
        case 4:
            agentOrAgencyType = 'Builder/Developer';
            break;
        case 6:
            agentOrAgencyType = 'Seller/Owner';
            break;
    }

    return agentOrAgencyType;
}


vash.helpers.MultiConcatenation = function () {  
    var strings = [];
    for (var i = 0; i < arguments.length; i++) {
        if (RB.isNotNullOrEmpty(arguments[i])) {
            strings.push(arguments[i]);
        }
    }    
    return strings.join(', ');
}

vash.helpers.ShowTag = function (data) {
    var value = '';
    if (data) {
        var dataList = [];
        dataList = data.split(',');
        for (var i = 0; i < dataList.length; i++) {
            this.buffer.push('<span class="tag label label-info">' + dataList[i] + '</span> ');
        }
        return value;
    }
    return '';
}


vash.helpers.ShowAnswerOption = function (data,isMultiple) {
    var value = '';
    if (data) {
        for (var i = 0; i < data.length; i++) {
            if (isMultiple == true) {
                this.buffer.push('<label><input type="checkbox" name="radio1" class="icheck answer_option" data-multipleanswer="' + isMultiple + '" data-questionid="' + data[i].QuestionId + '" data-answeroptionid="' + data[i].AnswerOptionId + '" > ' + data[i].AnswerOptionText + '</label>');
            } else {
                this.buffer.push('<label><input type="radio" name="radio1" class="icheck answer_option" data-multipleanswer="' + isMultiple + '" data-questionid="' + data[i].QuestionId + '" data-answeroptionid="' + data[i].AnswerOptionId + '" > ' + data[i].AnswerOptionText + '</label>');
            }
        }
        return value;
    }
    return '';
}