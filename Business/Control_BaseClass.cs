using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using DataContract;
using System.Data;
using System.Reflection;

namespace BusinessControls
{
    public abstract class Control_BaseClass
    {
        protected DAL_Base GD;

        protected virtual string DataSource
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
        /// 默认排序规则
        /// </summary>
        protected virtual string DataSourceDefaultOrder
        {
            get
            {
                return "";
            }

        }

        protected static object GetPropertyValue(DataContract.Common.BaseModelClass TheObject, string PorpertyName)  //从String名称获取属性值
        {
            Type type = TheObject.GetType();
            if (type.GetProperty(PorpertyName) == null)
            {
                return null;
            }
            return (type.GetProperty(PorpertyName)).GetValue(TheObject, null);
        }

        protected static void SetPorpertyValue(DataContract.Common.BaseModelClass TheObject, string PorpertyName, object value) //从String名称设置属性值
        {
            Type type = TheObject.GetType();

            if (value != DBNull.Value && (type.GetProperty(PorpertyName)) != null)
            {
                if ((type.GetProperty(PorpertyName)).PropertyType.Name == "DateTime")
                {
                    try
                    {
                        value = DateTime.Parse(value.ToString());
                    }
                    catch
                    {
                        value = new DateTime(1900, 1, 1);
                    }
                }
                if ((type.GetProperty(PorpertyName)).PropertyType.FullName == "System.Double")
                {
                    try
                    {
                        value = Double.Parse(value.ToString());
                    }
                    catch
                    {
                        value = 0;
                    }

                }
                if ((type.GetProperty(PorpertyName)).PropertyType.FullName == "System.Int32")
                {
                    try
                    {
                        value = int.Parse(value.ToString());
                    }
                    catch
                    {
                        value = 0;
                    }
                }
                if ((type.GetProperty(PorpertyName)).PropertyType.FullName == "System.Boolean")
                {
                    try
                    {
                        if (value.ToString() == "1")
                        {
                            value = true;
                        }
                        else
                        {
                            value = false;
                        }
                    }
                    catch
                    {
                        value = 0;
                    }
                }
                try
                {
                    if (value.GetType().Name == "DateTime" && (type.GetProperty(PorpertyName)).PropertyType.Name != "DateTime")
                    {
                        (type.GetProperty(PorpertyName)).SetValue(TheObject, ((DateTime)value).ToString("yyyy-MM-dd hh:mm:ss"), null);
                    }
                    else
                    {
                        (type.GetProperty(PorpertyName)).SetValue(TheObject, value, null);
                    }
                }
                catch
                {

                }
            }
        }

        private static PropertyInfo[] GetPorperties(DataContract.Common.BaseModelClass TheObject)
        {
            Type type = TheObject.GetType();
            return type.GetProperties();
        }

        protected DataContract.Common.BaseModelClass SetDataFromRow(DataContract.Common.BaseModelClass TheObject, DataRow Row) //从数据行获取属性数据
        {
            PropertyInfo[] ThePList = Control_BaseClass.GetPorperties(TheObject);

            for (int i = 0; i < ThePList.GetLength(0); i++)
            {
                if (ThePList[i].PropertyType.IsSubclassOf(typeof(DataContract.Common.BaseModelClass)))
                {
                    DataContract.Common.BaseModelClass ThePObject = (DataContract.Common.BaseModelClass)Control_BaseClass.GetPropertyValue(TheObject, ThePList[i].Name);
                    if (ThePObject == null)
                    {
                        ThePObject = (DataContract.Common.BaseModelClass)Activator.CreateInstance(ThePList[i].PropertyType);
                    }
                    if (ThePList[i].PropertyType.IsSubclassOf(typeof(DataContract.Common.Codes.BaseCode)))
                    {
                        if (Row.Table.Columns.IndexOf(ThePList[i].Name) != -1)
                        {
                            Control_BaseClass.SetPorpertyValue(ThePObject, "DM", Row[ThePList[i].Name]);
                        }
                        if (Row.Table.Columns.IndexOf(ThePList[i].Name + "MC") != -1)
                        {
                            Control_BaseClass.SetPorpertyValue(ThePObject, "MC", Row[ThePList[i].Name + "MC"]);
                        }
                    }
                    if (ThePObject.GetType() != TheObject.GetType())
                    {
                        this.SetDataFromRow(ThePObject, Row);
                    }
                    Control_BaseClass.SetPorpertyValue(TheObject, ThePList[i].Name, ThePObject);
                }
                else
                {
                    if (Row.Table.Columns.IndexOf(ThePList[i].Name) != -1)
                    {
                        Control_BaseClass.SetPorpertyValue(TheObject, ThePList[i].Name, Row[ThePList[i].Name]);
                    }
                }
            }
            return TheObject;
        }

