using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace DataAccess
{
    public static class DAL_Contol
    {
        private static DataConnectionInfo GetConnectObject(DataContract.SystemName TheSystemName)
        {

            if (TheSystemName == DataContract.SystemName.ZFSystem)
            {
                return new DataConnectionInfo("Data Source=10.10.200.203/hhxydb;Persist Security Info=True;User ID=zfxfzb;PassWord=yqy*74tm ", "Oracle");
            }
            else if (TheSystemName == DataContract.SystemName.YKTMid)
            {
                return new DataConnectionInfo("Data Source=10.21.11.11/yktdb;Persist Security Info=True;User ID=smart_mid;PassWord=c*iECY1Tp ", "Oracle");
                // return new DataConnectionInfo("Data Source=10.21.11.6/dcdb;Persist Security Info=True;User ID=UOH_DTR;PassWord=UOH_DTR ", "Oracle");
            }
            else if (TheSystemName == DataContract.SystemName.RuijieDB)
            {
                return new DataConnectionInfo("Server=10.10.0.1;Database=SAMDB;User ID=sa;PassWord=hhxy@2018", "SQL");
            }
            return null;
        }

        private static DataConnectionInfo GetConnectObject(DataContract.DataTrans.DataTrans_DataSourceInfo TheDataSource)
        {
            return new DataConnectionInfo(TheDataSource.DATASOURCECONNECTIONSTRING, TheDataSource.DATASOURCETYPE.MC);
        }

        public static DAL_Base Get_GetDataObject(DataContract.SystemName TheSystemName)
        {
            DataConnectionInfo TheDataAccess = DAL_Contol.GetConnectObject(TheSystemName);
            if (TheDataAccess != null)
            {
                switch (TheDataAccess.DataBaseType)
                {
                    case "SQL": return new DAL_SQL(TheDataAccess);
                    case "Oracle": return new DAL_Oracle(TheDataAccess);
                    case "PostgreSQL": return new DAL_Postgre(TheDataAccess);
                }
            }
            return null;
        }

        public static DAL_Base Get_GetDataObject(DataContract.DataTrans.DataTrans_DataSourceInfo TheDataSource)
        {
            DataConnectionInfo TheDataAccess = DAL_Contol.GetConnectObject(TheDataSource);
            if (TheDataAccess != null)
            {
                switch (TheDataAccess.DataBaseType)
                {
                    case "SQL": return new DAL_SQL(TheDataAccess);
                    case "Oracle": return new DAL_Oracle(TheDataAccess);
                    case "PostgreSQL": return new DAL_Postgre(TheDataAccess);
                }
            }
            return null;
        }

        public static DAL_Base Get_GetDataObject()
        {
            DataConnectionInfo TheDataAccess = DAL_Contol.GetConnectObject(DataContract.SystemName.ZFSystem);
            return new DAL_Oracle(TheDataAccess);
        }
    }

    /// <summary>
    /// 数据库通讯类基类
    /// </summary>
    public abstract class DAL_Base
    {
        protected DataConnectionInfo DC;

        //检查连接是否关闭
        public void CheckConnectionAndClose()
        {
            if (this.DC.myConn.State == ConnectionState.Open)
            {
                this.DC.myConn.Close();
            }
        }

        //获取某一数据列表
        public abstract DataTable GetDataTable(string SQL);

        //获取某一数据列表(指定数据表)
        public abstract void GetDataTable(string SQL, DataTable Table);

        //获取某一数据值
        public abstract string GetDataValue(string SQL);

        //获取某一数据值
        public abstract string GetDataNumValue(string SQL);

        /// <summary>
        /// 暂不使用
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="Theparameteres"></param>
        /// <returns></returns>
        public abstract DataTable GetDataTableFormProcedure(string ProcedureName, List<DbParameter> Theparameteres);

        //验证是否存在某一项数据
        public abstract bool Validate(string SQL);

        public abstract bool UpdateTable(IDbCommand InsertCom, IDbCommand DeleteCom, IDbCommand UpdateCom, DataTable Table, DataTableMapping Mapping);

        public abstract bool Excute(string SQL);

        public abstract bool Excute(IDbCommand Comd);

        public abstract bool Excute(string SQL, ArrayList PName, ArrayList PValue);

        public abstract bool Excute(ArrayList SQL, ArrayList PName, ArrayList PValue, bool IsTransaction);

        public abstract bool Excute(string ProduceName, ArrayList TheParamesterList);

    }
}
