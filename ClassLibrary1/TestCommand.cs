using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace TestMediatR
{
    public class TestCommand:IRequest<string>
    {
        public string test { get; set; }
    }
}
