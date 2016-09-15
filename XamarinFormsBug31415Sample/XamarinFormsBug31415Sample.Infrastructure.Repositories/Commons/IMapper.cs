using System;
namespace XamarinFormsBug31415Sample.Infrastructure.Repositories.Commons
{
    public interface IMapper<TDomainEntity, TRepositoryEntity>
    {
        TDomainEntity ToDomainEntity(TRepositoryEntity repositoryEntity);
        TRepositoryEntity ToRepositoryEntity(TDomainEntity domainEntity);
    }
}

