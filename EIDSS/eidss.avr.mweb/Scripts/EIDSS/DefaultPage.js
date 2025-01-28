(function () {
    $.ajaxSetup({
        cache: false,
        error: function (jqXhr, textStatus, errorThrown) {
            window.location = avrUrls.getAccountLogin();
        }
    });
})();


var defaultPage = {
    changePasswordPath: "/Account/ChangePassword",
    loginPath: "/Account/Login",
    regExp: /(http(s?):\/\/)/ig,
    formValidated: false,
    edsLogin: false,
    loginSuccessfull: false,
    numberOfTry: 1,
    maxAttempts: 3,
    ticket: null,
    loginModel: null,
    openWebsocket: false,

    /*regDocumentEvents: function () {
        $(document.body).click(defaultPage.hideLanguagePanel);
    },*/

    timeoutPageOnLoad: function () {
    },

    loginPageOnLoad: function () {
        defaultPage.autocompleteOff();
        defaultPage.changeFont();
        //defaultPage.regDocumentEvents();
        //defaultPage.initLanguagePanel();
        //defaultPage.hideLanguagePanel();
        defaultPage.showError();
        defaultPage.initCleanButtons();
        defaultPage.initForm();
        twinkleTextBox.init();
    },

    autocompleteOff: function () {
        $('form').attr('autocomplete', 'off');
    },

    checkTimeoutError: function (message) {
        var url = avrUrls.getAccountLogin();
        if (message.indexOf(url) >= 0)
            document.location = url;
    },

    changeFont: function () {
        var curLang = $("#LanguagePreference").val();
        if (curLang === "hy-AM") {
            $("body").addClass("fntAm");
        } else if (curLang === "ka-GE") {
            $("body").addClass("fntGe");
        } else if (curLang === "th-TH") {
            $("body").addClass("fntTh");
        }
    },

    showError: function () {
        var errorMessageSpan = $("#errorMessageSpan");
        var errorMessageText = errorMessageSpan[0].innerHTML;
        if (errorMessageText) {
            errorMessageSpan.show();
        }
    },

    changePasswordPageOnLoad: function () {
        defaultPage.autocompleteOff();
        defaultPage.changeFont();
        defaultPage.showChangePasswordErrorOrSuccess();
        defaultPage.initCleanButtons();
    },

    showChangePasswordErrorOrSuccess: function () {
        var errorMessageSpan = $("#errorMessageSpan")[0];
        var errorMessageText = errorMessageSpan.innerHTML;
        if (errorMessageText === 'success') {
            //defaultPage.hideChangePasswordPanel();
            //defaultPage.showChangePasswordSuccess();
        }
        else {
            defaultPage.showError();
            //defaultPage.hideChangePasswordSuccess();
        }
    },

    /*showChangePasswordSuccess: function () {
        $("#pnSuccessMessage").show();
    },

    hideChangePasswordSuccess: function () {
        $("#pnSuccessMessage").hide();
    },*/

    hideChangePasswordPanel: function () {
        $("#pnChangePassword").hide();
    },

    initCleanButtons: function () {
        if ($(".k-ie9").length > 0 || $(".k-webkit").length > 0) {
            $("span.k-textbox").addClass("k-space-right");
            $(".k-i-close").click(defaultPage.onCleanButtonClick);
        } else {
            $(".k-i-close").remove();
        }
    },

    initForm: function () {
        var url = avrUrls.getEdsIsNCALayerEnabled();
        $.ajax({
            url: url,
            dataType: "json",
            type: "Get",
            success: function (result) {
                if (result.CommunicateWithNCALayer) {
                    defaultPage.openWebsocket = result.CommunicateWithNCALayer;
                }
                var form = $('#idButtonLogin').parents('form:first');
                form.submit(defaultPage.onLoginSubmit);
            },
            error: function () {
            }
        });
    },

    onCleanButtonClick: function () {
        $(this).closest('span').children('input').val('').focus();
        return false;
    },

    /*initLanguagePanel: function () {
        $("#curLanguage").click(defaultPage.showLanguagePanel);
        $(".langItem").click(defaultPage.onLanguageSelect);
    },

    showLanguagePanel: function (e) {
        $("#languagePanel").show();
        e.stopPropagation();
    },

    hideLanguagePanel: function () {
        $("#languagePanel").hide();
    },

    onLanguageSelect: function () {
        var selectedItem = $(this);
        var selectedLangKey = selectedItem.data("key");
        var curLang = $("#LanguagePreference").val();
        var newUrl = document.URL.replace(curLang, selectedLangKey);
        document.location = newUrl;
    },*/

    onLanguageSelectNew: function (lang) {
        var curLang = $("#LanguagePreference").val();
        var newUrl = document.URL.replace(curLang, lang);
        document.location = newUrl;
    },

    onLoginSubmit: function (e) {
        debugger;
        if (defaultPage.formValidated) {
            return;
        } else {
            e.preventDefault();
        }
        var form = e.target;
        var isValid = defaultPage.validateLoginPage();
        if (!isValid)
            return;
        var data =
        {
            Organization: form.Organization.value,
            UserName: form.UserName.value,
            Password: form.Password.value
        };

        if (defaultPage.edsLogin)
            defaultPage.loginInternal(data);
        else
            form.submit();
    },

    loginInternal(data) {
        if (!data)
            return;

        var url = avrUrls.getLoginInternal();

        $.ajax({
            url: url,
            dataType: "json",
            type: "Post",
            data: data,
            success: function (result) {
                if (result.showEds) {
                    defaultPage.edsLogin = result.showEds;
                    defaultPage.maxAttempts = result.numberOfAttempts;
                    if (result.errorMessage) {
                        alert(result.errorMessage);
                        return;
                    }

                    if (result.ticket) {
                        defaultPage.ticket = result.ticket;
                        defaultPage.loginSuccessfull = true;

                        signXmlCall(defaultPage.ticket, "AUTHENTICATION");
                    }
                    else {
                        $("#errorMessageSpan")[0].innerHTML = result.ErrorMessage;
                        defaultPage.showError();
                        console.log('rejected NCA call');
                    }
                }
                else {
                    if (result.returnUrl) {
                        window.location.replace(result.returnUrl);
                    } else {
                        defaultPage.edsLogin = true;
                        $("#errorMessageSpan")[0].innerHTML = result.ErrorMessage;
                        defaultPage.showError();
                        console.log('rejected NCA call');
                    }
                }
            },
            error: function () {
            }
        });
    },

    checkEds(eds) {
        if (!eds)
            return;

        var url = avrUrls.getCheckEds();
        var data = {};
        data.Xml = btoa(eds);

        $.ajax({
            url: url,
            dataType: "json",
            type: "Post",
            data: data,
            success: function (result) {
                if (result.returnUrl) {
                    var form = $('#idButtonOk').parents('form:first')[0];
                    if (window.location.pathname == result.returnUrl)
                        form.submit();
                    else
                        window.location.replace(result.returnUrl);
                }
                else {
                    if (defaultPage.numberOfTry < defaultPage.maxAttempts) {
                        alert(result.ErrorMessage);
                        defaultPage.numberOfTry++;
                        signXmlCall(defaultPage.ticket, "AUTHENTICATION");
                    } else {
                            alert(EIDSS.BvMessages['msgEdsResultNumberOfAttemptsExceeded']);
                            defaultPage.logEdsExceededAttempts(function () {
                                defaultPage.numberOfTry = 1;
                                debugger;
                                var location = window.location.href.replace('Account/LoginLang', 'Account/Logout');
                                window.location.replace(location);
                            });
                        }
                    }
            },
            error: function (error) {
                console.log(error);
            }
        });
    },

    logEdsCanceled(callback) {
        var url = avrUrls.getEdsCanceled();

        $.ajax({
            url: url,
            dataType: "json",
            type: "Post",
            success: function (result) {
                if (callback)
                    callback();
            },
            error: function (error) {
                console.log(error);
            }
        });
    },

    logEdsExceededAttempts(callback) {
        var url = avrUrls.getEdsExceededAttempts();

        $.ajax({
            url: url,
            dataType: "json",
            type: "Post",
            success: function (result) {
                if (callback)
                    callback();
            },
            error: function (error) {
                console.log(error);
            }
        });
    },

    logEdsNCALayerClosed(callback) {
        var url = avrUrls.getEdsNCALayerClosed();

        $.ajax({
            url: url,
            dataType: "json",
            type: "Post",
            success: function (result) {
                if (callback)
                    callback();
            },
            error: function (error) {
                console.log(error);
            }
        });
    },

    validateLoginPage: function () {
        var org = $("#Organization").val();
        var log = $("#UserName").val();
        var pwd = $("#Password").val();
        if (org && log && pwd) {
            return true;
        }
        $("#errorMessageSpan")[0].innerHTML = EIDSS.BvMessages['errLoginMandatoryFields'];
        defaultPage.showError();
        return false;
    },

    goToChangePassword: function () {
        var org = $("#Organization").val();
        var userName = $("#UserName").val();
        var curLang = $("#LanguagePreference").val();
        var url = "/" + curLang + defaultPage.changePasswordPath + "?organization=" + org + "&userName=" + userName;
        document.location = url;
    },

    closeChangePassword: function () {
        document.location = avrUrls.getAccountLogin();
    },

    validateChangePasswordPage: function () {
        var org = $("#Organization").val();
        var log = $("#UserName").val();
        var opwd = $("#OldPassword").val();
        var npwd = $("#NewPassword").val();
        var cpwd = $("#ConfirmPassword").val();
        if (org && log && opwd && npwd && cpwd) {
            return true;
        }
        $("#errorMessageSpan")[0].innerHTML = EIDSS.BvMessages['errLoginMandatoryFields'];
        defaultPage.showError();
        return false;
    },

    getLanguage: function () {
        var lang = document.URL.replace(defaultPage.regExp, "", "$1");
        lang = lang.substring(lang.indexOf("/") + 1, lang.length);
        lang = lang.substring(0, lang.indexOf("/"));
        return lang;
    }
}