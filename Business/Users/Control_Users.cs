using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataContract.Common;

namespace BusinessControls.Users
{
    //登录验证、密码修改等方法，当登录成功后，要返回一个User实例
    public class Control_Users : Control_BaseClass
    {
        /// <summary>
        /// 数据来源
        /// </summary>
        protected override string DataSource
        {
            get
            {
                return DataSourceList.Users_Source;
            }
        }

        //protected override string DataSource
        //{
        //    get
        //    {
        //        return DataSourceList.Users_Source;
        //    }
        //}


        public List<DataContract.Common.BaseModelClass> ToBaseModelClassList(List<DataContract.Users.User> TheSList)
        {
            return TheSList.Select(d => (DataContract.Common.BaseModelClass)d).ToList();
        }

        public Control_Users()
        { this.GD = DataAccess.DAL_Contol.Get_GetDataObject(); }

        /// <summary>
        /// 获取产品对象
        /// </summary>
        /// <param name="TheParamesterList">条件参数列表</param>
        /// <returns></returns>
        private List<DataContract.Users.User> Get_ModelObjectList(List<DataContract.Common.DataParamester> TheParamesterList)
        {
            DataTable Table = GD.GetDataTable(this.CreateSQLString(TheParamesterList));
            if (Table == null)
            {
                return null;
            }
            List<DataContract.Users.User> TheList = new List<DataContract.Users.User>();
            for (int i = 0; i < Table.Rows.Count; i++)
            {
                DataContract.Users.User TheObject = (DataContract.Users.User)this.SetDataFromRow(new DataContract.Users.User(), Table.Rows[i]);
                TheList.Add(TheObject);
            }
            return TheList;
        }

        /// <summary>
        /// 获取产品对象列表
        /// </summary>
        /// <returns>获取产品对象列表</returns>
        public List<DataContract.Users.User> Get_ObjectList()
        {
            return this.Get_ModelObjectList(new List<DataContract.Common.DataParamester>() { });
        }

        /// <summary>
        /// 获取产品对象列表
        /// </summary>
        /// <param name="TheParamesterList">条件参数列表</param>
        /// <returns>获取产品对象列表</returns>
        public List<DataContract.Users.User> Get_ObjectList(List<DataContract.Common.DataParamester> TheParamesterList)
        {
            return this.Get_ModelObjectList(TheParamesterList);
        }

        /// <summary>
        /// 获取用户对象
        /// </summary>
        /// <param name="ID">产品ID</param>
        public DataContract.Users.User GetObject(string _USERNAME)
        {
            List<DataContract.Users.User> TheList = this.Get_ModelObjectList(new List<DataParamester>() { new DataParamester() { PName = "U_USERNAME", PType = "String", PValue = _USERNAME, Operators = "=" } });
            ; if (TheList.Count == 1)
            {

                return TheList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="_OldPaw">旧密码</param>
        /// <param name="_NewPaw">新密码</param>
        /// <param name="_QRnewPaw">重复新密码</param>
        /// <param name="_TheUser">针对用户对象</param>
        /// <returns>处理结果提示</returns>
        public string ChangePassword(string _OldPaw, string _NewPaw, string _QRnewPaw, DataContract.Users.User _TheUser)
        {
            //三个空都不为空
            if (_OldPaw != "" && _NewPaw != "" && _QRnewPaw != "")
            {
                if (this.GD.GetDataNumValue(String.Format(" select count(*) from " + this.DataSource + " where U_UserName = '{0}' and  U_PassWord = '{1}'", _TheUser.U_USERNAME, Common.Control_Action.MD5OP(_OldPaw))) == "0")
                {
                    return "旧密码输入不正确";
                }

                //新密码长度判断
                if (_NewPaw.Length < 6 || _QRnewPaw.Length < 6)
                {
                    return "密码长度输入不合法，密码至少为6位";
                }

                //判断新旧密码是否一致   
                else if (_OldPaw == _NewPaw)
                {
                    return "新旧密码一样,请重新输入";

                }
                //两次密码输入不一致
                else if (_NewPaw != _QRnewPaw)
                {
                    return "两次输入密码不一致，请重新输入";
                }
                else
                {
                    string Pwd = Common.Control_Action.MD5OP(_QRnewPaw);

                    if (this.GD.Excute(String.Format("Update {0} SET U_PassWord='{1}' where U_UserName = '{2}'", DataSourceList.Users_TargetTable, Pwd, _TheUser.U_USERNAME)))
                    {
                        return "密码修改成功";
                    }
                    else
                    {
                        return "密码修改失败";
                    }
                }
            }

            else
            {
                return "密码不能为空！";
            }
        }

        /// <summary>
        /// 后台修改密码
        /// </summary>
        /// <param name="_NewPaw"></param>
        /// <param name="TheUser"></param>
        /// <returns></returns>
        public string ChangePassword(string _NewPaw, string _UserName)
        {
            string Pwd = Common.Control_Action.MD5OP(_NewPaw);

            if (this.GD.Excute(String.Format("Update {0} SET U_PassWord='{1}' where U_UserName = '{2}'", DataSourceList.Users_TargetTable, Pwd, _UserName)))
            {
                return "密码修改成功";
            }
            else
            {
                return "密码修改失败";
            }

        }

        /// <summary>
        /// 初始化密码
        /// </summary>
        /// <param name="_TheUser">针对用户对象</param>
        /// <returns>处理结果提示</returns>
        public static string InitializePassword(DataContract.Users.User _TheUser)
        {
            _TheUser.U_PASSWORD = Common.Control_Action.MD5OP(_TheUser.U_USERNAME);
            return "初始密码为用户名。";
        }

        /// <summary>
        /// 登录密码验证
        /// </summary>
        /// <param name="_UserName">用户名</param>
        /// <param name="_PassWord">密码</param>
        /// <param name="_SourceIP">登录IP</param>
        /// <returns>登录成功返回true；登录失败返回false</returns>
        public bool LoginChecking(string _UserName, string _PassWord, string _SourceIP)
        {
            if ((new Common.Control_Action().ControlAction(_UserName, _SourceIP, "尝试登录", "") == 1))
            {
                if (this.GD.GetDataNumValue(String.Format(" select count(*) from " + this.DataSource + " where U_UserName = '{0}' and  U_PassWord = '{1}'",
                                                _UserName,
                                                Common.Control_Action.MD5OP(_PassWord))) == "1")
                {
                    if (new Common.Control_Action().ControlAction(_UserName, _SourceIP, "尝试登录", "登录成功") == 1)
                    {
                        return true;
                    }
                }



                if (new Common.Control_Action().ControlAction(_UserName, _SourceIP, "尝试登录", "用户名密码错误") == 1)
                {

                }
            }
            return false;
        }





        /// <summary>
        /// 验证某用户是否已经存在于数据库中
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public bool IsExist(DataContract.Users.User _TheObject)
        {
            if (int.Parse(this.GD.GetDataNumValue(String.Format("Select count(*) from {0} where  U_UserName='{1}'", this.DataSource, _TheObject.U_USERNAME))) > 0)
            {
                return true;
            }
            return false;
        }


    }
}
