using System;
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
                Console.WriteLine("Enter or paste the file path:");
                string filePath = Console.ReadLine();

                // Check if the file exists
                if (File.Exists(filePath))
                {
                    // Check the file type based on the file extension
                    FileType fileType = GetFileType(filePath);

                    // Verify if the file type is text
                    if (fileType == FileType.Text)
                    {
                        // Read the contents of the text file
                        string fileContents = File.ReadAllText(filePath);

                        // Display the file contents to the user
                        Console.WriteLine("\nFile Contents:");
                        Console.WriteLine(fileContents);
                    }
                    else
                    {
                        Console.WriteLine("Invalid file type. Only text files are supported.");
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

                default:
                    throw new NotSupportedException("Unsupported file type. Only text files are supported.");
            }
        }
    }
}