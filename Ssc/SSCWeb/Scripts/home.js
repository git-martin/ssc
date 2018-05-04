currentOption = {
    title: {
        text: '',
    },
    tooltip: {
        trigger: 'axis',
        axisPointer: {
            type: 'shadow'
        }
    },
    legend: {
        data: ['异常次数Top20']
    },
    grid: {
        left: '3%',
        right: '4%',
        bottom: '3%',
        containLabel: true
    },
    xAxis: {
        type: 'value',
        boundaryGap: [0, 0.01]
    },
    yAxis: {
        type: 'category',
        data: [],
        axisLabel: {                 //坐标轴刻度标签的相关设置
            show: true,              //是否显示
            interval: 0,        //坐标轴刻度标签的显示间隔，在类目轴中有效。默认会采用标签不重叠的策略间隔显示标签。可以设置成 0 强制显示所有标签。如果设置为 1，表示『隔一个标签显示一个标签』，如果值为 2，表示隔两个标签显示一个标签，以此类推
            inside: false,
        }
    },
    series: [
        {
            name: '异常次数Top20',
            type: 'bar',
            data: [],
            //顶部数字展示pzr
            itemStyle: {
                normal: {
                    label: {
                        show: true,//是否展示  
                        textStyle: {
                            fontWeight: 'bolder',
                            fontSize: '12',
                            fontFamily: '微软雅黑',
                        }
                    }
                }
            },
        },
    ]
};


$(document).ready(function () {
    var dom = document.getElementById('main');
    var myChart = echarts.init(dom, 'light');
    var timestamp = Date.parse(new Date());

    $.getJSON("/Scripts/today.exceptionlog.json?v=" + timestamp, function (json) {
        var queryTime = json.LastQueryTime;
        var logData = json.Data;
        var len = logData.length;
        var total = 20;
        if (len < 20)
            total = len
        var index = 0;
        while (total >= 0) {
            var severity = logData[index].Severity
            if (severity > 3) {
                index++;
                continue;
            }
            var count = logData[index].Count;
            var action = logData[index].Action;
            //currentOption.xAxis.data[i] = action;
            currentOption.yAxis.data.push(action);
            currentOption.series[0].data.push(count);
            index++;
            total--;
        }
        //for (var i = 0; i < count; i++) {
        //    var index = i;
        //    var action = logData[index].Action;
        //    var count = logData[index].Count;
        //    var severity = logData[index].Severity
        //    currentOption.yAxis.data.push(action);
        //    currentOption.series[0].data.push(count);
        //}
        currentOption.yAxis.data.reverse();
        currentOption.series[0].data.reverse();
        currentOption.title.text = "最后更新时间：" + queryTime;
        //dom.style.height = len * 50 + "px";
        // 使用刚指定的配置项和数据显示图表。
        myChart.setOption(currentOption);
        FillDetail(logData);
    });

    myChart.on('click', function (params) {
        if (params.seriesType === 'bar') {
            // 点击到了 markPoint 上
            var action = params.name;
            GetLogDetails(action,0);
        }
    });
});

function GetLogDetails(action,minLv) {
    var fl = $('#ckFlag').is(':checked');
    if (!fl)
        return;
    $('#myModalLabel').text(action);
    $("#myModalBody").html("<div>loading data....</div>");
    $('#myModal').modal('toggle');
    var url = "/api/log/getdetails?action=" + action + "&n=5&lv=" + minLv;
    $.get(url, function (data) {
        var lis = "";
        var json = eval('(' + data + ')');
        if (json.result.length > 0) {
            for (var i = 0; i < json.result.length; i++) {
                var item = json.result[i];
                lis += "<div class='log_item'><dl><dt>Contract: " + item.Contract + "</dt><dd></dd><dt>ExMsg: " + item.ExMsg + "</dt><dd></dd><dt>ExDetail - " + item.ExTime + "</dt><dd>" + item.ExDetail + "</dd></dl></div>";
            }
            $("#myModalBody").html(lis);
        } else {
            $("#myModalBody").html("<div>no data....lol</div>");
        }
        $('#myModal').modal('show');
    });
}

function showLog(obj) {
    var action = $(obj).next().text();
    var lv = $(obj).next().next().text();
    GetLogDetails(action,lv);
}

function FillDetail(data) {
    var len = data.length;
    var tb1 = "<thead><tr><th>#</th><th>Action || ExMsg 字段</th><th>级别</th><th>错误次数</th></tr ></thead >";
    for (var i = 0; i < len; i++) {
        var lv = data[i].Severity;
        tb1 += "<tr class='lv"+lv+"'><td onclick=showLog(this)>" + (i + 1) + "</td><td>" + data[i].Action + "</td><td>" + data[i].Severity + "</td><td>" + data[i].Count + "</td></tr>";
    }
    $("#tb").append(tb1);
}

function ObjectId(uid) {
    return uid;
}
function ISODate(date) {
    return date;
}

