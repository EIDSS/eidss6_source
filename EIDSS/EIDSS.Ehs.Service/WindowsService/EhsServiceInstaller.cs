using System.ComponentModel;
using System.Configuration;
using BLToolkit.Common;
using bv.common.Configuration;
using bv.common.Core;
using eidss.model.WindowsService;

namespace EIDSS.EHS.Service.WindowsService
{
    [RunInstaller(true)]
    public class EhsServiceInstaller : EidssServiceInstaller
    {
        protected override ServiceConfig GetServiceConfig()
        {
            System.Configuration.Configuration conf = ConfigurationManager.OpenExeConfiguration(Utils.GetExecutingPath() + "\\EIDSS.EHS.Service.exe");

            string url = Config.GetFromSettingOrConfiguration(conf, @"EhsServiceHostURL", @"http://localhost:50120/");
            string defautDescr = string.Format(
                @"Service provides EHS import operations for Electronic Integrated Disease Surveillance System. EHS methods are accessible calling WCF service with endpoint '{0}'",
                url);

            var result = new ServiceConfig
                {
                    ServiceName = Config.GetFromSettingOrConfiguration(conf, @"EhsServiceSystemName", @"EIDSSEhsService_v6"),
                    DisplayName =
                        Config.GetFromSettingOrConfiguration(conf, @"EhsServiceDisplayName", @"EIDSS Ehs Service version 6"),
                    Description = Config.GetFromSettingOrConfiguration(conf, @"EhsServiceDescription", defautDescr)
                };
            return result;
        }
    }
}