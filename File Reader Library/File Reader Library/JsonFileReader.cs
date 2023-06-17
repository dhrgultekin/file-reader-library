using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderLibrary
{
    class JsonFileReader : IFileReader
    {
        // Field _encryptionStrategy of type IEncryptionStrategy
        private readonly IEncryptionStrategy _encryptionStrategy;

        // The class has a constructor that takes an IEncryptionStrategy object as parameter, and assign them to the corresponding field
        public JsonFileReader(IEncryptionStrategy encryptionStrategy)
        {
            _encryptionStrategy = encryptionStrategy;
        }

        public string ReadFileContents(string filePath)
        {
            // Read the encrypted contents of the Json file
            string encryptedContents = File.ReadAllText(filePath);
            // Decrypt the contents using the Decrypt method of _encryptionContext object 
            string decryptedContents = _encryptionStrategy.Decrypt(encryptedContents);
            // Return the decrypted contents
            return decryptedContents;
        }
    }
}