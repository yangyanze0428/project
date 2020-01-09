using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.CmsManager.Models
{
    [Serializable]
    public class GridColumn
    {
        public string Field { get; set; }
        public string Title { get; set; }
        public string Align { get; set; }
        public string HeadAlign { get; set; }

        public string Width { get; set; }

        public int Order { get; set; }

        public bool Hidden { get; set; }
    }
}
