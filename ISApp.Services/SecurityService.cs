using ISApp.Core.Data.UnitOfWorks;
using ISApp.Core.Entities;
using ISApp.Core.Services;
using ISApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISApp.Services
{
    public class SecurityService : BaseService<ApplicationKey>, ISecurityService
    {
        #region Declarations and Constructors

        public SecurityService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion Declarations and Constructors

        #region Interface Implementations

        public bool IsValidKey(Guid key, string name)
        {
            return base.GetAllQueryable().Any(s => s.AppKey == key && s.AppName == name);
        }

        #endregion Interface Implementations

        #region Private Methods

        #endregion Private Methods
    }
}
