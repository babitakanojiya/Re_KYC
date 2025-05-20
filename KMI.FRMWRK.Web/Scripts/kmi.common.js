//added by kalpak for handling page querystring
// use "queryString" function for reading value in page querysting
//eg. queryString("key")
function PageQuery(q) {
    strFunctionName = "PageQuery";
    if (q.length > 1)
        this.q = q.substring(1, q.length);
    else
        this.q = null;
    this.keyValuePairs = new Array();
    if (q) {
        for (var i = 0; i < this.q.split("&").length; i++) {
            this.keyValuePairs[i] = this.q.split("&")[i];
        }
    }

    this.getKeyValuePairs = function () { return this.keyValuePairs; }

    this.getValue = function (s) {
        for (var j = 0; j < this.keyValuePairs.length; j++) {
            if (this.keyValuePairs[j].split("=")[0] == s)
                return this.keyValuePairs[j].split("=")[1];
        }
        return false;
    }
    this.getParameters = function () {
        var a = new Array(this.getLength());
        for (var j = 0; j < this.keyValuePairs.length; j++) {
            a[j] = this.keyValuePairs[j].split("=")[0];
        }
        return a;
    }
    this.getLength = function () { return this.keyValuePairs.length; }
}
function queryString(key) {
    strFunctionName = "queryString";
    var page = new PageQuery(window.location.search);
    return unescape(page.getValue(key));
}

function allnumeric(FieldName) {
	var field = document.getElementById(FieldName)
	var numbers = /^[0-9]+$/;
	if (field.value.match(numbers)) {
		return true;
	}
	else {
	    AlertMsg('Please enter numeric values only.');
		field.value = "";
		return false;
	}
}

function fncInputNumericValuesOnly() {
	if (!(event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
		event.returnValue = false;
	}
}

function fncInputcharacterOnly() {
	if (!(event.keyCode == 32 || event.keyCode == 65 || event.keyCode == 66 || event.keyCode == 67 || event.keyCode == 68 || event.keyCode == 69 || event.keyCode == 70 || event.keyCode == 71 || event.keyCode == 72 || event.keyCode == 73 || event.keyCode == 74 || event.keyCode == 75 || event.keyCode == 76 || event.keyCode == 77 || event.keyCode == 78 || event.keyCode == 79 || event.keyCode == 80 || event.keyCode == 81 || event.keyCode == 82 || event.keyCode == 83 || event.keyCode == 84 || event.keyCode == 85 || event.keyCode == 86 || event.keyCode == 87 || event.keyCode == 88 || event.keyCode == 89 || event.keyCode == 90 || event.keyCode == 97 || event.keyCode == 98 || event.keyCode == 99 || event.keyCode == 100 || event.keyCode == 101 || event.keyCode == 102 || event.keyCode == 103 || event.keyCode == 104 || event.keyCode == 105 || event.keyCode == 106 || event.keyCode == 107 || event.keyCode == 108 || event.keyCode == 109 || event.keyCode == 110 || event.keyCode == 111 || event.keyCode == 112 || event.keyCode == 113 || event.keyCode == 114 || event.keyCode == 115 || event.keyCode == 116 || event.keyCode == 117 || event.keyCode == 118 || event.keyCode == 119 || event.keyCode == 120 || event.keyCode == 121 || event.keyCode == 122)) {
		event.returnValue = true;
		e.preventDefault();
	}
}

function funInputNumericCharOnly() {
	if (!(event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57 || event.keyCode == 47)) {
		event.returnValue = false;
	}
}

function funIsAlphaNumeric() {
	if (((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 65 && event.keyCode <= 90) || (event.keyCode >= 97 && event.keyCode <= 122)) == false) {
		event.returnValue = false;
	}
}

function funIsAlphaNumericWithSpace() {
    // if  (((event.keyCode == 32) || (event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 65 && event.keyCode <= 90) || (event.keyCode >= 97 && event.keyCode <= 122)|| (event.keyCode!=39)) == false )  {
        // event.returnValue = false;
    // }
	var charCode = event.keyCode;
            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32 || charCode==39)
                return true;
            else
                return false;
}

function funIsAlphaNumericWithoutSpace() {
    if (((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 65 && event.keyCode <= 90) || (event.keyCode >= 97 && event.keyCode <= 122)) == false) {
        event.returnValue = false;
    }
}

function funIsAmount() {
	if (((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode == 46)) == false) {
		event.returnValue = false;
	}
}


function funEmailFormat(EmailId) {
    var emailPattern = /^(?:[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+\.)*[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!\.)){0,61}[a-zA-Z0-9]?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\[(?:(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\.){3}(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\]))$/;
    return emailPattern.test(EmailId);
}

function funEmailValidation(txtID) {
    var val = document.getElementById(txtID).value;
    var newtext = val.split(",");
    var newtext1 = val.split(" ");
    for (var i = 0 ; i < newtext.length; i++) {
        var validEmail = funEmailFormat(newtext[i])
        if (!validEmail) {
            AlertMsg("Please enter valid email address.");
            return false;
        }
    }
}

