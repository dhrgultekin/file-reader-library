using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderLibrary
{
    interface IEncryptionStrategy
    {
        string Encrypt(string input);
    }
}
