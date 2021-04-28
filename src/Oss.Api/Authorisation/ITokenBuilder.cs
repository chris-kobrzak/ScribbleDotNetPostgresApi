using System;
using Oss.Core.Models;

namespace Oss.Api
{
    public interface ITokenBuilder
    {
        public string Build(User user, String secret);
    }
}