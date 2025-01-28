using eidss.model.Core;
using eidss.model.Reports.AZ;
using eidss.model.Reports.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using bv.common.Core;
using eidss.model.Enums;
using eidss.web.common.Utils;
using bv.model.BLToolkit;
using bv.model.Model.Core;

namespace eidss.webclient.Models.Reports
{
    [Serializable]
    public sealed class VetComparativeByMonthWebModel : BaseModel
    {
        public static readonly int VetMaxSpeciesTypeCount = 3;

        public VetComparativeByMonthWebModel()
        {
        }

        public VetComparativeByMonthWebModel(long? regionId, long? rayonId)
        {
            Address = new AddressModel(regionId, rayonId);
        }

        private List<SelectListItemSurrogate> _speciesDatasource;
        public List<SelectListItemSurrogate> SpeciesDatasource
        {
            get
            {
                if (_speciesDatasource == null)
                {
                    FilterHelper.GetSpeciesTypesParam param =
                        new FilterHelper.GetSpeciesTypesParam(HACode.All, false);

                    _speciesDatasource = ObjectStorage.Using<List<SelectListItemSurrogate>, List<SelectListItemSurrogate>>(
                        obj => obj,
                        String.Empty,
                        param.GenerateUniqueKey(),
                        param.GenerateUniqueAdditionalKey(),
                        false);

                    if (_speciesDatasource == null)
                    {
                        _speciesDatasource = FilterHelper.GetSpeciesTypes(param);

                        if (_speciesDatasource != null)
                        {
                            ObjectStorage.Put(String.Empty, 0, param.GenerateUniqueKey(), param.GenerateUniqueAdditionalKey(), _speciesDatasource);
                        }
                    }
                }

                return _speciesDatasource;
            }
        }

        [LocalizedDisplayName("FromYearLabel")]
        public int YearFrom { get; set; }

        [LocalizedDisplayName("ToYearLabel")]
        public int YearTo { get; set; }

        [LocalizedDisplayName("DiagnosisName")]
        public long? DiagnosisId { get; set; }

        public string DiagnosisName { get; set; }

        List<SelectListItemSurrogate> _diagnosisList;
        public List<SelectListItemSurrogate> DiagnosisList
        {
            get
            {
                if (_diagnosisList == null)
                {
                    FilterHelper.GetDiagnosisListParam param = new FilterHelper.GetDiagnosisListParam(
                            Localizer.CurrentCultureLanguageID, HACode.LivestockAvian, DiagnosisUsingTypeEnum.StandardCase);

                    _diagnosisList = ObjectStorage.Using<List<SelectListItemSurrogate>, List<SelectListItemSurrogate>>(
                        obj => obj,
                        String.Empty,
                        param.GenerateUniqueKey(),
                        param.GenerateUniqueAdditionalKey(),
                        false);

                    if (_diagnosisList == null)
                    {
                        _diagnosisList = FilterHelper.GetDiagnosisList(param);

                        if (_diagnosisList != null)
                        {
                            if (_diagnosisList.Count > 0)
                            {
                                SelectListItemSurrogate emptyItem = FilterHelper.EmptyItem;
                                emptyItem.Selected = true;
                                emptyItem.Value = "-1";

                                _diagnosisList.Insert(0, emptyItem);

                                ObjectStorage.Put(String.Empty, 0, param.GenerateUniqueKey(), param.GenerateUniqueAdditionalKey(), _diagnosisList);
                            }
                        }
                    }
                }

                return _diagnosisList;
            }
        }

        private string _speciesType_CheckedItems;
        public string SpeciesType_CheckedItems
        {
            get
            {
                return _speciesType_CheckedItems;
            }
            set
            {
                _speciesType_CheckedItems = value;
                if (!String.IsNullOrEmpty(value))
                {
                    string[] ids = value.Split(',');
                    _speciesType_Checked_IDS_AsInt = new long[ids.Length];
                    _speciesType_Checked_IDS_AsString = new string[ids.Length];
                    for (int i = 0; i < ids.Length; ++i)
                    {
                        _speciesType_Checked_IDS_AsInt[i] = Convert.ToInt64(ids[i]);
                        _speciesType_Checked_IDS_AsString[i] = ids[i];
                    }
                }
            }
        }

        private long[] _speciesType_Checked_IDS_AsInt;
        public long[] SpeciesType_Checked_IDS_AsInt
        {
            get
            {
                return _speciesType_Checked_IDS_AsInt ?? new long[0];
            }
        }

