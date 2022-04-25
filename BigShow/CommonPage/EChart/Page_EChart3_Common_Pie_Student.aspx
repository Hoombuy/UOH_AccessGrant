<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page_EChart3_Common_Pie_Student.aspx.cs" Inherits="CommonPage.EChart.Page_EChart3_Common_Pie_Student" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/js/Echart3/echarts.min.js"></script>
    <script src="/js/Echart3/theme/roma.js"></script>
    <script src="/js/Echart3/theme/vintage.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="col-sm-12" style="text-align: center;">
            <div id="Div1" class="widget" runat="server" style="margin-left: auto; margin-right: auto;">
                <div class="widget-content">
                    <div id="mainpie" style="height: 100px; width: 100px;" runat="server">
                    </div>
                </div>
            </div>
        </div>
                <script type="text/javascript">
            var myChart = echarts.init(document.getElementById('mainpie'));
            var option = {
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b}: {c} ({d}%)"
                },
                legend: {
                    orient: 'vertical',
                    x: 'left',
                    data: ['在校学生', '已毕业学生']
                },
                series: [
                    {
                        name: '学生人数',
                        type: 'pie',
                        radius: ['40%', '55%'],
                        label: {
                            normal: {
                                formatter: '{a|{a}}{abg|}\n{hr|}\n  {b|{b}：}{c}  {per|{d}%}  ',
                                backgroundColor: '#eee',
                                borderColor: '#aaa',
                                borderWidth: 1,
                                borderRadius: 4,
                                // shadowBlur:3,
                                // shadowOffsetX: 2,
                                // shadowOffsetY: 2,
                                // shadowColor: '#999',
                                // padding: [0, 7],
                                rich: {
                                    a: {
                                        color: '#999',
                                        lineHeight: 22,
                                        align: 'center'
                                    },
                                    // abg: {
                                    //     backgroundColor: '#333',
                                    //     width: '100%',
                                    //     align: 'right',
                                    //     height: 22,
                                    //     borderRadius: [4, 4, 0, 0]
                                    // },
                                    hr: {
                                        borderColor: '#aaa',
                                        width: '100%',
                                        borderWidth: 0.5,
                                        height: 0
                                    },
                                    b: {
                                        fontSize: 16,
                                        lineHeight: 33
                                    },
                                    per: {
                                        color: '#eee',
                                        backgroundColor: '#334455',
                                        padding: [2, 4],
                                        borderRadius: 2
                                    }
                                }
                            }
                        },
                        data: [<%=DataValue%>]
                    }
                ]
            };
            myChart.setOption(option);
                </script>

           </form>
        </body>
</html>

