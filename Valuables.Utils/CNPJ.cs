using Messages.Core;
using Messages.Core.Extensions;
using System.Collections.Generic;
using Valuables.Utils.Extensions;

namespace Valuables.Utils
{
    public sealed class CNPJ : ValueObject
    {
        public string Text { get; private set; }

        CNPJ() { }

        CNPJ(string text) { Text = Clear(text); }

        public static string Clear(string cnpj) => cnpj.ToNumbersOnly();

        public bool IsValid() => IsValid(Text);

        public static bool IsValid(string text)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0;
            int resto;
            string digito;
            string tempCnpj;
            text = Clear(text);

            if (text.Length != 14)
                return false;

            tempCnpj = text.Substring(0, 12);

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj += digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();

            return text.EndsWith(digito);
        }

        public static Response<CNPJ> Create(string text)
        {
            var response = Response<CNPJ>.Create();

            text = Clear(text);

            if (!IsValid(text))
                return response.WithBusinessError(nameof(CNPJ), "CNPJ inválido.");

            return response.SetValue(new CNPJ(text));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Text;
        }

        public static implicit operator CNPJ(Maybe<CNPJ> entity) => entity.Value;

        public static implicit operator CNPJ(Response<CNPJ> entity) => entity.Data;
    }
}
