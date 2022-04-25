using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.IO;
using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;


namespace BusinessControls
{
    public static class AutoCreateCode
    {
        public static string AutoCreateCommonClass(string _TableName, string _Path, string _ClassDescribe)
        {

            //Stream codeStream = File.Open("_TableName.cs", FileMode.Create);
            //StreamWriter codeWriter = new StreamWriter(codeStream);

            //CSharpCodeProvider provider = new CSharpCodeProvider();
            //ICodeGenerator codeGenerator = provider.CreateGenerator(codeWriter);
            //CodeGeneratorOptions codeGeneratorOptions = new CodeGeneratorOptions();


            //CodeSnippetCompileUnit literal = new CodeSnippetCompileUnit("using System;\nusing System.Data;\nusing System.Data.SqlClient;\n");
            //codeGenerator.GenerateCodeFromCompileUnit(literal, codeWriter, codeGeneratorOptions);
            //CodeNamespace codeNamespace = new CodeNamespace("DataContract");//

            //CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration();
            //codeTypeDeclaration.Name = _TableName; // 
            //codeTypeDeclaration.IsClass = true;
            //codeTypeDeclaration.TypeAttributes = TypeAttributes.Public;
            //codeNamespace.Types.Add(codeTypeDeclaration);

            //CodeConstructor codeConstrustor = new CodeConstructor();
            //codeConstrustor.Attributes = MemberAttributes.Public;
            //codeTypeDeclaration.Members.Add(codeConstrustor);

            CreateDataContractFile(_TableName, _Path, _ClassDescribe);
            CreateBusniessDBOPFile(_TableName, _Path, _ClassDescribe);
            CreateBusniessControlFile(_TableName, _Path, _ClassDescribe);
            CreatePageInfoFile(_TableName, _Path, _ClassDescribe);
            CreatePageEditFile(_TableName, _Path, _ClassDescribe);
            return "成功";

        }

        private static string CreateDataContractFile(string _TableName, string _Path, string _ClassDescribe)
        {
            string _DataContractClass = "using System;   \n " +
                                                   "using System.Collections.Generic;  \n " +
                                                   "using System.Linq;  \n " +
                                                   "using System.Text;  \n \n" +
                                                   "namespace DataContract  \n" +
                                                   "{ \n" +
                                                   "/// <summary> \n " +
                                                   "/// " + _ClassDescribe + " \n " +
                                                   "/// </summary> \n " +
                                                   "public class " + _TableName + " : Common.BaseModelClass \n " +
                                                   "{  \n";
            var GD = DataAccess.DAL_Contol.Get_GetDataObject();


            if (GD.GetType().ToString().Contains("DAL_Oracle"))
            {
                DataTable _TheTable = GD.GetDataTable(String.Format("select a.*, b.comments from  user_tab_columns  a left outer join  user_col_comments b on a.table_name = b.TABLE_NAME and a.column_name = b.COLUMN_NAME   where   a.Table_Name = '{0}'   ", _TableName));
                if (_TheTable != null)
                {
                    for (int i = 0; i < _TheTable.Rows.Count; i++)
                    {
                        string _type = "string";
                        if (_TheTable.Rows[i]["COMMENTS"].ToString().Contains("NUMBER"))
                        {
                            _type = "double";
                        }
                        _DataContractClass += "    /// <summary> \n" +
                                                                "/// " + _TheTable.Rows[i]["COMMENTS"].ToString().Trim() + " \n" +
                                                                "/// </summary> \n" +
                                                                "public " + _type + " " + _TheTable.Rows[i]["COLUMN_NAME"].ToString() + "{ get; set; } \n";
                    }


                    _DataContractClass += "   public override string ToString()  \n " +
                                                   " { \n " +
                                                   " return this." + _TheTable.Rows[0]["COLUMN_NAME"].ToString() + ".ToString(); \n " +
                                                   " } \n ";

                }
            }

            _DataContractClass += "} \n " +
                                                  "} \n";
            FileInfo file = new FileInfo(_Path + "/" + _TableName + ".cs");
            //创建文件 
            FileStream fs = file.Create();

            fs.Write(System.Text.Encoding.Default.GetBytes(_DataContractClass), 0, System.Text.Encoding.Default.GetBytes(_DataContractClass).Length);
            //关闭文件流 
            fs.Close();


            return _DataContractClass;
        }

