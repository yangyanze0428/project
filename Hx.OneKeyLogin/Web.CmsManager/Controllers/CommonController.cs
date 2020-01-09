using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.CmsManager.Helper;
using Web.CmsManager.Models;

namespace Web.CmsManager.Controllers
{
    public class CommonController : Controller
    {
        [Route("scheme")]
        public async Task<List<GridColumn>> Get(string scheme)
        {
            if (ModelSchemeHelper.Schemes.TryGetValue(scheme.ToLower(), out var cols))
            {
                //var cols = GridCreator.Create(t);
                return await Task.FromResult(cols);
            }
            return null;
        }
    }
}