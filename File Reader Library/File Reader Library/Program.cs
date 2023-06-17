﻿using System;
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
                    // Determine the file type based on the file extension
                    FileType fileType = GetFileType(filePath);

                    // Get the appropriate file reader based on the file type
                    IFileReader fileReader = GetFileReader(fileType);
                    
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

                default:
                    throw new NotSupportedException("Unsupported file type. Only text and XML files are supported.");
            }
        }

        // Method to get the appropriate file reader based on the file type
        static IFileReader GetFileReader(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.Text:
                    // Create an instance of the EncryptionContext with the desired encryption strategy
                    EncryptionContext encryptionContext = new EncryptionContext(new ReverseEncryptionStrategy());
                    // Pass the EncryptionContext to the TextFileReader constructor
                    return new TextFileReader(encryptionContext);

                case FileType.XML:
                    return new XMLFileReader();

                default:
                    return null;
            }
        }
    }
}