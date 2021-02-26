using System.Collections.Generic;

namespace ALSEA.Shared
{
    public interface IValidate
    {
        IList<ErrorViewModel> Validations { get; set; }
    }
}
