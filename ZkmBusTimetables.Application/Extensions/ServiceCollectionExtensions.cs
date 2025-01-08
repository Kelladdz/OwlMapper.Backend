using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.Mappings;
using ZkmBusTimetables.Application.Utils.UserContext;
using ZkmBusTimetables.Core.Interfaces;
/*using ZkmBusTimetables.Application.Features.GenericEntity.Read;*/


namespace ZkmBusTimetables.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            services.AddAuthorizersFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(ZkmMappingProfile));
            services.AddScoped<IUserContext, UserContext>();
        }
    }
}
