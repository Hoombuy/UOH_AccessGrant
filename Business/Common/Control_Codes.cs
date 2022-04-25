using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataContract.Common.Codes;

namespace BusinessControls.Common
{
    public class Control_Codes : Control_BaseClass
    {
        /// <summary>
        /// 数据来源
        /// </summary>
        protected override string DataSource
        {
            get;
            set;
        }

        protected override string DataSourceDefaultOrder
        {
            get
            {
                return "DM";
            }

        }

        public List<DataContract.Common.BaseModelClass> ToBaseModelClassList(List<BaseCode> TheSList)
        {
            return TheSList.Select(d => (DataContract.Common.BaseModelClass)d).ToList();
        }

        public Control_Codes(string TableName)
        {
            this.GD = DataAccess.DAL_Contol.Get_GetDataObject();  
            this.DataSource = TableName;
        }

        /// <summary>
        /// 验证某代码是否已经存在于数据库中
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public bool IsExist(BaseCode TheObject)
        {
            if (int.Parse(this.GD.GetDataNumValue(String.Format("Select count(*) from {0} where  DM='{1}'", this.DataSource, TheObject.DM))) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="TheParamesterList">条件参数列表</param>
        /// <returns></returns>
        private DataTable _Get_DataTable(List<DataContract.Common.DataParamester> TheParamesterList)
        {
            DataTable Table = GD.GetDataTable(this.CreateSQLString(TheParamesterList));
            if (Table == null)
            {
                return null;
            }
            return Table;
        }

        private DataContract.Common.BaseModelClass _GetObject(string DM, DataContract.Common.BaseModelClass TheObject)
        {
            DataTable Table = GD.GetDataTable(String.Format("select * from {1} where DM = '{0}'", DM, this.DataSource));
            if (Table.Rows.Count == 1)
            {
                return this.SetDataFromRow(TheObject, Table.Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///获取通用代码对象
        /// </summary>
        /// <param name="ID">DM</param>
        public BaseCode GetObject(string DM)
        {
            return (BaseCode)this._GetObject(DM, new BaseCode());
        }

        /// <summary>
        /// 获取通用代码对象列表
        /// </summary>
        /// <returns>获取的通用代码对象列表</returns>
        public List<BaseCode> Get_CommonCodesList()
        {
            return this.Get_CommonCodesList(new List<DataContract.Common.DataParamester>() { });
        }

        /// <summary>
        /// 获取通用代码对象列表
        /// </summary>
        /// <returns>获取的通用代码对象列表</returns>
        public List<BaseCode> Get_CommonCodesList(DataTable _Table)
        {
            List<DataContract.Common.Codes.BaseCode> TheList = new List<DataContract.Common.Codes.BaseCode>();
            for (int i = 0; i < _Table.Rows.Count; i++)
            {
                DataContract.Common.Codes.BaseCode TheObject = (DataContract.Common.Codes.BaseCode)this.SetDataFromRow(new DataContract.Common.Codes.BaseCode(), _Table.Rows[i]);
                TheList.Add(TheObject);
            }
            return TheList;
        }

        /// <summary>
        /// 获取通用代码对象列表
        /// </summary>
        /// <returns>获取的通用代码对象列表</returns>
        public List<BaseCode> Get_CommonCodesList(List<DataContract.Common.DataParamester> TheParamesterList)
        {
            DataTable Table = this._Get_DataTable(TheParamesterList);
            return this.Get_CommonCodesList(Table);
        }

        public string GetDMByMC(string _MC)
        {
            return this.GD.GetDataValue(String.Format("Select DM from {0} where  MC='{1}'", this.DataSource, _MC));
        }
    }
}
