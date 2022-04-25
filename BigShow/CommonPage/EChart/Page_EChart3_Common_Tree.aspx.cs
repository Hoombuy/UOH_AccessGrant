﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommonPage.EChart
{
    public partial class Page_EChart3_Common_Tree : System.Web.UI.Page
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
                        RT = this.GetChildrenText(_TheData);
                    }
                }
                return RT;
            }
        }


        private string GetChildrenText(DataContract.Common.DataObject_ECharts _TheData)
        {
            string _r = "{name:'" + _TheData.TheName + "', children:[";
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