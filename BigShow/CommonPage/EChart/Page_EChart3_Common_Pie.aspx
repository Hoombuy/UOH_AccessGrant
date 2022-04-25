<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page_EChart3_Common_Pie.aspx.cs" Inherits="CommonPage.EChart.Page_EChart3_Common_Pie" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/js/Echart3/echarts.min.js"></script>
    <script src="/js/Echart3/theme/roma.js"></script>
    <script src="/js/Echart3/theme/vintage.js"></script>
    <script src="/js/Echart3/theme/pz.js"></script>
    <script src="/js/Echart3/theme/shineA.js"></script>
</head>
<body style="overflow-x : hidden;  overflow-y : hidden;">
    <form id="form1" runat="server">
        <div id="main" style="height: 90%; width: 90%; position: fixed;" runat="server">
      
        </div>

        <script type="text/javascript">
            var myChart = echarts.init(document.getElementById('main'), 'shineA');
            var option = {
                title: {
                    text: ' <%=ChartTitle %>',
                    x: 'center'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b} : {c} ({d}%)"
                },

                legend: {
                    top: '3%',
                    left: '5%',
                    textStyle: {
                        color: '#f4f8f9'

                    },

                    show: false
                },

                toolbox: {
                    show: true,
                    feature: {
                        mark: { show: true },
                        //dataView: { show: true, readOnly: false },
                        magicType: {
                            show: true,
                            type: ['pie', 'funnel']
                        },
                        //restore: { show: true },
                        //saveAsImage: { show: true }
                    }
                },
                calculable: false,
                series: [
                    {
                        name: ' ',
                        type: 'pie',
                        selectedMode: 'single',
                      <%=Style %>
                       <%=ifshowlable %>
                        // for funnel



                        funnelAlign: 'right',

                        itemStyle: {
                            borderRadius: 5,
                            borderColor: '#f4f8f9',
                            borderWidth: 1,


                            //normal: {
                            //    label: {
                            //        position: 'inner',
                            //        formatter: "{c}"
                            //        //formatter: function (params) {
                            //        //    return (params.percent - 0).toFixed(0) + '%'
                            //        //}
                            //    },
                            //},
                            //emphasis: {
                            //    label: {
                            //        show: true,
                            //        formatter: "{b}\n{d}%"
                            //        fontSize: '20',
                            //        formatter: "{c}",
                            //        fontWeight: 'bold'
                            //    }
                            //}
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