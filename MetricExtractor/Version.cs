using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricExtractor
{
    class Version
    {
        private List<string> fileinfo = new List<string>();
        private string versionname;
        private string versionpath;

        public List<string> FileInfo { get { return fileinfo; } }
        public string VersionName { get { return versionname; } }           //a version knows its name
        public string VersionPath { get { return versionpath; } }           //a version knows its location in the folder hierarchy
        
        //gets the file(s) contained in the version folder
        //uses StreamReader object to read through file rows
        public Version(string version)
        {
            versionname = Path.GetFileName(version);
            versionpath = version;

            string[] versionfiles = Directory.GetFiles(version);            //version folder could more than one file
            foreach (string file in versionfiles)
            {
                //opens file and reads every row in and adds it to FileInfo list           
                StreamReader reader = new StreamReader(File.OpenRead(file));
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    fileinfo.Add(line);
                }
            }
        }
    }
}
