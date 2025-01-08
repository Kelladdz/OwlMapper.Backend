using ZkmBusTimetables.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using ZkmBusTimetables.Core.Models;
using System.Text;
using System.Reflection;

namespace ZkmBusTimetables.WebApi.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    internal class GenericControllerConventions : Attribute, IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            StringBuilder result = new StringBuilder();
            foreach (var controller in application.Controllers)
            {
                var hasAttributeRouteModels = controller.Selectors
                    .Any(selector => selector.AttributeRouteModel != null);

                var entityType = controller.ControllerType.GenericTypeArguments[0];
                var entityTypeName = entityType.Name;

                if (entityType == typeof(Address))
                {
                    controller.ControllerName = "Addresses";
                }

                else
                {
                    controller.ControllerName = result.ToString();
                }

                var routeValues = controller.RouteValues;
                var routeTemplate = "api/v1/";

                foreach (var value in routeValues)
                {
                    routeTemplate += "{" + value.Key + "}" + "/{" + value.Key + "}/";
                }

                if (entityType == typeof(Address))
                {
                    routeTemplate += entityTypeName.ToLower() + "es";
                } 
                else
                {
                    for (var i = 0; i < entityTypeName.Length; i++)
                    {
                        if (char.IsUpper(entityTypeName[i]))
                        {

                            for (var j = 0; j < entityTypeName.Length; j++)
                            {
                                if (j == i && j != 0)
                                    result.Append("-" + entityTypeName[j].ToString().ToLower());

                                else
                                    result.Append(entityTypeName[j]);
                            }
                            break;
                        }
                        else continue;
                    }
                    routeTemplate += result.ToString() + "s";
                } 
                    
                controller.Selectors[0].AttributeRouteModel = new AttributeRouteModel()
                {
                    Template = routeTemplate
                };
                
                if (controller.ControllerType.GenericTypeArguments[0] != typeof(Address) || controller.ControllerType.GenericTypeArguments[0] != typeof(BusStop))
                {
                    var searchAction = controller.Actions.FirstOrDefault(a => a.ActionName == "Search");

                    if (searchAction != null)
                        controller.Actions.Remove(searchAction);
                }
            }
        }
    }
}
