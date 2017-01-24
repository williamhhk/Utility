using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveDirectoryDemo
{
    public class IAd
    {
        public SearchResultCollection GetDomainControllers()
        {
            using (var searcher = new DirectorySearcher(new DirectoryEntry()))
            {
                searcher.Filter = "(primaryGroupID=516)";
                return searcher.FindAll();
            }
        }
    }
}
