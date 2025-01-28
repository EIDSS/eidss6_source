using System;
using eidss.model.Schema;

namespace eidss.model.Core
{
    public interface ICustomization
    {
        PersonIdentityServiceFeatures PersonIdentityServiceFeatures { get; }
        Tuple<Patient, string> GetFromPersonIdentityService(Patient p);
        VisibilityFeatures VisibilityFeatures { get; }
        string BuildFullName(string last, string first, string middle);
    }
}