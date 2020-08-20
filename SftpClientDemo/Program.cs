using System;
using System.Threading;

namespace SftpClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello! This Console sends a file securefly through ftp (SFTP) to a host.");
            Console.WriteLine("Please provide the details below\n");

            Console.Write(@"Enter full path of the file you want to send, [e.g: D:\Documents\SampleFile.txt] : ");
            string fullFilePath = Console.ReadLine();

            Console.Write("Enter the HostName/IP, [e.g: 10.000.00.00] : ");
            string hostName = Console.ReadLine();

            Console.Write("Enter the userName : ");
            string userName = Console.ReadLine();

            Console.Write("Enter the password : ");
            string password = Console.ReadLine();

            Console.Write("Enter the sftp key fingerprint : ");
            string sshHostKeyFingerprint = Console.ReadLine();

            Console.Write("Enter the path on the host, [e.g: /files/] : ");
            string pathOnHost = Console.ReadLine();
            Console.WriteLine();

            FTPService fTPService = new FTPService();
            fTPService.SendFileThroughSFTP(fullFilePath, hostName, userName, password, sshHostKeyFingerprint, pathOnHost);

            Console.Write("\nExecution will end in 10 seconds. Thank you.\n");
            Thread.Sleep(10000);
        }
    }
}
