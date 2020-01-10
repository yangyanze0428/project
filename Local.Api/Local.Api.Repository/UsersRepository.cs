using Local.Api.Common.Entities;
using Local.Api.IRepository;
using Local.Api.Repository.sugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Local.Api.Repository
{
    public class UsersRepository : BaseRepository<Users>, IUsersRepository
    {
        
    }
}
