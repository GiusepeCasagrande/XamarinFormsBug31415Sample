using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinFormsBug31415Sample.Domain.Samples;
using XamarinFormsBug31415Sample.Infrastructure.Framework.Repositories;
using XamarinFormsBug31415Sample.Infrastructure.Repositories.Commons;
using XamarinFormsBug31415Sample.Infrastructure.Repositories.Commons.Caching;
using XamarinFormsBug31415Sample.Infrastructure.Repositories.Rest.Mappers;
using XamarinFormsBug31415Sample.Infrastructure.Repositories.Rest.Responses;
using Skahal.Infrastructure.Framework.Repositories;

namespace XamarinFormsBug31415Sample.Infrastructure.Repositories.Rest.Samples
{
    public class RestSampleRepository : RestRepositoryBase<Sample, IRestApiClient>, ISampleRepository
    {
        RestSampleMapper m_mapper;

        public RestSampleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            m_mapper = new RestSampleMapper();
            ApiBaseAddress = "http://jsonplaceholder.typicode.com";
        }

        public async Task<Sample> FindSampleAsync(string id, Priorities priority = Priorities.Background)
        {
            var response = await Cache.GetAndFetchLatest(Cache.GetMethodSignature(parameters: id), () => FindSampleRemoteAsync(id, priority));

            var result = m_mapper.ToDomainEntity(response);

            return result;
        }

        async Task<RestSample> FindSampleRemoteAsync(string id, Priorities priority)
        {
            var results = await ExecuteApiRequest((arg) => GetClientWithPriority(priority).FetchSample(id));
            return results;
        }

        public async Task<IEnumerable<Sample>> FindSampleByNameAsync(string name, Priorities priority = Priorities.Background)
        {
            var response = await Cache.GetAndFetchLatest(Cache.GetMethodSignature(parameters: name), () => FindSamplesByNameRemoteAsync(name, priority));

            var result = MapperHelper.ToDomainEntities(response, m_mapper);

            return result;
        }

        async Task<IEnumerable<RestSample>> FindSamplesByNameRemoteAsync(string name, Priorities priority)
        {
            var results = await ExecuteApiRequest((arg) => GetClientWithPriority(priority).FetchSamplesByName(name));
            return results;
        }
   }
}


