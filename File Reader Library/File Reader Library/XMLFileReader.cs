using System;
using System.IO;

namespace FileReaderLibrary
{
    public class XmlFileReader : IFileReader
    {
        // Field _securityContext of type RoleBasedSecurityContext, which will hold the security context used for file access permissions
        private readonly RoleBasedSecurityContext _securityContext;

        // The class has a constructor that takes a RoleBasedSecurityContext object as a parameter and assigns it to the _securityContext field
        public XmlFileReader(RoleBasedSecurityContext securityContext)
        {
            _securityContext = securityContext;
        }

        // The ReadFileContents method is implemented as required by the IFileReader interface
        // It takes a filePath parameter, representing the path of the file to be read
        public string ReadFileContents(string filePath)
        {
            // Checks whether the security context allows reading the file by calling the CanReadFile method of the _securityContext object
            if (_securityContext.CanReadFile(filePath))
            {
                string fileContents = File.ReadAllText(filePath);
                return fileContents;
            }
            else
            {
                throw new UnauthorizedAccessException("Access to the file is denied.");
            }
        }
    }
}
