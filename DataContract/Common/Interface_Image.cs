using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Common
{
    public interface IImageObject
    {
        string IImageUrl
        {
            get;
            set;
        }

        byte[] IImageByte
        {
            get;
            set;
        }
    }
}
