using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinFormsBug31415Sample.Infrastructure.Framework.Repositories;
using Splat;

namespace XamarinFormsBug31415Sample.Domain.Samples
{
    public class SampleService
    {
        ISampleRepository m_sampleRepository;

        public SampleService(ISampleRepository sampleRepository = null)
        {
            m_sampleRepository = sampleRepository ?? Locator.Current.GetService<ISampleRepository>();
        }

        public async Task<Sample> GetSample(string id, Priorities priority = Priorities.Background)
        {
            return await m_sampleRepository.FindSampleAsync(id, priority);
        }

        public async Task<IEnumerable<Sample>> GetSampleByNameAsync(string name, Priorities priority = Priorities.Background)
        {
            return await m_sampleRepository.FindSampleByNameAsync(name, priority);
        }
    }
}

