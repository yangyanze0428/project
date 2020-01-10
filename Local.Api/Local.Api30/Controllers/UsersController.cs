using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Local.Api.Common.Entities;
using Local.Api.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Local.Api30.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        // GET: api/Users
        [HttpGet]
        public async   Task<List<Users>> GetList()
        {
            var rlt =await _usersService.Query();
            return rlt;
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Users> Get(string id)
        {
            var rlt = await _usersService.QueryByID(id);
            return rlt;
        }

        // POST: api/Users
        [HttpPost]
        public async Task<int> Post(Users entity)
        {
            var rlt =await _usersService.Add(entity);
            return rlt;
        }

        // PUT: api/Users/5
        [HttpPut]
        public async Task<bool> Put([FromBody] Users entity)
        {
            var rlt = await _usersService.Update(entity);
            return rlt;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            var rlt = await _usersService.DeleteById(id);
            return rlt;
        }
    }
}
