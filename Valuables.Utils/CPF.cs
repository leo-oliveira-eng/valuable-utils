using Messages.Core;
using Messages.Core.Extensions;
using System.Collections.Generic;
using Valuables.Utils.Extensions;

namespace Valuables.Utils
{
    public sealed class CPF : ValueObject
    {
        public string Text { get; private set; }

        CPF() { }

        CPF(string text) { Text = Clear(text); }

        public static string Clear(string text)
            => text.ToNumbersOnly();

        public bool IsValid() => IsValid(Text);

        public static bool IsValid(string text)
        {
            text = Clear(text);

            if (text.Length > 11)
                return false;

            while (text.Length != 11)
                text = '0' + text;

            var igual = true;
            for (var i = 1; i < 11 && igual; i++)
            {
                if (text[i] != text[0])
                {
                    igual = false;
                }
            }

            if (igual || text == "12345678909")
                return false;

            var numeros = new int[11];

            for (var i = 0; i < 11; i++)
                numeros[i] = int.Parse(text[i].ToString());

            var soma = 0;
            for (var i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            var resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
            {
                return false;
            }

            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else if (numeros[10] != 11 - resultado)
            {
                return false;
            }

            return true;
        }

        public static Response<CPF> Create(string text)
        {
            var response = Response<CPF>.Create();

            text = Clear(text);

            if (!IsValid(text))
                return response.WithBusinessError(nameof(CPF), "CPF inválido.");

            return response.SetValue(new CPF(text));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Text;
        }

        public static implicit operator CPF(Maybe<CPF> entity) => entity.Value;

        public static implicit operator CPF(Response<CPF> entity) => entity.Data;
    }
}
