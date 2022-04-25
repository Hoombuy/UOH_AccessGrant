using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommonPage.EChart
{
    public partial class Page_EChart3_Common_Bar_H_3D : System.Web.UI.Page
    {

        //private string _cs = "   itemStyle: { normal: { barBorderRadius: [30, 30, 30, 30] , color:function(params) { let colorList = [ new echarts.graphic.LinearGradient( 0, 0, 0, 1,[{ offset: 0, color: '#fbf208' }, {offset: 0.5, color: '#f5d815'} , {offset: 1, color: '#f53515'}] ) ] return colorList[params.dataIndex]; }}}"; 
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
       
                //return RT;
                string RT = " ";
                if (Request["SN"] != null)
                {
                    if (Session[Request["SN"].ToString()] != null)
                    {
                        DataContract.Common.DataObject_ECharts _TheData = (DataContract.Common.DataObject_ECharts)Session[Request["SN"].ToString()];
                        if (_TheData.TheDatasList[0] != null)
                        {
                            for (int i = 0; i < _TheData.TheNodes.Count; i++)  // 这里取的不是 TheDatasList 而是 TheNodes 是为了取 TheNodes 中的对象Name
                            {
                                string _D = "";
                                for (int j = 0; j < _TheData.TheDatasList[i].Count; j++)
                                {
                                    _D += _TheData.TheDatasList[i][j].VALUE + ",";
                                }
                            
                              
                           
                                if (this.stack == "true")
                                {
                                          RT += string.Format(" {{name: '{0}',type: 'bar3D',  stack: '{2}',   label: {{ normal: {{    show: true,     position: 'insideRight'  }} }},  data: [{1}]}},", _TheData.TheNodes[i].NAME, _D.Remove(_D.Length - 1), this.stack);


                                }
                                else
                                {
                                    RT += string.Format(" {{name: '{0}',type: 'bar3D',   label: {{ normal: {{    show: true,     position: 'insideRight'  }} }},  data: [{1}] }}, ", _TheData.TheNodes[i].NAME, _D.Remove(_D.Length - 1));

                          
                                } 

                            }
                        }
                        RT = RT.Remove(RT.Length - 1);
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