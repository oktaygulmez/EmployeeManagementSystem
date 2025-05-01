using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.MediatR.PipeLineBehavior
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var failures = _validators
                    .Select(v => v.Validate(context))
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    var response = Activator.CreateInstance<TResponse>();
                    var type = typeof(TResponse);

                    type.GetProperty("StatusCode")?.SetValue(response, 422);
                    type.GetProperty("Messages")?.SetValue(response, failures.Select(e => e.ErrorMessage).ToList());
                    type.GetProperty("Errors")?.SetValue(response, failures.Select(e => e.ErrorMessage).ToList());

                    return response;
                }
            }

            return await next();
        }

    }
}