        private static string CreateBusniessDBOPFile(string _TableName, string _Path, string _ClassDescribe)
        {
            string _DBOP_Class = "using System;   \n " +
                                                   "using System.Collections.Generic;  \n " +
                                                   "using System.Linq;  \n " +
                                                   "using System.Text;  \n \n" +
                                                   "namespace BusinessControls.DataBaseObjectOP \n" +
                                                   "{\n" +
                                                   "/// <summary> \n " +
                                                   "/// " + _ClassDescribe + "数据提交操作类 \n " +
                                                   "/// </summary> \n " +
                                                   "public class DBOP_" + _TableName + " : DBOP_BaseClass \n " +
                                                   "{  \n" +
                                                   "  /// <summary> \n" +
                                                    "/// 数据来源 \n" +
                                                    "/// </summary> \n" +
                                                    "protected override string DataTargetTable  \n" +
                                                    "{ \n" +
                                                        "get \n" +
                                                        "{  \n" +
                                                           " return DataSourceList." + _TableName + "_TargetTable; \n" +
                                                        "} \n" +
                                                    "} \n" +
                                                         " protected override List<string> Pkey \n" +
                                                       "{ \n" +
                                                           "get \n" +
                                                           "{ \n" +
                                                               "return new List<string>() { \"手工填入\" }; \n" +
                                                           "} \n" +
                                                      " } \n" +

                                                       "public DBOP_" + _TableName + "() \n" +
                                                       "{ this.GD = (new DataAccess.DAL_Contol()).Get_GetDataObject(); } \n" +
                                                  " } \n" +
                                               "} \n";



            FileInfo file = new FileInfo(_Path + "/DBOP_" + _TableName + ".cs");
            //创建文件 
            FileStream fs = file.Create();

            fs.Write(System.Text.Encoding.Default.GetBytes(_DBOP_Class), 0, System.Text.Encoding.Default.GetBytes(_DBOP_Class).Length);
            //关闭文件流 
            fs.Close();

            return _DBOP_Class;
        }

