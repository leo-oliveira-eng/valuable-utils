﻿using Messages.Core;
using Messages.Core.Extensions;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Valuables.Utils
{
    public sealed class Email : ValueObject
    {
        private const string REGEX_TO_VALIDATE_EMAIL_RFC822 = @"(?:(?:\r\n)?[ \t])*(?:(?:(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|'(?:[^\'\r\\]|\\.|(?:(?:\r\n)?[ \t]))*'(?:(?:\r\n)?[ \t])*)(?:\.(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|'(?:[^\'\r\\]|\\.|(?:(?:\r\n)?[ \t]))*'(?:(?:\r\n)?[ \t])*))*@(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*)(?:\.(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*))*|(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|'(?:[^\'\r\\]|\\.|(?:(?:\r\n)?[ \t]))*'(?:(?:\r\n)?[ \t])*)*\<(?:(?:\r\n)?[ \t])*(?:@(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[\t])*)(?:\.(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*))*(?:,@(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[\t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*)(?:\.(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*))*)*:(?:(?:\r\n)?[ \t])*)?(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|'(?:[^\'\r\\]|\\.|(?:(?:\r\n)?[ \t]))*'(?:(?:\r\n)?[ \t])*)(?:\.(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|'(?:[^\'\r\\]|\\.|(?:(?:\r\n)?[ \t]))*'(?:(?:\r\n)?[ \t])*))*@(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*)(?:\.(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*))*\>(?:(?:\r\n)?[ \t])*)|(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|'(?:[^\'\r\\]|\\.|(?:(?:\r\n)?[ \t]))*'(?:(?:\r\n)?[ \t])*)*:(?:(?:\r\n)?[ \t])*(?:(?:(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|'(?:[^\'\r\\]|\\.|(?:(?:\r\n)?[ \t]))*'(?:(?:\r\n)?[ \t])*)(?:\.(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|'(?:[^\'\r\\]|\\.|(?:(?:\r\n)?[ \t]))*'(?:(?:\r\n)?[ \t])*))*@(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*)(?:\.(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*))*|(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|'(?:[^\'\r\\]|\\.|(?:(?:\r\n)?[ \t]))*'(?:(?:\r\n)?[ \t])*)*\<(?:(?:\r\n)?[ \t])*(?:@(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*)(?:\.(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*))*(?:,@(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*)(?:\.(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*))*)*:(?:(?:\r\n)?[ \t])*)?(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|'(?:[^\'\r\\]|\\.|(?:(?:\r\n)?[ \t]))*'(?:(?:\r\n)?[ \t])*)(?:\.(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|'(?:[^\'\r\\]|\\.|(?:(?:\r\n)?[ \t]))*'(?:(?:\r\n)?[ \t])*))*@(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*)(?:\.(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*))*\>(?:(?:\r\n)?[ \t])*)(?:,\s*(?:(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|'(?:[^\'\r\\]|\\.|(?:(?:\r\n)?[ \t]))*'(?:(?:\r\n)?[ \t])*)(?:\.(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|'(?:[^\'\r\\]|\\.|(?:(?:\r\n)?[ \t]))*'(?:(?:\r\n)?[ \t])*))*@(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*)(?:\.(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*))*|(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|'(?:[^\'\r\\]|\\.|(?:(?:\r\n)?[ \t]))*'(?:(?:\r\n)?[ \t])*)*\<(?:(?:\r\n)?[ \t])*(?:@(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*)(?:\.(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*))*(?:,@(?:(?:\r\n)?[\t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*)(?:\.(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*))*)*:(?:(?:\r\n)?[ \t])*)?(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|'(?:[^\'\r\\]|\\.|(?:(?:\r\n)?[ \t]))*'(?:(?:\r\n)?[ \t])*)(?:\.(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|'(?:[^\'\r\\]|\\.|(?:(?:\r\n)?[ \t]))*'(?:(?:\r\n)?[ \t])*))*@(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*)(?:\.(?:(?:\r\n)?[ \t])*(?:[^()<>@,;:\\'.\[\] \000-\031]+(?:(?:(?:\r\n)?[ \t])+|\Z|(?=[\['()<>@,;:\\'.\[\]]))|\[([^\[\]\r\\]|\\.)*\](?:(?:\r\n)?[ \t])*))*\>(?:(?:\r\n)?[ \t])*))*)?;\s*)";

        public string Address { get; private set; }

        Email() { }

        Email(string address) { Address = address; }

        public bool IsValid() => IsValid(Address);

        public static bool IsValid(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return false;

            var regexEmail = new Regex(REGEX_TO_VALIDATE_EMAIL_RFC822);

            return regexEmail.IsMatch(address);
        }

        public static Response<Email> Create(string address)
        {
            if (!IsValid(address))
                return Response<Email>.Create().WithBusinessError(nameof(Email), "E-mail não é válido.");

            return new Email(address);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Address;
        }

        public static implicit operator Email(Maybe<Email> entity) => entity.Value;

        public static implicit operator Email(Response<Email> entity) => entity.Data;
    }
}