using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bv.common.Core;
using bv.model.BLToolkit;
using bv.model.Model.Core;
using eidss.model.Avr.Pivot;
using eidss.model.Avr.Tree;
using eidss.model.Core;
using eidss.model.Resources;
using eidss.model.Schema;

namespace eidss.model.Avr
{
    public class AvrQueryLayoutTreeDbHelper
    {
        private static string m_DefaultFolderExists;
        private static string m_NationalFolderExists;
        private static string m_DefaultLayoutExists;
        private static string m_NationalLayoutExists;

        static AvrQueryLayoutTreeDbHelper()
        {
            InitWarnings();
        }

        public static void InitWarnings()
        {
            m_DefaultFolderExists = EidssMessages.Get("msgNoUniqueFolderName",
                "A folder with the name you specified already exists. Specify a different folder name.");
            m_NationalFolderExists = EidssMessages.Get("msgNoUniqueNatFolderName",
                "A folder with the national name you specified already exists. Specify a different folder name.");
            m_DefaultLayoutExists = EidssMessages.Get("msgNoUniqueLayoutName",
                "Layout with the name you specified already exists. Specify a different layout name.");
            m_NationalLayoutExists = EidssMessages.Get("msgNoUniqueNatLayoutName",
                "Layout with the national name you specified already exists. Specify a different layout name.");
        }

        public static List<AvrTreeElement> LoadQueriesLayoutsFolders()
        {
            var treeElements = LoadQueries();
            treeElements.AddRange(LoadFolders());
            treeElements.AddRange(LoadLayouts());
            return treeElements;
        }

        public static List<AvrTreeElement> LoadQueries(bool readOnly = false)
        {
            LookupManager.AddObject("Query", null, AvrQueryLookup.Accessor.Instance(null).GetType(), "_SelectListInternal");
            using (var manager = DbManagerFactory.Factory.Create())
            {
                var accessor = AvrQueryLookup.Accessor.Instance(null);
                var lookup = accessor.SelectLookupList(manager, null);

                var treeElements = lookup
                    .Where(
                        q =>
                            (!readOnly || q.blnReadOnly) &&
                            (q.idfsSearchObject == null ||
                             ((EidssUser) ModelUserContext.Instance.CurrentUser).IsAvrSearchObjectAvailable(q.idfsSearchObject.Value)))
                    .Select(q => (AvrTreeElement) q)
                    .ToList();
                return treeElements;
            }
        }

        public static List<AvrTreeElement> ReLoadQueries(bool readOnly = false)
        {
            LookupManager.AddObject("Query", null, AvrQueryLookup.Accessor.Instance(null).GetType(), "_SelectListInternal");
            LookupManager.ClearByTable("Query");
            LookupManager.ClearAndReloadOnIdle();
            return LoadQueries(readOnly);
        }

        public static AvrTreeElement ReloadQuery(long queryId)
        {
            LookupManager.AddObject("Query", null, AvrQueryLookup.Accessor.Instance(null).GetType(), "_SelectListInternal");
            LookupManager.ClearByTable("Query");
            LookupManager.ClearAndReloadOnIdle();
            using (var manager = DbManagerFactory.Factory.Create())
            {
                var accessor = AvrQueryLookup.Accessor.Instance(null);
                var lookup = accessor.SelectLookupList(manager, queryId);

                var foundQuery = lookup.FirstOrDefault();
                return foundQuery != null
                    ? (AvrTreeElement) foundQuery
                    : null;
            }
        }

        public static List<AvrTreeElement> LoadFolders(bool readOnly = false, long? queryId = null)
        {
            LookupManager.AddObject("LayoutFolder", null, AvrFolderLookup.Accessor.Instance(null).GetType(), "_SelectListInternal");
            using (var manager = DbManagerFactory.Factory.Create())
            {
                var accessor = AvrFolderLookup.Accessor.Instance(null);
                var lookup = accessor.SelectLookupList(manager, null, queryId);
                var eidssUser = (EidssUser) ModelUserContext.Instance.CurrentUser;
                var treeElements = lookup
                    .Where(
                        f =>
                            (!readOnly || f.blnReadOnly) &&
                            (!f.idfsSearchObject.HasValue || eidssUser.IsAvrSearchObjectAvailable(f.idfsSearchObject.Value)))
                    .Select(f => (AvrTreeElement) f)
                    .ToList();
                return treeElements;
            }
        }

