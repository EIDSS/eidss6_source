var redirectUrl = null;
var numberOfTry = 0;

function ReportContextMenu() {
    var action = "Reports";
    var caseId = $("#idfCase").val();
    if (!caseId) {
        caseId = $("#idfMonitoringSession").val();
    }
    if (!caseId) {
        caseId = $("#idfAggrCase").val();
        /*if (caseId) {
            action = "ReportsForAggr";
        }*/
    }
    if (!caseId) {
        caseId = $("#idfOutbreak").val();
    }
    if (!caseId) {
        caseId = $("#idfVectorSurveillanceSessionHeartbeat").val();
    }
    var btn = $("#bMainPanel_ReportContextMenu");
    var x = btn.position().left;
    var y = btn.position().top + btn.height() + 1;
    popupMenu.showPopupMenu(action, "", caseId, x, y);
    setTimeout(
        function () {
            $('body').on("click", popupMenu.hidePopupMenuAndUnbind);
        }, 200);

}

function EmergencyNotificationReport() {
    humanCase.onENReportClick();
}

function EmergencyNotificationDTRAReport() {
    humanCase.onENDReportClick();
}

function EmergencyNotificationUkraineReport() {
    humanCase.onENDReportClick();
}

function UrgentNotificationReportJo() {
    vetCase.onUNRReportClick();
}

function EmergencyNotificationTanzaniaReport() {
    humanCase.onENTReportClick();
}
function HumanInvestigationReport() {
    humanCase.onHIReportClick();
}

function VetInvestigationReport() {
    vetCase.onVIReportClick();
}

function HumanAggregateCaseReport() {
    aggregateCase.onHumanAggrCaseReportClick();
}

function VetAggregateCaseReport() {
    aggregateCase.onVetAggrCaseReportClick();
}

function VetAggregateActionReport() {
    aggregateCase.onVetAggrActionReportClick();
}

function OutbreakReport() {
    outbreak.onOutbreakReportClick();
}

function AsSampleCollectedListReport() {
    asSession.onReportAsSampleCollectedListClick();
}

function AsSessionTestsReport() {
    asSession.onReportAsSessionTestsClick();
}

function ShowGeneralCaseInvestigationForm() {
    var url = GetReportFilePath("General Case Investigation Form.pdf");
    OpenPopup(url, " ");
}

function ShowAvianDiseaseOutbreaksForm() {
    var url = GetReportFilePath("Investigation Form For Avian Disease Outbreaks.pdf");
    OpenPopup(url, " ");
}

function ShowLivestockDiseaseOutbreaksForm() {
    var url = GetReportFilePath("Investigation Form For Livestock Disease Outbreaks.pdf");
    OpenPopup(url, " ");
}

function GetReportFilePath(fileName) {
    var lang = GetSiteLanguage();
    var index = lang.indexOf('-');
    var language = lang.substring(0, index).toLowerCase();
    var url1 = "/Content/PaperForms/" + language + "/" + fileName;
    return url1;
}

function DeleteTest(url) {
    var selecteditem = listForm.getSelectedItemId();
    if ((selecteditem == null) || (selecteditem.length == 0)) return;
    bvDialog.showDeletePrompt(function () {
        selecteditem = listForm.getSelectedItemId();
        var url1 = url + "?id=" + selecteditem;
        actions.doDelete(url1);
    });
}

function SelectTest(url) {
    var selecteditem = listForm.getSelectedItemId();
    if ((selecteditem == null) || (selecteditem.length == 0)) return;
    url += "?id=" + selecteditem;
    OpenPopup(url, " ", 840, 670);
}

function EditTest(url) {
    var selecteditem = listForm.getSelectedItemId();
    if ((selecteditem == null) || (selecteditem.length == 0)) return;
    url += "?id=" + selecteditem;
    OpenPopup(url, " ", 840, 670);
}

function EditSample(url) {
    var selecteditem = listForm.getSelectedItemId();
    if ((selecteditem == null) || (selecteditem.length == 0)) return;
    url += "?id=" + selecteditem;
    OpenPopup(url, " ", 840, 550);
}

