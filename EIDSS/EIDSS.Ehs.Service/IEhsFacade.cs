using eidss.model.Model.UploadEhs;
using eidss.model.Schema;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EIDSS.EHS.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEhsFacade" in both code and config file together.
    [ServiceContract]
    public interface IEhsFacade
    {
        [OperationContract]
        ValidateDataResult ValidateData(string patientJson, string eventJson, long idfsUploadEhs, string lang);

        [OperationContract]
        SaveDataResult SaveUploadedData(long idfsUploadEhs, long idfPersonEnteredBy, long idfUserID, string lang);
    }
}
