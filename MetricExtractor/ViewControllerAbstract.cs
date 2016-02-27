using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricExtractor
{
    abstract class ViewControllerAbstract
    {
        public void MetricKeywords()
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("You are now running the Command Line Metric Extractor.");
            Console.WriteLine("This view is a simple verification that the program is running.");
            Console.WriteLine("Please review the full list of working commands below.");
            Console.WriteLine("---------------------------------------------------------------------------\n");
            
            Console.WriteLine("SYSTEM METRICS");
            Console.WriteLine("-count-versions");       //number of versions
            Console.WriteLine("-version-classes");      //number of classes in each version
            Console.WriteLine("-version-sizes\n");      //version size (bytes) (CUSTOM METRIC)

            Console.WriteLine("VERSION METRICS");   
            Console.WriteLine("-count-fields");         //number of fields for each class
            Console.WriteLine("-size-classes");         //size of each class (bytes)
            Console.WriteLine("-count-methods\n\n");    //number of methods for each class (CUSTOM METRIC)
        }
    }
}
