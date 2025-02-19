using FluentValidation.Results;

namespace Blog.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("There are one or more validation errors")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failuresGroup => failuresGroup.Key, failuresGroup => failuresGroup.ToArray());
        }


        public IDictionary<string, string[]> Errors { get; set; }
    }
}
