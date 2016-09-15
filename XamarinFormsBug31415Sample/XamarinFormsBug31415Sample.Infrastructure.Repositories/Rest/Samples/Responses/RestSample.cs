using System;
using Newtonsoft.Json;

namespace XamarinFormsBug31415Sample.Infrastructure.Repositories.Rest.Responses
{
    public class RestSample
    {
        [JsonProperty("userId")]
        public long UserId
        {
            get;
            set;
        }

        [JsonProperty("id")]
        public long Id
        {
            get;
            set;
        }

        [JsonProperty("title")]
        public string Title
        {
            get;
            set;
        }

        [JsonProperty("body")]
        public string Body
        {
            get;
            set;
        }

    }
}


