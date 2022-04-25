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
    public partial class FormIndex : Form
    {
        //private delegate void UpdateTimeHandle(DataContract.Common.ThreadingInfo.UpdateTypeAndTime TheUpdateTypeAndTime);    //更新更新时间的委托，包括与服务器联系时间、获取各类监控数据时间
        //private delegate void UpdateStateIconHandle(DataContract.Common.ThreadingInfo.TypeAndState TheTypeAndState);    //更新各类监控状态图标
        //private delegate void UpdateMonitorInfoHandle(DataContract.Common.ThreadingInfo.InfoAndColor TheInfoAndColor);    //更新监控结果信息的委托 
        private delegate void TreadingUpdateLogListHandle();
        private delegate void TreadingUpdateRunningStateListHandle(DataContract.DataTrans.DataTrans_Log TheLog);
        private delegate void TreadingInitializeRunningStateListHandle(List<DataContract.DataTrans.DataTrans_RunningStateInfo> TheBusinessList);

        public FormIndex()
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
            var GD_YKT = DataAccess.DAL_Contol.Get_GetDataObject(SystemName.YKTMid);

            DataTable Table_ZFJXRW = GD_ZF.GetDataTable(string.Format("select xkkh, xn, xq, kcdm, kcmc, kcxz, xf, zxs, kclb, kcgs, kkxy , jszgh, jsxm, sksj, skdd, qsz, jsz  from zfxfzb.jxrwbview where xn = '{0}' and xq = '{1}' group by xkkh, xn, xq, kcdm, kcmc, kcxz, xf, zxs, kclb, kcgs, kkxy, jszgh, jsxm, sksj, skdd, qsz, jsz", this.textBox1.Text.Trim(), this.textBox2.Text.Trim()));

            if (!GD_ZF.Excute("delete from zfxfzb.UOH_ACCESSGRANT_XXB") || !GD_ZF.Excute("delete from zfxfzb.UOH_ACCESSGRANT_JGB"))
            {
                MessageBox.Show("系统错误！无法清除历史数据");
            }
            else
            {
                DataTable Table_ZF_UOH_ACCESSGRANT_XXB = GD_ZF.GetDataTable("select * from zfxfzb.UOH_ACCESSGRANT_XXB");

                if (Table_ZFJXRW.Rows.Count > 0 && Table_ZF_UOH_ACCESSGRANT_XXB.Rows.Count == 0)
                {
                    int _c = 1;
                    for (int _i = 0; _i < Table_ZFJXRW.Rows.Count; _i++)
                    {
                        DataRow _row = Table_ZFJXRW.Rows[_i];
                        if (_row["SKSJ"] != DBNull.Value && _row["SKSJ"] != DBNull.Value)
                        {
                            string[] skdd = _row["SKDD"].ToString().Split(new char[] { ';' });
                            string[] sksj = _row["SKSJ"].ToString().Split(new char[] { ';' });
                            for (int i = 0; i < skdd.GetLength(0); i++)
                            {
                                string[] Node = sksj[i].Substring(sksj[i].IndexOf("第") + 1, sksj[i].IndexOf("节") - sksj[i].IndexOf("第") - 1).Split(new char[] { ',' });
                                if (Node.Length > 0)
                                {
                                    string _kssd = Node[0];
                                    string _jssd = Node[Node.Length - 1];
                                    string _qsz = sksj[i].Substring(sksj[i].IndexOf("{第") + 2, sksj[i].IndexOf("-") - sksj[i].IndexOf("{第") - 2);
                                    string _jsz = sksj[i].Substring(sksj[i].IndexOf("-") + 1, sksj[i].IndexOf("周", 2) - sksj[i].IndexOf("-") - 1);
                                    string _skcd = (int.Parse(_jssd) + 1 - int.Parse(_kssd)).ToString();
                                    string _dsz = "";
                                    if (sksj[i].IndexOf("|") != -1)
                                    {
                                        _dsz = sksj[i].Substring(sksj[i].IndexOf("|") + 1, 2);
                                    }
                                    string _jsmc = skdd[i];
                                    string _xq = this.TranXQMC(sksj[i].Substring(0, 2));

                                    if (!GD_ZF.Excute(String.Format(" insert into zfxfzb.UOH_ACCESSGRANT_XXB values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}',{15}) ",
                                        _row["xkkh"].ToString(),
                                        _row["kcdm"].ToString(),
                                        _row["kcmc"].ToString(),
                                        _row["jszgh"].ToString(),
                                        _row["jsxm"].ToString(),
                                        _row["sksj"].ToString(),
                                        _row["skdd"].ToString(),
                                        _qsz,
                                        _jsz,
                                        _kssd,
                                        _jssd,
                                        _skcd,
                                        _dsz,
                                        _xq,
                                        _jsmc,
                                           _c)))
                                    {
                                        MessageBox.Show("数据错误，导致中断");
                                        return false;
                                    }
                                    _c++;
                                }
                            }
                        }
                        this.backgroundWorkerMain.ReportProgress(40 * _i / Table_ZFJXRW.Rows.Count);
                    }
                }

                // this.backgroundWorkerMain.ReportProgress(40);

                for (int c = 1; c <= 18; c++)
                {
                    int _countnow = int.Parse(GD_ZF.GetDataNumValue("select count(*) from UOH_ACCESSGRANT_JGB "));
                    string _sql = String.Format("insert into UOH_ACCESSGRANT_JGB select sqrq,   to_DATE(sqrq ||' '||sqkssj, 'yyyy/MM/dd hh24:mi') sqkssj,   " +
                                        " to_DATE(sqrq || ' ' || sqjssj, 'yyyy/MM/dd hh24:mi') sqjssj, " +
                                         " jsbh， jsmc， xkkh， kcmc，jszgh  , jsxm , {0}  as zs,  " +
                                        " kssd, jssd, xqj as xq, rownum+ {3} as id, '' as bz " +
                                        " from " +
                                        " (select   to_char(to_date('{1}', 'yyyy/MM/dd hh24:mi:ss') + ({0}-1)*7 + xqj - 1, 'yyyy/MM/dd') as sqrq, " +
                                               "  {0} xzs, " +
                                                 "    case when mod( {0}, 2) = 1 then '单' else '双' end  xdsz, " +
                                          "  a.qsz, a.jsz, a.dsz,  " +
                                        " a.qsz || '-' || a.jsz || a.dsz   ,c.xq  , a.xq as xqj, a.xkkh  ,a.jszgh, a.jsxm, a.kcmc, " +
                                        " a.kssd  , to_char(TO_DATE(b.dzsj, 'hh24:mi') - {2} / (24 * 60), 'hh24:mi')  as sqkssj  ,a.skcd, a.jssd  , a.jsmc,  " +
                                        " to_char(TO_DATE(b2.dzsj, 'hh24:mi') + (45+{2}) / (24 * 60), 'hh24:mi') as sqjssj  , cd.jsbh  ,a.skdd , a.id as xxbh " +
                                        " from UOH_ACCESSGRANT_XXB a left " +
                                        " join cs_rqdzb b on a.kssd = b.qssjd left " +
                                        " join cs_rqdzb b2 on a.jssd = b2.qssjd  left " +
                                        " join cs_xqdzb c on a.xq = c.xqj left " +
                                        " join zfxfzb.jxcdxxb cd on cd.jsmc = a.jsmc " +
                                        " where a.kssd is not null   order by a.xq  ) j " +
                                        " where xzs >= qsz and xzs <= jsz and(xdsz = dsz or dsz is null) ", c, this.dateTimePicker1.Value.ToString("yyyy/MM/dd hh:mm:ss"), int.Parse(this.textBox3.Text.Trim()), _countnow);
                    GD_ZF.Excute(_sql);
                    this.backgroundWorkerMain.ReportProgress(40 + (30 * c / 18));
                }

                DataTable Table_ZF_UOH_ACCESSGRANT_JGB = GD_ZF.GetDataTable("select * from zfxfzb.UOH_ACCESSGRANT_JGB");

                if (Table_ZF_UOH_ACCESSGRANT_JGB.Rows.Count > 0)
                {
                    int _c = int.Parse(GD_YKT.GetDataNumValue("select max(smt_kcxh) from SMART_MID.SMART13002 t"));
                    for (int c = 0; c < Table_ZF_UOH_ACCESSGRANT_JGB.Rows.Count; c++)
                    {

                        DataRow _Row = Table_ZF_UOH_ACCESSGRANT_JGB.Rows[c];
                        if (_Row["jsbh"] != DBNull.Value)
                        {
                            GD_YKT.Excute(String.Format("insert into  SMART_MID.SMART13002 values({0}, to_date('{1}', 'yyyy/MM/DD hh24:mi:ss') , to_date('{2}', 'yyyy/MM/DD hh24:mi:ss') , '{3}', '{4}' ) ", _c + int.Parse(_Row["id"].ToString()), _Row["sqkssj"].ToString(), _Row["sqjssj"].ToString(), _Row["jsbh"].ToString(), _Row["jsmc"].ToString()));
                            GD_YKT.Excute(String.Format("insert into  SMART_MID.SMART13009 values({0}, '{0}', '{1}' ) ", _c + int.Parse(_Row["id"].ToString()), _Row["jszgh"].ToString()));
                            this.backgroundWorkerMain.ReportProgress(70 + (30 * c / Table_ZF_UOH_ACCESSGRANT_JGB.Rows.Count));
                        }
                    }
                }
                return true;
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
            this.simpleButton1.Enabled = true;

        }

        private void backgroundWorkerMain_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
        }
        #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            if (this.textBox1.Text != "" && this.textBox2.Text != "")
            {
                this.simpleButton1.Enabled = false;
                this.backgroundWorkerMain.CancelAsync();
                this.backgroundWorkerMain.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("填入正确的学年学期");
            }
        }

        private void FormIndex_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.backgroundWorkerMain.CancelAsync();

        }

        private string TranXQMC(string _XQM)
        {
            if (_XQM == "周一")
            {
                return "1";
            }
            if (_XQM == "周二")
            {
                return "2";
            }
            if (_XQM == "周三")
            {
                return "3";
            }
            if (_XQM == "周四")
            {
                return "4";
            }
            if (_XQM == "周五")
            {
                return "5";
            }
            if (_XQM == "周六")
            {
                return "6";
            }
            if (_XQM == "周日")
            {
                return "7";
            }

            return "";

        }

    }
}
