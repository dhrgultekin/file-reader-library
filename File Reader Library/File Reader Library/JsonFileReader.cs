using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderLibrary
{
    class JsonFileReader : IFileReader
    {
        public string ReadFileContents(string filePath)
        {
            // Read the contents of the Json file
            return File.ReadAllText(filePath);
        }
    }
}