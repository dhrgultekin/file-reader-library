using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderLibrary
{
    class TextFileReader : IFileReader
    {
        public string ReadFileContents(string filePath)
        {
            // Read the contents of the TEXT file
            return File.ReadAllText(filePath);
        }
    }
}
