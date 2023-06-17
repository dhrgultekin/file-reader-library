using System;
using System.Collections.Generic;
using System.IO;

namespace FileReaderLibrary
{
    class Program
    {
        static void Main()
        {
            bool exit = false;

            while (!exit)
            {
                try
                {
                    // Prompt the user to enter the file type
                    Console.WriteLine("Enter the file type (txt/xml/json):");
                    string fileTypeInput = Console.ReadLine();

                    // Prompt the user to specify if encryption should be used
                    Console.WriteLine("Do you want to use encryption? (y/n):");
                    bool useEncryption = Console.ReadLine().ToLower() == "y";

                    // Prompt the user to specify if role-based security should be used
                    Console.WriteLine("Do you want to use role-based security? (y/n):");
                    bool useRoleBasedSecurity = Console.ReadLine().ToLower() == "y";

                    RoleBasedSecurityContext securityContext = null;

                    if (useRoleBasedSecurity)
                    {
                        // Create the role-based security context
                        securityContext = new RoleBasedSecurityContext();

                        // Prompt the user to enter their role
                        Console.WriteLine("Enter your role (Admin/User):");
                        string roleInput = Console.ReadLine();

                        if (Enum.TryParse(roleInput, out UserRole userRole))
                        {
                            // Define the role permissions based on the user role and file type
                            DefineRolePermissions(userRole, fileTypeInput, securityContext);
                        }
                        else
                        {
                            Console.WriteLine("Invalid role. Role-based security will not be applied.");
                            useRoleBasedSecurity = false;
                        }
                    }

                    // Prompt the user to enter or paste the file path
                    Console.WriteLine("Enter the file path:");
                    string filePath = Console.ReadLine();

                    if (File.Exists(filePath))
                    {
                        FileType fileType = GetFileType(fileTypeInput);

                        // Get the appropriate file reader based on the file type, security context, and encryption strategy
                        IFileReader fileReader = GetFileReader(fileType, securityContext, useEncryption);

                        if (fileReader != null)
                        {
                            // Read and display the contents of the file
                            string fileContents = fileReader.ReadFileContents(filePath);
                            Console.WriteLine("\nFile Contents:");
                            Console.WriteLine(fileContents);
                        }
                        else
                        {
                            Console.WriteLine("Unsupported file type. Only text, XML, and JSON files are supported.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("File not found!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                // Prompt the user to continue or exit
                Console.WriteLine("\nEnter 'q' to quit or any other key to continue:");
                exit = Console.ReadLine().ToLower() == "q";
            }
        }

        // Method to determine the file type based on the user input
        static FileType GetFileType(string fileTypeInput)
        {
            switch (fileTypeInput.ToLower())
            {
                case "txt":
                    return FileType.Text;

                case "xml":
                    return FileType.XML;

                case "json":
                    return FileType.Json;

                default:
                    throw new NotSupportedException("Unsupported file type. Only text, XML, and JSON files are supported.");
            }
        }

        // Method to define role permissions based on the user role and file type
        static void DefineRolePermissions(UserRole userRole, string fileTypeInput, RoleBasedSecurityContext securityContext)
        {
            switch (userRole)
            {
                case UserRole.Admin:
                    // Define admin role permissions
                    if (fileTypeInput.ToLower() == "txt")
                    {
                        securityContext.AddRolePermissions(UserRole.Admin, new List<string> { "C:\\Temp\\Test\\allowed.txt", "C:\\Temp\\Test\\test.txt" });
                    }
                    else if (fileTypeInput.ToLower() == "xml")
                    {
                        securityContext.AddRolePermissions(UserRole.Admin, new List<string> { "C:\\Temp\\Test\\settings.xml" });
                    }
                    else if (fileTypeInput.ToLower() == "json")
                    {
                        securityContext.AddRolePermissions(UserRole.Admin, new List<string> { "C:\\Temp\\Test\\schema.json" });
                    }
                    break;

                case UserRole.User:
                    // Define user role permissions
                    if (fileTypeInput.ToLower() == "txt")
                    {
                        securityContext.AddRolePermissions(UserRole.User, new List<string> { "C:\\Temp\\Test\\sample.txt" });
                    }
                    else if (fileTypeInput.ToLower() == "xml")
                    {
                        securityContext.AddRolePermissions(UserRole.User, new List<string> { "C:\\Temp\\Test\\sample.xml" });
                    }
                    else if (fileTypeInput.ToLower() == "json")
                    {
                        securityContext.AddRolePermissions(UserRole.User, new List<string> { "C:\\Temp\\Test\\sample.json" });
                    }
                    break;
            }
        }

        // Method to get the appropriate file reader based on the file type, security context, and encryption strategy
        static IFileReader GetFileReader(FileType fileType, RoleBasedSecurityContext securityContext, bool useEncryption)
        {
            IEncryptionStrategy encryptionStrategy = null;

            if (useEncryption)
            {
                encryptionStrategy = new ReverseEncryptionStrategy();
            }

            switch (fileType)
            {
                case FileType.Text:
                    // Create an instance of the EncryptionContext with the desired encryption strategy
                    EncryptionContext encryptionContext = null;

                    if (useEncryption)
                    {
                        encryptionContext = new EncryptionContext(encryptionStrategy);
                    }

                    // Pass the RoleBasedSecurityContext and the encryptionContext to the TextFileReader constructor
                    return new TextFileReader(securityContext, encryptionContext);

                case FileType.XML:
                    // Pass the RoleBasedSecurityContext and the encryption strategy to the XmlFileReader constructor
                    return new XmlFileReader(securityContext, encryptionStrategy);

                case FileType.Json:
                    // Pass the RoleBasedSecurityContext and the encryption strategy to the JsonFileReader constructor
                    return new JsonFileReader(securityContext, encryptionStrategy);

                default:
                    return null;
            }
        }
    }
}
