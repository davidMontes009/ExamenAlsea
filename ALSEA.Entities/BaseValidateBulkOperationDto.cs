using ALSEA.Shared;
using System.Collections.Generic;

namespace ALSEA.Entities
{
    public abstract class BaseValidateBulkOperationDto : IValidateBulkOperation
    {
        public IList<ErrorViewModel> Validations { get; set; } = new List<ErrorViewModel>();
    }
}
