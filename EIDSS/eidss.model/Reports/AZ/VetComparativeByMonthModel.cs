using System;
using System.Collections.Generic;
using eidss.model.Reports.Common;
using eidss.model.Enums;

namespace eidss.model.Reports.AZ
{
    [Serializable]
    public sealed class VetComparativeByMonthModel : BaseModel
    {
        public enum ModelStateType
        {
            ByYear,
            BySpecies,
            NoSpeciesWrongState,
            WrongState
        }

        public VetComparativeByMonthModel()
        {
        }

        public int YearFrom { get; set; }

        public int YearTo { get; set; }

        public long? DiagnosisId { get; set; }

        public string Diagnosis { get; set; }

        public string[] SpecieIds { get; set; }

        public string[] Species { get; set; }

        public long? RegionId { get; set; }

        public long? RayonId { get; set; }

        public static ModelStateType FigureOutTheState(int yearFrom, int yearTo, int amountOfSpecies)
        {
            if (yearFrom != yearTo)
            {
                return ModelStateType.ByYear;
            }
            else if (yearFrom == yearTo)
            {
                if (amountOfSpecies > 0)
                {
                    return ModelStateType.BySpecies;
                }
                else if (amountOfSpecies == 0)
                {
                    return ModelStateType.NoSpeciesWrongState;
                }
            }

            return ModelStateType.WrongState;
        }

        public ModelStateType StateType
        {
            get
            {
                return FigureOutTheState(YearFrom, YearTo, SpecieIds.Length);
            }
        }

        private string _regionName = String.Empty;
        public string RegionName
        {
            get { return _regionName; }
            set { _regionName = value ?? String.Empty; }
        }

        private string _rayonName = String.Empty;
        public string RayonName
        {
            get { return _rayonName; }
            set { _rayonName = value ?? String.Empty; }
        }

        public string GetSpeciesAsXml()
        {
            return FilterHelper.GetXmlFromList(SpecieIds);
        }
    }
}