var bvCheckedGroupComboBox = (function () {

    function getCheckedItemsWithoutChildren(controlId) {
        var texts = [];
        var values = [];
        var itemsListId = controlId + "_listbox";
        var items = $("#" + itemsListId + " ." + comboBoxFacade.itemCheckBoxClassName + ":checked");
        for (var i = 0; i < items.length; i++) {
            if (items[i].id) {
                var isToAdd = false;
                var strId = items[i].id;
                if (strId == "chb0") {
                    continue;
                }
                var groupItems = $("#" + itemsListId).find("." + comboBoxFacade.itemCheckBoxClassName + "[group='" + strId.substr(3) + "']");
                if (groupItems.length > 0) {
                    isToAdd = true;
                }
                else {
                    var group = $("#" + itemsListId).find("." + comboBoxFacade.itemCheckBoxClassName + "[id='" + strId + "']")[0].getAttribute("group");
                    if (group) {
                        var groupItem = $("#" + itemsListId).find("." + comboBoxFacade.itemCheckBoxClassName + "[id='chb" + group + "']");
                        if (groupItem.length == 1) {
                            if (!groupItem[0].checked) {

                                isToAdd = true;
                            }
                        }
                        else {
                            isToAdd = true;
                        }
                    }
                    else {
                        isToAdd = true;
                    }
                }
                if (isToAdd) {
                    var strText = $("#" + itemsListId).find("." + comboBoxFacade.itemCheckBoxClassName + "[id='" + strId + "']")[0].getAttribute("checkedname");
                    strId = strId.substr(3);
                    texts.push(strText);
                    values.push(strId);
                }
            }
        }
        return { texts: texts, values: values };
    }

    function hideEmptyItemInCheckedGroupComboBox() {
        $("." + comboBoxFacade.itemCheckBoxClassName + "[checkedname='']").hide();
    }

    function clearCheckedGroupComboBox(controlId) {
        var checked = comboBoxFacade.getCheckedCheckBoxes(controlId);
        checked.removeAttr("checked");
        comboBoxFacade.setTextById(controlId, "", true);
        $("#" + controlId).val("");
        //formRefresher.onFieldChanged(controlId, "");
    }

    return {
        onClearCheckedGroupComboBox: function (controlId) {
            clearCheckedGroupComboBox(controlId);
        },
        onComboBoxOpen: function (e, width, isDropDown) {
            if (width > 0) {
                var element = e.sender.element[0];
                var id = element.id;
                var combobox = comboBoxFacade.getControlData(id, isDropDown);
                if (combobox.list.width() < width)
                    combobox.list.width(width);
            }
        },
        onCheckedGroupComboBoxChanged: function (e) {
            var args = comboBoxFacade.getOnChangedEventArgs(e, this);
            var controlId = args.senderId;
            if (args.selectedIndex != 0/* || args.selectedValue == -1*/) {
                e.preventDefault();
            } else {
                //clearCheckedGroupComboBox(controlId); //#13179
                e.preventDefault();
            }
        },
        onCheckedGroupComboBoxClick: function (e, controlId, currentCheckBoxId) {
            comboBoxFacade.cancelCloseOnClick(e);
            var combobox = $("#" + controlId);
            var maxcheckedcount = 0;
            if ((combobox != null)
                    && (combobox.length == 1)
            ) {
                var maxCheckCountAttr = combobox[0].getAttribute("maxcheckedcount");
                if (maxCheckCountAttr != null && maxCheckCountAttr !== '')
                    maxcheckedcount = parseInt(maxCheckCountAttr, 0);
            }

            var chk = $("#" + controlId + "_listbox").find("." + comboBoxFacade.itemCheckBoxClassName + "[id='" + currentCheckBoxId + "']")[0].checked;
            if (currentCheckBoxId == "chb0") { // select all
                var items = $("#" + controlId + "_listbox").find("." + comboBoxFacade.itemCheckBoxClassName + "[id!='" + currentCheckBoxId + "']");
                var counter = 0;
                var lastCheckItem = 0;

                for (var i = 0; i < items.length; i++) {
                    if ((chk == true) && maxcheckedcount && (maxcheckedcount < items.length)) {
                        if (counter > maxcheckedcount) {
                            break;
                        }
                        var childItems = $("#" + controlId + "_listbox").find("." + comboBoxFacade.itemCheckBoxClassName + "[group='" + items[i].id().substr(3) + "']");
                        if (childItems.length > 0) {
                            counter++;
                        }
                        else {
                            var parentGroup = $("#" + controlId + "_listbox").find("." + comboBoxFacade.itemCheckBoxClassName + "[id='" + items[i].id() + "']")[0].getAttribute("group");
                            if (!parentGroup) {
                                counter++;
                            }
                            else {
                                var parentGroupItem = $("#" + controlId + "_listbox").find("." + comboBoxFacade.itemCheckBoxClassName + "[id='chb" + parentGroup + "']");
                                if (parentGroupItem.length != 1) {
                                    counter++;
                                }
                            }
                        }
                    }
                    items[i].checked = chk;
                    lastCheckItem = i;
                }
                for (var i = lastCheckItem+1; i < items.length; i++) {
                    items[i].checked = false;
                }
                if ((chk==true)&&(lastCheckItem < items.length-1)) {
                    $("#" + controlId + "_listbox").find("." + comboBoxFacade.itemCheckBoxClassName + "[id='" + currentCheckBoxId + "']")[0].checked = false;
                }

            } else {
                var groupItems = $("#" + controlId + "_listbox").find("." + comboBoxFacade.itemCheckBoxClassName + "[group='" + currentCheckBoxId.substr(3) + "']");
                if (groupItems.length > 0) {
                    for (var i = 0; i < groupItems.length; i++) {
                        groupItems[i].checked = chk;
                    }
                } else {
                    var group = $("#" + controlId + "_listbox").find("." + comboBoxFacade.itemCheckBoxClassName + "[id='" + currentCheckBoxId + "']")[0].getAttribute("group");
                    if (group && !chk) {
                        var groupItem = $("#" + controlId + "_listbox").find("." + comboBoxFacade.itemCheckBoxClassName + "[id='chb" + group + "']");
                        if (groupItem.length == 1) {
                            groupItem[0].checked = false;
                        }
                    }
                    if (group && chk) {
                        var childGroupItems = $("#" + controlId + "_listbox").find("." + comboBoxFacade.itemCheckBoxClassName + "[group='" + group + "']");
                        var childGroupItemsChecked = true;
                        if (childGroupItems.length > 0) {
                            for (var i = 0; i < childGroupItems.length; i++) {
                                if (!childGroupItems[i].checked) {
                                    childGroupItemsChecked = false;
                                    break;
                                }
                            }
                            if (childGroupItemsChecked) {
                                var groupItem = $("#" + controlId + "_listbox").find("." + comboBoxFacade.itemCheckBoxClassName + "[id='chb" + group + "']");
                                if (groupItem.length == 1) {
                                    groupItem[0].checked = true;
                                }
                            }
                        }
                    }
                }

                var selAll = $("#" + controlId + "_listbox").find("." + comboBoxFacade.itemCheckBoxClassName + "[id='chb0']");
                if (selAll && selAll.length == 1 && !chk) {
                    selAll[0].checked = false;
                }
                if (selAll && selAll.length == 1 && chk) {
                    var allItems = $("#" + controlId + "_listbox").find("." + comboBoxFacade.itemCheckBoxClassName + "[id!='chb0']");
                    var allItemsChecked = true;
                    for (var i = 0; i < allItems.length; i++) {
                        if (!allItems[i].checked) {
                            allItemsChecked = false;
                            break;
                        }
                    }
                    if (allItemsChecked) {
                        selAll[0].checked = true;
                    }
                }
            }


            var checkedItems = getCheckedItemsWithoutChildren(controlId);

            if ((maxcheckedcount > 0) && (checkedItems.values.length > maxcheckedcount)) {
                var mess = EIDSS.BvMessages.msgTooCheckedItems;
                var messageId = combobox[0].getAttribute("validationMessage");
                if (messageId != undefined && messageId != '') {
                    mess = eval('EIDSS.BvMessages.' + messageId);
                }
                if (mess != null) {
                    mess = mess.replace('{0}', maxcheckedcount);
                    //clearCheckedGroupComboBox(controlId);
                    if (currentCheckBoxId != null && currentCheckBoxId != "chb0") {
                        var items = $("#" + currentCheckBoxId);
                        if (items.length > 0) {
                            items[0].checked = false;
                        }
                    }
                    bvDialog.showError(mess);
                }
            }
            checkedItems = getCheckedItemsWithoutChildren(controlId);
            var text = checkedItems.texts.toString();
            comboBoxFacade.setTextById(controlId, text, true);
            $("#" + controlId).val(checkedItems.values.toString());
            formRefresher.onFieldChanged(controlId, checkedItems.values.toString());
        },

        bindCheckedGroupComboBoxClickEvent: function (controlId, selectedIndexes) {
            bvCheckedGroupComboBox.bindCheckedGroupComboBoxClickEventInternal(controlId, selectedIndexes, bvCheckedGroupComboBox.onCheckedGroupComboBoxClick);
        },

        bindCheckedGroupComboBoxClickEventInternal: function (controlId, selectedIndexes, onCheckClickFunction) {
            hideEmptyItemInCheckedGroupComboBox();
            clearCheckedGroupComboBox(controlId);
            $("." + comboBoxFacade.itemCheckBoxClassName).bind("click", { controlId: controlId }, function (event) {
                var e = arguments[0];
                var currentCheckBoxId = null;
                if (e.currentTarget != null) currentCheckBoxId = e.currentTarget.id;
                onCheckClickFunction(e, event.data.controlId, currentCheckBoxId);
            });

            // prevent keyboard actions
            //$("span.k-dropdown")
            $("#" + controlId).parent()
                .off("keydown")
                .on("keydown", function (e) {
                    var keyCode = e.keyCode;
                    //if (keyCode !== kendo.keys.LEFT && keyCode !== kendo.keys.RIGHT) {
                    //    $(this).find('select').data('kendoDropDownList')._keydown(e);
                    //}
                });

            var data = comboBoxFacade.getControlData(controlId, true).dataSource.data();
            var isCheckedDataSource = false;
            if (data.length > 0 && data[0].IsChecked !== undefined) {
                isCheckedDataSource = true;
            }

            if (isCheckedDataSource) {
                selectedIndexes = "";
                for (var i = 0; i < data.length; i++) {
                    if (data[i].IsChecked) {
                        if (selectedIndexes.length > 0) {
                            selectedIndexes = selectedIndexes + ",";
                        }
                        selectedIndexes = selectedIndexes + data[i].Value;
                    }
                }
            }
            bvCheckedGroupComboBox.setCheckedCheckBox(controlId, selectedIndexes);
        },

        setCheckedCheckBox: function (id, indexes, checked) {
            var arr = indexes.split(',');
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].length > 0) {
                    var items = $("#chb" + arr[i]);
                    if (items.length > 0) {
                        var item = items[0];
                        if (checked != null)
                            item.checked = checked;
                        else
                            item.checked = true;
                    }
                }
            }

            var checkedItems = getCheckedItemsWithoutChildren(id);
            var text = checkedItems.texts.toString();
            comboBoxFacade.setTextById(id, text, true);
            $("#" + id).val(checkedItems.values.toString());
        },

    };
})();