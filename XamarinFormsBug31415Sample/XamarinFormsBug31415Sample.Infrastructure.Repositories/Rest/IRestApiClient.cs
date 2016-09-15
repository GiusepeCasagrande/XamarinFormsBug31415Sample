using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using XamarinFormsBug31415Sample.Infrastructure.Repositories.Rest.Responses;

namespace XamarinFormsBug31415Sample.Infrastructure.Repositories.Rest
{
    public interface IRestApiClient
    {
        [Get("/posts/{id}")]
        Task<RestSample> FetchSample(string id);

        [Get("/posts?title={name}")]
        Task<IEnumerable<RestSample>> FetchSamplesByName(string name);
    }
}