        public static List<AvrTreeElement> ReLoadFolders(bool readOnly = false, long? queryId = null)
        {
            LookupManager.AddObject("LayoutFolder", null, AvrFolderLookup.Accessor.Instance(null).GetType(), "_SelectListInternal");
            LookupManager.ClearByTable("LayoutFolder");
            LookupManager.ClearAndReloadOnIdle();
            return LoadFolders(readOnly, queryId);
        }

        public static AvrTreeElement ReloadFolder(long folderId)
        {
            LookupManager.AddObject("LayoutFolder", null, AvrFolderLookup.Accessor.Instance(null).GetType(), "_SelectListInternal");
            LookupManager.ClearByTable("LayoutFolder");
            LookupManager.ClearAndReloadOnIdle();
            using (var manager = DbManagerFactory.Factory.Create())
            {
                var accessor = AvrFolderLookup.Accessor.Instance(null);
                var lookup = accessor.SelectLookupList(manager, folderId, null);

                var foundFolder = lookup.FirstOrDefault();
                return foundFolder != null
                    ? (AvrTreeElement) foundFolder
                    : null;
            }
        }

        public static List<AvrTreeElement> LoadLayouts(bool readOnly = false, long? queryId = null)
        {
            LookupManager.AddObject("Layout", null, AvrLayoutLookup.Accessor.Instance(null).GetType(), "_SelectListInternal");
            using (var manager = DbManagerFactory.Factory.Create())
            {
                var accessor = AvrLayoutLookup.Accessor.Instance(null);
                var lookup = accessor.SelectLookupList(manager, null, queryId);
                var employeeId = EidssUserContext.User.EmployeeID is long ? (long) EidssUserContext.User.EmployeeID : -1;
                var user = (EidssUser) ModelUserContext.Instance.CurrentUser;
                Func<AvrLayoutLookup, bool> selector =
                    lay => (!readOnly || lay.blnReadOnly) &&
                           (!lay.idfsSearchObject.HasValue || user.IsAvrSearchObjectAvailable(lay.idfsSearchObject.Value)) &&
                           (lay.idfPerson.HasValue && lay.idfPerson.Value == employeeId ||
                            lay.blnShareLayout ||
                            AvrPermissions.AccessToAVRAdministrationPermission);
                var treeElements = lookup.Where(selector).Select(lay => (AvrTreeElement) lay).ToList();

                return treeElements;
            }
        }

        public static List<AvrTreeElement> ReLoadLayouts(bool readOnly = false, long? queryId = null)
        {
            LookupManager.AddObject("Layout", null, AvrLayoutLookup.Accessor.Instance(null).GetType(), "_SelectListInternal");
            LookupManager.ClearByTable("Layout");
            LookupManager.ClearAndReloadOnIdle();
            return LoadLayouts(readOnly, queryId);
        }

        public static AvrTreeElement ReloadLayout(long layoutId)
        {
            LookupManager.AddObject("Layout", null, AvrLayoutLookup.Accessor.Instance(null).GetType(), "_SelectListInternal");
            LookupManager.ClearByTable("Layout");
            LookupManager.ClearAndReloadOnIdle();
            using (var manager = DbManagerFactory.Factory.Create())
            {
                var accessor = AvrLayoutLookup.Accessor.Instance(null);
                var lookup = accessor.SelectLookupList(manager, layoutId, null);

                var foundLayout = lookup.FirstOrDefault();
                return foundLayout != null
                    ? (AvrTreeElement) foundLayout
                    : null;
            }
        }

        public static void SaveLayoutLocation(long layoutId, long? folderId)
        {
            using (var manager = DbManagerFactory.Factory.Create())
            {
                var command = manager.SetSpCommand("spAsLayoutParentPost",
                    manager.Parameter("idflLayout", layoutId),
                    manager.Parameter("idflFolder", folderId ?? (object) DBNull.Value));
                command.ExecuteNonQuery();
            }

            //LookupCache.NotifyChange("Layout");
            LookupManager.ClearByTable("Layout");
        }

