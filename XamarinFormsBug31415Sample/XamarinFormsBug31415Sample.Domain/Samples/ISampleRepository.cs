using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinFormsBug31415Sample.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Framework.Repositories;

namespace XamarinFormsBug31415Sample.Domain.Samples
{
    public interface ISampleRepository : IRepository<Sample>
    {
        Task<Sample> FindSampleAsync(string id, Priorities priority = Priorities.Background);
        Task<IEnumerable<Sample>> FindSampleByNameAsync(string name, Priorities priority = Priorities.Background);
    }
}

