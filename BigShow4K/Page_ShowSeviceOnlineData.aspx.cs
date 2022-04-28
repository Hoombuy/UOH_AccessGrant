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
    public partial class Page_ShowSeviceOnlineData : System.Web.UI.Page
    {
        DataAccess.DAL_Base GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.ZFSystem);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {

                this.ShowData();
            }

        }

        public void ShowData()
        {

            this.ShowTypePie();
            this.ShowTimeLine();
            this.ShowTypeDrink();
            this.ShowLastPList();

        }

        private void ShowTypePie()
        {
                  DataTable Table_Pie = GD_ZF.GetDataTable("select processname as Name, count(*) as Value from ZFBPMX.BPM_PRO_RUN_HIS t group by processname ");
            Session["ShowTypePie"] = BusinessControls.Chart.Control_Chart.Fill_Common(Table_Pie);
        }

        private void ShowTimeLine()
        {
                DataTable Table_Line = GD_ZF.GetDataTable(" select  '服务项' as ITEMID , to_char(t.createtime, 'yyyy-mm-dd') as Name, count(*) as Value from ZFBPMX.BPM_PRO_RUN_HIS t  group by to_char(t.createtime, 'yyyy-mm-dd') order by name");
            Session["TimeLine"] = BusinessControls.Chart.Control_Chart.Fill_ForLine_ObjectItem(Table_Line);
        }

        private void ShowTypeDrink()
        {

            DataTable Table_ZFJXRW = GD_ZF.GetDataTable("  select descp,typename, 1 as bs,   case when status = 4 then '测试' else '发布' end  status    from   (select defId, def.typeId, subject, defKey, taskNameRule, descp, status, actDeployId, actDefKey, actDefId,   createtime, updatetime, createBy, updateBy, reason, versionNo, parentDefId, isMain, toFirstNode,   showFirstAssignee, canChoicePath, isUseOutForm, formDetailUrl, allowFinishedCc, submitConfirm,   allowDivert, informStart, informType, attachment, sameExecutorJump, allowRefer, instanceAmount,   allowFinishedDivert, isPrintForm, directstart, ccMessageType, testStatusTag, allowMobile,   type.typeName typeName, skipSetting, chkPendingInformStart from zfbpmx.BPM_DEFINITION def      left join zfbpmx.SYS_GL_TYPE type on def.typeId = type.typeId where 1 = 1 and def.isMain = 1     order by def.CREATETIME DESC)     where descp <> 'null'   ");

            Session["TypeSun"] = BusinessControls.Chart.Control_Chart.Fill_TreeMap(Table_ZFJXRW, new ArrayList() { "STATUS","TYPENAME" }, "DESCP", "BS");

        }


        private void ShowLastPList()
        {

            DataTable Table_ZFJXRW = GD_ZF.GetDataTable("  select * from  (select processname, createtime,  rpad(substr(creator,1,1), length(creator)+1,'*') as creator  from ZFBPMX.BPM_PRO_RUN_HIS t order by createtime desc) where rownum <= 15");
            this.GridView_Main.DataSource = Table_ZFJXRW;
            this.GridView_Main.DataBind();
        
        }

      


        public string FWSXZS
        {
            get
            {
                return GD_ZF.GetDataNumValue(" select count(*) from ZFBPMX.BPM_PRO_RUN_HIS  ");

            }
        }
        public string BJSXZS
        {
            get
            {
                return GD_ZF.GetDataNumValue(" select count(*) from ZFBPMX.BPM_PRO_RUN_HIS where status <> 1  ");
            }
        }
        public string FWLBS
        {
            get
            {
                return GD_ZF.GetDataNumValue(" select count( distinct processname) from ZFBPMX.BPM_PRO_RUN_HIS    ");
            }
        }
        public string BLHJCZS
        {
            get
            {
                return GD_ZF.GetDataNumValue("  select count(*) from ZFBPMX.BPM_PRO_STATUS t   ");
            }
        }

        public string JRFWSX
        {
            get
            {
                return GD_ZF.GetDataNumValue("  select count(*) from ZFBPMX.BPM_PRO_RUN_HIS t where to_char(createtime,'YYYY:MM:DD') = to_char(sysdate,'YYYY:MM:DD') ");
            }
        }

        public string BZFWSX
        {
            get
            {
                return GD_ZF.GetDataNumValue(" select count(*) from ZFBPMX.BPM_PRO_RUN_HIS t where to_char(createtime, 'YYYY:IW') = to_char(sysdate - 1, 'YYYY:IW')");
            }
        }
        
        public string BYFWSX
        {
            get
            {
                return GD_ZF.GetDataNumValue(" select count(*) from  ZFBPMX.BPM_PRO_RUN_HIS t where to_char(createtime, 'YYYY:MM') = to_char(sysdate , 'YYYY:MM')");
            }
        }

        public string BNFWSX
        {
            get
            {
                return GD_ZF.GetDataNumValue(" select count(*) from  ZFBPMX.BPM_PRO_RUN_HIS t where to_char(createtime, 'YYYY') = to_char(sysdate , 'YYYY') ");
            }
        }



        protected void BigShow_Click(object sender, EventArgs e)
        {
            if (((LinkButton)sender).CommandName == "TypeSun")
            {
                this.if_BigShow.Src = "CommonPage/EChart/Page_EChart3_Common_SunDrink.aspx?SN=TypeSun&&Title=服务事项分类图";
            }
         
            if (((LinkButton)sender).CommandName == "TimeLine")
            {
                this.if_BigShow.Src = "CommonPage/EChart/Page_EChart3_Common_Line.aspx?SN=TimeLine&&Title=服务事项时间分布图";
            }


            this.ClientScript.RegisterStartupScript(this.GetType(), "updateScriptmessage", @"<script>$('#BigShowModal').modal('show');</script>");

        }

        protected string GetColor(string proname)
        {
            if(proname == "学生外出申请审批V3")
            {
                return "background-color : #0562ad;";
            }        
            return "background-color:#ff9800;";
        }

        protected void DevDataLineShow_Click(object sender, EventArgs e)
        {

        }

        protected void GridView_Main_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}
