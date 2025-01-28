userSession = {
    showDisconnectOthers: function (loginForm, forced) {
        var hasParallelSessions = $("#ShowParallelSessionsMessage").val();
        if (hasParallelSessions || forced) {
            return bvDialog.showYesNo(bvDialog.title.loginConfirmation, bvDialog.messageText.hasParallelSessions, yesCallback, noCallback)
        }

        function yesCallback() {
            $(loginForm).append('<input type="hidden" name="RemoveParallelSessions" value="true" /> ');
            $(loginForm).append('<input type="hidden" name="KeepParallelSessions" value="false" /> ');
            defaultPage.formValidated = true;
            if (defaultPage.loginModel)
            {
                defaultPage.loginModel.RemoveParallelSessions = true;
                defaultPage.loginModel.KeepParallelSessions = false;
            }
            loginForm.submit();
        }

        function noCallback() {
            $(loginForm).append('<input type="hidden" name="RemoveParallelSessions" value="false" /> ');
            $(loginForm).append('<input type="hidden" name="KeepParallelSessions" value="true" /> ');
            defaultPage.formValidated = true;
            if (defaultPage.loginModel) {
                defaultPage.loginModel.RemoveParallelSessions = true;
                defaultPage.loginModel.KeepParallelSessions = false;
            }
            loginForm.submit();
        }
    },

    checkIsUserSessionActive: function () {
        var url = bvUrls.getIsDisconnectedUrl();
        var timeout = 10000;
        var sessionId = document.cookie.replace(/(?:(?:^|.*;\s*)ClientID\s*\=\s*([^;]*).*$)|^.*$/, "$1");
        var username = $('#idButtonLogin').parents('form:first')[0];
        if (!username) {
            username = $('#idButtonOk').parents('form:first')[0];
        }
        setInterval(function () {
            $.ajax({
                url: url,
                dataType: "json",
                type: "Get",
                success: function (result) {
                    if (result.IsDisconnected) {
                        actions.redirect(bvUrls.getDisconnectedUrl());
                    }
                },
                error: function () {
                }
            });
        }, timeout);
    }
}