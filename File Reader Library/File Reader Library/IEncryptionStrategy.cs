using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderLibrary
{
    public interface IEncryptionStrategy
    {
        string Decrypt(string input);
    }
}
