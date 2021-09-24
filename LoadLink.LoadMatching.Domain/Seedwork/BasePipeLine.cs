using System;
using System.Collections.Generic;
using System.Text;

namespace LoadLink.LoadMatching.Domain.Seedwork
{
    public abstract class BasePipeLine<T>
    {
        private List<IFilter<IEnumerable<T>>> _filters = new List<IFilter<IEnumerable<T>>>();
        public void RegisterFilter(IFilter<IEnumerable<T>> filter)
        {
            _filters.Add(filter);
        }
        

    }
}
