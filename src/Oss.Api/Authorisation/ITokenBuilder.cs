using System;
using Microsoft.IdentityModel.Tokens;
using Oss.Core.Models;

namespace Oss.Api
{
    public interface ITokenBuilder
    {
        public SecurityToken Build(User user, String secret);
    }
}