        public static void SaveLayoutMetadata
        (string strLanguage, long idflLayout, string strDefaultLayoutName
            , string strLayoutName, long idfsDefaultGroupDate, long idflQuery, long idflDescription
            , string strDescription, string strDescriptionEnglish
            , bool blnReadOnly = false, bool blnShareLayout = false)
        {
            using (var manager = DbManagerFactory.Factory.Create())
            {
                var employeeId = EidssUserContext.User == null || EidssUserContext.User.EmployeeID == null
                    ? (long?) null
                    : (long) EidssUserContext.User.EmployeeID;
                var command = manager.SetSpCommand("dbo.spAsLayoutMetadataPost",
                    manager.Parameter("strLanguage", strLanguage),
                    manager.Parameter("idflLayout", idflLayout),
                    manager.Parameter("strDefaultLayoutName", strDefaultLayoutName),
                    manager.Parameter("strLayoutName", strLayoutName),
                    manager.Parameter("idfsDefaultGroupDate", idfsDefaultGroupDate),
                    manager.Parameter("idflQuery", idflQuery),
                    manager.Parameter("idflDescription", idflDescription),
                    manager.Parameter("strDescription", strDescription),
                    manager.Parameter("strDescriptionEnglish", strDescriptionEnglish),
                    manager.Parameter("intPivotGridXmlVersion", PivotGridXmlVersion.Version7),
                    manager.Parameter("blnReadOnly", blnReadOnly),
                    manager.Parameter("blnShareLayout", blnShareLayout),
                    manager.Parameter("idfPerson", employeeId)
                );
                command.ExecuteNonQuery();
            }

            //LookupCache.NotifyChange("Layout");
            LookupManager.ClearByTable("Layout");
        }

        public static void SaveFolder(long folderId, long? parentFolderId, long queryId, string defaultName, string nationalName)
        {
            using (var manager = DbManagerFactory.Factory.Create())
            {
                var command = manager.SetSpCommand("spAsFolderPost",
                    manager.Parameter("strLanguage", ModelUserContext.CurrentLanguage),
                    manager.Parameter("idflFolder", folderId),
                    manager.Parameter("idflParentFolder", parentFolderId ?? (object) DBNull.Value),
                    manager.Parameter("strFolderName", nationalName),
                    manager.Parameter("strDefaultFolderName", defaultName),
                    manager.Parameter("idflQuery", queryId)
                );
                command.ExecuteNonQuery();
            }

            //LookupCache.NotifyChange("LayoutFolder");
            LookupManager.ClearByTable("LayoutFolder");
        }

        public static long GetGlobalId(long id, AvrTreeElementType type)
        {
            using (var manager = DbManagerFactory.Factory.Create())
            {
                switch (type)
                {
                    case AvrTreeElementType.Layout:
                        var layoutAccessor = AvrLayoutLookup.Accessor.Instance(null);
                        var layoutLookup = layoutAccessor.SelectLookupList(manager, id, null);
                        var foundLayout = layoutLookup.SingleOrDefault();
                        if (foundLayout == null)
                        {
                            throw new AvrException(string.Format("Could not find Layout with ID {0} to unpublish", id));
                        }

                        if (!foundLayout.idfsGlobalLayout.HasValue)
                        {
                            throw new AvrException(string.Format("Could not unpublish non-published Layout with ID {0}", id));
                        }

                        return foundLayout.idfsGlobalLayout.Value;
                    case AvrTreeElementType.Folder:
                        var folderAccessor = AvrFolderLookup.Accessor.Instance(null);
                        var folderLookup = folderAccessor.SelectLookupList(manager, id, null);
                        var foundFolder = folderLookup.SingleOrDefault();
                        if (foundFolder == null)
                        {
                            throw new AvrException(string.Format("Could not find Folder with ID {0} to unpublish", id));
                        }

                        if (!foundFolder.idfsGlobalFolder.HasValue)
                        {
                            throw new AvrException(string.Format("Could not unpublish non-published Folder with ID {0}", id));
                        }

                        return foundFolder.idfsGlobalFolder.Value;
                    case AvrTreeElementType.Query:
                        var queryAccessor = AvrQueryLookup.Accessor.Instance(null);
                        var queryLookup = queryAccessor.SelectLookupList(manager, id);
                        var foundQuery = queryLookup.SingleOrDefault();
                        if (foundQuery == null)
                        {
                            throw new AvrException(string.Format("Could not find Query with ID {0} to unpublish", id));
                        }

                        if (!foundQuery.idfsGlobalQuery.HasValue)
                        {
                            throw new AvrException(string.Format("Could not unpublish non-published Query with ID {0}", id));
                        }

                        return foundQuery.idfsGlobalQuery.Value;
                    default:
                        throw new AvrException("Unsupported AvrTreeElementType " + type);
                }
            }
        }

