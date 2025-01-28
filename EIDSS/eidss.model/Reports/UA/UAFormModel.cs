using eidss.model.Reports.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eidss.model.Core;

namespace eidss.model.Reports.UA
{
    [Serializable]
    public class UAFormModel : BaseYearModel
    {
        [LocalizedDisplayName("Month")]
        public int? Month { get; set; }
        public long? RegionId { get; set; }

        private AddressModel _address;
        public AddressModel Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                RegionId = value.RegionId;
            }
        }

        public UAFormModel()
        {
            Address = new AddressModel();
        }

        public UAFormModel(long? regionId)
        {
            Address = new AddressModel(regionId, null);
        }

        public UAFormModel(string language, int year, int? month, long? regionId, bool useArchive)
            : base(language, year, useArchive)
        {
            Month = month;
            Address = new AddressModel(regionId, null);
        }

        public virtual List<SelectListItemSurrogate> SelectedCurrentMonthList
        {
            get { return FilterHelper.GetWebMonthList(DateTime.Now.Month, false); }
        }

        public string GenerateName()
        {
            StringBuilder name = new StringBuilder();

            name.Append(Year.ToString());
            name.Append('-');

            if (Month.HasValue)
            {
                name.Append(Month.Value);
                name.Append('-');
            }

            if (Address.RegionId.HasValue)
            {
                name.Append(Address.RegionName("en"));
            }
            else
            {
                name.Append("NoRegion");
            }

            return name.ToString();
        }
    }
}
