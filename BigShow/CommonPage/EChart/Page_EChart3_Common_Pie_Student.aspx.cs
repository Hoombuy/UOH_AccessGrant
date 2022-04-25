using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommonPage.EChart
{
    public partial class Page_EChart3_Common_Pie_Student : System.Web.UI.Page
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



        protected string DataValue
        {
            get
            {
                string RT = " ";
                if (Request["SN"] != null)
                {
                    if (Session[Request["SN"]] != null)
                    {
                        DataContract.Common.DataObject_ECharts _TheData = (DataContract.Common.DataObject_ECharts)Session[Request["SN"]];
                        if (_TheData.TheDatasList[0] != null)
                        {
                            for (int i = 0; i < _TheData.TheDatasList[0].Count; i++)
                            {
                                if (_TheData.TheDatasList[0][i].VALUE != "0")
                                {
                                    RT += "{value:" + _TheData.TheDatasList[0][i].VALUE + ", name:'" + _TheData.TheDatasList[0][i].NAME + "'},";
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
                this.mainpie.Style.Remove("width");
                this.mainpie.Style.Add("width", Request["W"].ToString().Trim() + "px");
            }
            if (Request["H"] != null)
            {
                this.mainpie.Style.Remove("height");
                this.mainpie.Style.Add("height", Request["H"].ToString().Trim() + "px");
            }
        }
    }
}