function CleanUpPendingEds(url) {
    $.ajax({
        url: url,
        dataType: "json",
        type: "Post",
        success: function (result) {
            if (result.returnUrl) {
                window.location.reload(true);
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function SignPendingEds(url) {
    redirectUrl = url;

    var urlInfoForSignPendingEds = bvUrls.getInfoForSignPendingEds();
    var dataInfoForSignPendingEds = {};

    $.ajax({
        url: urlInfoForSignPendingEds,
        dataType: "json",
        type: "Post",
        data: dataInfoForSignPendingEds,
        success: function (result) {
            if (result.ticket) {
                defaultPage.signTicket = result.ticket;
            }
            else {
                defaultPage.signTicket = generateEdsXml();
            }
            if (numberOfTry < defaultPage.maxAttempts) {
                numberOfTry++;
                signXmlCall(defaultPage.signTicket, "SIGNATURE");
            } else {
                alert(EIDSS.BvMessages['msgEdsResultNumberOfAttemptsExceeded']);
                numberOfTry = 0;
                defaultPage.logEdsExceededAttempts(function () {
                    var location = window.location.href.replace('PendingJournal/Index', 'Account/Logout');
                    window.location.replace(location);
                });
            }        },
        error: function (error) {
            console.log(error);
        }
    });
}



function generateEdsXml() {
    var doc = `<?xml version="1.0" encoding="utf-8" ?>
                <config xmlns="urn:config-schema">
                <signdata>
                  <eventlist>
                  </eventlist>
                  <signdate></signdate>
                  <signuser></signuser>
                </signdata>
                </config>`;
    var xml = $.parseXML(doc);
    $(xml).find('signdate').each(function () {
        $(this).text(createDateForXml());
    });
    var eventlist = $(xml).find('eventlist');
    $('.lfGridTd tbody tr').each(function (index, tr) {
        var date = $(tr).find('td:eq(3)').text();
        date = createDateForXml(date);
        $($.parseXML('<event id=' + '"' + $(tr).find('.gridId').text() + '"' + ' date=' + '"' +date + '"' + '/>')).find("event").appendTo(eventlist);
    });

    return (new XMLSerializer()).serializeToString(xml);
}

function createDateForXml(date) {
    if (date) {
        var d = new Date(date);
    } else {
        var d = new Date();
    }
    var second = d.getSeconds();
    var minute = d.getMinutes();
    var hour = d.getHours();
    var day = d.getDate();
    var month = d.getMonth() + 1;
    var year = d.getFullYear();
    if (day < 10) {
        day = "0" + day;
    }
    if (month < 10) {
        month = "0" + month;
    }
    var date = year + "-" + month + "-" + day + " " + hour + ":" + minute + ":" + second;

    return date;
}

function PostSignPendingEds(eds)
{
    var data = {};
    data.Xml = btoa(eds);

    $.ajax({
        url: redirectUrl,
        dataType: "json",
        type: "Post",
        data: data,
        success: function (result) {
            if (result.returnUrl) {
                alert(EIDSS.BvMessages['msgEdsResultSignSuccess']);
                window.location.replace(result.returnUrl);
            }
            else {
                
                //var ticket = generateEdsXml();
                if (numberOfTry < defaultPage.maxAttempts) {
                    alert(result.ErrorMessage);
                    numberOfTry++;
                    //signXmlCall(ticket, "SIGNATURE");
                    signXmlCall(defaultPage.signTicket, "SIGNATURE");
                } else {
                    alert(EIDSS.BvMessages['msgEdsResultNumberOfAttemptsExceeded']);
                    numberOfTry = 0;
                    defaultPage.logEdsExceededAttempts(function () {
                        var location = window.location.href.replace('PendingJournal/Index', 'Account/Logout');
                        window.location.replace(location);
                    });
                }
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}
/*
function ActionPreferencesExecution(url) {
    $.ajax({
        url: url,
        dataType: "json",
        type: "GET",
        success: function (data) {
            //alert('complete');                  
        }
    });
}

function AddToPreferences(url) {
    var selecteditem = listForm.getSelectedItemId();
    if ((selecteditem == null) || (selecteditem.length == 0)) return;
    bvDialog.showDialog(msgConfimation, EIDSS.BvMessages['msgAddToPreferencesPrompt'], EIDSS.BvMessages['strAdd_Id'],
        function () {
            selecteditem = listForm.getSelectedItemId();
            var url1 = url + "?id=" + selecteditem;
            ActionPreferencesExecution(url1);
        }, bvDialog.buttonText.cancel, null, null);
}

function RemoveFromPreferences(url) {
    var selecteditem = listForm.getSelectedItemId();
    if ((selecteditem == null) || (selecteditem.length == 0)) return;
    bvDialog.showDialog(msgConfimation, EIDSS.BvMessages['msgRemoveFromPreferencesPrompt'], EIDSS.BvMessages['strRemove_Id'],
        function () {
            selecteditem = listForm.getSelectedItemId();
            var url1 = url + "?id=" + selecteditem;
            ActionPreferencesExecution(url1);
        }, bvDialog.buttonText.cancel, null, null);
}
*/
/*
function HumanAccessionIn(url) {
    detailPage.open(url, true, true);
}
function VetAccessionIn(url) {
    detailPage.open(url, true, true);
}
function AsAccessionIn(url) {
    detailPage.open(url, true, true);
}
function VsAccessionIn(url) {
    detailPage.open(url, true, true);
}
*/
function VsSessionReport() {
    vssession.onVsSessionReportClick();
}