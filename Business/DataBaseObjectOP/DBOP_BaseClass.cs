using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using DataContract;
using System.Data;
using System.Reflection;

namespace BusinessControls.DataBaseObjectOP
{
    public class DBOP_BaseClass : Control_BaseClass
    {
        protected virtual string DataUpdateProcedure
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        protected virtual string DataTargetTable
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        /// <summary>
        /// 不参与插入的字段，如自增字段、默认值字段
        /// </summary>
        protected virtual List<string> NoInsertField
        {
            get
            {
                return new List<string>();
            }

        }

        protected virtual List<string> Pkey
        {
            get
            { return null; }
        }

        //protected abstract bool Submit();
        //protected abstract bool Delete();s

        public bool Delete(DataContract.Common.BaseModelClass TheObject)
        {
            if (this.Pkey != null)
            {
                Type type = TheObject.GetType();
                string _sqlp = " 1= 1 ";
                for (int i = 0; i < this.Pkey.Count; i++)
                {
                    if ((type.GetProperty(this.Pkey[i])).PropertyType.Name == "System.Double" || (type.GetProperty(this.Pkey[i])).PropertyType.Name == "System.Int32" || (type.GetProperty(this.Pkey[i])).PropertyType.Name == "Int32" || (type.GetProperty(this.Pkey[i])).PropertyType.Name == "System.Float")
                    {
                        _sqlp += " and " + this.Pkey[i] + "= " + Control_BaseClass.GetPropertyValue(TheObject, this.Pkey[i]);
                    }
                    else
                    {
                        _sqlp += " and " + this.Pkey[i] + "= '" + Control_BaseClass.GetPropertyValue(TheObject, this.Pkey[i]) + "'";
                    }

                }
                return this.GD.Excute(String.Format(" delete from {0} where 1 = 0 or ({1}) ", this.DataTargetTable, _sqlp));
            }
            return false;
        }

