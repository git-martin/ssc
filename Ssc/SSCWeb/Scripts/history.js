$(function () {
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        // 获取已激活的标签页的名称
        var activeTab = $(e.target).text();
        // 获取前一个激活的标签页的名称
        var previousTab = $(e.relatedTarget).text();
        $(".active-tab span").html(activeTab);
        $(".previous-tab span").html(previousTab);
    });
    Handlebars.registerHelper('ifCond', function (v1, operator, v2, options) {
        switch (operator) {
            case '==':
                return (v1 == v2) ? options.fn(this) : options.inverse(this);
            case '===':
                return (v1 === v2) ? options.fn(this) : options.inverse(this);
            case '!=':
                return (v1 != v2) ? options.fn(this) : options.inverse(this);
            case '!==':
                return (v1 !== v2) ? options.fn(this) : options.inverse(this);
            case '<':
                return (v1 < v2) ? options.fn(this) : options.inverse(this);
            case '<=':
                return (v1 <= v2) ? options.fn(this) : options.inverse(this);
            case '>':
                return (v1 > v2) ? options.fn(this) : options.inverse(this);
            case '>=':
                return (v1 >= v2) ? options.fn(this) : options.inverse(this);
            case '&&':
                return (v1 && v2) ? options.fn(this) : options.inverse(this);
            case '||':
                return (v1 || v2) ? options.fn(this) : options.inverse(this);
            default:
                return options.inverse(this);
        }
    });
    GetHistoryData();
});



function GetHistoryData() {
    var subsiteName = $("#subSite").val();
    var urlPrefix = "";
    if (subsiteName != "") {
        urlPrefix = "/" + subsiteName;
    }
    var timestamp = Date.parse(new Date());
    $.getJSON(urlPrefix + "/api/sschistory/10?&v=" + timestamp, function (data) {
        var tabTitleTemplate = Handlebars.compile($("#tabTitle_template").html());
        $("#myTab").html(tabTitleTemplate(data));

        var tabContentTemplate = Handlebars.compile($("#tabContent_template").html());
        $("#myTabContent").html(tabContentTemplate(data));
    });
}