/// <reference path="root_a_utility.js" />


var Group = function () {
    this.GroupName;
    this.Id;
    this.ServerConnectId;
    this.ServerConnectionName;
    this.Description;
    this.Size;
    this.ConditionQuery;
    this.SlNo;
}

//Jquery equvalent
//(function () {
//    alert('loaded');
//})

function SetGroupData(data) {

    if (data !== null) {
        GroupList = [];
        $.each(data, function (i, item) {
            var newGroup = new Group();
            newGroup.SlNo = i + 1; //not need 
            newGroup.Id = item.Id;
            newGroup.GroupName = item.GroupName;
            newGroup.ServerConnectionName = item.ServerConnectionName;
            newGroup.Description = item.Description;
            newGroup.ConditionQuery = item.ConditionQuery;
            GroupList.push(newGroup);
            //contentData += "<tr><td>" + user.Image + "</td><td>" + user.Title + "</td></tr>";
        });
        return true;
    }
    return false;
}
function CallGroupData() {
    $.ajax({
        url: Url_GetGroupList, //'/Mail/GetDatabaseList',
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 120000, //2 min 3000,= 3 sec
        success: function (result) {
            // alert(result.message);
            if (result.message === "ok") {
                if (SetGroupData(result.data)) {
                    ShowGroupInView();
                } else {
                    ShowMessageInView('', '');
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

function ShowGroupInView() {
    $("#itemTemplate").tmpl(GroupList).appendTo("#grouptable tbody");
}

function ShowMessageInView(conatinerId, message) {

}
function GetGroupData() {
    if (GroupList !== null && GroupList.length > 0) {
        // return GroupList;
        displayNone('loading');
    } else {
        CallGroupData();
    }
}

$('#group').click(function () {
    //alert('hited');
    displyInlineBlock('loading');
    // document.getElementById("loading").style.display = "block";
    GetGroupData();
})

