using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Application.Exceptions
{
    public  class MyCustomValidationException : ApplicationException
    {
        public MyCustomValidationException() : base("On or more validation failure have occured")
        {
            Error = new Dictionary<string, string[]>();
        }

        public MyCustomValidationException(IEnumerable<ValidationFailure> failures) : base("On or more validation failure have occured")
        {
            Error = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage).ToDictionary(failureGrp => failureGrp.Key, failureGrp => failureGrp.ToArray());
        }
        public Dictionary<string,string[]> Error { get; set; }
    }
}
