using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using DevExpress.Utils;
using System.Drawing;



namespace DataTransTable.Common
{

    /// <summary>
    /// 一般windows窗口操作类，本类提供通用的一系列操作方法
    /// </summary>
    public class FormOperateMethod
    {
        /// <summary>
        /// 一般windows窗口操作类构造函数，并不承担任何业务
        /// </summary>
        public FormOperateMethod()
        {

        }

        /// <summary>
        /// 获取用户自定义控件所应生成的sql查询条件,其中textbox控件为相似查询，combobox控件为比较查询
        /// </summary>
        /// <param name="TheForm">windows用户控件、或窗体</param>
        /// <returns>相对应的查询条件</returns>
        public string GetSQLConditionFormUserControl(System.Windows.Forms.Control TheForm)
        {
            string TheCondition = "";
            for (int i = 0; i < TheForm.Controls.Count; i++)
            {
                if (TheForm.Controls[i].ToString().Contains("System.Windows.Forms.GroupBox"))
                {
                    for (int j = 0; j < TheForm.Controls[i].Controls.Count; j++)
                    {
                        if (TheForm.Controls[i].Controls[j].ToString().Contains("System.Windows.Forms.TextBox"))
                        {
                            if (((TextBox)TheForm.Controls[i].Controls[j]).Text.Trim() != "")
                            {
                                TheCondition += "and " + ((TextBox)TheForm.Controls[i].Controls[j]).Tag + " like '%" + ((TextBox)TheForm.Controls[i].Controls[j]).Text + "%' ";
                            }
                        }
                        else if (TheForm.Controls[i].Controls[j].ToString().Contains("System.Windows.Forms.NumericUpDown"))
                        {
                            TheCondition += "and " + ((NumericUpDown)TheForm.Controls[i].Controls[j]).Tag + " = '" + ((NumericUpDown)TheForm.Controls[i].Controls[j]).Value.ToString() + "' ";
                        }
                        else if (TheForm.Controls[i].Controls[j].ToString().Contains("System.Windows.Forms.ComboBox"))
                        {
                            if (((ComboBox)TheForm.Controls[i].Controls[j]).SelectedValue.ToString() != "%")
                            {
                                TheCondition += "and " + ((ComboBox)TheForm.Controls[i].Controls[j]).Tag + " = '" + ((ComboBox)TheForm.Controls[i].Controls[j]).SelectedValue.ToString() + "' ";
                            }
                        }
                    }
                }
                if (TheForm.Controls[i].ToString().Contains("System.Windows.Forms.TextBox"))
                {
                    if (((TextBox)TheForm.Controls[i]).Text.Trim() != "")
                    {
                        TheCondition += "and " + ((TextBox)TheForm.Controls[i]).Tag + " like '%" + ((TextBox)TheForm.Controls[i]).Text + "%' ";
                    }
                }
                else if (TheForm.Controls[i].ToString().Contains("System.Windows.Forms.NumericUpDown"))
                {
                    TheCondition += "and " + ((NumericUpDown)TheForm.Controls[i]).Tag + " = '" + ((NumericUpDown)TheForm.Controls[i]).Value.ToString() + "' ";
                }
                else if (TheForm.Controls[i].ToString().Contains("System.Windows.Forms.ComboBox"))
                {
                    if (((ComboBox)TheForm.Controls[i]).SelectedValue.ToString() != "%")
                    {
                        TheCondition += "and " + ((ComboBox)TheForm.Controls[i]).Tag + " = '" + ((ComboBox)TheForm.Controls[i]).SelectedValue.ToString() + "' ";
                    }
                }
            }
            if (TheCondition.Trim() != "")      //去掉第一个and
            {
                TheCondition = TheCondition.Remove(0, 3);
            }
            return TheCondition;
        }

