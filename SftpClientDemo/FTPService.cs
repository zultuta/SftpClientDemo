using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WinSCP;

namespace SftpClientDemo
{
    public class FTPService
    {
        public void SendFileThroughSFTP(string fulFilePath, string hostName, string userName, string password, string sshHostKeyFingerprint, string pathOnHost)
        {
            LoggerService logger = new LoggerService();
            try
            {
                Console.WriteLine("Attempting to connect to SFTP session...");
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Sftp,
                    HostName = hostName,
                    UserName = userName,
                    Password = password,
                    PortNumber = 22,
                    SshHostKeyFingerprint = sshHostKeyFingerprint
                };

                using Session session = new Session();
                // Connect
                session.Open(sessionOptions);

                Console.WriteLine("Connection to SFTP session succeeded.");
                // Upload files
                TransferOptions transferOptions = new TransferOptions();
                transferOptions.TransferMode = TransferMode.Binary;
                transferOptions.ResumeSupport.State = TransferResumeSupportState.Off;

                TransferOperationResult transferResult;

                //copy file to new location on host machine
                if (File.Exists(fulFilePath))
                {
                    Console.WriteLine("Attempting to send file through SFTP...");
                    string MTNTRNLOG_PushPath = pathOnHost;
                    transferResult = session.PutFiles(fulFilePath, MTNTRNLOG_PushPath, false, transferOptions);

                    // Throw on any error
                    transferResult.Check();
                    Console.WriteLine("File sent through SFTP successfully.");
                }
                else
                {
                    Console.WriteLine("The file specified does not exist.");
                }
            }
            catch (WinSCP.SessionException se)
            {
                Console.WriteLine($"Something went wrong:  {se.Message}\n");
                Console.WriteLine("Please check the logs for the full exception.\n\n");
                logger.LogError(se);
                GC.Collect();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong:  {ex.Message}\n");
                Console.WriteLine("Please check the logs for the full exception.\n\n");
                logger.LogError(ex);
            }
        }
    }
}