        public static string GetCopyLayoutNameXml(AvrTreeElement layout)
        {
            Utils.CheckNotNull(layout, "layout");
            if (layout.ElementType != AvrTreeElementType.Layout)
            {
                throw new ArgumentException(@"Parameter should has ElementType == AvrTreeElementType.Layout", "layout");
            }

            var newDefault = GetLayoutNameWithPrefix(layout.DefaultName, layout.QueryID, layout.ID, Localizer.lngEn, true);

            var xmlBuilder = new StringBuilder(@"<?xml version=""1.0"" encoding=""UTF-16""?><ROOT>");
            xmlBuilder.AppendFormat(@"<LayoutName LanguageId=""{0}""  Translation=""{1}"" />", Localizer.lngEn, newDefault);
            if (ModelUserContext.CurrentLanguage != Localizer.lngEn)
            {
                var newNational = GetLayoutNameWithPrefix(layout.NationalName, layout.QueryID, layout.ID,
                    ModelUserContext.CurrentLanguage, true);
                xmlBuilder.AppendFormat(@"<LayoutName LanguageId=""{0}""  Translation=""{1}"" />", ModelUserContext.CurrentLanguage,
                    newNational);
            }

            xmlBuilder.Append(@"</ROOT>");

            return xmlBuilder.ToString();
        }

        public static string GetLayoutNameWithPrefix(string layoutName, long queryId, long layoutId, string lang, bool isNewObject)
        {
            Utils.CheckNotNullOrEmpty(lang, "lang");

            var result = layoutName;
            for (var index = 0; index < int.MaxValue; index++)
            {
                var strIndex = index > 0 ? string.Format(" ({0})", index) : string.Empty;
                var prefix = EidssMessages.Get("msgCopyPrefix", "Copy{0} of", lang);
                prefix = string.Format(Utils.Str(prefix).Trim(), strIndex);
                var format = EidssMessages.Get("msgCopyFormat", "{0} {1}", lang);
                result = string.Format(format, prefix, Utils.Str(layoutName));

                if (!IsLayoutExists(result, queryId, layoutId, lang, isNewObject))
                {
                    break;
                }
            }

            return result;
        }

        public static string ValidateElementName(AvrTreeElement element, bool isNewObject)
        {
            Func<string, long, long, string, bool, bool> exists = (s, l, arg3, arg4, arg5) => false;
            var defaultMessage = string.Empty;
            var nationalMessage = string.Empty;
            if (element.IsLayout)
            {
                exists = IsLayoutExists;
                defaultMessage = m_DefaultLayoutExists;
                nationalMessage = m_NationalLayoutExists;
            }

            if (element.IsFolder)
            {
                exists = IsFolderExists;
                defaultMessage = m_DefaultFolderExists;
                nationalMessage = m_NationalFolderExists;
            }

            if (exists(element.DefaultName, element.QueryID, element.ID, Localizer.lngEn, isNewObject))
            {
                return defaultMessage;
            }

            if (ModelUserContext.CurrentLanguage != Localizer.lngEn &&
                exists(element.NationalName, element.QueryID, element.ID, ModelUserContext.CurrentLanguage, isNewObject))
            {
                return nationalMessage;
            }

            return string.Empty;
        }

        public static bool IsLayoutExists(string layoutName, long queryId, long layoutId, string lang, bool isNewObject)
        {
            Utils.CheckNotNull(layoutName, "layoutName");

            layoutName = layoutName.Replace("'", "''");
            using (var manager = DbManagerFactory.Factory.Create())
            {
                var accessor = AvrLayoutLookup.Accessor.Instance(null);
                var lookup = accessor.SelectLookupList(manager, null, queryId);

                var found = lang == Localizer.lngEn
                    ? lookup.Where(lay => layoutName == lay.strDefaultLayoutName).ToArray()
                    : lookup.Where(lay => layoutName == lay.strLayoutName).ToArray();

                var isExists = found.Length > 1 ||
                               isNewObject && found.Length > 0 ||
                               found.Length == 1 && found[0].idflLayout != layoutId;
                return isExists;
            }
        }

        public static bool IsFolderExists(string folderName, long queryId, long folderId, string lang, bool isNewObject)
        {
            Utils.CheckNotNull(folderName, "folderName");

            folderName = folderName.Replace("'", "''");
            using (var manager = DbManagerFactory.Factory.Create())
            {
                var accessor = AvrFolderLookup.Accessor.Instance(null);
                var lookup = accessor.SelectLookupList(manager, null, queryId);

                var found = lang == Localizer.lngEn
                    ? lookup.Where(f => folderName == f.strDefaultFolderName).ToArray()
                    : lookup.Where(f => folderName == f.strFolderName).ToArray();

                var isExists = found.Length > 1 ||
                               isNewObject && found.Length > 0 ||
                               found.Length == 1 && found[0].idflFolder != folderId;
                return isExists;
            }
        }
    }
}