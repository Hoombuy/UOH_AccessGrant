using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommonPage.EChart
{
    public partial class Page_EChart3_Common_SunDrink : System.Web.UI.Page
    {
        int BB = -1;
        int BG = 1;
        int BR = 1;

        int B = 120;
        int G = 20;
        int R = 10;


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

                        if (_TheData.TheChildren != null && _TheData.TheChildren.Count > 0)
                        {
                            for (int i = 0; i < _TheData.TheChildren.Count; i++)
                            {
                                RT += this.GetChildrenText(_TheData.TheChildren[i]) + ",";
                            }
                            if (RT[RT.Length - 1] == ',')
                            {
                                RT = RT.Remove(RT.Length - 1);
                            }
                        }
                    }
                }
                return RT;
            }
        }


        private string GetChildrenText(DataContract.Common.DataObject_ECharts _TheData)
        {
            if (B > 236 || B < 20)
            {
                BB = BB * -1;
            }
            B = B + BB * 5;


            if (G > 236 || G < 20)
            {
                BG = BG * -1;
            }

            G = G + BG *  20;


            if (R > 246 || R < 10)
            {
                BR = BR * -1;
            }

            R = R + BR *  10;





            string _r = "{name:'" + _TheData.TheName + "',  itemStyle: { color:'#" + (R % 255).ToString("X") + (G % 255).ToString("X") + (System.Math.Abs(B) % 255).ToString("X") + "'}, children:[";
            if (_TheData.TheChildren != null && _TheData.TheChildren.Count > 0)
            {

                for (int i = 0; i < _TheData.TheChildren.Count; i++)
                {
                    _r += this.GetChildrenText(_TheData.TheChildren[i]) + ",";
                }
            }
            else
            {
                if (_TheData.TheDatasList[0] != null)
                {

                    for (int i = 0; i < _TheData.TheDatasList[0].Count; i++)
                    {
                        if (_TheData.TheDatasList[0][i].VALUE != "0")
                        {
                            _r += "{name:'" + _TheData.TheDatasList[0][i].NAME + "', value:" + _TheData.TheDatasList[0][i].VALUE + " },";
                        }
                    }

                }
            }
            if (_r[_r.Length - 1] == ',')
            {
                _r = _r.Remove(_r.Length - 1);
            }
            _r = _r + " ]}";
            return _r;
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