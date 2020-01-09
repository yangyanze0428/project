using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Service.Handler
{
  public  class SyncHandel: IRequestHandler<SyncModel,bool>
    {
        public Task<bool> Handle(SyncModel request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
