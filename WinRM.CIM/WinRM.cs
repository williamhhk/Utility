using Microsoft.Management.Infrastructure;
using Microsoft.Management.Infrastructure.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace WinRM.CIM
{
    public static class WinRM
    {
        public static CimSession Connect(string hostName, string userName, string passWord, string domain)
        {
            SecureString securePassword = new SecureString();
            Array.ForEach(passWord.ToArray(), securePassword.AppendChar);
            securePassword.MakeReadOnly();
            CimCredential credentials = new CimCredential(PasswordAuthenticationMechanism.Default, domain, userName, securePassword);
            WSManSessionOptions sessionOptions = new WSManSessionOptions();
            sessionOptions.AddDestinationCredentials(credentials);
            return CimSession.Create(hostName, sessionOptions);
         }

        public static IEnumerable<CimInstance> Query(string nameSpace, string queryExpression)
        {
            return CimSession.Create(null).QueryInstances(nameSpace, "WQL", queryExpression);
        }

     }
}
