using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommonPage.EChart
{
    public partial class Page_EChart3_Common_PctorialBar : System.Web.UI.Page
    {
        //重复调用数组里面的颜色
        private String s = "itemStyle:{{normal:{{barBorderRadius:[30, 30, 30, 30] ,color: function (params){{var colorList = [['#fbc72a','#f87d11','#f9280d'],['#087afb','#610ff8','#8008fb'],['#a208fb','#d608fb','#f208fb'],['#a3f835','#08fb1f','#2eb020'],['#35bff8','#3588f8','#284af5']];var index=params.dataIndex;if(params.dataIndex >= colorList.length){{index=params.dataIndex%colorList.length;}};return new echarts.graphic.LinearGradient(0, 0, 0, 1,[{{offset: 0, color: colorList[index][0]}},{{offset: 0.5, color: colorList[index][1]}},{{offset: 1, color: colorList[index][2]}}]);}},}},}}";
        private string stack
        {
            get
            {
                if (ViewState["stack"] != null)
                {
                    return ViewState["stack"].ToString();
                }
                return "";
            }
            set
            {
                ViewState["stack"] = value;
            }
        }

        protected string ChartTitle
        {
            get
            {
                if (Request["Title"] != null)
                {
                    return Request["Title"].ToString();
                }
                return "";
            }
        }

        protected string Size
        {
            get
            {
                if (Request["Size"] != null)
                {
                    return Request["Size"].ToString();
                }
                return "120";
            }
        }

        protected string DataTextObjectName
        {
            get
            {
                string RT = " ";
                if (Request["SN"] != null)
                {
                    if (Session[Request["SN"].ToString()] != null)
                    {
                        DataContract.Common.DataObject_ECharts _TheData = (DataContract.Common.DataObject_ECharts)Session[Request["SN"].ToString()];
                        if (_TheData.TheNodes != null)
                        {
                            for (int i = 0; i < _TheData.TheNodes.Count; i++)
                            {
                                RT += "'" + _TheData.TheNodes[i].NAME + "',";
                            }
                            RT = RT.Remove(RT.Length - 1);
                        }
                    }
                }
                return RT;
            }
        }

        protected string DataTextName
        {
            get
            {
                string RT = " ";
                if (Request["SN"] != null)
                {
                    if (Session[Request["SN"].ToString()] != null)
                    {

                        DataContract.Common.DataObject_ECharts _TheData = (DataContract.Common.DataObject_ECharts)Session[Request["SN"].ToString()];
                        if (_TheData.TheDatasList[0] != null)
                        {
                            for (int i = 0; i < _TheData.TheDatasList[0].Count; i++)
                            {

                                RT += "'" + _TheData.TheDatasList[0][i].NAME + "',";
                            }
                            RT = RT.Remove(RT.Length - 1);
                        }



                    }
                }
                return RT;
            }
        }

        protected string DataTextValue
        {
            get
            {
                string RT = " ";
                if (Request["SN"] != null)
                {
                    if (Session[Request["SN"].ToString()] != null)
                    {

                        DataContract.Common.DataObject_ECharts _TheData = (DataContract.Common.DataObject_ECharts)Session[Request["SN"].ToString()];
                        if (_TheData.TheDatasList[0] != null)
                        {
                            for (int i = 0; i < _TheData.TheDatasList[0].Count; i++)
                            {
                                string _D = "";
                                for (int j = 0; j < _TheData.TheDatasList.Count; j++)
                                {
                                    if (_TheData.TheDatasList[j][i].VALUE != "0")
                                    {
                                        _D += _TheData.TheDatasList[j][i].VALUE + ",";
                                    }
                                    else
                                    {
                                        _D += " ,";
                                    }
                                }
                                if (this.stack == "")
                                {
                                    RT += string.Format(" {{name: '{0}',type: 'bar', label: {{ normal: {{    show: false,     position: 'insideRight'  }} }},     data: [{1}],  showBackground:true, " +  s  +  " }} ", _TheData.TheDatasList[0][i].NAME, _D.Remove(_D.Length - 1));
                                }
                                else
                                {
                                    RT += string.Format(" {{name: '{0}',type: 'bar',  stack: '{2}',   label: {{ normal: {{    show: true,     position: 'insideRight'  }} }},  data: [{1}],}},", _TheData.TheDatasList[0][i].NAME, _D.Remove(_D.Length - 1), this.stack);

                                }

                            }
                            RT = RT.Remove(RT.Length - 1);
                        }
                    }
                }

                return RT;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["W"] != null)
            {
                this.main.Style.Remove("width");
                this.main.Style.Add("width", Request["W"].ToString().Trim() + "px");
            }
            if (Request["H"] != null)
            {
                this.main.Style.Remove("height");
                this.main.Style.Add("height", Request["H"].ToString().Trim() + "px");
            }
            if (Request["Size"] != null)
            {
                DataContract.Common.DataObject_ECharts _TheData = (DataContract.Common.DataObject_ECharts)Session[Request["SN"].ToString()];
                //if (_TheData.TheNodes != null)
                //{
                //    this.main.Style.Remove("height");
                //    this.main.Style.Add("height", (_TheData.TheNodes.Count * int.Parse(Request["Size"].ToString().Trim()) + 200) + "px");
                //}
            }
            if (Request["stack"] != null)
            {
                this.stack = Request["stack"];
            }

        }
   
    }
}