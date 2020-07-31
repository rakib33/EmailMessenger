/// <reference path="root_a_utility.js" />

var Server = function () {
    this.ConnectionName;
    this.ServerHostNameIP;
    this.ServerUserName;
    this.ServerPassword;
    this.DatabaseName;
    this.TableName;
    this.Id;
}


function SetServerData(data) {

    if (data !== null) {
        DSList = [];
        $.each(data, function (i, item) {
            var newServer = new Server();
            newServer.SlNo = i + 1; //not need 
            newServer.Id = item.Id;
            newServer.ConnectionName = item.ConnectionName;
            newServer.ServerHostNameIP = item.ServerHostNameIP;
            newServer.ServerUserName = item.ServerUserName;
            newServer.DatabaseName = item.DatabaseName;
            newServer.TableName = item.TableName;
            DSList.push(newServer);
        });
        return true;
    }
    return false;
}
function CallServerData() {
    $.ajax({
        url: Url_GetServerList, //'/Mail/GetDatabaseList',
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 120000, //2 min 3000,= 3 sec
        success: function (result) {
            // alert(result.message);
            if (result.message === "ok") {
                if (SetServerData(result.data)) {
                    ShowServeyDataInView();
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
            if (textStatus == 'timeout') {
                alert(textStatus);
            }
            else {
                //console.log(jqXHR);
                alert(jqXHR);
            }
            displayNone('loading');
        }
    })
}

function ShowServeyDataInView() {
    $("#serverTemplate").tmpl(DSList).appendTo("#servertable tbody");
}


function GetServerData() {
    displyInlineBlock('loading');
    if (DSList !== null && DSList.length > 0) {
        // return GroupList;
        displayNone('loading');
    } else {
        CallServerData();
    }
}


$('#server').click(function () {
    //alert('hited click'); 
    GetServerData();
})

$(document).ready(function () {
    //alert('hited load');
    GetServerData();
})