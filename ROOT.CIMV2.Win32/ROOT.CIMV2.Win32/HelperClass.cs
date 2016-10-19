using System;
using System.Management;
using Impersonation;

namespace ROOT.CIMV2.Win32
{
    public static class WmiService
    {
        public static ManagementScope StaticScope => new ManagementScope();
        public static ManagementScope Connect(string sNameSpace)
        {
            return Connect(sNameSpace, null, null, null);
        }

        public static ManagementScope Connect(string sNameSpace, string userName, string password, string domain)
        {
            ManagementScope scope = null;
            ConnectionOptions options = new ConnectionOptions();
            
            try
            {
                if (!sNameSpace.ToUpper().StartsWith("\\\\" + Environment.MachineName.ToUpper() + "\\"))
                {
                    options.Username = userName;
                    options.Password = password;
                    options.Authority = "ntlmdomain:" + domain;
                    options.Impersonation = ImpersonationLevel.Impersonate;
                    options.EnablePrivileges = true;

                }
                scope = new ManagementScope(sNameSpace, options);
            }
            catch
            {
            }
            return scope;
        }
    }
}
