using FluenAPIExample.Assets;
using FluenAPIExample.Util;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core.CollectionActions;
using Microsoft.Azure.Management.Sql.Fluent;
using Microsoft.Azure.Management.Storage.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluenAPIExample
{
    public class Program
    {
        static void Main(string[] args)
        {
            var credentials = new AzureCredentialsFactory()
                      .FromServicePrincipal(SecretConstants.ApplicationId, SecretConstants.Password, SecretConstants.Tenant, AzureEnvironment.AzureGlobalCloud);
            IAzure azure = Azure.Authenticate(credentials).WithDefaultSubscription();
            
            Console.WriteLine($"Subscription ID : {azure.SubscriptionId}");
            
            foreach (IDeployment deployment in azure.Deployments.List())
            {
                Console.WriteLine($"{deployment.Name}");
            }

            IEnumerable<ISqlServer> servers = azure.SqlServers.List();
            foreach (ISqlServer server in servers)
            {
                Console.WriteLine($"{server.Id} - {server.Name} - {server.RegionName}");
            }

            ISqlServer sqlServer = azure.SqlServers.List().FirstOrDefault();

            IEnumerable<IStorageAccount> storageAccounts = azure.StorageAccounts.List();
            foreach (IStorageAccount storageAccount in storageAccounts)
            {
                Console.WriteLine($"{storageAccount.Id} - {storageAccount.Name}");
            }

            try
            {
                CreateNewDatabase(azure, sqlServer.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            Console.ReadLine();
        }

        private static void CreateNewDatabase(IAzure azure, string serverId)
        {
            try
            {
                ISqlDatabase tenantDatabase = azure.SqlServers.GetById(serverId).Databases
                    .Define(TenantParameters.GetFormattedShortName)
                    .Create();

                MiscellaneousUtils.PrintDatabase(tenantDatabase);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private static void CreateNewDatabaseWithActiveDirectory(IAzure azure, string serverId)
        {
            try
            {
                IStorageAccount account = azure.StorageAccounts.GetById(TenantParameters.StorageAccountId);
                ISqlDatabase tenantDatabase = azure.SqlServers.GetById(serverId).Databases
                    .Define(TenantParameters.GetFormattedShortName)
                    .ImportFrom(account, TenantParameters.ContainerName, TenantParameters.TenantTemplateName)
                        .WithActiveDirectoryLoginAndPassword(UserCredentials.UserName, UserCredentials.Password)
                        .Create();
                
                MiscellaneousUtils.PrintDatabase(tenantDatabase);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
