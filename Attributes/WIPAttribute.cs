using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ComIT_eLearning.Attributes
{
  public class WIPAttribute : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      var controller = (Controller)context.Controller;
      context.Result = controller.View("WIP");
    }
  }

}
