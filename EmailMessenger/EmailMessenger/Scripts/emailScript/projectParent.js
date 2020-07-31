
var Project = function () {

    //this.Groups = [];
    this.Id;
    this.ProjectName;
    this.Status;
    this.StartTime; //date time
    this.TimeInterval;//1,2,3...
    this.IntervalOption; //Day(s),Month,Year
    this.ExpiredTime;
    this.ProjectDialoguePath;
}


function SetProjectData(data) {

    if (data !== null) {

        ProjectList = [];
        $.each(data, function (i, item) {
            var newTemplate = new Project();
            newTemplate.SlNo = i + 1; //not need 
            newTemplate.Id = item.Id;
            newTemplate.ProjectName = item.ProjectName;
            newTemplate.Status = item.Status;
            newTemplate.StartTime = item.StartTime;
            newTemplate.TimeInterval = item.TimeInterval;
            newTemplate.IntervalOption = item.IntervalOption;
            newTemplate.ExpiredTime = item.ExpiredTime;

            ProjectList.push(newTemplate);

        });
        return true;
    }
    return false;
}
function CallProjectData() {
    $.ajax({
        url: '/Mail/GetProjectList',
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 120000, //2 min 3000,= 3 sec
        success: function (result) {
            // alert(result.message);
            if (result.message === "ok") {
                if (SetProjectData(result.data)) {
                    ShowProjectInView();
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

function ShowProjectInView() {
    $("#projectTemplate").tmpl(ProjectList).appendTo("#projecttable tbody");
}

function GetProjectData() {
    if (ProjectList !== null && ProjectList.length > 0) {
        // return GroupList;
        displayNone('loading');
    } else {
        CallProjectData();
    }
}

function Dispose() {
    $('#template_form').data('bootstrapValidator').resetForm();
    AddMailTemplate = null;
}

$('#project').click(function () {
    alert('hited');
    GetProjectData();
})