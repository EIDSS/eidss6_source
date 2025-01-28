namespace eidss.model.Enums
{
    public enum SecurityAuditEvent : long
	{
		Logon = 10110000,
		Logoff = 10110001,
		ProcessStart = 10110002,
		ProcessStop = 10110003,
		ProtecteDataUpdate = 10110004,
		Update = 10110005,
        DataArciving = 10110006,
        AuthorizationEds = 10110007,
        SignEds = 10110008
	}
	
}
