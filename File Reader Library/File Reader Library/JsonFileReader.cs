using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileReaderLibrary
{
    class JsonFileReader : IFileReader
    {
        // Field _securityContext of type RoleBasedSecurityContext, which will hold the security context used for file access permissions
        private readonly RoleBasedSecurityContext _securityContext;

        // Field _encryptionStrategy of type IEncryptionStrategy
        private readonly IEncryptionStrategy _encryptionStrategy;

        // The class has a constructor that takes a RoleBasedSecurityContext object and an IEncryptionStrategy object as parameters, and assigns them to the corresponding fields
        public JsonFileReader(RoleBasedSecurityContext securityContext, IEncryptionStrategy encryptionStrategy)
        {
            _securityContext = securityContext;
            _encryptionStrategy = encryptionStrategy;
        }

        // The ReadFileContents method is implemented as required by the IFileReader interface
        // It takes a filePath parameter, representing the path of the file to be read
        public string ReadFileContents(string filePath)
        {
            if (_securityContext == null)
            {
                if (_encryptionStrategy == null)
                {
                    // Read the contents of the file
                    return File.ReadAllText(filePath);
                }
                else
                {
                    // Read the encrypted contents of the file
                    string encryptedContents = File.ReadAllText(filePath);
                    // Decrypt the contents using the encryption strategy
                    string decryptedContents = _encryptionStrategy.Decrypt(encryptedContents);
                    // Return the decrypted contents
                    return decryptedContents;
                }
            }
            else if (_securityContext.CanReadFile(filePath))
            {
                if (_encryptionStrategy == null)
                {
                    // Read the contents of the file
                    return File.ReadAllText(filePath);
                }
                else
                {
                    // Read the encrypted contents of the file
                    string encryptedContents = File.ReadAllText(filePath);
                    // Decrypt the contents using the encryption strategy
                    string decryptedContents = _encryptionStrategy.Decrypt(encryptedContents);
                    // Return the decrypted contents
                    return decryptedContents;
                }
            }
            else
            {
                throw new UnauthorizedAccessException("Access to the file is denied.");
            }
        }
    }
}