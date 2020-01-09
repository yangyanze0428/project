using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.CmsManager.Models
{
    public class MenuModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string State { get; set; } = "closed";
        public Dictionary<string, string> Attributes { get; } = new Dictionary<string, string>();

        public List<MenuModel> Children { get; } = new List<MenuModel>();
    }
}
