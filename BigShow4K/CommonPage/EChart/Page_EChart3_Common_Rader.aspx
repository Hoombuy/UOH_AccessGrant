<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page_EChart3_Common_Rader.aspx.cs" Inherits="CommonPage.EChart.Page_EChart3_Common_Rader" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/js/Echart3/echarts.min.js"></script>
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
            option = {
                title: {
                    text: '<%=ChartTitle%>'
                },
                tooltip: {},
                <%--         legend: {
                    data: [<%=DataTextObjectName %>]
                },--%>
                color: ['#FFE434'],
                radar: {
                    shape: 'circle',
                    splitArea: {
                        areaStyle: {
                            color: ['#0e1734'],

                            shadowBlur: 10
                        }
                    },
                    axisLine: {
                        lineStyle: {
                            color: '#0e1734'
                        }
                    },
                    splitLine: {
                        lineStyle: {
                            color: '#5db2db'
                        }
                    },
                    name: {
                        formatter: '【{value}】',
                        textStyle: {
                            color: '#5db2db',
                            borderRadius: 3,
                            padding: [3, 5]
                        }
                    },
                    indicator: [<%=DataTextName %>]
                },
                series: [{
                    type: 'radar',
                    // areaStyle: {normal: {}},
                    data: [<%=DataTextValue %>]
            }]
            };

        myChart.setOption(option);

        </script>
    </form>
</body>
</html>
