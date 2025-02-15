function getActiveTokensCall() {
    blockScreen();
    getActiveTokens("getActiveTokensBack");
}

function getActiveTokensBack(result) {
    unblockScreen();
    if (result['code'] === "500") {
        alert(result['message']);
    } else if (result['code'] === "200") {
        var listOfTokens = result['responseObject'];
        $('#storageSelect').empty();
        $('#storageSelect').append('<option value="PKCS12">PKCS12</option>');
        for (var i = 0; i < listOfTokens.length; i++) {
            $('#storageSelect').append('<option value="' + listOfTokens[i] + '">' + listOfTokens[i] + '</option>');
        }
    }
}

function getKeyInfoCall() {
    blockScreen();
    //var selectedStorage = $('#storageSelect').val();
    var selectedStorage = "PKCS12";
    getKeyInfo(selectedStorage, "getKeyInfoBack");
}

function getKeyInfoBack(result) {
    unblockScreen();
    if (result['code'] === "500") {
        alert(result['message']);
    } else if (result['code'] === "200") {
        var res = result['responseObject'];
        res['subjectDn'] = res['subjectDn'].replaceAll('\"', '&quot');
        var date = new Date(Number(res['certNotAfter']));
        res['certNotAfter'] = date.toLocaleString();

        if (defaultPage.loginSuccessfull) {
            var form = $('#idButtonLogin').parents('form:first');
            form.submit(function (eventObj) {
                $(this).append('<input type="hidden" name="alias" value="' + res['alias'] + '" /> ');
                $(this).append('<input type="hidden" name="keyId" value="' + res['keyId'] + '" /> ');
                $(this).append('<input type="hidden" name="algorithm" value="' + res['algorithm'] + '" /> ');
                $(this).append('<input type="hidden" name="subjectCn" value="' + res['subjectCn'] + '" /> ');
                $(this).append('<input type="hidden" name="subjectDn" value="' + res['subjectDn'] + '" /> ');
                $(this).append('<input type="hidden" name="issuerCn" value="' + res['issuerCn'] + '" /> ');
                $(this).append('<input type="hidden" name="issuerDn" value="' + res['issuerDn'] + '" /> ');
                $(this).append('<input type="hidden" name="serialNumber" value="' + res['serialNumber'] + '" /> ');
                $(this).append('<input type="hidden" name="certNotAfter" value="' + res['certNotAfter'] + '" /> ');
                $(this).append('<input type="hidden" name="certNotBefore" value="' + res['certNotBefore'] + '" /> ');
                $(this).append('<input type="hidden" name="authorityKeyIdentifier" value="' + res['authorityKeyIdentifier'] + '" /> ');
                $(this).append('<input type="hidden" name="pem" value="' + res['pem'] + '" /> ');
                return true;
            });
            form.submit();
        }
        //var alias = res['alias'];
        //$("#alias").val(alias);

        //var keyId = res['keyId'];
        //$("#keyId").val(keyId);

        //var algorithm = res['algorithm'];
        //$("#algorithm").val(algorithm);

        //var subjectCn = res['subjectCn'];
        //$("#subjectCn").val(subjectCn);

        //var subjectDn = res['subjectDn'];
        //$("#subjectDn").val(subjectDn);

        //var issuerCn = res['issuerCn'];
        //$("#issuerCn").val(issuerCn);

        //var issuerDn = res['issuerDn'];
        //$("#issuerDn").val(issuerDn);

        //var serialNumber = res['serialNumber'];
        //$("#serialNumber").val(serialNumber);

        //var dateString = res['certNotAfter'];
        //var date = new Date(Number(dateString));
        //$("#notafter").val(date.toLocaleString());

        //dateString = res['certNotBefore'];
        //date = new Date(Number(dateString));
        //$("#notbefore").val(date.toLocaleString());

        //var authorityKeyIdentifier = res['authorityKeyIdentifier'];
        //$("#authorityKeyIdentifier").val(authorityKeyIdentifier);

        //var pem = res['pem'];
        //$("#pem").val(pem);
    }
}

function signXmlCall(xml, keyType) {//"SIGNATURE", "AUTHENTICATION"
    var xmlToSign = xml;
    /*var selectedStorage = $('#storageSelect').val();*/
    var selectedStorage = "PKCS12";
    blockScreen();
    if (keyType == "AUTHENTICATION") {
        signXml(selectedStorage, keyType, xmlToSign, "signXmlBack");
    } else if (keyType == "SIGNATURE") {
        signXml(selectedStorage, keyType, xmlToSign, "signXmlBackSignature");
    }
}

function signXmlBack(result) {
    unblockScreen();
    if (result['code'] === "500") {
        if (result['message'] == 'action.canceled') {
            alert(EIDSS.BvMessages['msgEdsResultActionCanceled']);
            defaultPage.logEdsCanceled();
        }
    } else if (result['code'] === "200") {
        var res = result['responseObject'];
        defaultPage.checkEds(res);
    }
}
function signXmlBackSignature(result) {
    unblockScreen();
    if (result['code'] === "500") {
        if (result['message'] == 'action.canceled') {
            alert(EIDSS.BvMessages['msgEdsResultActionCanceled']);
            defaultPage.logEdsCanceled();
            window.numberOfTry = 0;
        }
    } else if (result['code'] === "200") {
        var res = result['responseObject'];
        PostSignPendingEds(res);
    }
}

function signXmlsCall() {
    var xmlToSign1 = $("#xmlToSign1").val();
    var xmlToSign2 = $("#xmlToSign2").val();
    var xmlsToSign = new Array(xmlToSign1, xmlToSign2);
    var selectedStorage = $('#storageSelect').val();
    blockScreen();
    signXmls(selectedStorage, "SIGNATURE", xmlsToSign, "signXmlsBack");
}

