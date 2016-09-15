using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace XamarinFormsBug31415Sample.Infrastructure.Repositories.Commons
{
    internal static class MapperHelper
    {
        public static IList<TDomainEntity> ToDomainEntities<TDomainEntity, TRepositoryEntity>(IEnumerable<TRepositoryEntity> sources, IMapper<TDomainEntity, TRepositoryEntity> mapper)
        {
            var result = new List<TDomainEntity>();

            if (sources != null)
            {
                foreach (var source in sources)
                {
                    result.Add(mapper.ToDomainEntity(source));
                }
            }

            return result;
        }

        public static IList<TRepositoryEntity> ToRepositoryEntities<TDomainEntity, TRepositoryEntity>(IEnumerable<TDomainEntity> sources, IMapper<TDomainEntity, TRepositoryEntity> mapper)
        {
            var result = new List<TRepositoryEntity>();

            if (sources != null)
            {
                foreach (var source in sources)
                {
                    result.Add(mapper.ToRepositoryEntity(source));
                }
            }

            return result;
        }

        public static TDomainEnum ParseToDomainEnum<TDomainEnum>(string repositoryEnumValue, char[] charsToRemove) where TDomainEnum : struct
        {
            if (repositoryEnumValue == null)
            {
                throw new ArgumentNullException(nameof(repositoryEnumValue));
            }

            var domainEnumValue = repositoryEnumValue.ToString();

            foreach (var c in charsToRemove)
            {
                domainEnumValue = domainEnumValue.Replace(c.ToString(), "");
            }

            TDomainEnum domainEnum;

            if (Enum.TryParse<TDomainEnum>(domainEnumValue, true, out domainEnum))
            {
                return domainEnum;
            }
            else
            {
                var msg = String.Format(CultureInfo.InvariantCulture, "Não foi possível converter a enumeração com valor '{0}' para a enumeração de domínio do tipo '{1}'.",
                    repositoryEnumValue, typeof(TDomainEnum));

                throw new ArgumentException(msg);
            }
        }

        public static IList<TDomainEnum> ParseToDomainEnum<TDomainEnum>(IEnumerable<string> repositoryEnumValues, char[] charsToRemove) where TDomainEnum : struct
        {
            var result = new List<TDomainEnum>();

            foreach (var e in repositoryEnumValues)
            {
                result.Add(ParseToDomainEnum<TDomainEnum>(e, charsToRemove));
            }

            return result;
        }

        public static IList<TDomainEnum> ParseToDomainEnum<TDomainEnum>(IEnumerable<Enum> repositoryEnumValues, char[] charsToRemove) where TDomainEnum : struct
        {
            return ParseToDomainEnum<TDomainEnum>(repositoryEnumValues.Select(r => r.ToString()), charsToRemove);
        }
    }
}

