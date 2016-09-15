using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamarinFormsBug31415Sample.Domain.Schedule;
using XamarinFormsBug31415Sample.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Framework.Repositories;

namespace XamarinFormsBug31415Sample.Infrastructure.Repositories.Memory
{
    public class MemorySpeakerRepository : MemoryRepository<Speaker>, ISpeakerRepository
    {
        static long s_lastKey;

        public MemorySpeakerRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork, u =>
            {
                ++s_lastKey;
                return s_lastKey.ToString();
            })
        {
            s_lastKey = 0;
        }

        public Task<IEnumerable<Speaker>> FindAllSpeakersAsync(Priorities priority = Priorities.Background)
        {
            return Task.FromResult((IEnumerable<Speaker>)Entities);
        }

        public Task<IEnumerable<Speaker>> FindSpeakersByNameAsync(string name, Priorities priority = Priorities.Background)
        {
            IEnumerable<Speaker> results = new List<Speaker>();
            results = Entities.Where(p => p.Name.Contains(name) && !string.IsNullOrWhiteSpace(name));

            return Task.FromResult(results);
        }

        Task<Speaker> ISpeakerRepository.FindSpeakerAsync(string id, Priorities priority = Priorities.Background)
        {
            return Task.FromResult(Entities.FirstOrDefault(s => s.Id == id));
        }
    }
}