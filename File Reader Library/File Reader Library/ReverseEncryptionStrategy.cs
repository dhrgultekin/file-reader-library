using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderLibrary
{
    class ReverseEncryptionStrategy : IEncryptionStrategy
    {
        public string Encrypt(string input)
        {
            // Reverse the input
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
