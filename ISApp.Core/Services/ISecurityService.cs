using ISApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISApp.Core.Services
{
    public interface ISecurityService: IBaseService<ApplicationKey>
    {
        bool IsValidKey(Guid key, string name);
    }
}
