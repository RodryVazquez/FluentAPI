using Microsoft.Azure.Management.Sql.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluenAPIExample.Util
{
    public class MiscellaneousUtils
    {
        public static void PrintDatabase(ISqlDatabase database)
        {
            var builder = new StringBuilder().Append("Sql Database: ").Append(database.Id)
                    .Append("Name: ").Append(database.Name)
                    .Append("\n\tResource group: ").Append(database.ResourceGroupName)
                    .Append("\n\tRegion: ").Append(database.Region)
                    .Append("\n\tSqlServer Name: ").Append(database.SqlServerName)
                    .Append("\n\tEdition of SQL database: ").Append(database.Edition)
                    .Append("\n\tCollation of SQL database: ").Append(database.Collation)
                    .Append("\n\tCreation date of SQL database: ").Append(database.CreationDate)
                    .Append("\n\tIs data warehouse: ").Append(database.IsDataWarehouse)
                    .Append("\n\tCurrent service objective of SQL database: ").Append(database.ServiceLevelObjective)
                    .Append("\n\tId of current service objective of SQL database: ").Append(database.CurrentServiceObjectiveId)
                    .Append("\n\tMax size bytes of SQL database: ").Append(database.MaxSizeBytes)
                    .Append("\n\tDefault secondary location of SQL database: ").Append(database.DefaultSecondaryLocation);

            Log(builder.ToString());
        }

        public static void Log(string message)
        {
            LoggerMethod.Invoke(message);
        }

        public static Action<string> LoggerMethod { get; set; }
    }
}
