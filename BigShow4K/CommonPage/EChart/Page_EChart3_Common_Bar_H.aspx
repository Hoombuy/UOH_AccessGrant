<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page_EChart3_Common_Bar_H.aspx.cs" Inherits="CommonPage.EChart.Page_EChart3_Common_Bar_H" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/js/Echart3/echarts.min.js"></script>
    <script src="/js/Echart3/theme/roma.js"></script>
    <script src="/js/Echart3/theme/vintage.js"></script>
</head>
<body style="overflow-x : hidden;  overflow-y : hidden;">
    <form id="form1" runat="server">
        <div id="main" style="height: 90%; width: 95%; position: fixed;" runat="server">
        </div>
        <script type="text/javascript">
            var myChart = echarts.init(document.getElementById('main'), 'pz');
            var option = {
                tooltip: {
                    trigger: 'axis',
                    axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                        type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                    }
                },
                legend: {
                    data: [<%=DataTextObjectName %>],
                    textStyle: {
                        color: "lightblue"
                    },
                    y: 'top',    //延Y轴居顶
                    x: 'right', //居右显示
                    show: true

                },
                grid: {
                    top: '3%',
                    left: '3%',
                    right: '4%',
                    bottom: '3%',
                    containLabel: true
                },
                xAxis: {
                    type: 'category',
                    axisLine: {
                        show: false,
                        lineStyle: {
                            color: '#fff'
                        }
                    },
                    data: [<%=DataTextName %>]
                },
                yAxis: {
                    type: 'value',
                    axisLine: {
                        show: false,
                        lineStyle: {
                            color: '#fff'
                        }
                    },
                    splitLine: { show: false }

                },
                series: [<%=DataTextValue %>]
            };
            myChart.setOption(option);

        </script>
    </form>
</body>
</html>

