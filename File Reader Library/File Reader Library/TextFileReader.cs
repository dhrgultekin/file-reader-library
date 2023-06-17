using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderLibrary
{
    class TextFileReader : IFileReader
    {
        private readonly EncryptionContext _encryptionContext;

        public TextFileReader(EncryptionContext encryptionContext)
        {
            _encryptionContext = encryptionContext;
        }

        public string ReadFileContents(string filePath)
        {
            // Read the encrypted contents of the file
            string encryptedContents = File.ReadAllText(filePath);
            // Decrypt the contents using the _encryptionContext method
            string decryptedContents = _encryptionContext.Encrypt(encryptedContents);
            // Return the decrypted contents
            return decryptedContents;
        }
    }

}
