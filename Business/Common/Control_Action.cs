using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessControls.Common
{
    public class Control_Action : Control_BaseClass
    {
        private static string Key_MD5
        {
            get
            {
                return "2jIbSagUx_sRqP0g";
            }
        }

        private string Source
        {
            get;
            set;
        }

        public Control_Action()
        { this.GD = DataAccess.DAL_Contol.Get_GetDataObject(); }

        public int ControlAction(string UserName, string IP, string OPDescribe, string InvString)
        {
            if (!this.RecordOp(UserName, IP, OPDescribe, InvString))
            {
                return -1;
            }
            return 1;
        }

        private static bool MD5KeyChecking(string Key, string InvString)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider hashmd5 = new System.Security.Cryptography.MD5CryptoServiceProvider();

            if (String.Compare(Key, BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(String.Format("{0}{1}", InvString, Control_Action.Key_MD5)))).Replace("-", "").ToLower(), false) == 0)
            {
                return true;
            }
            return false;
        }

        public static string MD5OP(string SourceString)
        {
            return BitConverter.ToString(new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(String.Format("{0}{1}", SourceString.Trim(), Key_MD5)))).Replace("-", "").ToLower();
        }

        private bool RecordOp(string UserName, string IP, string OPDescribe, string RequestParameter)
        {
            return this.GD.Excute(String.Format("Insert into ServiceRequestLog( UserName, OPDescribe, SourceIP, RequestParameter, DateTime) values('{0}', '{1}','{2}','{3}', sysdate)", UserName, OPDescribe, IP, RequestParameter));
        }

        public static string GetInsertSQL(DataRow TheRow, string DataBaseType)
        {
            string _sqlInsert = "";
            string _sqlValue = "";
            //if (DataBaseType == "Oracle")
            //{
            for (int j = 0; j < TheRow.Table.Columns.Count; j++)
            {
                if ((TheRow.Table.Columns[j].ColumnName.ToString() == "UID" || TheRow.Table.Columns[j].ColumnName.ToString() == "uid") && DataBaseType == "Oracle")
                {
                    _sqlInsert += ", USID";
                }
                else
                {
                    _sqlInsert += "," + TheRow.Table.Columns[j].ColumnName.ToString();
                }

                if (String.Compare(TheRow.Table.Columns[j].DataType.Name, "Double", false) == 0 || String.Compare(TheRow.Table.Columns[j].DataType.Name, "Int32", false) == 0 || String.Compare(TheRow.Table.Columns[j].DataType.Name, "Int64", false) == 0 || String.Compare(TheRow.Table.Columns[j].DataType.Name, "Float", false) == 0)
                {
                    if (TheRow[j].ToString() == "")
                    {
                        _sqlValue += String.Format(", {0} ", "null");
                    }
                    else
                    {
                        _sqlValue += String.Format(", {0} ", TheRow[j]);
                    }
                }
                else if (String.Compare(TheRow.Table.Columns[j].DataType.Name, "DateTime", false) == 0 && DataBaseType == "Oracle")
                {
                    _sqlValue += String.Format(", to_date('{0}','yyyy/mm/dd hh24:mi:ss') ", TheRow[j]);
                }
                else if (String.Compare(TheRow.Table.Columns[j].DataType.Name, "Byte[]", false) == 0 && DataBaseType == "Oracle")
                {
                    _sqlValue += String.Format(",  :{0} ", TheRow.Table.Columns[j].ColumnName.ToString());
                }
                else
                {
                    _sqlValue += String.Format(",  '{0}' ", TheRow[j]);
                }
            }
            if (String.Compare(_sqlInsert, "", false) != 0 && _sqlValue != "")
            {
                _sqlInsert = _sqlInsert.Remove(0, 1);
                _sqlValue = _sqlValue.Remove(0, 1);
                return String.Format(" insert into {0}({1}) values( {2}) ", TheRow.Table.TableName, _sqlInsert, _sqlValue);
            }
            return "";
            //}
            //else if (DataBaseType == "PostGreSQL")
            //{

            //}
        }

        public static string GetUpdateSQL(DataRow TheRow, string _KeyField, string DataBaseType)
        {

            string _sqlUpdate = "";
            string _sqlp = " 1= 1 ";
            //if (DataBaseType == "Oracle")
            //{
            for (int j = 0; j < TheRow.Table.Columns.Count; j++)
            {
                if (TheRow.Table.Columns[j].ColumnName == _KeyField)
                {

                    if (String.Compare(TheRow.Table.Columns[j].DataType.Name, "Double", false) == 0 || String.Compare(TheRow.Table.Columns[j].DataType.Name, "Int32", false) == 0 || String.Compare(TheRow.Table.Columns[j].DataType.Name, "Int64", false) == 0 || String.Compare(TheRow.Table.Columns[j].DataType.Name, "Float", false) == 0 || String.Compare(TheRow.Table.Columns[j].DataType.Name, "Decimal", false) == 0)
                    {

                        if ((TheRow.Table.Columns[j].ColumnName.ToString() == "USID" || TheRow.Table.Columns[j].ColumnName.ToString() == "usid") && DataBaseType == "PostGreSQL")
                        {
                            _sqlp += " and UID= " + TheRow[j];
                        }
                        else
                        {
                            _sqlp += " and " + _KeyField + "= " + TheRow[j];
                        }

                    }
                    else
                    {
                        if ((TheRow.Table.Columns[j].ColumnName.ToString() == "USID" || TheRow.Table.Columns[j].ColumnName.ToString() == "usid") && DataBaseType == "PostGreSQL")
                        {
                            _sqlp += " and UID= '" + TheRow[j] + "' ";
                        }
                        else
                        {
                            _sqlp += " and " + _KeyField + "= '" + TheRow[j] + "'";
                        }

                    }
                }
                else if (TheRow[j] == DBNull.Value   )
                {
                    _sqlUpdate += "," + TheRow.Table.Columns[j].ColumnName.ToString() + " = null ";

                }
                else if (String.Compare(TheRow.Table.Columns[j].DataType.Name, "Double", false) == 0 || String.Compare(TheRow.Table.Columns[j].DataType.Name, "Int32", false) == 0 || String.Compare(TheRow.Table.Columns[j].DataType.Name, "Int64", false) == 0 || String.Compare(TheRow.Table.Columns[j].DataType.Name, "Float", false) == 0 || String.Compare(TheRow.Table.Columns[j].DataType.Name, "Decimal", false) == 0)
                {

                }
                else
                {
                    _sqlUpdate += "," + TheRow.Table.Columns[j].ColumnName.ToString() + " = '" + TheRow[j] + "' ";
                }
            }
            if (String.Compare(_sqlUpdate, "", false) != 0)
            {
                _sqlUpdate = _sqlUpdate.Remove(0, 1);

                return String.Format(" update {0} set {2} where 1 = 0 or({1}) ", TheRow.Table.TableName, _sqlp, _sqlUpdate);
            }

            return "";
            //}
            //else if (DataBaseType == "PostGreSQL")
            //{

            //}
        }
    }
}
