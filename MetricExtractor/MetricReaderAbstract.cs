using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MetricExtractor
{
    abstract class MetricReaderAbstract
    {
        private List<string> names = new List<string>();
        Corpus corpus;

        public Corpus Corpus { get { return corpus; } }
        
        //Creates corpus instance
        //fills corpus instance with system from commandline
        public Corpus CorpusInitialiser(string commandline, string fullpath)
        {
            systemNames(fullpath);

            //checks system entered exists
            for (int i = 0; i < names.Count; i++)
            {
                if (commandline.Contains(names[i]))
                { 
                    corpus = new Corpus(names[i], fullpath);     //populates corpus instance
                    return corpus;
                }
            }
            
            Console.WriteLine("System name entered doesn't exist.");
            return null;
        }

        //simple input validation
        //returns a list of names of systems in the directory
        private List<string> systemNames(string path)
        {
            DirectoryInfo main = new DirectoryInfo(path);
            DirectoryInfo[] subfolders = main.GetDirectories();

            foreach (DirectoryInfo folder in subfolders)
                names.Add(folder.Name);

            return names;
        }
    }
}
