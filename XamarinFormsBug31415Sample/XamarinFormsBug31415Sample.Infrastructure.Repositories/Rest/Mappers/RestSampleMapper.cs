using System;
using XamarinFormsBug31415Sample.Domain.Samples;
using XamarinFormsBug31415Sample.Infrastructure.Repositories.Commons;
using XamarinFormsBug31415Sample.Infrastructure.Repositories.Rest.Responses;

namespace XamarinFormsBug31415Sample.Infrastructure.Repositories.Rest.Mappers
{
    public class RestSampleMapper : IMapper<Sample, RestSample>
    {
        public Sample ToDomainEntity(RestSample repositoryEntity)
        {
            if (repositoryEntity == null)
                return null;

            var result = new Sample()
            {
                Id = repositoryEntity.Id.ToString(),
                Name = repositoryEntity.Title
            };

            return result;
        }

        public RestSample ToRepositoryEntity(Sample domainEntity)
        {
            throw new NotImplementedException();
        }
    }
}


