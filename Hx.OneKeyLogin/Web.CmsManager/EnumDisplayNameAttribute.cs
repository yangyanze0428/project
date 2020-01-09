using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.CmsManager
{
    public class EnumDisplayNameAttribute : Attribute
    {
        private string _displayName;



        public EnumDisplayNameAttribute(string displayName)

        {

            this._displayName = displayName;

        }



        public string DisplayName

        {

            get { return _displayName; }

        }
    }
}
