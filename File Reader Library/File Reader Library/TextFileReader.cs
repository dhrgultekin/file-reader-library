using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderLibrary
{
    class TextFileReader : IFileReader
    {
        // Field _securityContext of type RoleBasedSecurityContext, which will hold the security context used for file access permissions
        private readonly RoleBasedSecurityContext _securityContext;

        // Field _encryptionContext of type EncryptionContext
        private readonly EncryptionContext _encryptionContext;

        // The class has a constructor that takes an IEncryptionStrategy object as parameter, and assign them to the corresponding field
        public TextFileReader(RoleBasedSecurityContext securityContext, EncryptionContext encryptionContext)
        {
            _securityContext = securityContext;
            _encryptionContext = encryptionContext;
        }

        public string ReadFileContents(string filePath)
        {
            // Checks whether the security context allows reading the file by calling the CanReadFile method of the _securityContext object
            if (_securityContext.CanReadFile(filePath))
            {
                // Read the encrypted contents of the file
                string encryptedContents = File.ReadAllText(filePath);
                // Decrypt the contents using the Decrypt method of _encryptionContext object
                string decryptedContents = _encryptionContext.Decrypt(encryptedContents);
                // Return the decrypted contents
                return decryptedContents;
            }
            else
            {
                throw new UnauthorizedAccessException("Access to the file is denied.");
            }
        }
    }

}
