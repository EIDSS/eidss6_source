using eidss.model.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eidss.model.Model.UploadEhs
{
    public class ValidateDataResult
    {
        public UploadEhsMasterState PatientState { get; set; }
        public UploadEhsMasterState EventState { get; set; } 

        public UploadEhsFileResult PatientError { get; set; }
        public UploadEhsFileResult EventError { get; set; }
        public bool HasPatientErrorFile { get; set; }
        public bool HasEventErrorFile { get; set; }

        public byte[] PatientFileContent { get; set; }
        public byte[] EventFileContent { get; set; }
    }
}
