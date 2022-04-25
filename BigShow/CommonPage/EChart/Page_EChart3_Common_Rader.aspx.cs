using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommonPage.EChart
{
    public partial class Page_EChart3_Common_Rader : System.Web.UI.Page
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


        /// <summary>
        /// 作为一个数据对象的名称， 雷达图的数据对象名应对应 legend 值，一个Series 数据为一个数据对象（雷达图的统计项数固定，而序列数【Series】可变，因此一个数据对象对应一个Series 数据，此规则与柱形图或线形图都不一样）
        /// </summary>
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
                        if (_TheData.TheIndicator != null)
                        {
                            for (int i = 0; i < _TheData.TheIndicator.Count; i++)
                            {
                                RT += string.Format(" {{ name: '{0}', max: {1} }},", _TheData.TheIndicator[i].NAME, _TheData.TheIndicator[i].VALUE);
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
                            for (int i = 0; i < _TheData.TheNodes.Count; i++)  // 这里取的不是 TheDatasList 而是 TheNodes 是为了取 TheNodes 中的对象Name
                            {
                                string _D = "";
                                for (int j = 0; j < _TheData.TheDatasList[i].Count; j++)
                                {
                                    _D += _TheData.TheDatasList[i][j].VALUE + ",";
                                }
                                RT += string.Format(" {{ value: [{0}],name: '{1}',symbolSize: 7, areaStyle:{{opacity: 0.3,color: new echarts.graphic.RadialGradient(0.5, 0.5, 0.3, [{{color: '#B8D3E4',offset: 0}},{{color: '#72ACD1',offset: 1}}] ) }}}},", _D.Remove(_D.Length - 1), _TheData.TheNodes[i].NAME);
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
            //if (Request["W"] != null)
            //{
            //    this.main.Style.Remove("width");
            //    this.main.Style.Add("width", Request["W"].ToString().Trim() + "px");
            //}
            //if (Request["H"] != null)
            //{
            //    this.main.Style.Remove("height");
            //    this.main.Style.Add("height", Request["H"].ToString().Trim() + "px");
            //}
        
        }
    }
}