using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OtherServices
{
    public class CustomJwtRequirement : IAuthorizationRequirement
    {
        public CustomJwtRequirement() { }
    }
}