function setDateFormat(txtdate) {
    debugger;
	try {
		if (isValidDate(txtdate) == true) {
			var date = document.getElementById(txtdate).value;
			var dtArr = new Array();
			//dtArr = date.split("/");
			dtArr = date.split(/[\s,./]+/);
			var month = dtArr[1];
			var val = "";

			if (month == '1' || month == '01') {
				val = 'Jan';
			} else
				if (month == '2' || month == '02') {
					val = 'Feb';

				} else
					if (month == '3' || month == '03') {
						val = 'Mar';

					} else
						if (month == '4' || month == '04') {
							val = 'Apr';

						} else
							if (month == '5' || month == '05') {
								val = 'May';

							} else
								if (month == '6' || month == '06') {
									val = 'Jun';
								} else
									if (month == '7' || month == '07') {
										val = 'Jul';
									} else
										if (month == '8' || month == '08') {
											val = 'Aug';
										} else
											if (month == '9' || month == '09') {
												val = 'Sep';
											} else
												if (month == '10') {
													val = 'Oct';
												} else
													if (month == '11') {
														val = 'Nov';
													} else
														if (month == '12') {
															val = 'Dec';
														}

			if (val != "") {
				document.getElementById(txtdate).value = dtArr[0] + " " + val + " " + dtArr[2]
			}

			return val;
		}
	}
	catch (err) {
		ShowError(err.description);
	}
}

//added by kalpak - show modal popup in division
// set jquery datepicker with image
function setupDatePicker() {
	$(function () {
		$(".picker-control").datepicker({
			showOn: "button",
			buttonImage: "../Content/Images/calenderIcon.png",
			buttonImageOnly: true,
			buttonText: "Select date"
		});

		debugger;
		var img = $(".datepicker img");
		$(".datepicker > .input-group-btn").append(img);
	});
}

//added by kalpak - show modal popup in division
// division must have attributes class="modal fade" &  role="dialog"
//eg. <div class="modal fade" id="myModal" role="dialog">
function showModal(element, popupHeader, headerCSS, popupFooter, footerCSS, bodyHtml) {
    var html = '<div class="modal-dialog">';
    html += '<div class="modal-content">';
    if (popupHeader != '') {
        html += '<div class="modal-header ' + headerCSS + '">';
        html += '<button type="button" class="close" data-dismiss="modal">&times;</button>';
        html += '<center><h4 class="modal-title">' + popupHeader + '</h4></center>';
        html += '</div>';
    }
    html += '<div class="modal-body">';
    html += bodyHtml;
    html += '</div>';
    if (popupFooter != '') {
        html += '<div class="modal-footer ' + footerCSS + '">' + popupFooter + '</div>';
    }
    html += '</div>';
    $(element).html(html);
    $("#myModal").modal();
}

function showModalAlert(element, popupHeader, headerCSS, popupFooter, footerCSS, bodyHtml,id) {
    var html = '<div class="modal-dialog">';
    html += '<div class="modal-content">';
    if (popupHeader != '') {
        html += '<div class="modal-header ' + headerCSS + '">';
        html += '<button type="button" class="close" data-dismiss="modal">&times;</button>';
        html += '<center><h4 class="modal-title">' + popupHeader + '</h4></center>';
        html += '</div>';
    }
    html += '<div class="modal-body">';
    html += bodyHtml;
    html += '</div>';
    if (popupFooter != '') {
        html += '<div class="modal-footer ' + footerCSS + '">' + popupFooter + '</div>';
    }
    html += '</div>';
    $(element).html(html);
    $("#" + id).modal();
}

//added by kalpak - show page in modal popup division
// division must have attributes class="modal fade" &  role="dialog"
//eg. <div class="modal fade" id="myModal" role="dialog">
function showPageInModal(element, popupHeader, popupFooter, pageURL, cssClass) {
    var html = '<div class="modal-dialog">';
    html += '<div class="modal-content">';
    if (popupHeader != '') {
        html += '<div class="modal-header">';
        html += '<button type="button" class="close" data-dismiss="modal">&times;</button>';
        html += '<h4 class="modal-title">' + popupHeader + '</h4>';
        html += '</div>';
    }
    html += '<div class="modal-body">';
    html += '<iframe id="frmPopupBody" src="' + pageURL + '" class="' + cssClass + '">';
    html += '</div>';
    if (popupFooter != '') {
        html += '<div class="modal-footer">' + popupFooter + '</div>';
    }
    html += '</div>';
    $(element).html(html);
    $(element).modal();
}

//ADDED BY KALYANI TO HIDE AND SHOW BODY DIV
function showHideDiv(divName, btnName) {
    try {
        var objnewdiv = document.getElementById(divName)
        var objnewbtn = document.getElementById(btnName);
        if (objnewdiv.style.display == "block") {
            objnewdiv.style.display = "none";
            objnewbtn.className = 'glyphicon glyphicon-collapse-up'
        }
        else {
            objnewdiv.style.display = "block";
            objnewbtn.className = 'glyphicon glyphicon-collapse-down'
        }
    }
    catch (err) {
        ShowError(err.description);
    }
}

function loadPageInIframe(element, url) {
    $(element).attr('src', url);
}