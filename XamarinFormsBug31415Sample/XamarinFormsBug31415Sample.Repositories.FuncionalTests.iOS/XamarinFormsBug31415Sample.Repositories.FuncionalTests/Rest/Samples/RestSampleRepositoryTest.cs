using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using XamarinFormsBug31415Sample.Infrastructure.Repositories.Rest.Samples;
using Skahal.Infrastructure.Framework.Repositories;

namespace XamarinFormsBug31415Sample.Repositories.FuncionalTests.Rest.Samples
{
    [TestFixture]
    public class RestSampleRepositoryTest
    {
        RestSampleRepository m_target;

        [SetUp]
        public void Setup()
        {
            var unitOfWork = new MemoryUnitOfWork();
            m_target = new RestSampleRepository(unitOfWork);
        }

        [Test]
        public async Task FindSampleAsync_1_SampleWithId1()
        {
            var actual = await m_target.FindSampleAsync("1");
            Assert.AreEqual("1", actual.Id);
            Assert.IsTrue(!string.IsNullOrEmpty(actual.Name));
        }

        [Test]
        public async Task FindSampleByNameAsync_quiestesse_Sample()
        {
            var actual = await m_target.FindSampleByNameAsync("qui est esse");
            Assert.AreEqual("2", actual.First().Id);
            Assert.AreEqual("qui est esse", actual.First().Name);
        }
        
    }
}

