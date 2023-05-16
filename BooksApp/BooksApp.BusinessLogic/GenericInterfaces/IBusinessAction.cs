using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.BusinessLogic.GenericInterfaces
{
    public interface IBusinessAction<in T, out W>
    {
        IImmutableList<ValidationResult> Errors { get; }
        bool HasErrors { get; }
        W Action(T dto);
    }
}
