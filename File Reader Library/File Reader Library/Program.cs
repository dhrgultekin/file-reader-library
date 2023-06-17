﻿using System;
using System.Collections.Generic;
using System.IO;

namespace FileReaderLibrary
{
    class Program
    {
        static void Main()
        {
            try
            {
                // Prompt the user to enter or to paste the file path
                Console.WriteLine("Enter the file path:");
                string filePath = Console.ReadLine();

                if (File.Exists(filePath))
                {
                    // Create the role-based security context
                    RoleBasedSecurityContext securityContext = new RoleBasedSecurityContext();

                    // Define the role permissions for XML files
                    securityContext.AddRolePermissions(UserRole.Admin, new List<string> { "C:\\Temp\\Test\\allowed.xml", "C:\\Temp\\Test\\test.txt", "C:\\Temp\\Test\\schema.json" });
                    securityContext.AddRolePermissions(UserRole.User, new List<string> { "C:\\Temp\\Test\\sample.xml" });

                    // Determine the file type based on the file extension
                    FileType fileType = GetFileType(filePath);

                    // Get the appropriate file reader based on the file type and security context and encryption strategy
                    IFileReader fileReader = GetFileReader(fileType, securityContext, new ReverseEncryptionStrategy());

                    if (fileReader != null)
                    {
                        // Read and display the contents of the file
                        string fileContents = fileReader.ReadFileContents(filePath);
                        Console.WriteLine("\nFile Contents:");
                        Console.WriteLine(fileContents);
                    }
                    else
                    {
                        Console.WriteLine("Unsupported file type. Only text and XML files are supported.");
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

            // Wait for user input before closing the console window
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }

        // Method to determine the file type based on the file extension
        static FileType GetFileType(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);

            switch (fileExtension.ToLower())
            {
                case ".txt":
                    return FileType.Text;

                case ".xml":
                    return FileType.XML;

                case ".json":
                    return FileType.Json;

                default:
                    throw new NotSupportedException("Unsupported file type. Only text, XML and Json files are supported.");
            }
        }

        // Method to get the appropriate file reader based on the file type
        static IFileReader GetFileReader(FileType fileType, RoleBasedSecurityContext securityContext, IEncryptionStrategy encryptionStrategy)
        {
            switch (fileType)
            {
                case FileType.Text:
                    // Create an instance of the EncryptionContext with the desired encryption strategy
                    EncryptionContext encryptionContext = new EncryptionContext(new ReverseEncryptionStrategy());
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
