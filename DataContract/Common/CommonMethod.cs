using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContract.Common
{
    public static class CommonMethod
    {
        public static string GetParamesterListValueString(List<DataContract.Common.DataParamester> TheParamesterList)
        {
            string ValueString = "";
            if (TheParamesterList != null)
            {
                for (int i = 0; i < TheParamesterList.Count; i++)
                {
                    ValueString += TheParamesterList[i].PValue.ToString();
                }
            }
            return ValueString;
        }
    }
}