function signXmlsBack(result) {
    unblockScreen();
    if (result['code'] === "500") {
        alert(result['message']);
    } else if (result['code'] === "200") {
        var res = result['responseObject'];
        $("#signedXml1").val(res[0]);
        $("#signedXml2").val(res[1]);
    }
}

function createCAdESFromFileCall() {
    var selectedStorage = $('#storageSelect').val();
    var flag = $("#flag").is(':checked');
    var filePath = $("#filePath").val();
    if (filePath !== null && filePath !== "") {
        blockScreen();
        createCAdESFromFile(selectedStorage, "SIGNATURE", filePath, flag, "createCAdESFromFileBack");
    } else {
        alert("Не выбран файл для подписи!");
    }
}

function createCAdESFromFileBack(result) {
    unblockScreen();
    if (result['code'] === "500") {
        alert(result['message']);
    } else if (result['code'] === "200") {
        var res = result['responseObject'];
        $("#createdCMS").val(res);
    }
}

function createCAdESFromBase64Call() {
    var selectedStorage = $('#storageSelect').val();
    var flag = $("#flagForBase64").is(':checked');
    var base64ToSign = $("#base64ToSign").val();
    if (base64ToSign !== null && base64ToSign !== "") {
        $.blockUI();
        createCAdESFromBase64(selectedStorage, "SIGNATURE", base64ToSign, flag, "createCAdESFromBase64Back");
    } else {
        alert("Нет данных для подписи!");
    }
}

function createCAdESFromBase64Back(result) {
    $.unblockUI();
    if (result['code'] === "500") {
        alert(result['message']);
    } else if (result['code'] === "200") {
        var res = result['responseObject'];
        $("#createdCMSforBase64").val(res);
    }
}

function createCAdESFromBase64HashCall() {
    var selectedStorage = $('#storageSelect').val();
    var base64ToSign = $("#base64HashToSign").val();
    if (base64ToSign !== null && base64ToSign !== "") {
        $.blockUI();
        createCAdESFromBase64Hash(selectedStorage, "SIGNATURE", base64ToSign, "createCAdESFromBase64HashBack");
    } else {
        alert("Нет данных для подписи!");
    }
}

function createCAdESFromBase64HashBack(result) {
    $.unblockUI();
    if (result['code'] === "500") {
        alert(result['message']);
    } else if (result['code'] === "200") {
        var res = result['responseObject'];
        $("#createdCMSforBase64Hash").val(res);
    }
}

function applyCAdESTCall() {
    var selectedStorage = $('#storageSelect').val();
    var cmsForTS = $("#CMSForTS").val();
    if (cmsForTS !== null && cmsForTS !== "") {
        $.blockUI();
        applyCAdEST(selectedStorage, "SIGNATURE", cmsForTS, "applyCAdESTBack");
    } else {
        alert("Нет данных для подписи!");
    }
}

function applyCAdESTBack(result) {
    $.unblockUI();
    if (result['code'] === "500") {
        alert(result['message']);
    } else if (result['code'] === "200") {
        var res = result['responseObject'];
        $("#createdCMSWithAppliedTS").val(res);
    }
}

function showFileChooserCall() {
    blockScreen();
    showFileChooser("ALL", "", "showFileChooserBack");
}

function showFileChooserBack(result) {
    unblockScreen();
    if (result['code'] === "500") {
        alert(result['message']);
    } else if (result['code'] === "200") {
        var res = result['responseObject'];
        $("#filePath").val(res);
    }
}

function showFileChooserForTSCall() {
    blockScreen();
    showFileChooser("ALL", "", "showFileChooserForTSBack");
}

function showFileChooserForTSBack(result) {
    unblockScreen();
    if (result['code'] === "500") {
        alert(result['message']);
    } else if (result['code'] === "200") {
        var res = result['responseObject'];
        $("#filePathWithTS").val(res);
    }
}

function changeLocaleCall() {
    var selectedLocale = $('#localeSelect').val();
    changeLocale(selectedLocale);
}

function createCMSSignatureFromFileCall() {
    var selectedStorage = $('#storageSelect').val();
    var flag = $("#flagForCMSWithTS").is(':checked');
    var filePath = $("#filePathWithTS").val();
    if (filePath !== null && filePath !== "") {
        blockScreen();
        createCMSSignatureFromFile(selectedStorage, "SIGNATURE", filePath, flag, "createCMSSignatureFromFileBack");
    } else {
        alert("Не выбран файл для подписи!");
    }
}

function createCMSSignatureFromFileBack(result) {
    unblockScreen();
    if (result['code'] === "500") {
        alert(result['message']);
    } else if (result['code'] === "200") {
        var res = result['responseObject'];
        $("#createdCMSWithTS").val(res);
    }
}

function createCMSSignatureFromBase64Call() {
    var selectedStorage = $('#storageSelect').val();
    var flag = $("#flagForBase64WithTS").is(':checked');
    var base64ToSign = $("#base64ToSignWithTS").val();
    if (base64ToSign !== null && base64ToSign !== "") {
        $.blockUI();
        createCMSSignatureFromBase64(selectedStorage, "SIGNATURE", base64ToSign, flag, "createCMSSignatureFromBase64Back");
    } else {
        alert("Нет данных для подписи!");
    }
}

function createCMSSignatureFromBase64Back(result) {
    $.unblockUI();
    if (result['code'] === "500") {
        alert(result['message']);
    } else if (result['code'] === "200") {
        var res = result['responseObject'];
        $("#createdCMSforBase64WithTS").val(res);
    }
}