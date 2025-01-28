using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using bv.common.Core;
using bv.model.BLToolkit;
using EIDSS.Reports.BaseControls.Report;

namespace EIDSS.Reports.BaseControls.FlexFormIntegration
{
    public static class FlexConverter
    {
        public static void FillCaseTable
            (DbManagerProxy manager, FlexParamDataSet sourceData, IDictionary<string, string> parameters, string lang)
        {
            Utils.CheckNotNull(sourceData, "sourceData");
            Utils.CheckNotNull(parameters, "parameters");
            Utils.CheckNotNullOrEmpty(lang, "lang");

            if (parameters.ContainsKey("@ObjID"))
            {
                var caseId = long.Parse(parameters["@ObjID"]);

                using (var adapter = new SqlDataAdapter())
                {
                    var command = ((SqlConnection) manager.Connection).CreateCommand();
                    command.Transaction = (SqlTransaction) manager.Transaction;
                    command.CommandTimeout = BaseReport.CommandTimeout;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "select * from dbo.fnRepGetAggParams (@idfAggrCase, @LangID)";
                    command.Parameters.Add(new SqlParameter("@idfAggrCase", caseId));
                    command.Parameters.Add(new SqlParameter("@LangID", lang));

                    var aggParamsDataSet = new DataSet();
                    adapter.SelectCommand = command;
                    adapter.Fill(aggParamsDataSet);
                    if (aggParamsDataSet.Tables.Count == 0)
                    {
                        throw new ApplicationException(string.Format("{0} returns no tables.", command.CommandText));
                    }
                    if (aggParamsDataSet.Tables[0].Rows.Count != 0)
                    {
                        var dataRow = aggParamsDataSet.Tables[0].Rows[0];
                        sourceData.tblCase.AddtblCaseRow((long) dataRow["idfActivity"],
                            dataRow["AdmUnitName"].ToString(),
                            dataRow["AdmUnitFullName"].ToString(),
                            (DateTime) dataRow["datStartDate"],
                            (DateTime) dataRow["datFinishDate"],
                            Utils.Str(dataRow["strCaseID"]),
                            (long) dataRow["idfsAdministrativeUnit"],
                            (long) dataRow["idfsAdmUnitType"]);
                    }
                }
            }

            sourceData.AcceptChanges();
        }
    }
}