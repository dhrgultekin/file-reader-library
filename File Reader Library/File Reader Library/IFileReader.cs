using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderLibrary
{
    interface IFileReader
    {
        string ReadFileContents(string filePath);
    }
}
