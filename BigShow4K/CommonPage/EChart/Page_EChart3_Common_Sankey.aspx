<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page_EChart3_Common_Sankey.aspx.cs" Inherits="CommonPage.EChart.Page_EChart3_Common_Sankey" %>

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
        <div class="col-sm-12" style="text-align: center;">
            <div id="Div1" class="widget" runat="server" style="margin-left: auto; margin-right: auto;">
                <div class="widget-content">
                    <div id="main" style="height: 100%; width: 100%; background-color: transparent;" runat="server">
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            var myChart = echarts.init(document.getElementById('main'), 'cly');
            var option = {

                tooltip: {
                    trigger: 'item',
                    triggerOn: 'mousemove'
                },
                series: {
                    type: 'sankey',
                    layout: 'none',
                    focusNodeAdjacency: 'allEdges',
                    data: [<%=DataText %>],
                    links: [<%=LinkText %>]



                }

            };

            myChart.setOption(option);

        </script>
    </form>
</body>
</html>
