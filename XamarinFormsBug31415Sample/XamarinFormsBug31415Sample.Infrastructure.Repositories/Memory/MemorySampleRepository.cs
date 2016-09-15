using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamarinFormsBug31415Sample.Domain.Samples;
using XamarinFormsBug31415Sample.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Framework.Repositories;


namespace XamarinFormsBug31415Sample.Infrastructure.Repositories.Memory
{
    public class MemorySampleRepository : MemoryRepository<Sample>, ISampleRepository
    {
        private static long s_lastKey;

        public MemorySampleRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork, u =>
            {
                ++s_lastKey;
                return s_lastKey.ToString();
            })
        {
            s_lastKey = 0;
        }

        public Task<Sample> FindSampleAsync(string id, Priorities priority = Priorities.Background)
        {
            Sample result = Entities.FirstOrDefault(p => p.Id == id);

            return Task.FromResult(result);
        }

        public Task<IEnumerable<Sample>> FindSampleByNameAsync(string name, Priorities priority = Priorities.Background)
        {
            IEnumerable<Sample> results = new List<Sample>();
            results = Entities.Where(p => p.Name.Contains(name) && !string.IsNullOrWhiteSpace(name));

            return Task.FromResult(results);
        }
    }
}