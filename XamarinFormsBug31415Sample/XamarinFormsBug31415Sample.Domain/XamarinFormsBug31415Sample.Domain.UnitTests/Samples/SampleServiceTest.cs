using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using XamarinFormsBug31415Sample.Domain.Samples;
using XamarinFormsBug31415Sample.Infrastructure.Repositories.Memory;
using Skahal.Infrastructure.Framework.Repositories;
using Splat;

namespace XamarinFormsBug31415Sample.Domain.UnitTests.Samples
{
    [TestFixture]
    public class SampleServiceTest
    {
        SampleService m_target;

        [SetUp]
        public void Setup()
        {
            var unitOfWork = new MemoryUnitOfWork();
            Locator.CurrentMutable.RegisterLazySingleton(() => unitOfWork, typeof(IUnitOfWork));

            var repository = new MemorySampleRepository(unitOfWork);
            Locator.CurrentMutable.RegisterConstant(repository, typeof(ISampleRepository));

            repository.Attach(new Sample
            {
                Id = "1",
                Name = "Primeiro"
            });

            repository.Attach(new Sample
            {
                Id = "2",
                Name = "Segundo"
            });

            repository.Attach(new Sample
            {
                Id = "3",
                Name = "Terceiro"
            });

            repository.Attach(new Sample
            {
                Id = "4",
                Name = "Quarto"
            });

            repository.Attach(new Sample
            {
                Id = "5",
                Name = "Quinto"
            });

            unitOfWork.CommitAsync();


            m_target = new SampleService();
        }

        [Test]
        public async Task GetSampleByName_Empty_EmptyList()
        {
            var actual = await m_target.GetSampleByNameAsync("");
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public async Task GetSampleByName_OneLetter_AllWordsWithThatLetter()
        {
            var actual = await m_target.GetSampleByNameAsync("o");
            Assert.AreEqual(5, actual.Count());
            Assert.IsNotEmpty(actual.FirstOrDefault().Name);
        }

        [Test]
        public async Task GetSampleByName_StartWith_AllWordsThatStartsWith()
        {
            var actual = await m_target.GetSampleByNameAsync("Qu");
            Assert.AreEqual(2, actual.Count());
            Assert.IsNotEmpty(actual.FirstOrDefault().Name);
        }

        [Test]
        public async Task GetSampleByName_EndWith_AllWordsThatSEndsWith()
        {
            var actual = await m_target.GetSampleByNameAsync("ro");
            Assert.AreEqual(2, actual.Count());
            Assert.IsNotEmpty(actual.FirstOrDefault().Name);

        }

        [Test]
        public async Task GetSampleByName_NonExistentWord_Empty()
        {
            var actual = await m_target.GetSampleByNameAsync("Oitavo");
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public async Task GetSample_NonExistentId_Null()
        {
            var actual = await m_target.GetSample("10");
            Assert.IsNull(actual);
        }

        [Test]
        public async Task GetSample_ValidId_Sample()
        {
            var actual = await m_target.GetSample("1");
            Assert.IsNotNull(actual);

            Assert.AreEqual("1", actual.Id);
            Assert.AreEqual("Primeiro", actual.Name);
        }
    }
}

