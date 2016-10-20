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
            WSManSessionOptions sessionOptions = new WSManSessionOptions();
            sessionOptions.AddDestinationCredentials(new CimCredential(PasswordAuthenticationMechanism.Default, domain, userName, securePassword));
            return CimSession.Create(hostName, sessionOptions);
         }

        public static IEnumerable<CimInstance> Query(string nameSpace, string queryExpression)
        {
            return CimSession.Create(null).QueryInstances(nameSpace, "WQL", queryExpression);
        }

        public static IEnumerable<CimInstance> GetAllCollections(string hostName, string userName, string passWord, string domain, string cmSite, string collectionId)
        {
            CimSession session = Connect(hostName, userName, passWord, domain);
            // Get All Collection
            //  Add Exception catching chain
            return session.EnumerateInstances($"root\\sms\\site_{cmSite}", "SMS_Collection")
                .Where(i => i.CimInstanceProperties["CollectionId"].Value.ToString().Contains(collectionId)).ToList();
        }

        public static void AddMembershipRule(string hostName, string userName, string passWord, string domain, string cmSite, string collectionId)
        {
            CimSession session = Connect(hostName, userName, passWord, domain);
            // Get All Collection
            //  Add Exception catching chain
            var collectionInstance = session.EnumerateInstances($"root\\sms\\site_{cmSite}", "SMS_Collection")
                .Where(i => i.CimInstanceProperties["CollectionId"].Value.ToString().Contains(collectionId)).ToList();

            if (!collectionInstance.Any()) return;

            var ruleInstance = new CimInstance(session.GetClass($"root\\sms\\site_{cmSite}", "SMS_CollectionRuleDirect"));
            ruleInstance.CimInstanceProperties["RuleName"].Value = "";
            ruleInstance.CimInstanceProperties["ResourceClassName"].Value = "SMS_R_System";
            ruleInstance.CimInstanceProperties["ResourceID"].Value = 1;

            var parameters = new CimMethodParametersCollection
            {
                CimMethodParameter.Create("collectionRule", ruleInstance, CimType.Instance,0)
            };
            CimMethodResult result = session.InvokeMethod(collectionInstance.FirstOrDefault(), "AddMembershipRule", parameters);
        }

    }
}
