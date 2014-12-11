using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientFTP
{
    public class managerExplorer
    {
        public List<Dossier> listerRacine() { return null; }

        private TreeView leTreeView;
        private ListView laListView;

        internal void setTreeView(TreeView p_leTreeView, ListView p_laListView)
        {
            this.leTreeView = p_leTreeView;
            this.laListView = p_laListView;
        }

        public void changeBgColor(Color couleur)
        {
            this.leTreeView.BackColor = couleur;
            this.laListView.BackColor = couleur;
        }

        public void PopulateTreeView()
        {
            TreeNode rootNode;
            

            DirectoryInfo info = new DirectoryInfo("C:/Users/alexa_000/Documents/My Games");
            if (info.Exists)
            {
                rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                GetDirectories(info.GetDirectories(), rootNode);
                leTreeView.Nodes.Add(rootNode);
            }
        }

        public void GetDirectories(DirectoryInfo[] subDirs,
            TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            DirectoryInfo[] subSubDirs;
            foreach (DirectoryInfo subDir in subDirs)
            {
                try
                {
                    aNode = new TreeNode(subDir.Name, 0, 0);
                    aNode.Tag = subDir;
                    aNode.ImageKey = "folder";
                    subSubDirs = subDir.GetDirectories();
                    if (subSubDirs.Length != 0)
                    {
                        GetDirectories(subSubDirs, aNode);
                    }
                    nodeToAddTo.Nodes.Add(aNode);
                }


                catch (Exception)
                {
                    //Acces denied

                }
            }
        }

        public void PopulateTreeView(Dossier dossier)
        {
            TreeNode ParentNode;
            ParentNode = new TreeNode("Root");
            dossier.isNodeAdded = true;
            ParentNode.Tag = dossier;
            TreeNode rootNode;
            foreach (FileFromFTP file in dossier.ListFileFTP)
            {
                if(file.isFolder)
                {
                    rootNode = new TreeNode(file.Name);
                    Dossier doss = new Dossier(true);
                    doss.path = "/" + file.Name + "/";
                    rootNode.Tag = doss;
                    ParentNode.Nodes.Add(rootNode);
                       
                }
            }
            leTreeView.Nodes.Add(ParentNode);
        }
    }
}
