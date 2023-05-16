using BooksApp.BusinessLogic.GenericInterfaces;
using BooksApp.Infrastructure.Data;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.BusinessLogic.ServiceLayer
{


    public class RunnerWriteDb<T, W>
    {
        private readonly IBusinessAction<T, W> _actionInstance;
        private readonly BooksAppDbContext _context;

        public RunnerWriteDb(IBusinessAction<T, W> actionInstance, BooksAppDbContext context)
        {
            _actionInstance = actionInstance;
            _context = context;
        }

        public IImmutableList<ValidationResult> Errors => _actionInstance.Errors;
        public bool HasErrors => _actionInstance.HasErrors;

        public W RunAction(T data)
        {
            var result = _actionInstance.Action(data);
            if (!HasErrors)
            {
                _context.SaveChanges();

            }
            return result;
        }
    }
}