/*using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;
using ZkmBusTimetables.WebApi.Controllers;

namespace ZkmBusTimetables.WebApi.Misc
{
    public class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var modelType in EntityTypes.ModelTypes)
            {
                var entityType = modelType.Key;
                var identityType = modelType.Value[0];
                var entityRequestType = modelType.Value[1];
                var entityResponseType = modelType.Value[2];

                Type[] typeArgs = { identityType, entityType, entityRequestType, entityResponseType };
                var controllerType = typeof(GenericController<,,,>).MakeGenericType(typeArgs).GetTypeInfo();
                feature.Controllers.Add(controllerType);
            }
        }
    }
}
*/