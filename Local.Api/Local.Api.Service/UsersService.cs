using Local.Api.Common.Entities;
using Local.Api.IRepository;
using Local.Api.IService;
using System;

namespace Local.Api.Service
{
    public class UsersService:BaseServices<Users>,IUsersService
    {
        IUsersRepository _repo;
        public UsersService(IUsersRepository userRepository)
        {
            _repo = userRepository;
            base.baseDal = userRepository;
        }
    }
}
