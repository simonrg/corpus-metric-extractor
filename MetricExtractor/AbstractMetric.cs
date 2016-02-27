using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricExtractor
{
    abstract class AbstractMetric : IEnumerable<AbstractMetric.SingleMetric>
    {
        //will be overridden by derived classes to return each respective derived classes List<>
        public abstract List<SingleMetric> Metrics { get; }
        
        //DATA HOLDER
        public struct SingleMetric
        {
            public string name;
            public int value;
        }
        

        //different metric types will have different implementations
        public abstract void ExtractMetrics(string[] args, Corpus c);

        
        //an abstraction that populates a List with values from the specified column in the csv
        //accessed only by AbstractMetrics derived classes
        protected Dictionary<string, object> CsvRows(string column, Corpus c, string versionname)
        {
            Dictionary<string, object> classval = new Dictionary<string, object>();

            for (int i = 0; i < c.System.Versions.Count; i++)
            {
                if (c.System.Versions[i].VersionName == versionname)
                {
                    //get first row of csv -- get class name AND the metric column (e.g. fields, bytes)
                    string[] colnamesrow = c.System.Versions[i].FileInfo[0].Split(',');
                    int clsname = Array.IndexOf(colnamesrow, "ClassName");
                    int colindx = Array.IndexOf(colnamesrow, column);

                    //get the values from every subsequent row with the matching column index
                    foreach (string row in c.System.Versions[i].FileInfo.Skip(1))                       //skip first row (csv column names)
                    {
                        string[] rowvalues = row.Split(',');
                        classval.Add(rowvalues[clsname], rowvalues[colindx]);
                    }
                }
            }

            return classval;
        }


        //creates a singlemetric instance for each kv pair from the provided dictionary
        //accessed only by AbstractMetrics derived classes
        //returns a List of singlemetric instances
        protected List<SingleMetric> CreateSingleMetric(Dictionary<string, object> classinfo, string information)
        {
            List<SingleMetric> metrics = new List<SingleMetric>();
            
            foreach (KeyValuePair<string, object> kv in classinfo)
            {
                SingleMetric sm = new SingleMetric();
                sm.name = information + " " + kv.Key;
                sm.value = Convert.ToInt32(kv.Value);

                metrics.Add(sm);
            }

            return metrics;
        }


        //methods to implement the IEnumerable interface 
        //required to iterate through the SingleMetric collection passed to the MetricReporterAbstract
        IEnumerator<AbstractMetric.SingleMetric> IEnumerable<AbstractMetric.SingleMetric>.GetEnumerator()
        {
            return Metrics.GetEnumerator();
        }

        global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
        {
            return Metrics.GetEnumerator();
        }
    }
}
