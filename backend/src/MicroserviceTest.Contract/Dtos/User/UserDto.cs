﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Contract.Dtos.User
{
    public record UserDto(string Id, string UserName, string Password);
}
