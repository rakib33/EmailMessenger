/// <reference path="templateParent.js" />

    $('#template_form').bootstrapValidator({
        // To use feedback icons, ensure that you use Bootstrap v3.1.0 or later
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            TemplateName: {
                validators: {
                    notEmpty: {
                        message: 'Please supply a template name'
                    }
                }
            },
            uploadtmp: {
                validators: {
                    notEmpty: {
                        message: 'Please upload a file'
                    }
                }
            },
        }
    }) 
    .on('success.form.bv', function (e) {
        console.log('hited');
        displyInlineBlock();
        //$('#contact_form').data('bootstrapValidator').resetForm();

        e.preventDefault();

        // Get the form instance
        var $form = $(e.target);

        // Get the BootstrapValidator instance contactServer_form
        var bv = $form.data('bootstrapValidator');

        FileAndDataUploadByAjax();
       

    });

    function FileAndDataUploadByAjax() {

        var formdata = new FormData(); //FormData object
        var fileInput = document.getElementById('files');
        if (fileInput != "" && fileInput.files.length > 0) {
            //Iterating through each files selected in fileInput
            for (i = 0; i < fileInput.files.length; i++) {
                //check file type is html then save
                if (fileInput.files[i].type !== FileType_Html) {
                    alert("Invalid File Type");
                    return false;
                }
                //Appending each file to FormData object
                formdata.append(fileInput.files[i].name, fileInput.files[i]);
            }

            formdata.append("TemplateName", $('#TemplateName').val());
            formdata.append("TemplateDescription", $('#TemplateDescription').val());
            
            //Creating an XMLHttpRequest and sending
            var xhr = new XMLHttpRequest();

            var url = Url_SaveTemplate; // '@Url.Action("Index","Home")';
            xhr.open('POST', url);
            xhr.send(formdata);
            xhr.onreadystatechange = function () {
                var result;
                if (xhr.readyState == 4 && xhr.status == 200) { //2,200
                    result = xhr.responseText;

                } else if (xhr.readyState == 2 && xhr.status == 200) {
                    result = xhr.responseText;
                }
                if (result.message === 'ok') {          
                    var newRecord = new MailTemplate();
                    newRecord.Id = result.data.Id;
                    newRecord.TemplateName = result.data.TemplateName;
                    newRecord.TemplateFilePath = result.data.TemplateFilePath;
                    newRecord.Description = result.data.Description;
                    TemplateDataList.push(result.data);
                    ShowTemplateInView();
                }
            }
            displayNone();
            return false;
        }
    }

    //The readyState property holds the status of the XMLHttpRequest.

    //The onreadystatechange property defines a function to be executed when the readyState changes.

    //The status property and the statusText property holds the status of the XMLHttpRequest object.

    //Property	Description
    //onreadystatechange	Defines a function to be called when the readyState property changes
    //readyState	Holds the status of the XMLHttpRequest. 
    //0: request not initialized 
    //1: server connection established
    //2: request received 
    //3: processing request 
    //4: request finished and response is ready
    //status	200: "OK"
    //403: "Forbidden"
    //404: "Page not found"
    //For a complete list go to the Http Messages Reference
    //statusText	Returns the status-text (e.g. "OK" or "Not Found")

// This function is used to get error message for all ajax calls
function getErrorMessage(jqXHR, exception) {
    var msg = '';
    if (jqXHR.status === 0) {
        msg = 'Not connect.\n Verify Network.';
    } else if (jqXHR.status == 404) {
        msg = 'Requested page not found. [404]';
    } else if (jqXHR.status == 500) {
        msg = 'Internal Server Error [500].';
    } else if (exception === 'parsererror') {
        msg = 'Requested JSON parse failed.';
    } else if (exception === 'timeout') {
        msg = 'Time out error.';
    } else if (exception === 'abort') {
        msg = 'Ajax request aborted.';
    } else {
        msg = 'Uncaught Error.\n' + jqXHR.responseText;
    }
    alert(msg);
    //$('#post').html(msg);
}

/*********************** Event ******************************/

$('#templateRefresh').click(function () {
    $('#template_form').data('bootstrapValidator').resetForm();
})