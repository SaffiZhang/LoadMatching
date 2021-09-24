using System;
using System.Collections.Generic;
using System.Text;

namespace LoadLink.LoadMatching.Domain.Seedwork
{
    public interface IFilter<T>
    {
        IEnumerable<T> Execute(IEnumerable<T> lists);
    }
}
