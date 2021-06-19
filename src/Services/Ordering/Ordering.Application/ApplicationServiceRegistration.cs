using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddOrderApplicationServices (this IServiceCollection services)
        {
            //add automapper extension
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //add validator extension
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //add mediatr
            services.AddMediatR(Assembly.GetExecutingAssembly());


            //register all behaviour
            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            return services;
        }


    }
}
