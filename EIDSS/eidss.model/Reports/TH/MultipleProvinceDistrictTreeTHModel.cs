using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using bv.model.BLToolkit;
using bv.model.Model.Core;
using eidss.model.Core;
using eidss.model.Reports.Common;
using eidss.model.Schema;

namespace eidss.model.Reports.TH
{
    public class MultipleProvinceDistrictTreeTHModel : BaseMultipleModel
    {
        public MultipleProvinceDistrictTreeTHModel()
        {
            CheckedItems = new string[0];
        }

        public MultipleProvinceDistrictTreeTHModel(string[] checkedItems)
        {
            CheckedItems = checkedItems ?? new string[0];
        }

        public long Key
        {
            get { return GetHashCode(); }
        }

        public static List<ThaiProvinceDistrictTreeLookup> GetDataSource()
        {
            using (DbManagerProxy manager = DbManagerFactory.Factory.Create(ModelUserContext.Instance))
            {
                return ThaiProvinceDistrictTreeLookup.Accessor.Instance(null).SelectList(manager, null, null);
            }
        }

        public override List<SelectListItemSurrogate> LoadItemList()
        {
            //Currently this method is used in web only
            //If it will be used in desktop too, it will be needed to make Select All item adding optional
            var result = new List<SelectListItemSurrogate>
            {
                FilterHelper.SelectAllItem
            };
            result.AddRange(
                GetDataSource().Select(districts => new SelectListItemSurrogate
                {
                    Value = districts.idfsProvinceOrDistrict.ToString(CultureInfo.InvariantCulture),
                    Text = districts.name.ToString(),
                    Selected = false
                })
                );
            return result;
        }
    }
}