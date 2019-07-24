using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluenAPIExample.Util
{
    public class TenantParameters
    {
        public static string ShortName = "";
        public static string GetFormattedShortName => $"tenant_{ShortName}";
        public static string ContainerName = "";
        public static string TenantTemplateName = "";

        public static string StorageAccountId = "";
    }
}
