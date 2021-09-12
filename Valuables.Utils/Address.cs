using Messages.Core;
using Messages.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Valuables.Utils
{
    public sealed class Address : ValueObject
    {
        private const string REGEX_TO_VALIDATE_CEP = @"^\d{5}-\d{3}$";

        private readonly static string[] ESTADOS_BRASILEIROS = new string[]{ "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" };

        public string Cep { get; private set; }

        public string Street { get; private set; }

        public string Complement { get; private set; }

        public string Neighborhood { get; private set; }

        public string Number { get; private set; }

        public string City { get; private set; }

        public string UF { get; private set; }

        Address() { }

        Address(string cep, string street, string neighborhood, string number, string city, string uf, string complement)
        {
            Cep = cep;
            Street = street;
            Complement = complement;
            Neighborhood = neighborhood;
            Number = number;
            City = city;
            UF = uf;
        }

        public static Response<Address> Create(string cep, string street, string neighborhood, string number, string city, string uf, string complement = "")
        {
            var response = Response<Address>.Create();

            var isValid = IsValid(cep, street, neighborhood, number, city, uf);

            if (isValid.HasError)
                return response.WithMessages(isValid.Messages);

            return response.SetValue(new Address(cep, street, neighborhood, number, city, uf, complement));
        }

        public Response<Address> Update(string cep, string street, string neighborhood, string number, string city, string uf, string complement = "")
        {
            var response = Response<Address>.Create();

            var isValid = IsValid(cep, street, neighborhood, number, city, uf);

            if (isValid.HasError)
                return response.WithMessages(isValid.Messages);

            return response.SetValue(new Address(cep, street, neighborhood, number, city, uf, complement));
        }

        private static Response<bool> IsValid(string cep, string street, string neighborhood, string number, string city, string uf)
        {
            var response = Response<bool>.Create();

            if (!CepIsValid(cep))
                response.WithBusinessError(nameof(cep), $"O campo {nameof(cep)} é obrigatório.");

            if (string.IsNullOrEmpty(street))
                response.WithBusinessError(nameof(street), $"O campo {nameof(street)} é obrigatório.");

            if (string.IsNullOrEmpty(number))
                response.WithBusinessError(nameof(number), $"O campo {nameof(number)} é obrigatório.");

            if (string.IsNullOrEmpty(city))
                response.WithBusinessError(nameof(city), $"O campo {nameof(city)} é obrigatório.");

            if (string.IsNullOrEmpty(neighborhood))
                response.WithBusinessError(nameof(neighborhood), $"O campo {nameof(neighborhood)} é obrigatório.");

            if (string.IsNullOrEmpty(uf) || !ESTADOS_BRASILEIROS.Contains(uf))
                response.WithBusinessError(nameof(uf), $"O campo {nameof(uf)} é obrigatório.");

            return response;
        }

        private static bool CepIsValid(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                return false;

            var regex = new Regex(REGEX_TO_VALIDATE_CEP);

            return regex.IsMatch(cep);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Cep;
            yield return Street;
            yield return Neighborhood;
            yield return Number;
            yield return City;
            yield return UF;
        }

        public static implicit operator Address(Maybe<Address> entity) => entity.Value;

        public static implicit operator Address(Response<Address> entity) => entity.Data;
    }
}
