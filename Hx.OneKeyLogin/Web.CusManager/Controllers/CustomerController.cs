using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.IService;
using Hx.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.CusManager.Controllers
{
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public IActionResult Index()
        {
            var customer = _customerService.GetModel(LoginInfo.Id);
            return View(customer);
        }
    }
}