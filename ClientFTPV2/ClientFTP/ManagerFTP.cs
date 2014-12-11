using ClientFTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Linq;
namespace ClientFTP
{
   public class ManagerFTP
    {
        private string AdresseIp;
        private int Port;
        private string Login;
        private string Password;
        public FtpWebRequest Request;
        public FtpWebResponse Response;

        private string url 
        { 
            get
            {
                return "ftp://"+AdresseIp+":"+Port ;
            }
        }

        public ManagerFTP(string ip, int port,string login,string password)
        {
            // TODO: Complete member initialization
            this.AdresseIp = ip;
            this.Port = port;
            this.Login = login;
            this.Password = password;
            //this.Request = (FtpWebRequest)WebRequest.Create(this.url);
            this.Request = (FtpWebRequest)WebRequest.Create(this.url);
           
            
            Request.Credentials = new NetworkCredential(this.Login, this.Password);
        }

       /// <summary>
        /// envoi d'un ping vers le server FTP
       /// </summary>
       /// <returns>true si réponse sinon false</returns>
        public bool Communiquer()
        {
            bool communication = false;
            try
            {
                TcpClient client = new TcpClient(this.AdresseIp, this.Port);
                communication = true;
            }
            catch (Exception ex)
            {    
                communication = false;          
            }

            return communication;
        }

        public void RequestServer()
        {
            try
            {
                Request.Credentials = new NetworkCredential(this.Login, this.Password);
                this.Response = (FtpWebResponse)Request.GetResponse();

            }
            catch (Exception ex)
            {
                throw ex;
            }     
        }

        public Dossier GetListFolder()
        {
            this.Request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            this.RequestServer();
            Stream responseStream = this.Response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            List<string> directories = new List<string>();

            string line = reader.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                directories.Add(line);
                line = reader.ReadLine();
            }


            reader.Close();
            responseStream.Close();
            return this.GenererDossier(directories);
             
        }

       /// <summary>
       /// Genere le Dossier avec une liste de string
       /// </summary>
       /// <param name="directories"></param>
        private Dossier GenererDossier(List<string> directories)
        {
            Dossier DossierRoot = new Dossier();
            DossierRoot.path = this.url;
            FileFromFTP oneFile;
            foreach (string oneStringFile in directories)
            {
               DossierRoot.Add(new FileFromFTP(oneStringFile));    
            }
            DossierRoot.isLoaded = true;

           
               for (int indexFile = 0; indexFile < DossierRoot.ListFileFTP.Count(); indexFile++)
               {
                   if(DossierRoot.ListFileFTP.ElementAt(indexFile).Name=="." || DossierRoot.ListFileFTP.ElementAt(indexFile).Name=="..")
                   {
                       DossierRoot.ListFileFTP.Remove(DossierRoot.ListFileFTP.ElementAt(indexFile));
                       indexFile--;
                   }
               }

               
            return DossierRoot;
        }



        public void GetSubdirectory()
        {
            this.Request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            this.RequestServer();
            StreamReader remoteDirContents = new StreamReader(this.Response.GetResponseStream());

            if (remoteDirContents == null)
            {
                //
                // Add appropriate error handling here, and exit out 
                // of your function as needed if we can't read the FTP
                // Request's response stream...
                //
            }

            bool finished = false;
            string directoryData = string.Empty;
            StringBuilder remoteFiles = new StringBuilder();

            do
            {
                directoryData = remoteDirContents.ReadLine();

                if (directoryData != null)
                {
                    if (remoteFiles.Length > 0)
                    {
                        remoteFiles.Append("\n");
                    }

                    remoteFiles.AppendFormat("{0}", directoryData);
                }

                else
                {
                    finished = true;
                }
            }
            while (!finished);
        }

        public void SetUrl(string newUrl)
        {


            bool isLongUrl = false;
            if (newUrl.ElementAt(0).ToString() == "f")
                if (newUrl.ElementAt(1).ToString() == "t")
                    if (newUrl.ElementAt(2).ToString() == "p")
                        if (newUrl.ElementAt(3).ToString() == ":")
                            isLongUrl = true;
                              
            if (isLongUrl)
            {
                this.Request = (FtpWebRequest)WebRequest.Create(newUrl );
            }
            else
            {
                this.Request = (FtpWebRequest)WebRequest.Create(this.url+"/"+newUrl );
            }


        }
    }
}
