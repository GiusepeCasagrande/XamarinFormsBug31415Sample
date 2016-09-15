using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinFormsBug31415Sample.Infrastructure.Framework.Repositories;
using Splat;

namespace XamarinFormsBug31415Sample.Domain.Schedule
{
    public class SpeakerService
    {
        ISpeakerRepository m_repository;

        public SpeakerService(ISpeakerRepository sampleRepository = null)
        {
            m_repository = sampleRepository ?? Locator.Current.GetService<ISpeakerRepository>();
        }

        public async Task<IEnumerable<Speaker>> GetAllSpeakersAsync(Priorities priority = Priorities.Background)
        {
            return await m_repository.FindAllSpeakersAsync(priority);
        }

        public async Task<Speaker> GetSpeakerAsync(string id, Priorities priority = Priorities.Background)
        {
            return await m_repository.FindSpeakerAsync(id, priority);
        }

        public async Task<IEnumerable<Speaker>> GetSpeakersByNameAsync(string name, Priorities priority = Priorities.Background)
        {
            return await m_repository.FindSpeakersByNameAsync(name, priority);
        }
    }
}
