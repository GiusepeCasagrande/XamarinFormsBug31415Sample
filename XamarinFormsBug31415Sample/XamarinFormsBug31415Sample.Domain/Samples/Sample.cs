using System;
using Skahal.Infrastructure.Framework.Domain;

namespace XamarinFormsBug31415Sample.Domain.Samples
{
    public class Sample : EntityWithIdBase<string>, IAggregateRoot
    {
        public string Name
        {
            get;
            set;
        }
    }
}

