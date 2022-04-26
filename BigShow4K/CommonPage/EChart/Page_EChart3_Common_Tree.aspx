<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page_EChart3_Common_Tree.aspx.cs" Inherits="CommonPage.EChart.Page_EChart3_Common_Tree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/js/Echart3/echarts.js"></script>
    <script src="/js/Echart3/theme/roma.js"></script>
    <script src="/js/Echart3/theme/vintage.js"></script>
    <script src="/js/Echart3/theme/cly.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="main" style="height: 90%; width: 90%; position: fixed;" runat="server">
        </div>
        <script type="text/javascript">
            var myChart = echarts.init(document.getElementById('main'), 'cly');
            var option = {

                tooltip: {
                    trigger: 'item',
                    triggerOn: 'mousemove'
                },

                series: [
                         {
                             type: 'tree',
                             data: [<%=DataText %>],

                             //data: [{
                             //    name: '1212', children: [
                             //        { name: '形势与政策（3）', value: 1 }, { name: '大学生生涯规划', value: 1 }
                             //    ]
                             //}],
                             top: '0%',
                             bottom: '5%',
                             right: '25%',

                             //layout: 'radial',
                             symbol: 'emptyCircle',
                         
                             symbolSize: 12,
                             initialTreeDepth: 3,
                             animationDurationUpdate: 750,

                             label: {
                                 position: 'left',
                                 verticalAlign: 'middle',
                                 align: 'right',
                                 color: "#ffffff",
                                 fontSize: 11
                             },

                             leaves: {
                                 label: {
                                     position: 'right',
                                     verticalAlign: 'middle',
                                     align: 'left'
                                 }
                             },

                         }
                ]
            };

                     myChart.setOption(option);

        </script>
    </form>
</body>
</html>
