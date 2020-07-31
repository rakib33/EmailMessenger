//Two why DataBinding in Js...
//https://jsfiddle.net/v5owbwf0/4/
//https://stackoverflow.com/questions/16483560/how-to-implement-dom-data-binding-in-javascript
var UrlBase = '';
var Url_GetGroupList = UrlBase + '/Mail/GetGroupList';
var Url_GetServerList = UrlBase + '/Mail/GetDbServerList';

var Url_GetTemplateList = UrlBase + '/Mail/GetTemplateData';
var Url_SaveTemplate = UrlBase + '/Mail/SaveEmailTemplate';

var DSList = [];
var GroupList = [];
var TemplateDataList = [];
var TemplateTextDataList = [];
var ProjectList = [];

/********** File Type **************/

var FileType_Html = "text/html";
var FileType_Pdf = "Application/pdf";
/************************/

var displayNone = function (id) {
    document.getElementById(id).style.display = "none";
}
var displyInlineBlock = function (id) {
    document.getElementById(id).style.display = "inline-block";
}


/************* Event Handler *****************/


$('.close').click(function () {
    ReleaseAll();
})

$('.release').click(function () {
    ReleaseAll();
})

function ReleaseAll() {
    $('#template_form').data('bootstrapValidator').resetForm();
}