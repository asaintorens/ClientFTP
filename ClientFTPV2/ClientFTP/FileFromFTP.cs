using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientFTP
{
  public  class FileFromFTP
    {
      public string Authorisation;
      public string Name;
      public bool isFolder;
      public string Extension;

      public FileFromFTP(string oneStringFile)
      {
          List<String> listParam = System.Text.RegularExpressions.Regex.Split(oneStringFile, @"\s{1,}").ToList();
          this.Authorisation = listParam.ElementAt((int)IndexFileFromFTP.Authorisation);
          this.Name = listParam.ElementAt((int)IndexFileFromFTP.Name);
          this.isFolder = this.IsFolder();
      }

      private bool IsFolder()
      {
          return  (this.Authorisation.ElementAt(0).ToString() == "d");      
      }

    }

    public enum IndexFileFromFTP
    {
       Authorisation = 0,
       Name = 8
    }
}
