<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page_EChart3_Common_CalendarHeatmap.aspx.cs" Inherits="CommonPage.EChart.Page_EChart3_Common_CalendarHeatmap" %>

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
            var myChart = echarts.init(document.getElementById('main') );


            option = {
                title: {
                    text: ' <%=ChartTitle %>',
                    x: 'center'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: "  {c} 项工作完成  ",
                },
                 visualMap: {
                    min: 0,                    
                    max:  <%=DataMax %>,
                    //type: 'piecewise',
                    orient: 'horizontal',
                    left: 'center',
                    bottom: 20,
                    textStyle: {
                        color: '#000'
                    }
                },
            
                calendar: {
                    left: 'center',
              
                    cellSize: [30, 30],
                    yearLabel: {show: false},
               orient: 'vertical',
                    dayLabel: {
                        firstDay: 1,
                        nameMap: 'cn'
                    },
                    monthLabel: {
                        nameMap:'cn'
                    },
                    range: <%=DataRange %>
                //range: '2020-01'
            },
                series:[ {
                    type: 'scatter',
                    coordinateSystem: 'calendar',
                    label: {
                        show: true,
                   
                        color: '#000'
                    },
                    data: [<%=DataText %>]
                },
                    {
                        type: 'heatmap',
                        coordinateSystem: 'calendar',
                        data: [<%=DataText %>]
                        //data: [['2020-01-01', 600], ['2020-01-02', 800]]
                    }
                ]
            };


                myChart.setOption(option);

        </script>
    </form>
</body>
</html>
