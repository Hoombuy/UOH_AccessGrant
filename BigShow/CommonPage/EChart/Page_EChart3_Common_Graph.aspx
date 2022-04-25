<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page_EChart3_Common_Graph.aspx.cs" Inherits="CommonPage.EChart.Page_EChart3_Common_Graph" %>

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
            var myChart = echarts.init(document.getElementById('main'));
            var option = {

                title: {
                    text: 'Les Miserables',
                    subtext: 'Circular layout',
                    top: 'bottom',
                    left: 'right'
                },
                tooltip: {},
                legend: {
                    data: [ <%=TypeText %>],
                    left: 'right',
                    top:'bottom',
                    textStyle: {
                        fontSize: 26,
                        color: '#ffffff'
                    }
                },
                animationDurationUpdate: 1500,
                animationEasingUpdate: 'quinticInOut',
                series: {
                    name: 'Les Miserables',
                    type: 'graph',
                    layout: 'circular',
                    circular: {
                        rotateLabel: true
                    },
                    data: [<%=DataText %>],
                    links: [<%=LinkText %>],
                    categories: [<%=TypeText %>],
                    roam: true,
                    label: {
                        position: 'right',
                        fontSize: 26,
                        textBorderType: 'solid',
                        color: '#ffffff'
                    },
                    lineStyle: {
                        color: 'source',
                        curveness: 0.5
                    },
                    emphasis: {
                        focus: 'adjacency',
                        lineStyle: {
                            width: 10
                        }
                    }
                }




            };
            myChart.setOption(option);

        </script>
    </form>
</body>
</html>
