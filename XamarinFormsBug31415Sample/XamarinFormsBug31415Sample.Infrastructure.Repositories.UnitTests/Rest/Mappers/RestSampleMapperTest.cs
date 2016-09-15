using NUnit.Framework;
using XamarinFormsBug31415Sample.Infrastructure.Repositories.Rest.Mappers;
using XamarinFormsBug31415Sample.Infrastructure.Repositories.Rest.Responses;

namespace XamarinFormsBug31415Sample.Infrastructure.Repositories.UnitTests.Rest.Mappers
{
    public class RestSampleMapperTest
    {
        RestSampleMapper m_mapper;

        [SetUp]
        public void Setup()
        {
            m_mapper = new RestSampleMapper();
        }

        [Test]
        public void ToDomainEntity_NeemuSearchResult_String()
        {

            var source = new RestSample()
            {
                Id = 1,
                UserId = 1,
                Body = "Body",
                Title = "Title"
            };

            var actual = m_mapper.ToDomainEntity(source);

            Assert.IsNotNull(actual);
            Assert.AreEqual(source.Id.ToString(), actual.Id);
            Assert.AreEqual(source.Title, actual.Name);
        }

        [Test]
        public void ToDomainEntity_null_null()
        {
            var actual = m_mapper.ToDomainEntity(null);

            Assert.IsNull(actual);
        }
    }
}


