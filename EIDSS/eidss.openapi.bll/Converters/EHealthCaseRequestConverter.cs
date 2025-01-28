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
    internal class EHealthCaseRequestConverter :
        BaseConverter<eidss.openapi.contract.EHealthCaseRequest, eidss.model.Schema.EHealthCaseAMRequest>
    {
        private static EHealthCaseRequestConverter _instance = new EHealthCaseRequestConverter();
        private EHealthCaseRequestConverter() { AutoConverter.Nop(); }
        public static EHealthCaseRequestConverter Instance { get { return _instance; } }

        internal static void Register()
        {
            Mapper.CreateMap<eidss.openapi.contract.EHealthCaseRequest, eidss.model.Schema.EHealthCaseAMRequest>()
                .ForMember(p => p.LangID, e => e.MapFrom(m => ModelUserContext.CurrentLanguage))
                .ForMember(p => p.EHealthCaseAMItems, e => e.MapFrom(m => m.Values))

                // ignore
                .ForMember(p => p.idfEHealthCaseAMRequest, e => e.Ignore())
                .ForMember(p => p.idfPersonEnteredBy, e => e.Ignore())
                .ForMember(p => p.idfUserID, e => e.Ignore())
                .ForMember(p => p.xmlEHealthCaseAM, e => e.Ignore())
                .ForMember(p => p.intRetCode, e => e.Ignore())
                .ForMember(p => p.strOutputComment, e => e.Ignore())
                ;
            Mapper.CreateMap<eidss.model.Schema.EHealthCaseAMRequest, eidss.openapi.contract.EHealthCaseRequest>()
                .ForMember(c => c.Values, e => e.MapFrom(m => m.EHealthCaseAMItems))
                ;
        }

    }
}