echartOption = {
    title: {
        text: '',
    },
    tooltip: {
        trigger: 'axis'
    },
    legend: {
        data: ['十位出现次数', '个位出现次数']
    },
    toolbox: {
        show: false,
        feature: {
            dataView: { show: true, readOnly: false },
            magicType: { show: true, type: ['line', 'bar'] },
            restore: { show: true },
            saveAsImage: { show: true }
        }
    },
    calculable: true,
    xAxis: [
        {
            type: 'category',
            data: [],
            axisLabel: {
                show: true,
                interval: 'auto',    // {number}
                rotate: 45,
                margin: 8,
                formatter: '数{value}',
                textStyle: {
                    color: '#333333',
                }
            }
        }
    ],
    yAxis: [
        {
            type: 'value',
            min:0,
            //max:10,
            //splitNumber: 1
            minInterval: 1,    // {number}
            axisLabel: {
                show: true,
            
                formatter: '{value} 次',    // Template formatter!
                textStyle: {
                    color: '#333333',
                }
            }
        }
    ],
    series: [
        {
            name: '十位出现次数',
            type: 'bar',
            data: [],
            markPoint: {
                data: [
                    { type: 'max', name: '最大值' },
                    { type: 'min', name: '最小值' }
                ]
            }
        },
        {
            name: '个位出现次数',
            type: 'bar',
            data: [],
            markPoint: {
                data: [
                    { type: 'max', name: '最大值' },
                    { type: 'min', name: '最小值' }
                ]
            }
        }
    ]
};


$(document).ready(function () {
    var subsiteName = $("#subSite").val();
    var urlPrefix = "";
    if (subsiteName != "") {
        urlPrefix = "/" + subsiteName;
    }
    var timestamp = Date.parse(new Date());
    $.getJSON(urlPrefix + "/api/values/today?id=1&v=" + timestamp, function (json) {
        var data = eval('(' + json + ')');
        FillDetail(data);
    });

    var dom = document.getElementById('main');
    var myChart = echarts.init(dom);
    $.getJSON(urlPrefix + "/api/ssc/noappear?id=0&v=" + timestamp, function (json) {
        var data = json;//eval('(' + json + ')');
        for (var i = 0; i < 10; i++) {
            echartOption.xAxis[0].data.push(i);
            echartOption.series[0].data.push(data.shi[i]);
            echartOption.series[1].data.push(data.ge[i]);
        }
        //echartOption.xAxis[0].data.reverse();
        myChart.setOption(echartOption);
    });
});

function FillDetail(data) {
    var len = data.length;
    var tb1 = "<thead><tr><th>期次</th><th>开奖号</th><th>二星投注</th><th>投</th><th>中</th></tr ></thead >";
    var totalbetZhuTimes = 0;
    var totalbetZhongTimes = 0;
    var totalBetTimes = 0;
    for (var i = 0; i < len; i++) {
        var item = data[i];
        var zhong = "";
        var bet = "";
        var tou = "";
        if (item.IsBeted == true) {
            bet = data[i].BetShi + "#" + data[i].BetGe;
            bet = bet.replace(/,/g, "");
            bet = bet.replace(/#/g, ",");
            tou = data[i].BetZhuCount * 2;
            totalbetZhuTimes += data[i].BetZhuCount;
            totalBetTimes++;
            if (item.IsZhong == true) {
                zhong = "195";
                totalbetZhongTimes++;
            }
        }
        tb1 += "<tr class='"+item.IsZhong+"'><td>" + data[i].Issue + "</td><td>" + data[i].OpenCode + "</td><td>" + bet + "</td><td>" + tou + "</td><td>" + zhong + "</td></tr>";
    }
    $("#tb").append(tb1);
    $("#s1").text(totalbetZhuTimes*2);
    $("#s2").text(totalbetZhongTimes*195);
    $("#s3").text(totalBetTimes);
    $("#s4").text(totalbetZhongTimes);
}