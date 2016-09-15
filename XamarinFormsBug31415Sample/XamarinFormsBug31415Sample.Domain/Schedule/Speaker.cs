using System;
using Skahal.Infrastructure.Framework.Domain;

namespace XamarinFormsBug31415Sample.Domain.Schedule
{
    public class Speaker : EntityWithIdBase<string>, IAggregateRoot
    {
        public string Name
        {
            get;
            set;
        }

        public string Bio
        {
            get;
            set;
        }

        public string ProfilePictureUrl
        {
            get;
            set;
        }
    }
}
