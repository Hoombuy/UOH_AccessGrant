using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContract
{
    /// <summary> 
    /// ²Ù×÷ÈÕÖ¾ 
    /// </summary> 
    [Serializable]
    public class ServiceRequestLog : Common.BaseModelClass
    {
        /// <summary> 
        ///  
        /// </summary> 
        public string OPDESCRIBE { get; set; }
        /// <summary> 
        ///  
        /// </summary> 
        public string SOURCEIP { get; set; }
        /// <summary> 
        ///  
        /// </summary> 
        public string REQUESTPARAMETER { get; set; }
        /// <summary> 
        ///  
        /// </summary> 
        public string USERNAME { get; set; }
        /// <summary> 
        ///  
        /// </summary> 
        public string DATETIME { get; set; }
        public override string ToString()
        {
            return this.OPDESCRIBE.ToString();
        }
    }
}
