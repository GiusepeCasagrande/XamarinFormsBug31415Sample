using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Fusillade;
using ModernHttpClient;
using Plugin.Connectivity;
using Polly;
using Refit;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;
using XamarinFormsBug31415Sample.Infrastructure.Framework.Repositories;

namespace XamarinFormsBug31415Sample.Infrastructure.Repositories.Rest
{
    public class RestRepositoryBase<TEntity, TRestApiClient> : RepositoryBase<TEntity>
        where TEntity : IAggregateRoot
        where TRestApiClient : IRestApiClient
    {
        public string ApiBaseAddress
        {
            get;
            protected set;
        }

        readonly Lazy<TRestApiClient> background;
        readonly Lazy<TRestApiClient> userInitiated;
        readonly Lazy<TRestApiClient> speculative;

        public RestRepositoryBase(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            background = new Lazy<TRestApiClient>(() => CreateClient(Priority.Background));
            userInitiated = new Lazy<TRestApiClient>(() => CreateClient(Priority.UserInitiated));
            speculative = new Lazy<TRestApiClient>(() => CreateClient(Priority.Speculative));
        }

        TRestApiClient CreateClient(Priority priority)
        {
            var handler = new RateLimitedHttpMessageHandler(new NativeMessageHandler(), priority);
            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri(ApiBaseAddress)
            };

            return RestService.For<TRestApiClient>(client);
        }

        protected TRestApiClient GetClientWithPriority(Priorities priority)
        {
            switch (priority)
            {
                case Priorities.UserInitiated:
                    return userInitiated.Value;
                case Priorities.Speculative:
                    return speculative.Value;
                case Priorities.Background:
                    return background.Value;
                default:
                    return background.Value;
            }
        }

        public async Task<TResult> ExecuteApiRequest<TResult>(Func<CancellationToken, Task<TResult>> remoteFunction, CancellationToken cancellationToken = default(CancellationToken), int attempts = 5)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                return await Policy.Handle<WebException>()
                               .WaitAndRetryAsync(attempts, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                               .ExecuteAsync(remoteFunction, cancellationToken)
                               .ConfigureAwait(false);
            }

            return default(TResult);
        }

        public async Task<TResult> ExecuteApiRequest<TResult>(Func<Task<TResult>> remoteFunction, int attempts = 5)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                return await Policy.Handle<WebException>()
                               .WaitAndRetryAsync(attempts, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                               .ExecuteAsync(remoteFunction)
                               .ConfigureAwait(false);
            }

            return default(TResult);
        }




        #region NotImplemented
        public override Task<TEntity> FindByAsync(object key, bool syncBeforeFind = true)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<TEntity>> FindAllAsync(int offset, int limit, Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<TEntity>> FindAllAscendingAsync<TKey>(int offset, int limit, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderBy)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<TEntity>> FindAllDescendingAsync<TKey>(int offset, int limit, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderBy)
        {
            throw new NotImplementedException();
        }

        public override Task<long> CountAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        protected override Task PersistNewItemAsync(TEntity item)
        {
            throw new NotImplementedException();
        }

        protected override Task PersistUpdatedItemAsync(TEntity item)
        {
            throw new NotImplementedException();
        }

        protected override Task PersistDeletedItemAsync(TEntity item)
        {
            throw new NotImplementedException();
        }
        #endregion NotImplemented
    }
}