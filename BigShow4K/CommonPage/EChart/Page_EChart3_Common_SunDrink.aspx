<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page_EChart3_Common_SunDrink.aspx.cs" Inherits="CommonPage.EChart.Page_EChart3_Common_SunDrink" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--  <script src="/js/Echart3/echarts.min.js"></script>--%>
    <script src="../../js/Echart3/echarts.min.js"></script>
    <script src="../../ProgramCourse/js/Echart3/theme/roma.js"></script>
    <script src="../../ProgramCourse/js/Echart3/theme/vintage.js"></script>
    <script src="../../ProgramCourse/js/Echart3/theme/cly.js"></script>
    <style type="text/css">
        html, body {
            height: 98%;
        }
    </style>
</head>
<body>

    <div id="main" style="width: 100%; height: 100%; text-align: center;" runat="server">
    </div>

    <script type="text/javascript">
        var myChart = echarts.init(document.getElementById('main'), 'cly');
        var option = {
            tooltip: {
                trigger: 'item',
                triggerOn: 'mousemove',
              
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
                sort: undefined,
                center: ["50%", "50%"],
                levels: [{}, {
                    r0: '15%',
                    r: '35%',
                    itemStyle: {
                        borderWidth: 2
                    },
                    label: {
                        color: "rgba(247, 247, 247, 1)",
                        fontSize:50,
                        rotate: 'tangential',
                        

                    }
                }, {
                    r0: '35%',
                    r: '68%',
                    label: {
                        color: "rgba(247, 247, 247, 1)",
                        fontSize:20,
                        align: 'right'
                    }
                }, {
                    r0: '68%',
                    r: '70%',

                    label: {
                        
                        position: 'outside',
                        padding: 2,
                        silent: false,
                        color: "rgba(247, 247, 247, 1)",
                        fontSize: 15,
                        width: 150,
                        height: 300,
                        overflow: "break"
                        
                        
                        
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
