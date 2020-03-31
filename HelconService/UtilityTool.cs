using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace HelconService
{
    public class UtilityTool
    {
        private HubConnection connection;
        public UtilityTool(string logFile)
        {
            var host = ConfigurationManager.AppSettings["server"];

            // establish a signalR to host
            connection = new HubConnectionBuilder()
                .WithUrl(host)
                .Build();

            

            // reconnect after connection closed
            connection.Closed += async (error) =>
            {
                var now = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                File.AppendAllLines(logFile, new[] { $"{now} : {error.ToString()}" });
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            Timer timer = new Timer((state) =>
            {
                // simulate the evaluation of helcon
                if (connection.State == HubConnectionState.Connected)
                {
                    var now = DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff");
                    connection.InvokeAsync("evaluationResult", "sent message1 at", now);
                    //File.AppendAllLines(logFile, new[] { $"send message at {now}" });
                }

            }, null, 0, 5000);


            connection.StartAsync();
        }
    }
}
