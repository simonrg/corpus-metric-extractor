using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricExtractor
{
    class System
    {
        private List<Version> versions = new List<Version>();
        private string systemname;
        private string systempath;

        public List<Version> Versions { get { return versions; } }
        public string SystemName { get { return systemname; } }         //a system knows its name
        public string SystemPath { get { return systempath; } }         //a system knows its location in the folder hierarchy

        //initialises new system instance
        //creates a new version instance from system path
        public System(string systempath)
        {
            systemname = Path.GetFileName(systempath);
            this.systempath = systempath;
            
            string[] versionpath = Directory.GetDirectories(systempath);
            for (var k = 0; k < versionpath.Length; k++)
            {
                Version version = new Version(versionpath[k]);
                versions.Add(version);
            }
        }
    }
}
