using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinFormsBug31415Sample.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Framework.Repositories;

namespace XamarinFormsBug31415Sample.Domain.Schedule
{
    public interface ISpeakerRepository : IRepository<Speaker>
    {
        Task<Speaker> FindSpeakerAsync(string id, Priorities priority = Priorities.Background);
        Task<IEnumerable<Speaker>> FindAllSpeakersAsync(Priorities priority = Priorities.Background);
        Task<IEnumerable<Speaker>> FindSpeakersByNameAsync(string name, Priorities priority = Priorities.Background);
    }
}