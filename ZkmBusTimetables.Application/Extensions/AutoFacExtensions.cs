/*using Autofac;
using System.Reflection;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.Features.GenericEntity.Read;
using ZkmBusTimetables.Application.Features.GenericEntity.ReadAll;
using ZkmBusTimetables.Application.Features.GenericEntity.Search;
using ZkmBusTimetables.Application.Features.GenericEntity.Insert;
using ZkmBusTimetables.Application.Features.GenericEntity.Update;
using ZkmBusTimetables.Application.Features.GenericEntity.Delete;
using ZkmBusTimetables.Application.Features.GenericEntity.DeleteAll;
using MediatR;
using Autofac.Features.Variance;
namespace ZkmBusTimetables.Application.Extensions
{
    public static class AutoFacExtensions
    {
        public static ContainerBuilder AddMediatrHandlersWithOpenGeneric(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
            .Where(t => t.IsClosedTypeOf(typeof(IRequestHandler<,>)))
                .AsImplementedInterfaces();

            var assembly = typeof(Read.QueryHandler<,,>).GetTypeInfo().Assembly;
            var configuration = MediatRConfigurationBuilder
                .Create(assembly)
                .WithAllOpenGenericHandlerTypesRegistered()
                .Build();
           
           *//* builder.RegisterGeneric(typeof(Read.QueryHandler<,,>)).AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(ReadAll.QueryHandler<,>)).AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(Search.QueryHandler<,>)).AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(Insert.CommandHandler<,,>)).AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(Update.CommandHandler<,>)).AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(Delete.CommandHandler<,>)).AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(DeleteAll.CommandHandler<>)).AsImplementedInterfaces();*//*
            builder.RegisterMediatR(configuration);
            builder.RegisterSource(new ContravariantRegistrationSource());
            return builder;
        }
    }
}
*/