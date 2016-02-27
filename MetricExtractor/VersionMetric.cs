using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricExtractor
{
    class VersionMetric : AbstractMetric
    {
        private List<SingleMetric> versionmetrics = new List<SingleMetric>();         //list of data ValueMetric is counting
        string commandline;
        
        public override List<AbstractMetric.SingleMetric> Metrics
        {
            get { return versionmetrics; }
        }
        

        public VersionMetric() { }
        

        //inherited from the base AbstractMetric class
        //to add metrics just add a new case and a method() to calculate the metric
        public override void ExtractMetrics(string[] args, Corpus c)
        {
            commandline = String.Join(" ", args);   //for validating the version specified in the command line
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-count-fields":
                        CountClassFields(c);
                        break;
                    case "-size-classes":
                        SizeVersionClasses(c);
                        break;
                    case "-count-methods":
                        CountClassMethods(c);
                        break;
                }
            }
        }


        //number of fields for each class
        //#fields column of the class-info.csv
        private void CountClassFields(Corpus c)
        {
            foreach(Version v in c.System.Versions)
            {
                if (commandline.Contains(v.VersionName))            //match version entered in the commandline
                {
                    Dictionary<string, object> classfields = new Dictionary<string, object>();
                    classfields = CsvRows("#Fields", c, v.VersionName);

                    //key class name, value fields
                    versionmetrics.AddRange(CreateSingleMetric(classfields, "CLASS FIELDS: "));
                }
            }
        }


        //size of each class in bytes
        //#bytes column of the class-info.csv
        private void SizeVersionClasses(Corpus c)
        {
            foreach(Version v in c.System.Versions)
            {
                if (commandline.Contains(v.VersionName))            //match version entered in the commandline
                {
                    Dictionary<string, object> classbytes = new Dictionary<string, object>();
                    classbytes = CsvRows("#Bytes", c, v.VersionName);

                    //key class name, value bytes
                    versionmetrics.AddRange(CreateSingleMetric(classbytes, "CLASS SIZE (BYTES): "));
                }
            }
        }


        //number of methods for each class
        //#Methods column of the class-info.csv
        private void CountClassMethods(Corpus c)
        {
            foreach (Version v in c.System.Versions)
            {
                if (commandline.Contains(v.VersionName))            //match version entered in the commandline
                {
                    Dictionary<string, object> classmethods = new Dictionary<string, object>();
                    classmethods = CsvRows("#Methods", c, v.VersionName);

                    //key class name, value methods
                    versionmetrics.AddRange(CreateSingleMetric(classmethods, "CLASS METHODS: "));
                }
            }
        }
    }
}
