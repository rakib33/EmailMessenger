/// <reference path="root_a_utility.js" />
var AddMailTemplate=null;

var MailTemplate = function () { 
    this.Id;
    this.TemplateName;
    this.TemplateFilePath;
    this.Description;
    this.TemplateTextList = [];
}

var TemplateText = function () {
    this.Id;
    this.Order;
    this.TemplateDataOrder;
    this.TemplateData;
    this.MailTemplateId;
}

//here MailTemplate.Id equals TemplateText.MailTemplateId

function SetTemplateData(data) {

    if (data !== null) {

        TemplateDataList = [];
        $.each(data, function (i, item) {
            var newTemplate = new MailTemplate();
            newTemplate.SlNo = i + 1; //not need 
            newTemplate.Id = item.Id;
            newTemplate.TemplateName = item.TemplateName;
            newTemplate.TemplateFilePath = item.TemplateFilePath;          
            TemplateDataList.push(newTemplate);
            
        });
        return true;
    }
    return false;
}
function CallTemplateData() {
    $.ajax({
        url: Url_GetTemplateList, //'/Mail/GetDatabaseList',
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 120000, //2 min 3000,= 3 sec
        success: function (result) {
            // alert(result.message);
            if (result.message === "ok") {
                if (SetTemplateData(result.data)) {
                    ShowTemplateInView();
                } else {
                  
                }

                displayNone('loading');

            } else {

                //check if result.message contain this sentence 'A network-related or instance-specific error occurred while establishing a connection to SQL Server'
                //then display sql service is not enable .
                alert(result.message);
                displayNone('loading');
            }


        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(textStatus);
            displayNone('loading');

        }
    })
}

function ShowTemplateInView() {
    $("#eMailTemplate").tmpl(TemplateDataList).appendTo("#emailTemplatetable tbody");
}

function GetTemplateData() {
    if (TemplateDataList !== null && TemplateDataList.length > 0) {
        // return GroupList;
        displayNone('loading');
    } else {
        CallTemplateData();
    }
}

function Dispose() {
    $('#template_form').data('bootstrapValidator').resetForm();
    AddMailTemplate = null;
}
/**************Event Handler****************/

$('#template').click(function () {
    //alert('hited');
    displyInlineBlock('loading');
    // document.getElementById("loading").style.display = "block";
    GetTemplateData();
})

$('.release').click(function () {
    Dispose();
})

$('.close').click(function () {
    Dispose();
})

//initilize new object
$('#addTemplate').click(function () {
    AddMailTemplate = new MailTemplate();
})

