<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page_EChart3_Common_Pie_Big.aspx.cs" Inherits="CommonPage.EChart.Page_EChart3_Common_Pie_Big" %>

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
                    <div id="main" style="height: 320px; width: 320px;" runat="server">
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            var myChart = echarts.init(document.getElementById('main'), 'infographic');

   
           option = {
                title: {
                    text: ' <%=ChartTitle %>',
                    x: 'center'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b}: {c} ({d}%)"
                },
                legend: {
                    orient: 'horizontal',
                    x: 'top',
                    data: [<%=LegendText %>]
                },
               toolbox: {
                   feature: {
                       saveAsImage: {}
                   }
               },
                series: [

                    {
                        name: '分项统计内容',
                        type: 'pie',
                        radius: ['40%', '55%'],
                        label: {
                            normal: {
                                formatter: ' {b|{b}：}{c}  {per|{d}%}  ',
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
                                        fontSize: 12,
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
                        data: [<%=DataText %>]
                    }
                ]
            };

 

            myChart.setOption(option);

        </script>
    </form>
</body>
</html>
