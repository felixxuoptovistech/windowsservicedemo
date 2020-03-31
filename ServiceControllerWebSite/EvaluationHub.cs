using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ServiceControllerWebSite
{
    public class EvaluationHub : Hub
    {
        private static readonly List<string> data = new List<string>();

        public void evaluationResult(string arg1, string arg2)
        {
            var message = $"received message at {DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff")} : {arg1}  {arg2}";
            data.Add(message);
            Debug.WriteLine(message);
            File.AppendAllLines(AppContext.BaseDirectory + @"/log.txt", new []{message});
        }
    }
}