        /// <summary>
        /// 初始化用户控件对象，将其中textbox设为空字符串，combobox控件设为选中第一项
        /// </summary>
        /// <param name="TheForm">windows用户控件、或窗体</param>
        public void InitializeFormUserControl(System.Windows.Forms.Control TheForm)
        {
            for (int i = 0; i < TheForm.Controls.Count; i++)
            {
                if (TheForm.Controls[i].HasChildren == true)
                {
                    this.InitializeFormUserControl(TheForm.Controls[i]);
                }
                else
                {
                    if (TheForm.Controls[i].ToString().Contains("System.Windows.Forms.TextBox"))
                    {
                        ((TextBox)TheForm.Controls[i]).Text = "";
                    }
                    else if (TheForm.Controls[i].ToString().Contains("System.Windows.Forms.ComboBox"))
                    {
                        if (((ComboBox)TheForm.Controls[i]).DataSource != null)
                        {
                            ((ComboBox)TheForm.Controls[i]).SelectedIndex = 0;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置控件的是否可修改（ReadOnly）属性, 暂时只接受readonly为true的处理，所有窗体的默认情况都应该为readonly=false;
        /// </summary>
        /// <param name="value"></param>
        /// <param name="thec"></param>
        public void SetControlReadOnly(bool value, Control thec)
        {
            for (int i = thec.Controls.Count - 1; i >= 0; i--)
            {
                if (thec.Controls[i].HasChildren == true)
                {
                    this.SetControlReadOnly(value, thec.Controls[i]);
                }
                else
                {
                    if (thec.Controls[i].ToString().Contains("System.Windows.Forms.TextBox"))
                    {
                        //TextBox tb = ((TextBox)thec.Controls[i]);
                        //Label NewLabel = new Label();
                        //NewLabel.Text = tb.Text;
                        //tb.Parent.Controls.Add(NewLabel);
                        //NewLabel.SetBounds(tb.Left, tb.Top, tb.Width, tb.Height);
                        //tb.Parent.Controls.Remove(tb);
                        //tb.Dispose();
                    }
                    else if (thec.Controls[i].ToString().Contains("System.Windows.Forms.Label"))
                    {
                        Label lb = ((Label)thec.Controls[i]);
                        if (lb.Text.Trim() == "*" && value == true)
                        {
                            lb.Parent.Controls.Remove(lb);
                            lb.Dispose();
                        }
                    }
                    else if (thec.Controls[i].ToString().Contains("System.Windows.Forms.ComboBox"))
                    {
                        ComboBox thecombobox = (ComboBox)thec.Controls[i];
                        if (value == true)
                        {
                            thecombobox.DropDownStyle = ComboBoxStyle.Simple;
                        }
                        else
                        {
                            thecombobox.DropDownStyle = ComboBoxStyle.DropDownList;
                        }
                    }
                }
            }
        }

        ///// <summary>
        ///// 导出数据到Excel(通过devexpress.gridview)(无进度条)
        ///// </summary>
        ///// <param name="dg">传送的需要导出的数据容器</param>
        //public void OutputToExcel(DevExpress.XtraGrid.Views.Grid.GridView dg)
        //{//构造函数

        //    #region 一些变量和一些本类成员初始化工作
        //    int i, j;
        //    int r, c;

        //    #endregion

        //    #region 得到行和列数,并且声明相应的处理数组
        //    r = dg.DataRowCount;
        //    //得到宽度不为0的列的列数,用c记录
        //    c = 0;
        //    for (i = 0; i < dg.Columns.Count; i++)
        //    {
        //        c++;
        //    }

        //    string[,] mystrs = new string[r + 1, c];
        //    string[] myhdtxts = new string[c];//记录列的hdtxt信息
        //    #endregion

        //    #region 得到列的hdtxt信息和构造表数组行等
        //    int cc = 0;
        //    //构造表数组第一行和得到列的hdtxt信息,不包含宽度为0的列
        //    j = 0;
        //    //构造表数组其他行
        //    //将dv中的所有行和宽度不为0的列导到数组中
        //    for (j = 0; j < c; j++)
        //    {
        //        if (dg.Columns[j].Width != 0 && dg.Columns[j].GetTextCaption() != "" && dg.Columns[j].Visible != false)
        //        {
        //            mystrs[0, cc] = dg.Columns[j].Caption;
        //            cc++;
        //        }
        //    }

        //    int ac = r * c;
        //    int js = 0;
        //    for (i = 0; i < r; i++)
        //    {
        //        cc = 0;
        //        for (j = 0; j < c; j++)
        //        {
        //            if (dg.Columns[j].Width != 0 && dg.Columns[j].GetTextCaption() != "" && dg.Columns[j].Visible != false)
        //            {
        //                object ob = dg.GetRowCellValue(i, dg.Columns[j]);
        //                if (ob != null)
        //                {
        //                    mystrs[i + 1, cc] = ob.ToString();
        //                }
        //                else
        //                {
        //                    mystrs[i + 1, cc] = "";
        //                }
        //                cc++;
        //                js++;
        //            }
        //        }
        //    }
        //    #endregion

        //    string[,] strs = (string[,])mystrs.Clone();
        //    //将数组中所有数据写到excel中

        //    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

        //    if (app == null)
        //    {
        //        MessageBox.Show("无法启动");
        //        return;
        //    }
        //    try
        //    {
        //        app.Visible = true;
        //        Microsoft.Office.Interop.Excel.Workbooks books = app.Workbooks;
        //        Microsoft.Office.Interop.Excel.Workbook book = books.Add(true);
        //        Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)book.Worksheets[1];
        //        Microsoft.Office.Interop.Excel.Range rg = sheet.get_Range("A1", Type.Missing);

        //        rg = rg.get_Resize(1, c);
        //        js = 0;
        //        for (i = 0; i < r + 1; i++)
        //        {
        //            for (j = 0; j < c; j++)
        //            {
        //                app.Cells[i + 1, j + 1] = strs[i, j];
        //                //Thread.Sleep(200);6
        //                js++;
        //            }
        //        }
        //        rg.EntireColumn.AutoFit();
        //    }
        //    catch
        //    { }
        //}

        ///// <summary>
        ///// 导出数据到Excel(通过devexpress.gridview)
        ///// </summary>
        ///// <param name="dg">传送的需要导出的数据容器</param>
        ///// <param name="TheBackgroudWorker">后台工作线程组件，用于显示导出进度条</param>
        //public void OutputToExcel(DevExpress.XtraGrid.Views.Grid.GridView dg, System.ComponentModel.BackgroundWorker TheBackgroudWorker)
        //{//构造函数

        //    #region 一些变量和一些本类成员初始化工作
        //    int i, j;
        //    int r, c;

        //    #endregion

        //    #region 得到行和列数,并且声明相应的处理数组
        //    r = dg.DataRowCount;
        //    //得到宽度不为0的列的列数,用c记录
        //    c = 0;
        //    for (i = 0; i < dg.Columns.Count; i++)
        //    {
        //        c++;
        //    }

        //    string[,] mystrs = new string[r + 1, c];
        //    string[] myhdtxts = new string[c];//记录列的hdtxt信息
        //    #endregion

        //    #region 得到列的hdtxt信息和构造表数组行等
        //    int cc = 0;
        //    //构造表数组第一行和得到列的hdtxt信息,不包含宽度为0的列
        //    j = 0;
        //    //构造表数组其他行
        //    //将dv中的所有行和宽度不为0的列导到数组中
        //    for (j = 0; j < c; j++)
        //    {
        //        if (dg.Columns[j].Width != 0 && dg.Columns[j].GetTextCaption() != "" && dg.Columns[j].Visible != false)
        //        {
        //            mystrs[0, cc] = dg.Columns[j].Caption;
        //            cc++;
        //        }
        //    }

        //    this.ReportBackgroudworderProgress(TheBackgroudWorker, 10);

        //    int ac = r * c;
        //    int js = 0;
        //    for (i = 0; i < r; i++)
        //    {
        //        cc = 0;
        //        for (j = 0; j < c; j++)
        //        {
        //            if (dg.Columns[j].Width != 0 && dg.Columns[j].GetTextCaption() != "" && dg.Columns[j].Visible != false)
        //            {
        //                object ob = dg.GetRowCellValue(i, dg.Columns[j]);
        //                if (ob != null)
        //                {
        //                    mystrs[i + 1, cc] = ob.ToString();
        //                }
        //                else
        //                {
        //                    mystrs[i + 1, cc] = "";
        //                }
        //                cc++;
        //                this.ReportBackgroudworderProgress(TheBackgroudWorker, 10 + (js * 30 / ac));
        //                js++;
        //            }
        //        }
        //    }
        //    #endregion

        //    string[,] strs = (string[,])mystrs.Clone();
        //    //将数组中所有数据写到excel中

        //    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

        //    if (app == null)
        //    {
        //        MessageBox.Show("无法启动");
        //        return;
        //    }
        //    try
        //    {
        //        app.Visible = true;
        //        Microsoft.Office.Interop.Excel.Workbooks books = app.Workbooks;
        //        Microsoft.Office.Interop.Excel.Workbook book = books.Add(true);
        //        Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)book.Worksheets[1];
        //        Microsoft.Office.Interop.Excel.Range rg = sheet.get_Range("A1", Type.Missing);

        //        rg = rg.get_Resize(1, c);
        //        js = 0;
        //        for (i = 0; i < r + 1; i++)
        //        {
        //            for (j = 0; j < c; j++)
        //            {
        //                app.Cells[i + 1, j + 1] = strs[i, j];
        //                this.ReportBackgroudworderProgress(TheBackgroudWorker, 40 + (js * 60 / (ac + c)));
        //                //Thread.Sleep(200);6
        //                js++;
        //            }
        //        }
        //        rg.EntireColumn.AutoFit();
        //    }
        //    catch
        //    { }
        //}

        private void ReportBackgroudworderProgress(System.ComponentModel.BackgroundWorker TheBackgroudWorker, int ProgressValue)
        {
            if (ProgressValue <= 100 && ProgressValue >= 0)
            {
                if (TheBackgroudWorker != null)
                {
                    if (TheBackgroudWorker.WorkerReportsProgress == true)
                    {
                        TheBackgroudWorker.ReportProgress(ProgressValue);
                    }
                }
            }
        }

        public AppearanceDefault appErrorFillRed = new AppearanceDefault(Color.White, Color.Red, Color.Empty, Color.Red, System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal);
        public AppearanceDefault appErrorFillBlue = new AppearanceDefault(Color.White, Color.RoyalBlue, Color.Empty, Color.RoyalBlue, System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal);
        public AppearanceDefault appErrorRed = new AppearanceDefault(Color.Black, Color.White, Color.Empty, Color.Red, System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal);
        public AppearanceDefault appErrorGreen = new AppearanceDefault(Color.White, Color.Green, Color.Empty, Color.White, System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal);
        public AppearanceDefault appErrorBlue = new AppearanceDefault(Color.Black, Color.RoyalBlue, Color.Empty, Color.White, System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal);

    }

    /// <summary>
    /// 用于WinForm与父窗体合并工具栏的接口
    /// </summary>
    public interface IToolStripMergableForm
    {
        ToolStrip MergedToolStrip
        {
            get;
        }


    }

    /// <summary>
    /// 用于WinForm与父窗体合并状态栏的接口
    /// </summary>
    public interface IStatusStripMergableForm
    {
        StatusStrip MergedStatusStrip
        {
            get;
        }
    }


}
