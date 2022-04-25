using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
 

namespace BusinessControls
{
    public class Control_Common : Control_BaseClass
    {
        /// <summary>
        /// 默认排序规则
        /// </summary>
        protected override string DataSourceDefaultOrder
        {
            get
            {
                return " DM ";
            }

        }

        /// <summary>
        /// 数据来源
        /// </summary>
        protected override string DataSource
        {
            get
            {
                return DataSource_F;
            }
            set
            {
                this.DataSource_F = value;
            }
        }
        private string DataSource_F;

        public Control_Common()
        { this.GD = DataAccess.DAL_Contol.Get_GetDataObject(); }

        /// <summary>
        /// 获取简单数据表（类代码表）
        /// </summary>
        /// <param name="TheParamesterList">条件参数列表</param>
        /// <returns></returns>
        public DataTable Get_SimpleDataTable(string DataSourceDataTable, List<DataContract.Common.DataParamester> TheParamesterList)
        {
            this.DataSource = DataSourceDataTable;
            DataTable TheTable = GD.GetDataTable(this.CreateSQLString(TheParamesterList));
            TheTable.TableName = DataSourceDataTable;
            return TheTable;
        }

        /// <summary>
        /// 获取强代码表数据表（带%所有项）
        /// </summary>
        /// <param name="TheParamesterList">条件参数列表</param>
        /// <returns></returns>
        public DataTable Get_DM_DataTableWithAllItem(string DataSourceDataTable, List<DataContract.Common.DataParamester> TheParamesterList)
        {
            this.DataSource = DataSourceDataTable;
        
            DataTable TheTable = GD.GetDataTable(this.CreateSQLString(TheParamesterList));
            if (TheTable != null)
            {
                DataRow NewRow = TheTable.NewRow();
                NewRow["MC"] = "不限";
                NewRow["DM"] = "%";
                TheTable.Rows.InsertAt(NewRow, 0);
            }
            TheTable.TableName = DataSourceDataTable;
            return TheTable;
        }

        /// <summary>
        /// 获取强代码表数据表（带%所有项）
        /// </summary>
        /// <param name="TheParamesterList">条件参数列表</param>
        /// <returns></returns>
        public DataTable Get_NJ_ByBJ()
        { 
            DataTable TheTable = GD.GetDataTable( "select  NJ as MC, NJ as DM from " + BusinessControls.DataSourceList.BJDMB_Source + " group by NJ order by NJ desc ");
            TheTable.TableName = "NJ";
            return TheTable;
        }


    }
}
