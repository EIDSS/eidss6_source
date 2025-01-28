using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eidss.model.Model.UploadEhs
{
    public class SaveDataResult
    {
        public bool IsSuccessful { get; set; }
        public byte[] EventJsonWithResults { get; set; }
    }
}
