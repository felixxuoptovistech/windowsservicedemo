using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace HelconService
{
    public partial class Service1 : ServiceBase
    {
       
        private const string EventSource = "VisionX";
        
       
        public Service1()
        {
            InitializeComponent();
            //eventLog = new EventLog();
            this.ServiceName = "OptoVistech";
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(System.IO.Path.GetDirectoryName(path));
            logFile = Path.Combine(directory, "VisionXlog.txt");

            new UtilityTool(logFile);

        }

        private static string logFile = "./VisionXlog.txt";
        protected override void OnStart(string[] args)
        {
            if (!File.Exists(logFile))
            {
                File.Create(logFile).Dispose();
            }
            File.AppendAllLines(logFile, new[] { $"service start at {DateTime.Now.ToString("yyyyMMdd HH:mm:ss")}" });
            
        }

        protected override void OnStop()
        {
            //eventLog.WriteEntry($"service stop at {DateTime.Now.ToString("yyyyMMdd HH:mm:ss")}", EventLogEntryType.Information);
            File.AppendAllLines(logFile, new[] { $"service stop at {DateTime.Now.ToString("yyyyMMdd HH:mm:ss")}" });
        }
    }
}
