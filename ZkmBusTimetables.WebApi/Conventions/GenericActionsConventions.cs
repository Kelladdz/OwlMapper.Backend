using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
using System.Text;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.WebApi.Conventions
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    internal class GenericActionsConventions : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            foreach(var action in controller.Actions)
            {
                var controllerRouteValues = controller.RouteValues;
                var actionName = action.ActionName;
                var actionParameters = action.Parameters;


                action.Selectors[0].AttributeRouteModel.Template = actionName switch
                {
                    "Get" when actionParameters.Any(p => p.ParameterName == "id") => "{id}",
                    "Get" when controller.ControllerType.GenericTypeArguments[0] == typeof(Variant) => "{variantName}",
                    "Get" when controller.ControllerType.GenericTypeArguments[0] == typeof(Line) => "{name}",
                    "Get" when !actionParameters.Any(p => p.ParameterName == "id" && p.ParameterType != typeof(IEnumerable<>)) => string.Empty,
                    "Search" => "search",
                    "Post" when !actionParameters.Any(p => p.ParameterType == typeof(IEnumerable<>)) => string.Empty,
                    "Post" when actionParameters.Any(p => p.ParameterType == typeof(IEnumerable<>)) => "batch",
                    "Patch" when actionParameters.Any(p => p.ParameterName == "id") => "{id}",
                    "Patch" when controller.ControllerType.GenericTypeArguments[0] == typeof(Variant) => "{variantName}",
                    "Patch" when controller.ControllerType.GenericTypeArguments[0] == typeof(Line) => "{name}",
                    "Delete" when actionParameters.Any(p => p.ParameterName == "id") => "{id}",
                    "Delete" when controller.ControllerType.GenericTypeArguments[0] == typeof(Variant) => "{variantName}",
                    "Delete" when controller.ControllerType.GenericTypeArguments[0] == typeof(Line) => "{name}",
                    "Delete" when !actionParameters.Any(p => p.ParameterName == "id" && p.ParameterType != typeof(IEnumerable<>)) => string.Empty
                };
            }
        }
    }
}
