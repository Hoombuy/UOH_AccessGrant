<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page_EChart3_MZ.aspx.cs" Inherits="CommonPage.EChart.Page_EChart3_MZ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/js/Echart3/echarts.min.js"></script>
    <script src="/js/Echart3/theme/cly.js"></script>
    <script src="/js/jquery.js"></script>
</head>
<body>
    <form id="form1" runat="server">


        <div id="main" style="height: 90%; width: 540px; position: fixed;" runat="server">
        </div>

        <script type="text/javascript">
            var myChart = echarts.init(document.getElementById('main'), 'cly');
            //   myChart.showLoading();
 

         
            $.get('/CommonPage/EChart/mz_vip.json', function (result) {

 
                //        myChart.hideLoading();

                echarts.registerMap('ge', result);
                option = {

                    tooltip: {
                        trigger: 'item',
                        showDelay: 0,
                        transitionDuration: 0.2,
                        formatter: function (params) {
                            var value = (params.value + '').split('.');
                            value = value[0].replace(/(\d{1,3})(?=(?:\d{3})+(?!\d))/g, '$1,');
                            return params.seriesName + '<br/>' + params.name + ': ' + value;
                        }
                    },
 
                 visualMap: {   
                        min: 0,
                        max:  <%=DataMax %>,
                        //inRange: {
                        //    color: ['#313695', '#4575b4', '#74add1', '#abd9e9', '#e0f3f8', '#ffffbf', '#fee090', '#fdae61', '#f46d43', '#d73027', '#a50026']
                        //},
                        type: 'piecewise',
                        orient: 'vertical',
                        x:'right',      //可设定图例在左、右、居中
                        y:'bottom', //可设定图例在上、下、居中
                        showDelay:false,
                        textStyle: {
                            color: '#fff'
                        }
                    },
                    toolbox: {
                        show: false,
                        orient: 'vertical',
                        left: 'left',
                        top: 'top'
                 
                    },
                    series: [
                        {
                            name:'项目建设情况',
                            type: 'map',
                            map:'ge',
                     
                            data:[<%=DataText %>]
                        }
                    ]
                };

                myChart.setOption(option);
            });







        </script>
    </form>
</body>
</html>
