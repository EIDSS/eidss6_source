var uploadEhs = (function () {

    return {
        downloadFile: downloadFile,
        showEventError:showEventError,
        showPatientError: showPatientError,
        selectFileForUploading: selectFileForUploading,
        validateFileSize: validateFileSize,
        patientOnchangeHandler: patientOnchangeHandler,
        eventOnchangeHandler: eventOnchangeHandler,
        showEditWindow: showEditWindow,
        dismissWhenFinalize: dismissWhenFinalize,
        markErrorDuplicates: markErrorDuplicates,
        dismissAllDuplicates: dismissAllDuplicates,
        getNextDuplicate: getNextDuplicate,
        getPreviousDuplicate: getPreviousDuplicate,
        setCreateAsNewGetNextDuplicate: setCreateAsNewGetNextDuplicate,
        setDismissedGetNextDuplicate: setDismissedGetNextDuplicate,
        setUpdatedGetNextDuplicate: setUpdatedGetNextDuplicate,
        finalizeResolveDuplicates: finalizeResolveDuplicates,
        showSaveFileDialog: showSaveFileDialog
    }

    function showEditWindow(root, id) {
        if (id != 0) {
            var url = bvUrls.getUploadEhsUpdatePickerUrl({ root: root, id: id });
            bvGrid.showEditWindow(url, 680, 350, EIDSS.BvMessages['titleEhsPatientForUpdate'], "U02");
        }
    }

    function showSaveFileDialog(id, message) {
        bvDialog.showYesNo(msgConfimation,
            message,
            function () {
                showLoading();
                window.location.href = bvUrls.getUploadEhsSaveUploadedDataUrl({ id: id });
            },
            function () {
                window.location.href = bvUrls.getuploadEhsCancelUploadedDataUrl({ id: id });
            });
    }

    function dismissWhenFinalize(root) {

        var url = bvUrls.getUploadEhsDismissAllExistingPatientItemsUrl({ root: root });

        window.location.href = url;
    }

    function dismissAllDuplicates(root) {

        var url = bvUrls.getUploadEhsDismissAllExistingPatientItemsUrl({ root: root });

        bvDialog.showYesNo(msgConfimation,
            EIDSS.BvMessages['msgUploadEhsDismissAllExistingPatients'],
            function () {
                bvPopup.closeDefaultPopup();
                window.location.href = url;
            });
    }

    function handleDuplicate(root, id, duplicateAction) {
        var url = bvUrls.getUploadEhsUpdatePickerUrl({ root: root, id: id, duplicateAction: duplicateAction });

        $.get(url, function (data) {
            $('#WindowBody').html(data);
        });
    }

    function getNextDuplicate(root, id) {
        handleDuplicate(root, id, "next");
    }

    function getPreviousDuplicate(root, id) {
        handleDuplicate(root, id, "previous");
    }

    function setCreateAsNewGetNextDuplicate(root, id) {
        handleDuplicate(root, id, "createasnew");
    }

    function setDismissedGetNextDuplicate(root, id) {
        handleDuplicate(root, id, "dismiss");
    }

    function setUpdatedGetNextDuplicate(root, id) {
        handleDuplicate(root, id, "update");
    }

    function markErrorDuplicates() {
        var eidssInputs = $("input[name*='EIDSS']");

        for (var i = 0; i < eidssInputs.length; i++) {
            var foreignName = "[id='" + eidssInputs[i].name.replace("EIDSS", "EHS") + "']";
            var foreignInput = $(foreignName);

            if (foreignInput.attr('name')) {
                if (foreignInput.val() !== eidssInputs[i].value) {
                    foreignInput.addClass("errorMessage");
                    //eidssInputs[i].className += " errorMessage";
                    $(eidssInputs[i]).addClass("errorMessage");
                }
            }
        }
    }

    function finalizeResolveDuplicates(root) {
        var url = bvUrls.getUploadEhsFinalizeResolveExistingPatientItemsUrl({ root: root });

        window.location.href = url;
    }

    function downloadFile(url) {
        var link = document.createElement("a");
        link.download = url;
        link.target = "_blank";

        link.href = url;
        document.body.appendChild(link);
        link.click();

        document.body.removeChild(link);
        delete link;
    }

    function showPatientError(error) {
        hidePatientErrors();
        hideEventErrors();
        document.getElementById('patientMessagePlaceHolder').innerHTML += '<div class=\'errorMessage\'>' + error + '</div>';
    }

    function showEventError(error) {
        hidePatientErrors();
        hideEventErrors();
        document.getElementById('eventMessagePlaceHolder').innerHTML += '<div class=\'errorMessage\'>' + error + '</div>';
    }

    function hidePatientErrors() {
        var errorMessage = document.getElementById('patientMessagePlaceHolder');
        var errors = errorMessage.getElementsByClassName('errorMessage');

        for (var i = 0; i < errors.length; i++) 
            errorMessage.removeChild(errors[i]);
    }

    function hideEventErrors() {
        var errorMessage = document.getElementById('eventMessagePlaceHolder');
        var errors = errorMessage.getElementsByClassName('errorMessage');

        for (var i = 0; i < errors.length; i++)
            errorMessage.removeChild(errors[i]);
    }

    function selectFileForUploading(inputId) {

        hidePatientErrors();
        hideEventErrors();

        var filename = this.value.replace(/.+[\\\/]/, '');
        var format = filename.split('.').pop().toLowerCase();
        if (format != 'json') {

            if (inputId == "labFileValue")
                showEventError(EIDSS.BvMessages['msgEhsUnsupportedExtention']);
            if (inputId == "idaddressFileValue")
                showPatientError(EIDSS.BvMessages['msgEhsUnsupportedExtention']);
            return;
        }

        document.getElementById(inputId).value = filename;

        ValidateForm();
    }

    function validateFileSize(file, callback, message, size) {
        var fileSize = file.files[0].size / 1024 / 1024; // in MiB
        if (fileSize > size) {
            callback(message);
            return false;
        }
        return true;
    }

    function eventOnchangeHandler(self, message, size) {
        if (uploadEhs.validateFileSize(self, showEventError, message, size))
            uploadEhs.selectFileForUploading.call(self, 'labFileValue');
    }

    function patientOnchangeHandler(self, message, size) {
        if (uploadEhs.validateFileSize(self, showPatientError, message, size))
            uploadEhs.selectFileForUploading.call(self, 'idaddressFileValue');
    }

    function ValidateForm() {

        var json1 = document.getElementById('labFileValue').value;
        var json2 = document.getElementById('idaddressFileValue').value;

        if (json1 && json2)
        {
            $('#btnUpload').removeAttr('disabled');
            $('#btnUpload').removeClass('upload-disabled');
        }
        else
        {
            $('#btnUpload').attr('disabled', 'disabled');
            $('#btnUpload').addClass('upload-disabled');
        }
    }

})();