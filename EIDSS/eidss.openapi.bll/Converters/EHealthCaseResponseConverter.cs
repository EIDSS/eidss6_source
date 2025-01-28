using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bv.model.BLToolkit;
using bv.model.Model.Core;
using eidss.model.Enums;
using eidss.model.Schema;
using eidss.openapi.bll.Exceptions;
using AutoMapper;

namespace eidss.openapi.bll.Converters
{
    internal class EHealthCaseResponseConverter :
        BaseConverter<eidss.openapi.contract.EHealthCaseResponse, eidss.model.Schema.EHealthCaseAMRequest>
    {
        private static EHealthCaseResponseConverter _instance = new EHealthCaseResponseConverter();
        private EHealthCaseResponseConverter() { AutoConverter.Nop(); }
        public static EHealthCaseResponseConverter Instance { get { return _instance; } }

        internal static void Register()
        {
            Mapper.CreateMap<eidss.openapi.contract.EHealthCaseResponse, eidss.model.Schema.EHealthCaseAMRequest>()
                .ForMember(p => p.LangID, e => e.MapFrom(m => ModelUserContext.CurrentLanguage))

                // ignore
                .ForMember(p => p.idfEHealthCaseAMRequest, e => e.Ignore())
                .ForMember(p => p.idfPersonEnteredBy, e => e.Ignore())
                .ForMember(p => p.idfUserID, e => e.Ignore())
                .ForMember(p => p.xmlEHealthCaseAM, e => e.Ignore())
                .ForMember(p => p.intRetCode, e => e.Ignore())
                .ForMember(p => p.strOutputComment, e => e.Ignore())
                .ForMember(p => p.EHealthCaseAMItems, e => e.Ignore())
                ;
            Mapper.CreateMap<eidss.model.Schema.EHealthCaseAMRequest, eidss.openapi.contract.EHealthCaseResponse>()
                .ForMember(c => c.Result, e => e.MapFrom(m => m.intRetCode.HasValue ? m.intRetCode.Value.ToString() : "Unknown"))
                .ForMember(c => c.Description, e => e.MapFrom(m => m.strOutputComment))
                ;
        }

    }
}