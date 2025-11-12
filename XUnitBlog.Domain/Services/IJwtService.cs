using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUnitBlog.Domain.Entities;

namespace XUnitBlog.Domain.Services;

public interface IJwtService
{
    string GenerateJwtToken(User user);
}
