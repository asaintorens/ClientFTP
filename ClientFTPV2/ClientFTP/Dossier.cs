using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClientFTP
{
    public class Dossier : ICloneable
    {
  

        public List<FileFromFTP> ListFileFTP { get; set; }
        public bool isLoaded { get; set; }
        public bool isNodeAdded { get; set; }
        public string path { get; set; }
        
        public Dossier(string path) 
        {
            this.path = path;
        }

        public Dossier()
        {
            this.ListFileFTP = new List<FileFromFTP>();
            this.isLoaded = false;
            this.isNodeAdded = false;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public Dossier(bool p)
        {
            this.path = "";
            this.isLoaded = false;
            this.isNodeAdded = true ;
            this.ListFileFTP = new List<FileFromFTP>();
            
        }

        
        public void Add(FileFromFTP fileFromFTP)
        {
            this.ListFileFTP.Add(fileFromFTP);
        }
    }
}
