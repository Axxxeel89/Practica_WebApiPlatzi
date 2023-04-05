using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practica_API.Middleware
{
    public class TimeMiddleware
    {
        //->Esta propiedad nos va a ayudar a invocar el middleware que sigue dentro del ciclo y de esta manera
        //Podemos construir la logica de uno detras de otro
        readonly RequestDelegate next; 

        public TimeMiddleware(RequestDelegate nextRequest)
        {
            next = nextRequest; //--> Se llena con lo que esta llegando al constructor. 
        }
        
      
        public async Task Invoke(HttpContext context)
        {
            
            //->En el Query, son los parametros que se colocan en la URL existe algun parametro que tenga una key igual a time
            if(context.Request.Query.Any(p => p.Key == "time"))
            { 
                await context.Response.WriteAsync(DateTime.Now.ToShortTimeString());
                return;
            }

             //toda la informacion del request esta en el HttpContext
            await next(context);   //--> Este nos invoca el siguiente middleware
           
        }

    }

    public static class TimeMiddlewareExtension
    {
        public static IApplicationBuilder UseTimeMiddleare(this IApplicationBuilder builder)
        {

            return builder.UseMiddleware<TimeMiddleware>();
        }

    }
     
}