using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataContract;
using BusinessControls;

namespace DataTransTable
{
    public partial class Form_Ruijie : Form
    {
        //private delegate void UpdateTimeHandle(DataContract.Common.ThreadingInfo.UpdateTypeAndTime TheUpdateTypeAndTime);    //更新更新时间的委托，包括与服务器联系时间、获取各类监控数据时间
        //private delegate void UpdateStateIconHandle(DataContract.Common.ThreadingInfo.TypeAndState TheTypeAndState);    //更新各类监控状态图标
        //private delegate void UpdateMonitorInfoHandle(DataContract.Common.ThreadingInfo.InfoAndColor TheInfoAndColor);    //更新监控结果信息的委托 
        private delegate void TreadingUpdateLogListHandle();
        private delegate void TreadingUpdateRunningStateListHandle(DataContract.DataTrans.DataTrans_Log TheLog);
        private delegate void TreadingInitializeRunningStateListHandle(List<DataContract.DataTrans.DataTrans_RunningStateInfo> TheBusinessList);

        public Form_Ruijie()
        {
            InitializeComponent();


        }

        #region 与界面的线程交流


        /// <summary>
        /// 初始化系统运行状态
        /// </summary>
        private void InitializeRunningStateList()
        {

            this.progressBar1.Value = 0;

        }



        #endregion

        #region 自动处理数据业务部分
        /// <summary>
        /// 自动监测工作流
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_Monitor_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            this.DataTrans(worker, e);

        }

        private bool DataTrans(BackgroundWorker worker, DoWorkEventArgs e)
        {


            var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(SystemName.ZFSystem);
            var GD_RJ = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.RuijieDB);
            //DataTable _T_Student = GD_RJ.GetDataTable(" SELECT  c.*   FROM[SAMDB].[dbo].[USER_GROUP] a,    SAMDB.dbo.GROUPINFO b,   SAMDB.dbo.USERINFO c   where a.GROUPINFO_UUID = b.GROUPINFO_UUID   and a.USERINFO_UUID = c.USERINFO_UUID  and(b.GROUPINFO_ID like '%级'  ) ");
            DataTable _T_Student = GD_RJ.GetDataTable(" SELECT  c.*   FROM[SAMDB].[dbo].[USER_GROUP] a,    SAMDB.dbo.GROUPINFO b,   SAMDB.dbo.USERINFO c   where a.GROUPINFO_UUID = b.GROUPINFO_UUID   and a.USERINFO_UUID = c.USERINFO_UUID  and(b.GROUPINFO_ID like '%级' or b.GROUPINFO_ID like '%学生%') ");
            this.backgroundWorkerMain.ReportProgress(10);
            try
            {
                RuijieSamapi.SamServicePortTypeClient _S = new RuijieSamapi.SamServicePortTypeClient();

                for (int i = 0; i < _T_Student.Rows.Count; i++)
                {
                    if (GD_ZF.GetDataNumValue(string.Format("select count(*) from zfxfzb.xsjbxxb where xh ='{0}' and xjzt ='有'  ", _T_Student.Rows[i]["USER_ID"].ToString().Trim())) != "1")
                    {
                        RuijieSamapi.samApiBaseResult _R = _S.logicDelUser(_T_Student.Rows[i]["USER_ID"].ToString().Trim());
                    }
                    this.backgroundWorkerMain.ReportProgress(90 * i / _T_Student.Rows.Count + 10);
                }



                //RuijieSamapi.addOrUpDateUserInfoParams _UP = new RuijieSamapi.addOrUpDateUserInfoParams()
                //{
                //    userId = "20190924",
                //    password = "190924"
                //};

                //_S.addOrUpDateUserInfo(_UP);


                return true;
            }
            catch
            {

            }


