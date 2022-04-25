using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommonPage.EChart
{
    public partial class Page_EChart3_MainGate_IOInfo : System.Web.UI.Page
    {


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
                                RT += " {name: '" + _TheData.TheDatasList[0][i].NAME + "',type: 'bar',  stack: '总量',  label: { normal: {    show: true,     position: 'insideRight'  } },  " +
                               " data: [" + _D.Remove(_D.Length - 1) + "] " +
                                 "   },";
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
                if (_TheData.TheNodes != null)
                {
                    this.main.Style.Remove("height");
                    this.main.Style.Add("height", (_TheData.TheNodes.Count * int.Parse(Request["Size"].ToString().Trim()) + 200) + "px");
                }
            }
        }
    }
}