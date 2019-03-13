using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;

namespace PersonDemoWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public void Session_OnStart()
        {
            var secreturl = System.Configuration.ConfigurationManager.AppSettings["dbcnstr"];            
            string cnstr = (string)GetCnString(secreturl).Result;
            Session["cnString"] = cnstr;
        }

        private async System.Threading.Tasks.Task<string> GetCnString(string secreturl)
        {
            // the following code is for using Keyvault and MSI:         
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            try
            {                
                var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
                var secret = await keyVaultClient.GetSecretAsync(secreturl).ConfigureAwait(false);
                return secret.Value;
            }
            catch (Exception exp)
            {
                throw new Exception($"Cannot get secret from Key Vault " + secreturl + ":" + exp.Message);
            }
        }
    }
}
