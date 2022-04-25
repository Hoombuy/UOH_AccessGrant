using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BigShow4K
{
    public partial class Page_ShowDataAssets : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {

                this.ShowData();
            }

        }

        public void ShowData()
        {

            this.ShowSystemTree();
            this.ShowDataTranRule();

        }

        private void ShowSystemTree()
        {
            var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);

            DataTable Table_ZFJXRW = GD_ZF.GetDataTable("select  t.*, '红院数字化校园' as title   from ZFDXC.UOH_SYSTEMINFO t ");
            Session["SystemTree"] = BusinessControls.Chart.Control_Chart.Fill_Tree(Table_ZFJXRW, new ArrayList() { "TYPEMC" }, "MC", "数字化校园");
        }
        private void ShowDataTranRule()
        {
            var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);

            DataTable TableCJ = GD_ZF.GetDataTable("   select a.datasource||a.datatarget||sm ITEMID, a.datasource as Name from   ZFDXC.UOH_DATATRANRULE  a union  select a.datasource || a.datatarget || sm ITEMID, a.datatarget as Name from ZFDXC.UOH_DATATRANRULE a      ");

            DataTable TableKC = GD_ZF.GetDataTable("    select MC as Name,Datacount   as Value, TypeMC as  LevelNo from  ZFDXC.UOH_SYSTEMINFO   ");

            Session["DataTranRule"] = BusinessControls.Chart.Control_Chart.Fill_ForGraph(TableKC, TableCJ);
        }



        //public int _X
        //{
        //    get
        //    {
        //        return (int)new BusinessControls.Member.Control_Organization().GetObjectList_Count();
        //    }
        //}
        //public int _Y
        //{
        //    get
        //    {
        //        return (int)(new BusinessControls.Member.Control_LeagueMember().GetObjectList_Count());
        //    }
        //}
        //public int _Z
        //{
        //    get
        //    {
        //        return (int)(new BusinessControls.Event.Control_Event_Record().GetObjectList_Count());
        //    }
        //}



        protected void BigShow_Click(object sender, EventArgs e)
        {
            if (((LinkButton)sender).CommandName == "SystemTree")
            {
                this.if_BigShow.Src = "/CommonPage/EChart/Page_EChart3_Common_Tree.aspx?SN=SystemTree&&Title=红河学院信息系统列表";
            }
            if (((LinkButton)sender).CommandName == "DataAssets")
            {
                this.if_BigShow.Src = "/CommonPage/EChart/Page_EChart3_Common_PctorialBar.aspx?SN=DataAssets&&Title=数据资产";
            }
            if (((LinkButton)sender).CommandName == "DataTranRule")
            {
                this.if_BigShow.Src = "/CommonPage/EChart/Page_EChart3_Common_Graph.aspx?SN=DataTranRule&&Title=数据交换规则";
            }


            this.ClientScript.RegisterStartupScript(this.GetType(), "updateScriptmessage", @"<script>$('#BigShowModal').modal('show');</script>");

        }
    }
}
