using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricExtractor
{
    abstract class MetricReporterAbstract
    {
        //a generic output implementation
        //takes an instance of an AbstractMetric derived class
        public void Output(AbstractMetric am)
        {
            foreach (AbstractMetric.SingleMetric metric in am)
            {
                Console.WriteLine("--------------------");
                Console.WriteLine(metric.name);
                Console.WriteLine("VALUE: " + metric.value);
            }
        }
    }
}