        public bool Update(DataContract.Common.BaseModelClass TheObject)
        {
            if (this.Pkey != null)
            {
                Type type = TheObject.GetType();
                string _sqlp = " 1= 1 ";
                for (int i = 0; i < this.Pkey.Count; i++)
                {
                    if ((type.GetProperty(this.Pkey[i])).PropertyType.Name == "System.Double" || (type.GetProperty(this.Pkey[i])).PropertyType.Name == "System.Int32" || (type.GetProperty(this.Pkey[i])).PropertyType.Name == "Int32" || (type.GetProperty(this.Pkey[i])).PropertyType.Name == "System.Float")
                    {
                        _sqlp += " and " + this.Pkey[i] + "= " + Control_BaseClass.GetPropertyValue(TheObject, this.Pkey[i]);
                    }
                    else
                    {
                        _sqlp += " and " + this.Pkey[i] + "= '" + Control_BaseClass.GetPropertyValue(TheObject, this.Pkey[i]) + "'";
                    }
                }
                DataTable TheTable = this.GD.GetDataTable(String.Format(" select * from {0} where {1} ", this.DataTargetTable, _sqlp));
                if (TheTable != null)
                {
                    if (TheTable.Rows.Count > 0)
                    {
                        string _sqlUpdate = "";
                        for (int j = 0; j < TheTable.Columns.Count; j++)
                        {
                            if (Control_BaseClass.GetPropertyValue(TheObject, TheTable.Columns[j].ColumnName) is DataContract.Common.Codes.BaseCode)
                            {
                                _sqlUpdate += "," + TheTable.Columns[j].ColumnName.ToString() + " = '" + ((DataContract.Common.Codes.BaseCode)Control_BaseClass.GetPropertyValue(TheObject, TheTable.Columns[j].ColumnName)).DM + "' ";

                            }
                            else if ((type.GetProperty(TheTable.Columns[j].ColumnName)).PropertyType.FullName == "System.Double" || (type.GetProperty(TheTable.Columns[j].ColumnName)).PropertyType.FullName == "System.Int32" || (type.GetProperty(TheTable.Columns[j].ColumnName)).PropertyType.FullName == "System.Float")
                            {
                                _sqlUpdate += "," + TheTable.Columns[j].ColumnName.ToString() + " = " + Control_BaseClass.GetPropertyValue(TheObject, TheTable.Columns[j].ColumnName) + " ";
                            }
                            else if ((type.GetProperty(TheTable.Columns[j].ColumnName)).PropertyType.Name == "Boolean")
                            {
                                string _v = Control_BaseClass.GetPropertyValue(TheObject, TheTable.Columns[j].ColumnName).ToString();
                                if (_v == "True")
                                {
                                    _v = "1";
                                }
                                else
                                {
                                    _v = "0";
                                }
                                _sqlUpdate += "," + TheTable.Columns[j].ColumnName.ToString() + " = " + _v + " ";
                            
                            }

                            else
                            {
                                _sqlUpdate += "," + TheTable.Columns[j].ColumnName.ToString() + " = '" + Control_BaseClass.GetPropertyValue(TheObject, TheTable.Columns[j].ColumnName) + "' ";
                            }
                        }
                        if (_sqlUpdate != "")
                        {
                            _sqlUpdate = _sqlUpdate.Remove(0, 1);
                            return this.GD.Excute(String.Format(" update {0} set {2} where 1 = 0 or({1}) ", this.DataTargetTable, _sqlp, _sqlUpdate));
                        }
                    }
                    else
                    {
                        string _sqlInsert = "";
                        string _sqlValue = "";
                        for (int j = 0; j < TheTable.Columns.Count; j++)
                        {
                            bool NoInsert = false;
                            if (this.NoInsertField != null)
                            {
                                for (int k = 0; k < this.NoInsertField.Count; k++)
                                {
                                    if (this.NoInsertField[k] == TheTable.Columns[j].ColumnName)
                                    {
                                        NoInsert = true;
                                        break;
                                    }
                                }
                            }
                            if (NoInsert == false)
                            {
                                _sqlInsert += "," + TheTable.Columns[j].ColumnName.ToString();
                                if (Control_BaseClass.GetPropertyValue(TheObject, TheTable.Columns[j].ColumnName) is DataContract.Common.Codes.BaseCode)
                                {
                                    _sqlValue += ",  '" + ((DataContract.Common.Codes.BaseCode)Control_BaseClass.GetPropertyValue(TheObject, TheTable.Columns[j].ColumnName)).DM + "' ";
                                }
                                else if ((type.GetProperty(TheTable.Columns[j].ColumnName)).PropertyType.Name == "System.Double" || (type.GetProperty(TheTable.Columns[j].ColumnName)).PropertyType.Name == "System.Int32" || (type.GetProperty(TheTable.Columns[j].ColumnName)).PropertyType.Name == "System.Float")
                                {
                                    _sqlValue += ", " + Control_BaseClass.GetPropertyValue(TheObject, TheTable.Columns[j].ColumnName) + " ";
                                }
                                else if ((type.GetProperty(TheTable.Columns[j].ColumnName)).PropertyType.Name == "Boolean" )
                                {
                                    string _v = Control_BaseClass.GetPropertyValue(TheObject, TheTable.Columns[j].ColumnName).ToString();
                                    if (_v == "True")
                                    {
                                        _v = "1";
                                    }
                                    else
                                    {
                                        _v = "0";
                                    }

                                    _sqlValue += ", " + _v + " ";
                                }
                          
                                else
                                {
                                    _sqlValue += ",  '" + Control_BaseClass.GetPropertyValue(TheObject, TheTable.Columns[j].ColumnName) + "' ";
                                }
                            }
                        }
                        if (_sqlInsert != "" && _sqlValue != "")
                        {
                            _sqlInsert = _sqlInsert.Remove(0, 1);
                            _sqlValue = _sqlValue.Remove(0, 1);
                            return this.GD.Excute(String.Format(" insert into {0}({1}) values( {2}) ", this.DataTargetTable, _sqlInsert, _sqlValue));
                        }
                    }
                }
            }
            return false;
        }
    }
}
