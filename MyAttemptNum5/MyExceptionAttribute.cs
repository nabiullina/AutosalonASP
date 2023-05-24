using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyAttemptNum5;

    public class MyExceptionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            StreamWriter sw = new StreamWriter("TEST.txt", true);
            sw.WriteLine("открыли");
            sw.Close();

        }

        public override void OnActionExecuted(ActionExecutedContext
            context)
        {
            StreamWriter sw = new StreamWriter("TESTT.txt", true);
            string s = context.ActionDescriptor.DisplayName;
            sw.WriteLine(DateTime.Now.ToString());
            sw.WriteLine(s);
            sw.WriteLine("закрыли");
            sw.Close();
        }
    }