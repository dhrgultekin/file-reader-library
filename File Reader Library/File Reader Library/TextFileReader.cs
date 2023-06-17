using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderLibrary
{
    class TextFileReader : IFileReader
    {
        // Field _encryptionStrategy of type IEncryptionStrategy
        private readonly EncryptionContext _encryptionContext;

        // The class has a constructor that takes an IEncryptionStrategy object as parameter, and assign them to the corresponding field
        public TextFileReader(EncryptionContext encryptionContext)
        {
            _encryptionContext = encryptionContext;
        }

        public string ReadFileContents(string filePath)
        {
            // Read the encrypted contents of the file
            string encryptedContents = File.ReadAllText(filePath);
            // Decrypt the contents using the Decrypt method of _encryptionContext object
            string decryptedContents = _encryptionContext.Decrypt(encryptedContents);
            // Return the decrypted contents
            return decryptedContents;
        }
    }

}