            return false;
        }

        private void backgroundWorker_Monitor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("操作被取消");
            }
            else
            {
                MessageBox.Show("同步成功！");
            }
            this.InitializeRunningStateList();
            this.simpleButton3.Enabled = true;
            this.simpleButton2.Enabled = true;
            this.simpleButton1.Enabled = true;

        }

        private void backgroundWorkerMain_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // this.progressBar1.Value = e.ProgressPercentage;
        }
        #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {


            this.backgroundWorkerMain.CancelAsync();
            this.backgroundWorkerMain.RunWorkerAsync();

        }

        private void FormIndex_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.backgroundWorkerMain.CancelAsync();

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.backgroundWorker_newStu.CancelAsync();
            this.backgroundWorker_newStu.RunWorkerAsync();
        }
        #region 新生数据推送

        private bool DataTrans_NewStu(BackgroundWorker worker, DoWorkEventArgs e)
        {



            var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(SystemName.ZFSystem);
            DataTable _T_Student = GD_ZF.GetDataTable(string.Format(" select xsxh, xm,  lxdh, sfzh, zsnd, xymc, zymc, bjmc  from  zfzsxt.VIEW_ZS_TZGL_TZSJDR_BKSLQXXB a where a.zsnd ='{0}' and xxpcmc not like '%预科%'  ", DateTime.Now.Year.ToString()));
            this.backgroundWorkerMain.ReportProgress(10);
            try
            {
                RuijieSamapi.SamServicePortTypeClient _S = new RuijieSamapi.SamServicePortTypeClient();

                for (int i = 0; i < _T_Student.Rows.Count; i++)
                {
                    RuijieSamapi.addOrUpDateUserInfoParams _UP = new RuijieSamapi.addOrUpDateUserInfoParams()
                    {
                        userId = _T_Student.Rows[i]["XSXH"].ToString(),
                        password = _T_Student.Rows[i]["SFZH"].ToString().Substring(_T_Student.Rows[i]["SFZH"].ToString().Length - 6, 6),
                        userName = _T_Student.Rows[i]["XM"].ToString(),
                        field3 = _T_Student.Rows[i]["XYMC"].ToString(),
                        field2 = _T_Student.Rows[i]["ZYMC"].ToString(),
                        field4 = _T_Student.Rows[i]["BJMC"].ToString(),
                        field5 = _T_Student.Rows[i]["ZSND"].ToString(),
                        field6 = "有",
                        userGroupName = _T_Student.Rows[i]["ZSND"].ToString() + "级",
                        atName = "学生模板",
                        packageName = "中国移动"

                    };
                    var x = _S.addOrUpDateUserInfo(_UP);
                    this.backgroundWorkerMain.ReportProgress(90 * i / _T_Student.Rows.Count + 10);
                }
                return true;
            }
            catch
            {

            }


            return false;
        }
        private void backgroundWorker_newStu_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            this.DataTrans_NewStu(worker, e);
        }

        private void backgroundWorker_newStu_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker_newStu_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("操作被取消");
            }
            else
            {
                MessageBox.Show("同步成功！");
            }
            this.InitializeRunningStateList();
            this.simpleButton3.Enabled = true;
            this.simpleButton2.Enabled = true;
            this.simpleButton1.Enabled = true;
        }

        #endregion

        #region 教工数据推送

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.backgroundWorker_tec.CancelAsync();
            this.backgroundWorker_tec.RunWorkerAsync();
        }

        private bool DataTrans_Tec(BackgroundWorker worker, DoWorkEventArgs e)
        {
            var GD_RJ = DataAccess.DAL_Contol.Get_GetDataObject(DataContract.SystemName.RuijieDB);
            var GD_ZF = DataAccess.DAL_Contol.Get_GetDataObject(SystemName.ZFSystem);
            DataTable _T_New = GD_ZF.GetDataTable(string.Format("  select  substr(a.jgh,3,6) as mm, a.*, b.dwmc from   zfdxc.uoh_view_jg_jzgjbsj a inner join  zfdxc.xx_yxsdwjbsjzl b    on   a.dwh = b.dwh   "));
            this.backgroundWorkerMain.ReportProgress(10);
            try
            {
                RuijieSamapi.SamServicePortTypeClient _S = new RuijieSamapi.SamServicePortTypeClient();

                for (int i = 0; i < _T_New.Rows.Count; i++)
                {

                    DataTable _T_Exists = GD_RJ.GetDataTable(" SELECT  c.*   FROM[SAMDB].[dbo].[USER_GROUP] a,    SAMDB.dbo.GROUPINFO b,   SAMDB.dbo.USERINFO c   where a.GROUPINFO_UUID = b.GROUPINFO_UUID   and a.USERINFO_UUID = c.USERINFO_UUID  and(b.GROUPINFO_ID like '教师组'  ) and c.USER_ID = '" + _T_New.Rows[i]["JGH"].ToString() + "'");

                    if (_T_Exists.Rows.Count == 0)
                    {
                        if (_T_New.Rows[i]["SFZJH"] == null || _T_New.Rows[i]["SFZJH"] == DBNull.Value)
                        {
                            _T_New.Rows[i]["SFZJH"] = _T_New.Rows[i]["MM"].ToString();
                        }
                        RuijieSamapi.addOrUpDateUserInfoParams _UP = new RuijieSamapi.addOrUpDateUserInfoParams()
                        {
                            userId = _T_New.Rows[i]["JGH"].ToString(),
                            password = _T_New.Rows[i]["SFZJH"].ToString().Substring(_T_New.Rows[i]["SFZJH"].ToString().Length - 6, 6),
                            userName = _T_New.Rows[i]["XM"].ToString(),
                            field1 = _T_New.Rows[i]["DWMC"].ToString(),
                            //field2 = _T_Student.Rows[i]["ZYMC"].ToString(),
                            //field4 = _T_Student.Rows[i]["BJMC"].ToString(),
                            //field5 = _T_Student.Rows[i]["ZSND"].ToString(),

                            field8 = _T_New.Rows[i]["DWMC"].ToString(),
                            field5 = "20218",
         
                            userGroupName = "教师组",
                            atName = "教师模板",
                            packageName = "中国移动"

                        };

                        var x = _S.addOrUpDateUserInfo(_UP);
                    }
                    this.backgroundWorkerMain.ReportProgress(90 * i / _T_New.Rows.Count + 10);
                }
                return true;
            }
            catch
            {

            }


            return false;
        }
        private void backgroundWorker_tec_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            this.DataTrans_Tec(worker, e);
        }

        private void backgroundWorker_tec_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker_tec_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("操作被取消");
            }
            else
            {
                MessageBox.Show("同步成功！");
            }
            this.InitializeRunningStateList();
            this.simpleButton3.Enabled = true;
            this.simpleButton2.Enabled = true;
            this.simpleButton1.Enabled = true;
        }
        #endregion


    }
}
