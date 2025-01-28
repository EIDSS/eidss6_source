using bv.model.BLToolkit;
using System;

namespace eidss.model.Schema
{

    public partial class UploadEhsExistingPatientItem
	{
		public partial class Accessor
		{
            public UploadEhsExistingPatientItem SelectByItem(DbManagerProxy manager, UploadEhsPatientItem checkItem)
			{
				var ret = SelectByKey(manager
					, checkItem.idfsUploadEhs
                    , checkItem.patient_id
                    , checkItem.first_name
                    , checkItem.last_name
                    , checkItem.second_name
                    , checkItem.person_birth_date
                    , checkItem.gender
                    , checkItem.phones
                    , checkItem.address_zip
                    , checkItem.address_area
                    , checkItem.address_region
                    , checkItem.address_settlement
                    , checkItem.address_street
                    , checkItem.address_building
                    , checkItem.address_apartment
                    , checkItem.address_type
					);
				if (ret != null)
				{
					ret.Item = checkItem;
				}
				return ret;
			}
        }


        public bool IsDistinct()
        {
            if (!first_name_EHS.Equals(first_name_EIDSS, StringComparison.CurrentCultureIgnoreCase))
                return true;
            if (!last_name_EHS.Equals(last_name_EIDSS, StringComparison.CurrentCultureIgnoreCase))
                return true;
            if (!second_name_EHS.Equals(second_name_EIDSS, StringComparison.CurrentCultureIgnoreCase))
                return true;
            if ((person_birth_date_EHS.HasValue && !person_birth_date_EIDSS.HasValue) ||
                (!person_birth_date_EHS.HasValue && person_birth_date_EIDSS.HasValue) ||
                (person_birth_date_EHS.HasValue && person_birth_date_EIDSS.HasValue && person_birth_date_EHS.Value != person_birth_date_EIDSS.Value))
                return true;
            if (!gender_EHS.Equals(gender_EIDSS, StringComparison.CurrentCultureIgnoreCase))
                return true;
            if (!address_zip_EHS.Equals(address_zip_EIDSS, StringComparison.CurrentCultureIgnoreCase))
                return true;
            if (!address_area_EHS.Equals(address_area_EIDSS, StringComparison.CurrentCultureIgnoreCase))
                return true;
            if (!address_region_EHS.Equals(address_region_EIDSS, StringComparison.CurrentCultureIgnoreCase))
                return true;
            if (!address_settlement_EHS.Equals(address_settlement_EIDSS, StringComparison.CurrentCultureIgnoreCase))
                return true;
            if (!address_street_EHS.Equals(address_street_EIDSS, StringComparison.CurrentCultureIgnoreCase))
                return true;
            if (!address_building_EHS.Equals(address_building_EIDSS, StringComparison.CurrentCultureIgnoreCase))
                return true;
            if (!address_apartment_EHS.Equals(address_apartment_EIDSS, StringComparison.CurrentCultureIgnoreCase))
                return true;
            if (!address_type_EHS.Equals(address_type_EIDSS, StringComparison.CurrentCultureIgnoreCase))
                return true;

            return false;

        }

    }
}