        private static string CreateBusniessControlFile(string _TableName, string _Path, string _ClassDescribe)
        {
            string _Control_Class = string.Format("using System;   \n " +
                                                   "using System.Collections.Generic;  \n " +
                                                   "using System.Linq;  \n " +
                                                   "using System.Text;   \n" +
                                                   "using System.Data; \n" +
                                                   "using DataContract.Common; \n" +
                                                    "namespace BusinessControls \n" +
                                                   "{{ \n\n" +
                                                   "/// <summary> \n " +
                                                   "/// {1}数据操作类 \n " +
                                                   "/// </summary> \n " +
                                                   "public class Control_{0} : Control_BaseClass \n " +
                                                   "{{   \n" +
                                                   "  /// <summary> \n" +
                                                    "/// 数据来源 \n" +
                                                    "/// </summary> \n" +
                                                    "protected override string DataSource  \n" +
                                                    "{{ \n" +
                                                        "get \n" +
                                                        "{{   \n" +
                                                           " return DataSourceList.{0}_Source; \n" +
                                                        "}} \n" +
                                                    "}} \n\n" +

                                                       "public List<DataContract.Common.BaseModelClass> ToBaseModelClassList (List<DataContract.{0} > TheSList) \n" +
                                                       " {{ \n" +
                                                           " return TheSList.Select(d => (DataContract.Common.BaseModelClass)d).ToList(); \n" +
                                                        "}} \n" +
                                                         "\n" +
                                                        "public Control_{0}() \n" +
                                                        "{{ this.GD = (new DataAccess.DAL_Contol()).Get_GetDataObject(); }} \n\n" +


                                                         "/// <summary> \n" +
                                                         "/// 获取{1}对象 \n" +
                                                         "/// </summary> \n" +
                                                         "/// <param name=\"TheParamesterList\">条件参数列表</param> \n" +
                                                         "/// <returns></returns> \n" +
                                                        "private List<DataContract.{0}> Get_ModelObjectList(List<DataContract.Common.DataParamester> TheParamesterList) \n" +
                                                        "{{ \n" +
                                                          "  DataTable Table = GD.GetDataTable(this.CreateSQLString(TheParamesterList)); \n" +
                                                            "if (Table == null) \n" +
                                                            "{{ \n" +
                                                             "   return null; \n" +
                                                            "}} \n" +
                                                             "return this.Get_ModelObjectList(Table); \n" +
                                                        "}} \n\n" +

                                                        "private List<DataContract.{0}> Get_ModelObjectList(DataTable TheTable) \n" +
                                                          "{{ \n" +
                                                              "List <DataContract.{0}> TheList = new List<DataContract.{0}>(); \n" +
                                                              "for (int i = 0; i < TheTable.Rows.Count; i++) \n" +
                                                              "{{ \n" +
                                                                  "DataContract.{0} TheObject = (DataContract.{0})this.SetDataFromRow(new DataContract.{0}(), TheTable.Rows[i]); \n" +
                                                                  "TheList.Add(TheObject); \n" +
                                                              "}} \n" +
                                                              "return TheList; \n" +
                                                         " }} \n\n" +

                                                                  "/// <summary> \n" +
                                                                  "/// 获取{1}对象列表 \n" +
                                                                  "/// </summary> \n" +
                                                                  "/// <returns>获取对象列表</returns> \n" +
                                                                "public List<DataContract.{0}> Get_ObjectList() \n" +
                                                                 "{{ \n" +
                                                                     "return this.Get_ModelObjectList(new List<DataContract.Common.DataParamester>() {{ }}); \n" +
                                                                 "}} \n" +
                                                                  " \n" +
                                                                 "/// <summary> \n" +
                                                                 "/// 获取{1}对象列表 \n" +
                                                                 "/// </summary> \n" +
                                                                 "/// <param name=\"TheParamesterList\">条件参数列表</param> \n" +
                                                                 "/// <returns>获取对象列表</returns> \n" +
                                                                 "public List<DataContract.{0}> Get_ObjectList(List<DataContract.Common.DataParamester> TheParamesterList) \n" +
                                                                "{{ \n" +
                                                                     "return this.Get_ModelObjectList(TheParamesterList); \n" +
                                                                 "}} \n\n" +

                                                                 "/// <summary> \n" +
                                                                 "/// 获取{1}对象 \n" +
                                                                 "/// </summary> \n" +
                                                                 "public DataContract.{0} GetObject(string _ID) \n" +
                                                                 "{{ \n" +
                                                                     "List<DataContract.{0}> TheList = this.Get_ModelObjectList(new List<DataParamester>() {{ new DataParamester() {{ PName = \"手工填入\", PType = \"String\", PValue = _ID.ToString(), Operators = \"= \" }} }}); \n" +
                                                                     "if (TheList.Count == 1) \n" +
                                                                     "{{ \n" +
                                                                        " return TheList[0]; \n" +
                                                                    " }} \n" +
                                                                     "else \n" +
                                                                     "{{ \n" +
                                                                         "return null; \n" +
                                                                     "}} \n" +
                                                                 "}} \n\n" +


                                                  " }} \n" +
                                               "}} \n", _TableName, _ClassDescribe);



            FileInfo file = new FileInfo(_Path + "/Control_" + _TableName + ".cs");
            //创建文件 
            FileStream fs = file.Create();

            fs.Write(System.Text.Encoding.Default.GetBytes(_Control_Class), 0, System.Text.Encoding.Default.GetBytes(_Control_Class).Length);
            //关闭文件流 
            fs.Close();

            return _Control_Class;
        }

