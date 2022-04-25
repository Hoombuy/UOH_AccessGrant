<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page_EChart3_Common_Sun.aspx.cs" Inherits="CommonPage.EChart.Page_EChart3_Common_Sun" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--  <script src="/js/Echart3/echarts.min.js"></script>--%>
    <script src="../../js/Echart3/echarts.min.js"></script>
    <script src="../../ProgramCourse/js/Echart3/theme/roma.js"></script>
    <script src="../../ProgramCourse/js/Echart3/theme/vintage.js"></script>
    <style type="text/css">
        html, body {
            height: 95%;
        }
    </style>
</head>
<body>

    <div id="main" style="width: 100%; text-align: center; height: 100%;" runat="server">
    </div>

    <script type="text/javascript">
        var myChart = echarts.init(document.getElementById('main'));
        var option = {
            tooltip: {
                trigger: 'item',
                triggerOn: 'mousemove'
            },
            series: {
                type: 'sunburst',
                data: [<%=DataText %>],
                radius: [0, '90%'],
                left: 'center',
                top: 20
            }
        };



        myChart.setOption(option);
        window.onresize = function () {

            myChart.resize();

        }
    </script>

</body>
</html>
