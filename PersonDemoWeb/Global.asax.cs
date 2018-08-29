using System;
using System.Collections.Generic;
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
            // commented out. used for testomg purposes when testing without KV
            //string cnstr = System.Configuration.ConfigurationManager.ConnectionStrings["personDemoEntities"].ConnectionString;
            string cnstr = (string)GetCnString().Result;
            Session["cnString"] = cnstr;
        }

        private async System.Threading.Tasks.Task<string> GetCnString()
        {
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            try
            {
                var secreturl = System.Configuration.ConfigurationManager.AppSettings["cnStringUrl"];
                var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
                var secret = await keyVaultClient.GetSecretAsync(secreturl)
                    .ConfigureAwait(false);
                return secret.Value;
            }
            catch (Exception exp)
            {
                throw new Exception($"Something went wrong: {exp.Message}");
            }

        }
    }
}
