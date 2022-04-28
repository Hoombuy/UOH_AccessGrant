<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page_EChart3_Common_Line.aspx.cs" Inherits="CommonPage.EChart.Page_EChart3_Common_Line" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/js/Echart3/echarts.min.js"></script>
    <script src="/js/Echart3/theme/roma.js"></script>
    <script src="/js/Echart3/theme/vintage.js"></script>
    <script src="/js/Echart3/theme/pz.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="main" style="height: 85%; width: 95%; position: fixed;" runat="server">
        </div>

        <script type="text/javascript">
            var myChart = echarts.init(document.getElementById('main'), 'pz');
            var option = {
                tooltip: {
                    trigger: 'axis',
                    axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                        type: 'line'       // 默认为直线，可选为：'line' | 'shadow'

                    }
                },


                legend: {
                    textStyle: {
                        color: "#f4f8f9"
                    },
                    orient: 'vertical',  //垂直显示
                    y: 'top',    //延Y轴居中
                    x: 'right', //居右显示

                    data: [<%=DataTextObjectName %>]



               },
               //toolbox: {
               //    feature: {
               //        saveAsImage: {}
               //    }
               //},
               grid: {
                   top: '5%',
                   left: '3%',
                   right: '4%',
                   bottom: '3%',
                   containLabel: true
               },
               xAxis: {
                   type: 'category',
                
                   boundaryGap: false,
                   axisLine: {
                       lineStyle: {
                           color: "#f4f8f9",
                       }
                   },
                   splitLine: { show: false },
                   data: [<%=DataTextName  %>],
                   axisLabel: { fontSize: 30, margin:20 }
                },
               yAxis: {
                   type: 'value',
               
                   splitLine: { show: false },
                   axisLine: {
                       lineStyle: {
                           color: "#f4f8f9",
                       }
                   },

                   axisLabel: { fontSize: 30 }


               },
               series: [<%=DataTextValue %>]
           };
            myChart.setOption(option);

        </script>
    </form>
</body>
</html>

