using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.DirectoryServices;


namespace CreateInvoiceSystem.Service
{
    public class ADAuthentication
    {
        public ADAuthentication()
        {

        }

        public string Authenticate(string UserName, string Password)
        {
            string _path = @"LDAP://" + System.Configuration.ConfigurationManager.AppSettings["AdServerAndPort"];
            String domainAndUsername = System.Configuration.ConfigurationManager.AppSettings["Domain"] + @"\" + UserName;

            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, Password);

            bool isfind = true;
            try
            {
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + UserName + ")";
                SearchResult result = search.FindOne();

                if (result != null)
                {

                }
                else
                {
                    isfind = false;
                }

                return isfind.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string AuthenticatePrifile(string UserName, string Password)
        {
            string _path = @"LDAP://" + System.Configuration.ConfigurationManager.AppSettings["AdServerAndPort"];
            String domainAndUsername = System.Configuration.ConfigurationManager.AppSettings["Domain"] + @"\" + UserName;

            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, Password);

            bool isfind = true;
            try
            {
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + UserName + ")";
                SearchResult result = search.FindOne();

                if (result != null)
                {
                    result.GetDirectoryEntry();
                    string json = JsonConvert.SerializeObject(result.Properties);
                    return json;
                }
                else
                {
                    isfind = false;
                }

                return isfind.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}