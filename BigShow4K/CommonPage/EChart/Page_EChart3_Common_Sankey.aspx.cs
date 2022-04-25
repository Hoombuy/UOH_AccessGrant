using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommonPage.EChart
{
    public partial class Page_EChart3_Common_Sankey : System.Web.UI.Page
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

        protected string DataText
        {
            get
            {
                string RT = " ";
                if (Request["SN"] != null)
                {
                    if (Session[Request["SN"].ToString()] != null)
                    {
                        DataContract.Common.DataObject_ECharts _TheData = (DataContract.Common.DataObject_ECharts)Session[Request["SN"].ToString()];

                        for (int i = 0; i < _TheData.TheNodes.Count; i++)
                        {
                            RT += "{name:'" + _TheData.TheNodes[i].NAME +"-" + _TheData.TheNodes[i].VALUE+  "'  },";
                        }
                        if (RT[RT.Length - 1] == ',')
                        {
                            RT = RT.Remove(RT.Length - 1);
                        }
                    }
                }
                return RT;
            }
        }

        protected string LinkText
        {
            get
            {
                string RT = " ";
                if (Request["SN"] != null)
                {
                    if (Session[Request["SN"].ToString()] != null)
                    {
                        DataContract.Common.DataObject_ECharts _TheData = (DataContract.Common.DataObject_ECharts)Session[Request["SN"].ToString()];
                        for (int i = 0; i < _TheData.TheLinks.Count; i++)
                        {
                            if (_TheData.TheLinks[i].Value != 0)
                            {
                                RT += "{source:'" + _TheData.TheLinks[i].Source + "', target:'" + _TheData.TheLinks[i].Target + "', value:" + _TheData.TheLinks[i].Value + " },";
                            }
                        }
                        if (RT[RT.Length - 1] == ',')
                        {
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
        }
    }
}