using Domain.Common;
using Domain.Common.Dtos.App;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IService
{
    public  interface IPreTestService
    {
        Task<Result> PreTest(PreTestDto dto);
    }
}
