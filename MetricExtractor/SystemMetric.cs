using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricExtractor
{
    class SystemMetric : AbstractMetric
    {
        private List<SingleMetric> systemmetrics = new List<SingleMetric>();        //list of data SystemMetric is counting

        public override List<AbstractMetric.SingleMetric> Metrics
        {
            get { return systemmetrics; }
        }


        public SystemMetric() { }
        

        //inherited from the base AbstractMetric class
        //to add metrics just add a new case and a method() to calculate the metric
        public override void ExtractMetrics(string[] args, Corpus c)
        {
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                { 
                    case "-count-versions":
                        CountVersions(c);
                        break;
                    case "-version-classes":
                        CountVersionClasses(c);
                        break;
                    case "-version-sizes":
                        VersionSize(c);
                        break;
                }
            }
        }


        //counts number of versions in the system
        //number of subdirectories for each system entered in the command line
        private void CountVersions(Corpus c)
        {
            string[] filecount = Directory.GetDirectories(c.System.SystemPath);

            SingleMetric sf = new SingleMetric();
            sf.name = "NUMBER OF VERSIONS: " + c.System.SystemName;
            sf.value = Directory.GetDirectories(c.System.SystemPath).Length;

            systemmetrics.Add(sf);
        }


        //counts number of classes in each version
        //contains list of classes to count for each version
        private void CountVersionClasses(Corpus c)
        {
            int count = 0;

            foreach (Version v in c.System.Versions)
            {
                foreach (string row in v.FileInfo.Skip(1))
                    count++;

                SingleMetric sm = new SingleMetric();
                sm.name = "NUMBER OF CLASSES: " + v.VersionName;
                sm.value = count;
                systemmetrics.Add(sm);
                count = 0;
            }  
        }


        //gets the size of each version in the system (from the total size of the file(s) it contains)
        //fileinfo object property length gets bytes size of the file(s) (in this case just the class-info.csv)
        private void VersionSize(Corpus c)
        {
            Dictionary<string, object> versionsize = new Dictionary<string, object>();
            
            foreach (Version v in c.System.Versions)
            {
                string path = v.VersionPath;
                string[] files = Directory.GetFiles(path);

                foreach (string csv in files)       //will iterate through every file in the version folder
                {
                    FileInfo f = new FileInfo(csv);
                    long bytes = f.Length;
                    versionsize.Add(v.VersionName, bytes);
                }
            }

            //key version name, value version size bytes
            systemmetrics.AddRange(CreateSingleMetric(versionsize, "SIZE OF VERSION: "));
        }
    }
}
