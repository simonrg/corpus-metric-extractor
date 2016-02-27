using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricExtractor
{
    class Program
    {        
        static void Main(string[] args)
        {
            string commandline = String.Join(" ", args);
            string fullpath = Directory.GetCurrentDirectory() + args[1] + "\\";     //appends part of the path entered by the user to directory (don't have to type so much in the command line)
            string check = "system command";
            
            //call view to output some program info
            ViewController v = new ViewController();
            v.MetricKeywords();


            //call reader to initialise corpus
            MetricReader r = new MetricReader();
            r.CorpusInitialiser(commandline, fullpath);

            if (r.Corpus == null) 
                return; 


            //check the commandline if user is calculating a system or version metric
            foreach (Version ver in r.Corpus.System.Versions)
            {
                if (commandline.Contains(ver.VersionName))
                    check = "version command";
            }


            //call metric type to extract metrics
            //create instance of metricreporter to pass derived metric type to output
            MetricReporter rm = new MetricReporter();

            switch (check)
            {
                case "system command":
                    SystemMetric sm = new SystemMetric();
                    sm.ExtractMetrics(args, r.Corpus);
                    rm.Output(sm);
                    break;
                case "version command":
                    VersionMetric vm = new VersionMetric();
                    vm.ExtractMetrics(args, r.Corpus);
                    rm.Output(vm);
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Metric extraction operation complete. Press any key to quit.");
            Console.ReadLine();
        }
    }
}