        private string[] _speciesType_Checked_IDS_AsString;
        public string[] SpeciesType_Checked_IDS_AsString
        {
            get
            {
                return _speciesType_Checked_IDS_AsString ?? new string[0];
            }
        }

        private string[] TranslatedSpeciesNames { get; set; }

        public AddressModel Address { get; set; }

        public void TranslateSelectedDiagnosisAndSpeciesTypes()
        {
            if (DiagnosisId < 1 && SpeciesType_Checked_IDS_AsInt.Length < 1)
            {
                // No diagnosis and species were selected. There is nothing to translate.
                return;
            }

            using (DbManagerProxy manager = DbManagerFactory.Factory.Create(ModelUserContext.Instance))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = (SqlConnection)manager.Connection;

                string sqlQuery = String.Empty;
                if (DiagnosisId > 0)
                {
                    sqlQuery = "exec dbo.spDiagnosis_SelectLookup @LangID, @HACode, @DiagnosisUsingType;";

                    cmd.Parameters.Add("@LangID", SqlDbType.NVarChar, 50).Value = Language;
                    cmd.Parameters.Add("@HACode", SqlDbType.Int, 4).Value = (int)HACode.LivestockAvian;
                    cmd.Parameters.Add("@DiagnosisUsingType", SqlDbType.BigInt, 8).Value = (long)DiagnosisUsingTypeEnum.StandardCase;
                }

                if (SpeciesType_Checked_IDS_AsInt.Length > 0)
                {
                    sqlQuery += "exec dbo.spSpeciesType_SelectLookup @LangID, @HACode;";
                    if (DiagnosisId < 1)
                    {
                        cmd.Parameters.Add("@LangID", SqlDbType.NVarChar, 50).Value = Language;
                        cmd.Parameters.Add("@HACode", SqlDbType.Int, 4).Value = (int)HACode.LivestockAvian;
                    }
                }

                cmd.CommandText = sqlQuery;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (DiagnosisId > 0)
                    {
                        while (reader.Read())
                        {
                            if (DiagnosisId.Value == reader.GetInt64(0))
                            {
                                DiagnosisName = reader.GetString(1);

                                // The selected diagnosis is translated and we are leaving this scope.
                                break;
                            }
                        }

                        reader.NextResult();
                    }

                    if (SpeciesType_Checked_IDS_AsInt.Length > 0)
                    {
                        LinkedList<long> ids = new LinkedList<long>(SpeciesType_Checked_IDS_AsInt);
                        List<string> translatedSpecies = new List<string>(SpeciesType_Checked_IDS_AsInt.Length);

                        while (reader.Read())
                        {
                            long currentId = reader.GetInt64(0);
                            if (ids.Contains(currentId))
                            {
                                ids.Remove(currentId);
                                translatedSpecies.Add(reader.GetString(1));

                                if (ids.Count == 0)
                                {
                                    // We translated all selected species. So let's escape.
                                    break;
                                }
                            }
                        }

                        TranslatedSpeciesNames = translatedSpecies.ToArray();
                    }
                }
            }
        }

        private string[] GetSelectedSpeciesNames()
        {
            if (TranslatedSpeciesNames != null)
            {
                return TranslatedSpeciesNames;
            }

            return SpeciesDatasource.Where(x => SpeciesType_Checked_IDS_AsString.Contains(x.Value)).Select(x => x.Text).ToArray();
        }

        public static explicit operator VetComparativeByMonthModel(VetComparativeByMonthWebModel model)
        {
            var result = new VetComparativeByMonthModel();

            result.Language = model.Language;

            result.YearFrom = model.YearFrom;
            result.YearTo = model.YearTo;

            result.DiagnosisId = model.DiagnosisId == -1 ? null : model.DiagnosisId;
            if (result.DiagnosisId != null)
            {
                result.Diagnosis = (from d in model.DiagnosisList
                                    where Convert.ToInt64(d.Value) == model.DiagnosisId.Value
                                    select d.Text).FirstOrDefault();
            }

            result.SpecieIds = model.SpeciesType_Checked_IDS_AsString;
            result.Species = model.GetSelectedSpeciesNames();

            result.RegionId = model.Address.RegionId;
            result.RegionName = model.Address.RegionName(model.Language);
            result.RayonId = model.Address.RayonId;
            result.RayonName = model.Address.RayonName(model.Language);

            result.ExportFormat = model.ExportFormat;
            result.IsOpenInNewWindow = model.IsOpenInNewWindow;

            return result;
        }
    }
}