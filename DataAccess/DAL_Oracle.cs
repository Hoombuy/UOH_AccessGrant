using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;
using System.Collections;

namespace DataAccess
{
    /// <summary>
    /// 该类用于与Oracle数据库通讯
    /// </summary>
    public class DAL_Oracle : DAL_Base
    {
        private OracleDataAdapter Da;
        private OracleCommand SelectCommand;

        //构造函数
        public DAL_Oracle(DataConnectionInfo P_DC)
        {
            DC = P_DC;
            this.Da = new OracleDataAdapter();
            this.SelectCommand = new OracleCommand();
            this.Da.SelectCommand = this.SelectCommand;
            this.SelectCommand.Connection = (OracleConnection)this.DC.myConn;
        }

        //获取某一数据列表
        public override DataTable GetDataTable(string Oracle)
        {
            using (DataTable Table = new DataTable())
            {
                this.SelectCommand.CommandText = Oracle;
                try
                {
                    this.Da.Fill(Table);
                    return Table;
                }
                catch
                {
                    this.CheckConnectionAndClose();
                    return null;
                }
            }
        }

        //获取某一数据列表(指定数据表)
        public override void GetDataTable(string Oracle, DataTable Table)
        {
            this.SelectCommand.CommandText = Oracle;
            try
            {
                this.Da.Fill(Table);
            }
            catch
            {
                this.CheckConnectionAndClose();
            }
        }

        //获取某一数据值
        public override string GetDataValue(string Oracle)
        {
            string Value;
            this.SelectCommand.CommandText = Oracle;
            try
            {
                this.DC.myConn.Open();
                Value = this.SelectCommand.ExecuteScalar().ToString();
                this.DC.myConn.Close();
                return Value;
            }
            catch
            {
                this.CheckConnectionAndClose();
                return "";
            }
        }

        //获取某一数据值
        public override string GetDataNumValue(string Oracle)
        {
            string Value;
            this.SelectCommand.CommandText = Oracle;
            try
            {
                this.DC.myConn.Open();
                Value = this.SelectCommand.ExecuteScalar().ToString();
                this.DC.myConn.Close();
                if (Value == "")
                {
                    return "0";
                }
                return Value;
            }
            catch
            {
                this.CheckConnectionAndClose();
                return "0";
            }
        }

        /// <summary>
        /// 暂不使用
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="Theparameteres"></param>
        /// <returns></returns>
        public override DataTable GetDataTableFormProcedure(string ProcedureName, List<DbParameter> Theparameteres)
        {
            DataTable Table = new DataTable();
            this.SelectCommand.CommandText = ProcedureName;
            this.SelectCommand.CommandType = CommandType.StoredProcedure;

            for (int i = 0; i < Theparameteres.Count; i++)
            {
                this.SelectCommand.Parameters.Add(Theparameteres[i]);
            }

            try
            {
                this.Da.Fill(Table);
                return Table;
            }
            catch
            {
                this.CheckConnectionAndClose();
                return null;
            }
            finally
            {
                this.SelectCommand.Parameters.Clear();
                this.SelectCommand.CommandType = CommandType.Text;
            }
        }

        /// <summary>
        /// 通过存储过程获取数据表
        /// </summary>
        /// <param name="ProduceName"></param>
        /// <param name="TheParamesterList"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string ProduceName, ArrayList TheParamesterList)
        {
            DataSet theset = new DataSet();
            this.SelectCommand.CommandText = ProduceName;
            this.SelectCommand.CommandType = CommandType.StoredProcedure;

            for (int i = 0; i < TheParamesterList.Count; i++)
            {
                this.SelectCommand.Parameters.Add(new OracleParameter(((DataContract.DataParamester)TheParamesterList[i]).PName, ((DataContract.DataParamester)TheParamesterList[i]).PValue));
            }
            //this.SelectCommand.Parameters.Add("jg", OracleType.Cursor).Direction = ParameterDirection.Output;
            //this.Da.TableMappings.Add("Table", "GXKLB");

            try
            {
                this.SelectCommand.Connection.Open();
                this.Da.Fill(theset);
                this.SelectCommand.Connection.Close();
                this.SelectCommand.CommandType = CommandType.Text;
                this.SelectCommand.Parameters.Clear();
                this.Da.TableMappings.Clear();
                return theset.Tables[0];
            }
            catch
            {
                this.SelectCommand.CommandType = CommandType.Text;
                this.SelectCommand.Parameters.Clear();
                this.Da.TableMappings.Clear();
                return null;
            }
        }