        protected string CreateSQLString(List<DataContract.Common.DataParamester> TheParamesterList)
        {
            string SQL = String.Format("select * from {0} where  1=1 ", this.DataSource);
            if (TheParamesterList != null)
            {
                for (int i = 0; i < TheParamesterList.Count; i++)
                {
                    if (TheParamesterList[i].PType.Contains("String") || TheParamesterList[i].PType.Contains("Char"))
                    {
                        if (TheParamesterList[i].Operators.Trim().Contains("like") == true)
                        {
                            SQL += String.Format(" and {0} {2} '%{1}%' ", TheParamesterList[i].PName, TheParamesterList[i].PValue, TheParamesterList[i].Operators);
                        }

                        else if (String.Compare(TheParamesterList[i].Operators.Trim(), "start", false) == 0)
                        {
                            SQL += String.Format(" and {0} like '{1}%' ", TheParamesterList[i].PName, TheParamesterList[i].PValue, TheParamesterList[i].Operators);
                        }
                        else
                        {
                            SQL += String.Format(" and {0} {2} '{1}' ", TheParamesterList[i].PName, TheParamesterList[i].PValue, TheParamesterList[i].Operators);
                        }
                    }
                    else if (TheParamesterList[i].PType.Contains("Int") || TheParamesterList[i].PType.Contains("Float"))
                    {
                        SQL += String.Format(" and {0} {2} {1} ", TheParamesterList[i].PName, TheParamesterList[i].PValue, TheParamesterList[i].Operators);
                    }
                    else if (TheParamesterList[i].PType.Contains("Date"))
                    {
                        SQL += String.Format(" and {0} {2} str_to_date('{1}','%Y-%m-%d %H:%i:%S') ", TheParamesterList[i].PName, TheParamesterList[i].PValue, TheParamesterList[i].Operators);
                    }
                    //else if (TheParamesterList[i].PType.Contains("Date2"))
                    //{
                    //    SQL += String.Format(" and  {0} {2} to_date('{1}','yyyy-MM-DD hh24:mi:ss') ", TheParamesterList[i].PName, TheParamesterList[i].PValue, TheParamesterList[i].Operators);
                    //}
                }
            }
            if (String.Compare(this.DataSourceDefaultOrder.Trim(), "", false) != 0)
            {
                SQL += " order by " + this.DataSourceDefaultOrder;
            }

            return SQL;
        }

        protected string CreateSQLString_Where(List<DataContract.Common.DataParamester> TheParamesterList)
        {
            string SQL = " 1=1 ";
            if (TheParamesterList != null)
            {
                for (int i = 0; i < TheParamesterList.Count; i++)
                {
                    if (TheParamesterList[i].PType.Contains("String") || TheParamesterList[i].PType.Contains("Char"))
                    {
                        if (String.Compare(TheParamesterList[i].Operators.Trim(), "like", false) == 0)
                        {
                            SQL += String.Format(" and {0} {2} '%{1}%' ", TheParamesterList[i].PName, TheParamesterList[i].PValue, TheParamesterList[i].Operators);
                        }
                        else
                        {
                            SQL += String.Format(" and {0} {2} '{1}' ", TheParamesterList[i].PName, TheParamesterList[i].PValue, TheParamesterList[i].Operators);
                        }
                    }
                    else if (TheParamesterList[i].PType.Contains("Int") || TheParamesterList[i].PType.Contains("Float"))
                    {
                        SQL += String.Format(" and {0} {2} {1} ", TheParamesterList[i].PName, TheParamesterList[i].PValue, TheParamesterList[i].Operators);
                    }
                    else if (TheParamesterList[i].PType.Contains("Date"))
                    {
                        SQL += String.Format(" and {0} {2} to_date('{1}','yyyy-MM-DD hh24:mi:ss') ", TheParamesterList[i].PName, TheParamesterList[i].PValue, TheParamesterList[i].Operators);
                    }
                    //else if (TheParamesterList[i].PType.Contains("Date2"))
                    //{
                    //    SQL += String.Format(" and to_date('{0}','yyyy-MM-DD hh24:mi:ss')  {2} to_date('{1}','yyyy-MM-DD hh24:mi:ss') ", TheParamesterList[i].PName, TheParamesterList[i].PValue, TheParamesterList[i].Operators);
                    //}
                }
            }

            return SQL;
        }

        public int Get_ObjectList_Count()
        {
            return int.Parse(this.GD.GetDataNumValue("select  count(*) as Value from " + this.DataSource));

        }

        public int Get_ObjectList_Count(List<DataContract.Common.DataParamester> TheParamesterList)
        {
            return int.Parse(this.GD.GetDataNumValue("select  count(*) as Value from " + this.DataSource + " where " + this.CreateSQLString_Where(TheParamesterList)));

        }


    }

    //interface IDataService_BaseMothed
    //{
    //    public DataContract.BaseModelClass Get_Object(string Key);
    //    public List<DataContract.BaseModelClass> Get_ObjectList();
    //}
}
