using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Handler
{
   public class SyncModel: IRequest<bool>
    {
        public int Method { get; set; }
        public string Body { get; set; }
    }
}
