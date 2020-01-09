using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Service.MediatHandler
{
    public class TestModelHandler : IRequestHandler<TestModel, bool>
    {
        public Task<bool> Handle(TestModel request, CancellationToken cancellationToken)
        {
            //Http请求
            return Task.FromResult(false);
        }
    }
}
