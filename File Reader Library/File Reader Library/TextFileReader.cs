using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileReaderLibrary
{
    class TextFileReader : IFileReader
    {
        private readonly RoleBasedSecurityContext _securityContext;
        private readonly EncryptionContext _encryptionContext;

        public TextFileReader(RoleBasedSecurityContext securityContext, EncryptionContext encryptionContext)
        {
            _securityContext = securityContext;
            _encryptionContext = encryptionContext;
        }

        // The ReadFileContents method is implemented as required by the IFileReader interface
        // It takes a filePath parameter, representing the path of the file to be read
        public string ReadFileContents(string filePath)
        {
            if (_securityContext == null)
            {
                if (_encryptionContext == null)
                {
                    // Read the contents of the file
                    return File.ReadAllText(filePath);
                }
                else
                {
                    // Read the encrypted contents of the file
                    string encryptedContents = File.ReadAllText(filePath);
                    // Decrypt the contents using the encryption strategy
                    string decryptedContents = _encryptionContext.Decrypt(encryptedContents);
                    // Return the decrypted contents
                    return decryptedContents;
                }
            }
            else if (_securityContext.CanReadFile(filePath))
            {
                if (_encryptionContext == null)
                {
                    // Read the contents of the file
                    return File.ReadAllText(filePath);
                }
                else
                {
                    // Read the encrypted contents of the file
                    string encryptedContents = File.ReadAllText(filePath);
                    // Decrypt the contents using the encryption strategy
                    string decryptedContents = _encryptionContext.Decrypt(encryptedContents);
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