        private static string CreatePageInfoFile(string _TableName, string _Path, string _ClassDescribe)
        {
            string _DataPageInfoClass = string.Format(
                                                   "<%@ Control Language = \"C#\" AutoEventWireup = \"true\" CodeBehind = \"UCT_{0}_Info.ascx.cs\"  Inherits = \"WebInterface.UCT_{0}_Info\" %> \n " +
                                                   "<%@ Register Src = \"~/CommonControls/PageItem/UCT_PageItem_Text.ascx\" TagName = \"UCT_PageItem_Text\" TagPrefix = \"uc1\" %> \n " +
                                                   "<%@ Register Src = \"~/CommonControls/PageItem/UCT_PageItem_ImageShow.ascx\" TagName = \"UCT_PageItem_ImageShow\" TagPrefix = \"uc2\" %>     \n " +
                                                   "<div style = \"margin-left: 40px; margin-right: 40px; \" > \n " +
                                                    "        <div class=\"  \" style=\"background-color: #FFFFFF; margin-bottom: 10px; padding: 15px;\"> \n ", _TableName);

            var GD = DataAccess.DAL_Contol.Get_GetDataObject();


            if (GD.GetType().ToString().Contains("DAL_Oracle"))
            {
                DataTable _TheTable = GD.GetDataTable(String.Format("select a.*, b.comments from  user_tab_columns  a left outer join  user_col_comments b on a.table_name = b.TABLE_NAME and a.column_name = b.COLUMN_NAME   where   a.Table_Name = '{0}'   ", _TableName));
                if (_TheTable != null)
                {
                    for (int i = 0; i < _TheTable.Rows.Count; i++)
                    {
                        _DataPageInfoClass += string.Format("<uc1:UCT_PageItem_Text ID = \"DataItem_{0}\" runat = \"server\" LabelText = \" {1}：\" TextWidth = \"200\" /> \n", _TheTable.Rows[i]["COLUMN_NAME"].ToString(), _TheTable.Rows[i]["COMMENTS"].ToString());
                    }
                }
            }

            _DataPageInfoClass += "</div> \n  </div> \n ";

            FileInfo file = new FileInfo(_Path + "/UCT_" + _TableName + "_Info.TXT");
            //创建文件 
            FileStream fs = file.Create();

            fs.Write(System.Text.Encoding.Default.GetBytes(_DataPageInfoClass), 0, System.Text.Encoding.Default.GetBytes(_DataPageInfoClass).Length);
            //关闭文件流 
            fs.Close();


            return _DataPageInfoClass;
        }

        private static string CreatePageEditFile(string _TableName, string _Path, string _ClassDescribe)
        {
            string _DataPageEditClass = string.Format(
                                                   "<%@ Control Language = \"C#\" AutoEventWireup = \"true\" CodeBehind = \"UCT_{0}_Edit.ascx.cs\"  Inherits = \"WebInterface.UCT_{0}_Edit\" %> \n " +
                                                   "<%@ Register Src = \"~/CommonControls/PageItem/UCT_PageItem_Edit.ascx\" TagName = \"UCT_PageItem_Edit\" TagPrefix = \"uc1\" %> \n " +
                                                   "<div style = \"margin-left: 40px; margin-right: 40px; \" > \n " +
                                                    "        <div class=\"  \" style=\"background-color: #FFFFFF; margin-bottom: 10px; padding: 15px;\"> \n ", _TableName);

            var GD = DataAccess.DAL_Contol.Get_GetDataObject();


            if (GD.GetType().ToString().Contains("DAL_Oracle"))
            {
                DataTable _TheTable = GD.GetDataTable(String.Format("select a.*, b.comments from  user_tab_columns  a left outer join  user_col_comments b on a.table_name = b.TABLE_NAME and a.column_name = b.COLUMN_NAME   where   a.Table_Name = '{0}'   ", _TableName));
                if (_TheTable != null)
                {
                    for (int i = 0; i < _TheTable.Rows.Count; i++)
                    {
                        _DataPageEditClass += string.Format("<uc1:UCT_PageItem_Edit ID = \"DataItem_{0}\" runat = \"server\" LabelText = \" {1}：\" TextWidth = \"200\" /> \n", _TheTable.Rows[i]["COLUMN_NAME"].ToString(), _TheTable.Rows[i]["COMMENTS"].ToString());
                    }
                }
            }

            _DataPageEditClass += "</div> \n  </div> \n ";

            FileInfo file = new FileInfo(_Path + "/UCT_" + _TableName + "_Edit.TXT");
            //创建文件 
            FileStream fs = file.Create();

            fs.Write(System.Text.Encoding.Default.GetBytes(_DataPageEditClass), 0, System.Text.Encoding.Default.GetBytes(_DataPageEditClass).Length);
            //关闭文件流 
            fs.Close();


            return _DataPageEditClass;
        }
    }
}
