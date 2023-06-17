using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderLibrary
{
    class EncryptionContext
    {
        private readonly IEncryptionStrategy _encryptionStrategy;

        public EncryptionContext(IEncryptionStrategy encryptionStrategy)
        {
            _encryptionStrategy = encryptionStrategy;
        }

        public string Decrypt(string input)
        {
            return _encryptionStrategy.Decrypt(input);
        }
    }
}
