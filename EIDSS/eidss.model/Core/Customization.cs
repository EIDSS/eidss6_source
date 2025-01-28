using System.Reflection;
using bv.common.Core;

namespace eidss.model.Core
{
    public class Customization
    {
        private static readonly object m_Lock = new object();
        private static volatile ICustomization g_Instance;

        public static ICustomization Instance
        {
            get
            {
                lock (m_Lock)
                {
                    if (g_Instance == null)
                    {
                        var assemblyName = Utils.GetExecutingPathBin() + "eidss.model.customization.dll";
                        //LogError.Log("MessageLog", new Exception(), c => c.WriteLine(assemblyName));
                        var a = Assembly.LoadFrom(assemblyName);
                        var t = a.GetType("eidss.model.customization.Core.CustomizationCreator", true);
                        var m = t.GetMethod("Create", BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
                        var p = new object[] {EidssSiteContext.Instance.CustomizationPackageID};
                        g_Instance = m.Invoke(null, p) as ICustomization;
                    }

                    return g_Instance;
                }
            }
        }
    }
}