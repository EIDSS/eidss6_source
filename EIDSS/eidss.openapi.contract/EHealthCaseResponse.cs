namespace eidss.openapi.contract
{
    /// <summary>Full description of the response for transferring cases from e-Health into EIDSS</summary>
    public class EHealthCaseResponse
    {
        /// <summary>
        /// Result code for the process of the transferring cases from e-Health into EIDSS.
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Comments for the process of the transferring cases from e-Health into EIDSS.
        /// </summary>
        public string Description { get; set; }
    }
}
