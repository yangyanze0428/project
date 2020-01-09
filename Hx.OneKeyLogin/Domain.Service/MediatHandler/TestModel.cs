using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.MediatHandler
{
    public class TestModel:IRequest<bool>
    {
        public string Name { get; set; }

        public string Password { get; set; }
    }
}