        /// <summary>
        /// 验证是否存在某一项数据
        /// </summary>
        /// <param name="Oracle"></param>
        /// <returns></returns>
        public override bool Validate(string Oracle)
        {
            bool Value = false;
            this.SelectCommand.CommandText = Oracle;
            try
            {
                this.DC.myConn.Open();
                OracleDataReader Read = this.SelectCommand.ExecuteReader();
                if (Read.Read())
                {
                    Value = true;
                }
                this.DC.myConn.Close();
            }
            catch
            {
                this.CheckConnectionAndClose();
            }
            return Value;
        }

        public override bool UpdateTable(IDbCommand InsertCom, IDbCommand DeleteCom, IDbCommand UpdateCom, DataTable Table, DataTableMapping Mapping)    //数据适配器数据提交
        {
            OracleDataAdapter OracleDa = new OracleDataAdapter();
            OracleDa.TableMappings.Add(Mapping);
            OracleDa.AcceptChangesDuringUpdate = true;
            InsertCom.Connection = this.DC.myConn;
            DeleteCom.Connection = this.DC.myConn;
            UpdateCom.Connection = this.DC.myConn;
            OracleDa.InsertCommand = (OracleCommand)InsertCom;
            OracleDa.UpdateCommand = (OracleCommand)UpdateCom;
            OracleDa.DeleteCommand = (OracleCommand)DeleteCom;
            try
            {
                OracleDa.Update(Table);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override bool Excute(string Oracle)              //简单数据提交
        {
            this.SelectCommand.CommandText = Oracle;
            try
            {
                this.DC.myConn.Open();
                this.SelectCommand.ExecuteNonQuery();
                this.DC.myConn.Close();
                return true;
            }
            catch
            {
                this.CheckConnectionAndClose();
                return false;
            }
        }

        public override bool Excute(IDbCommand Comd)              //简单数据提交
        {
            Comd.Connection = this.DC.myConn;
            try
            {
                this.DC.myConn.Open();
                Comd.ExecuteNonQuery();
                this.DC.myConn.Close();
                return true;
            }
            catch
            {
                this.CheckConnectionAndClose();
                return false;
            }
        }

        public override bool Excute(string Oracle, ArrayList PName, ArrayList PValue)
        {
            this.SelectCommand.CommandText = Oracle;
            for (int i = 0; i < PName.Count; i++)
            {
                this.SelectCommand.Parameters.Add(PName[i].ToString(), (byte[])PValue[i]);
            }

            try
            {
                this.DC.myConn.Open();
                this.SelectCommand.ExecuteNonQuery();
                this.DC.myConn.Close();
                return true;
            }
            catch
            {
                this.CheckConnectionAndClose();
                return false;
            }
        }     //使用二进制参数的数据提交

        public override bool Excute(ArrayList Oracle, ArrayList PName, ArrayList PValue, bool IsTransaction)  //使用事务的批量数据提交
        {
            if (IsTransaction)
            {
                this.DC.myConn.Open();
                OracleTransaction myTrans = ((OracleConnection)this.DC.myConn).BeginTransaction();
                try
                {
                    for (int j = 0; j < Oracle.Count; j++)
                    {
                        OracleCommand TCommand = new OracleCommand();
                        TCommand.CommandText = Oracle[j].ToString();
                        TCommand.Connection = (OracleConnection)this.DC.myConn;
                        TCommand.Transaction = myTrans;
                        for (int i = 0; i < ((ArrayList)PName[j]).Count; i++)
                        {
                            TCommand.Parameters.Add(((ArrayList)PName[j])[i].ToString(), (byte[])((ArrayList)PValue[j])[i]);
                        }
                        TCommand.ExecuteNonQuery();
                    }
                    myTrans.Commit();
                    this.DC.myConn.Close();
                    return true;
                }
                catch
                {
                    myTrans.Rollback();
                    this.CheckConnectionAndClose();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public override bool Excute(string ProduceName, ArrayList TheParamesterList)
        {
            this.SelectCommand.CommandText = ProduceName;
            this.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (TheParamesterList != null)
            {
                for (int i = 0; i < TheParamesterList.Count; i++)
                {
                    this.SelectCommand.Parameters.Add(new OracleParameter(((DataContract.DataParamester)TheParamesterList[i]).PName, ((DataContract.DataParamester)TheParamesterList[i]).PValue));
                }
            }
            try
            {
                this.SelectCommand.Connection.Open();
                this.SelectCommand.ExecuteNonQuery();
                this.SelectCommand.Connection.Close();
                this.SelectCommand.CommandType = CommandType.Text;
                this.SelectCommand.Parameters.Clear();
                this.Da.TableMappings.Clear();
                return true;
            }
            catch
            {
                this.CheckConnectionAndClose();
                this.SelectCommand.CommandType = CommandType.Text;
                this.SelectCommand.Parameters.Clear();
                this.Da.TableMappings.Clear();
                return false;
            }
        }
    }


}
