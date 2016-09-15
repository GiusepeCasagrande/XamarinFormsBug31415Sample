using System;
using System.Linq;
using System.Threading.Tasks;
using XamarinFormsBug31415Sample.Domain.Schedule;
using XamarinFormsBug31415Sample.Infrastructure.Repositories.Memory;
using NUnit.Framework;
using Skahal.Infrastructure.Framework.Repositories;
using Splat;

namespace XamarinFormsBug31415Sample.Domain.UnitTests.Schedule
{
    [TestFixture]
    public class SpeakerServiceTest
    {
        SpeakerService m_target;
        MemorySpeakerRepository m_repository;

        [SetUp]
        public void Setup()
        {
            var unitOfWork = new MemoryUnitOfWork();
            Locator.CurrentMutable.RegisterLazySingleton(() => unitOfWork, typeof(IUnitOfWork));

            m_repository = new MemorySpeakerRepository(unitOfWork);
            Locator.CurrentMutable.RegisterConstant(m_repository, typeof(ISpeakerRepository));

            m_repository.Attach(new Speaker
            {
                Id = "1",
                Name = "Primeiro"
            });

            m_repository.Attach(new Speaker
            {
                Id = "2",
                Name = "Segundo"
            });

            m_repository.Attach(new Speaker
            {
                Id = "3",
                Name = "Terceiro"
            });

            m_repository.Attach(new Speaker
            {
                Id = "4",
                Name = "Quarto"
            });

            m_repository.Attach(new Speaker
            {
                Id = "5",
                Name = "Quinto"
            });

            unitOfWork.CommitAsync();


            m_target = new SpeakerService();
        }

        [Test]
        public async Task GetAllSpeakersAsync_Empty_FullList()
        {
            var actual = await m_target.GetAllSpeakersAsync();
            Assert.AreEqual(5, actual.Count());

            Assert.IsNotEmpty(actual.First().Id);
            Assert.IsNotEmpty(actual.First().Name);
        }

        [Test]
        public async Task GetSpeaker_NonExistentId_Null()
        {
            var actual = await m_target.GetSpeakerAsync("10");
            Assert.IsNull(actual);
        }

        [Test]
        public async Task GetSpeaker_ValidId_Speaker()
        {
            var actual = await m_target.GetSpeakerAsync("1");
            Assert.IsNotNull(actual);

            Assert.AreEqual("1", actual.Id);
            Assert.AreEqual("Primeiro", actual.Name);
        }

        [Test]
        public async Task GetSpeakerByName_Empty_EmptyList()
        {
            var actual = await m_target.GetSpeakersByNameAsync("");
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public async Task GetSpeakerByName_OneLetter_AllWordsWithThatLetter()
        {
            var actual = await m_target.GetSpeakersByNameAsync("o");
            Assert.AreEqual(5, actual.Count());
            Assert.IsNotEmpty(actual.FirstOrDefault().Name);
        }

        [Test]
        public async Task GetSpeakerByName_StartWith_AllWordsThatStartsWith()
        {
            var actual = await m_target.GetSpeakersByNameAsync("Qu");
            Assert.AreEqual(2, actual.Count());
            Assert.IsNotEmpty(actual.FirstOrDefault().Name);
        }

        [Test]
        public async Task GetSpeakerByName_EndWith_AllWordsThatSEndsWith()
        {
            var actual = await m_target.GetSpeakersByNameAsync("ro");
            Assert.AreEqual(2, actual.Count());
            Assert.IsNotEmpty(actual.FirstOrDefault().Name);

        }

        [Test]
        public async Task GetSpeakerByName_NonExistentWord_Empty()
        {
            var actual = await m_target.GetSpeakersByNameAsync("Oitavo");
            Assert.AreEqual(0, actual.Count());
        }
    }
}
