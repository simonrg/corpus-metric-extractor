using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricExtractor
{
    class Corpus
    {
        private System singlesystem;
        
        public System System { get { return singlesystem; } }
        
        //initialise corpus instance
        //creates a new system instance from string system name
        //parameters: name of system, path to Qualitas Corpus
        public Corpus(string system, string path) 
        {
            string systempath = path + system;

            System s = new System(systempath);
            singlesystem = s;
        }
    }
}
