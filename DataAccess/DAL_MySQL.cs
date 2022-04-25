using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    /// <summary>
    /// 该类用于与 MySQL数据库通讯
    /// </summary>
    public class DAL_MySQL : DAL_Base
    {
              private MySqlDataAdapter Da;
        private MySqlCommand SelectCommand;

        //构造函数
        internal DAL_MySQL(DataConnectionInfo P_DC)
        {
            DC = P_DC;
            this.Da = new MySqlDataAdapter();
            this.SelectCommand = new MySqlCommand();
            this.Da.SelectCommand = this.SelectCommand;
            this.SelectCommand.Connection = (MySqlConnection)this.DC.myConn;
        }

        //获取某一数据列表
        public override DataTable GetDataTable(string MySql)
        {
            DataTable Table = new DataTable();
            this.SelectCommand.CommandText = MySql;
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

        //获取某一数据列表(指定数据表)
        public override void GetDataTable(string MySql, DataTable Table)
        {
            this.SelectCommand.CommandText = MySql;
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
        public override string GetDataValue(string MySql)
        {
            string Value;
            this.SelectCommand.CommandText = MySql;
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
        public override string GetDataNumValue(string MySql)
        {
            string Value;
            this.SelectCommand.CommandText = MySql;
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

        //验证是否存在某一项数据
        public override bool Validate(string MySql)
        {
            bool Value = false;
            this.SelectCommand.CommandText = MySql;
            try
            {
                this.DC.myConn.Open();
                MySqlDataReader Read = this.SelectCommand.ExecuteReader();
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
            MySqlDataAdapter MySqlDa = new MySqlDataAdapter();
            MySqlDa.TableMappings.Add(Mapping);
            MySqlDa.AcceptChangesDuringUpdate = true;
            InsertCom.Connection = this.DC.myConn;
            DeleteCom.Connection = this.DC.myConn;
            UpdateCom.Connection = this.DC.myConn;
            MySqlDa.InsertCommand = (MySqlCommand)InsertCom;
            MySqlDa.UpdateCommand = (MySqlCommand)UpdateCom;
            MySqlDa.DeleteCommand = (MySqlCommand)DeleteCom;
            try
            {
                MySqlDa.Update(Table);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Excute(string TableName, DataTable Table)
        {
            AutoCreatAdapterCommand_SQL AC = new AutoCreatAdapterCommand_SQL(TableName, "SQL Server", this.DC);
            this.Da.TableMappings.Add(AC.TableMapping);
            this.Da.AcceptChangesDuringUpdate = true;
            AC.InsertString.Connection = this.DC.myConn;
            AC.DeleteString.Connection = this.DC.myConn;
            AC.UpdateString.Connection = this.DC.myConn;
            this.Da.InsertCommand = (MySqlCommand)AC.InsertString;
            this.Da.UpdateCommand = (MySqlCommand)AC.UpdateString;
            this.Da.DeleteCommand = (MySqlCommand)AC.DeleteString;
            try
            {
                this.Da.Update(Table);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override bool Excute(string MySql)              //简单数据提交
        {
            this.SelectCommand.CommandText = MySql;
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

        public override bool Excute(string MySql, ArrayList PName, ArrayList PValue)
        {
            this.SelectCommand.CommandText = MySql;
            for (int i = 0; i < PName.Count; i++)
            {
                this.SelectCommand.Parameters.AddWithValue(PName[i].ToString(), (byte[])PValue[i]);
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

        public override bool Excute(ArrayList MySql, ArrayList PName, ArrayList PValue, bool IsTransaction)  //使用事务的批量数据提交
        {
            if (IsTransaction)
            {
                this.DC.myConn.Open();
                MySqlTransaction myTrans = ((MySqlConnection)this.DC.myConn).BeginTransaction();
                try
                {
                    for (int j = 0; j < MySql.Count; j++)
                    {
                        MySqlCommand TCommand = new MySqlCommand();
                        TCommand.CommandText = MySql[j].ToString();
                        TCommand.Connection = (MySqlConnection)this.DC.myConn;
                        TCommand.Transaction = myTrans;
                        for (int i = 0; i < ((ArrayList)PName[j]).Count; i++)
                        {
                            TCommand.Parameters.AddWithValue(((ArrayList)PName[j])[i].ToString(), (byte[])((ArrayList)PValue[j])[i]);
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
            for (int i = 0; i < TheParamesterList.Count; i++)
            {
                this.SelectCommand.Parameters.Add(new MySqlParameter(((DataContract.Common.DataParamester)TheParamesterList[i]).PName, ((DataContract.Common.DataParamester)TheParamesterList[i]).PValue));
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
