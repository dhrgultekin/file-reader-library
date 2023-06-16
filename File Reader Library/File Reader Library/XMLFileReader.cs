using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderLibrary
{
    class XMLFileReader : IFileReader
    {
        public string ReadFileContents(string filePath)
        {
            // Read the contents of the XML file
            return File.ReadAllText(filePath);
        }
    }
}
