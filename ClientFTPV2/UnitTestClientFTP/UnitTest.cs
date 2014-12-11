using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientFTP;
using System.IO;
using System.Collections.Generic;
using System.Net;

namespace UnitTestClientFTP
{
    [TestClass]
    public class UnitTest
    {
        public ManagerFTP monManagerFTP;

        [TestInitialize]
        public void initTest()
        {
            this.monManagerFTP = new ManagerFTP("127.0.0.1", 63001,"test","test");
        }

        [TestMethod]
        public void ReponseFTP()
        {
            bool reponse = monManagerFTP.Communiquer();
            Assert.IsTrue(reponse);
        }

         [TestMethod]
        public void testSubdirectory ()
        {
           monManagerFTP.Request.Method = WebRequestMethods.Ftp.PrintWorkingDirectory;
           monManagerFTP.RequestServer();
           Stream responseStream = monManagerFTP.Response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            List<string> directories = new List<string>();

            string line = reader.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                directories.Add(line);
                line = reader.ReadLine();
            }
        }

 
        

        [TestMethod]
        public void GetFolder()
        {        
                Dossier listDossier = monManagerFTP.GetListFolder();
                 Assert.IsNotNull(listDossier);       
        }


    }
}
