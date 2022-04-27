<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page_EChart3_Common_SunDrink.aspx.cs" Inherits="CommonPage.EChart.Page_EChart3_Common_SunDrink" %>

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

    <div id="main" style="width: 90%; height: 90%; text-align: center;" runat="server">
    </div>

    <script type="text/javascript">
        var myChart = echarts.init(document.getElementById('main'));
        var option = {
            tooltip: {
                trigger: 'item',
                triggerOn: 'mousemove'
            },
            title: {
                text: 'WORLD COFFEE RESEARCH SENSORY LEXICON',
                subtext: 'Source: https://worldcoffeeresearch.org/work/sensory-lexicon/',
                textStyle: {
                    fontSize: 9,
                    align: 'center'
                },
                subtextStyle: {
                    align: 'center'
                },
                sublink: 'https://worldcoffeeresearch.org/work/sensory-lexicon/'
            },
            series: {
                type: 'sunburst',
                highlightPolicy: 'ancestor',
                data: [<%=DataText %>],
                radius: [0, '95%'],
                sort: null,
                levels: [{}, {
                    r0: '15%',
                    r: '35%',
                    itemStyle: {
                        borderWidth: 2
                    },
                    label: {
                        rotate: 'tangential'
                    }
                }, {
                    r0: '35%',
                    r: '70%',
                    label: {
                        align: 'right'
                    }
                }, {
                    r0: '70%',
                    r: '72%',
                    label: {
                        position: 'outside',
                        padding: 3,
                        silent: false
                    },
                    itemStyle: {
                        borderWidth: 3
                    }
                }]
            }
        };



        myChart.setOption(option);
        window.onresize = function () {

            myChart.resize();

        }
    </script>

</body>
</html>
