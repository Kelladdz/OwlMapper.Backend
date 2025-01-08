using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ZkmBusTimetables.WebApi.Filters
{
    public class CreatedAtActionResultFilter : ActionFilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var method = context.HttpContext.Request.Method;

            // Sprawdzenie, czy metoda to POST
            if (method == HttpMethod.Post.Method)
            {
                var controller = context.Controller as ControllerBase;

                // Sprawdzamy, czy rezultat to IActionResult
                if (context.Result is ObjectResult result && result.Value != null)
                {
                    // Zakładamy, że akcja GET nazywa się "GetById"
                    var createdAtActionResult = controller.CreatedAtAction(
                        actionName: "Get",      // Akcja GET, która zwraca zasób
                        routeValues: new
                        {
                            id = result.Value.GetType().GetProperty("Id")?.GetValue(result.Value),
                            controller.RouteData.Values,
                        }, // Wartości trasy, tutaj zakładamy, że id to nazwa klucza
                        value: result.Value);       // Stworzony zasób

                    context.Result = createdAtActionResult;
                }
            }
        }
    }
}
