﻿using System;
using System.Collections.Generic;
using eidss.model.Enums;
using eidss.model.Reports.Common;

namespace eidss.model.Reports.AZ
{
    [Serializable]
    public class MultipleSpeciesTypeModel : BaseMultipleModel
    {
        public MultipleSpeciesTypeModel() : this(new string[0])
        {
        }

        public MultipleSpeciesTypeModel(string[] checkedItems)
        {
            CheckedItems = checkedItems ?? new string[0];
        }
        public bool IsRequired { get; set; }

        public override List<SelectListItemSurrogate> LoadItemList()
        {
            return FilterHelper.GetSpeciesTypes(new FilterHelper.GetSpeciesTypesParam(HACode.Livestock));
        }

        public List<SelectListItemSurrogate> LoadItemList(HACode code, bool addSelectAll = true)
        {
            return FilterHelper.GetSpeciesTypes(new FilterHelper.GetSpeciesTypesParam(code, addSelectAll));
        }
    }
}
