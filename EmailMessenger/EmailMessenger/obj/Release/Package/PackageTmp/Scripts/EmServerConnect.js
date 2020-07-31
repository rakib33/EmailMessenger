/// <reference path="EmUrl.js" />

var Url_Base = '';
var DataBaseList = null;
var TableList = null;
var SurverConnectionInfo = null;

var Url_SaveDatabase = Url_Base + '/Mail/SaveDatabase';
var Url_GetDatabaseList = Url_Base + '/Mail/GetDatabaseList';
var Url_SaveTableField = Url_Base + '/Mail/SaveTableField';

$(document).ready(function () {

    //Call API if any ServerList,then display Server List
    
    //

    $('#contactServer_form').bootstrapValidator({      
        // To use feedback icons, ensure that you use Bootstrap v3.1.0 or later
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            ConnectionName: {
                validators: {
                    notEmpty: {
                        message: 'Please supply a connection name'
                    }
                }
            },
            DataBaseUser: {
                validators: {
                    notEmpty: {
                        message: 'Please supply a database user name'
                    }
                }
            },
            UserPassword: {
                validators: {
                    notEmpty: {
                        message: 'Please supply database password'
                    }
                }
            },
            HostNameOrIP: {
                validators: {
                    notEmpty: {
                        message: 'Please supply server host name or IP'
                    }
                }
            },
            DbName: {
                validators: {
                    notEmpty: {
                        message: 'Please select your database name'
                    }
                }
            },
            TableName: {
                validators: {
                    notEmpty: {
                        message: 'Please select your table name'
                    }
                }
            },

            'option[]': {  //don't know how many field 
                validators: {
                    notEmpty: {
                        message: 'This option is required and cannot be empty'
                    },
                    stringLength: {
                        max: 100,
                        message: 'This option is must be less than 100 characters long'
                    }
                }
            },

        }
    })
         // Add checkbox click handler
    .on('click', '.ChkBoxdirectConnect', function (checked) { //if button then removed 

        console.log(this.checked); //true,false
        // alert(this.value); //on
        if (this.checked == true) {
            //Jquery code
            var $template = $('#dbBlock'),
                        $clone = $template
                       .clone()
                       .removeClass('hide')
                       .removeAttr('id')
                       .addClass('Added')
                       .insertBefore($template),
                 $option = $clone.find('[name="option[]"]');

            // Add new field or field group
            $('#contactServer_form').bootstrapValidator('addField', $option);

            //remove another filed from validation only and hide 
            $('#contactServer_form').bootstrapValidator('enableFieldValidators', 'DbName', false);
            $('#contactServer_form').bootstrapValidator('enableFieldValidators', 'TableName', false);
            var $default = $('#default').addClass('hide');
            var $serverConnect = $('#connect').addClass('hide');
        }
        else { //false or others

            var $row = $(this).find('.Added'), //get child element added
                $option = $row.find('[name="option[]"]');

            // Remove element containing the option
            $row.remove();

            // Remove field
            $('#surveyForm').bootstrapValidator('removeField', $option);
            $(".Added").remove();

            //open Default validation      
            $('#contactServer_form').bootstrapValidator('enableFieldValidators', 'DbName', true);
            $('#contactServer_form').bootstrapValidator('enableFieldValidators', 'TableName', true);
            var $default = $('#default').removeClass('hide');
            var $serverConnect = $('#connect').removeClass('hide');
        }

    })

            .on('success.form.bv', function (e) {
                console.log('hited');
              
                //$('#contact_form').data('bootstrapValidator').resetForm();
               
                e.preventDefault();

                // Get the form instance
                var $form = $(e.target);

                // Get the BootstrapValidator instance contactServer_form
                var bv = $form.data('bootstrapValidator');
                             

                var ConnectionName = $('#ConnectionName').val();
                var HostNameOrIP = $('#HostNameOrIP').val();
                var ServerUserName = $('#DataBaseUser').val();
                var UserPassword = $('#UserPassword').val();
                var DatabaseProvider = $('#DatabaseProvider').val();
                var DatabaseName = '';
                var TableName = '';
             
                var OptionArray = new Array();

                if ($("#ChkBoxdirectConnect").is(':checked')) {
                    $("input[name='option[]']").map(function () {
                        if (this.value !== null && this.value !== '')
                            OptionArray.push(this.value);
                    });

                    // var res = str.split(" ");
                    DatabaseName = OptionArray[0];
                    TableName = OptionArray[1];                
                }
                else {
         
                    DatabaseName = $('#DbName').val();
                    TableName = $('#TableName').val();
                }

                $.post("/Mail/SaveDatabase", {
                    connectionName: ConnectionName,
                    hostNameOrIp: HostNameOrIP,
                    serverUserName: ServerUserName,
                    databaseName: DatabaseName,
                    userPassword: UserPassword,
                    tableName: TableName,
                    databaseProvider : DatabaseProvider
                }).done( function (result) {
                  
                    if (result.message === 'ok') {
                        $('#success_message').slideDown({ opacity: "show" }, "slow") // Do something ...
                        SurverConnectionInfo = null;
                        SurverConnectionInfo = result;
                        fillTableFieldForm();
                        enableTableFieldForm();

                    } else {
                        alert(result.message);
                    }                  

                }).fail(function(xhr,textStatus,errorThrown){
                    console.log(xhr.responseText);
                    alert(xhr.responseText);
                    getErrorMessage(xhr, exception);
                }); //you can remove ,'json' part 
              
            });

    $('#ServerRefresh').click(function (e) {

        e.preventDefault();
        $('#contactServer_form').data('bootstrapValidator').resetForm();
    });



    $('#ConnectServer').click(function (e) {
        try {
            var ConnectionName = $('#ConnectionName').val();
            var HostNameOrIP = $('#HostNameOrIP').val();
            var DataBaseUser = $('#DataBaseUser').val();
            var UserPassword = $('#UserPassword').val();
            var DatabaseProvider = $('#DatabaseProvider').find(':selected').val();
          
            var OptionArray = new Array();
            $("input[name='option[]']").map(function () {
                OptionArray.push(this.value);
            });

            displyInlineBlock('load-image');
            $.ajax({
                url: Url_GetDatabaseList, //'/Mail/GetDatabaseList',
                type: 'GET',
                data: {
                 
                    'hostNameOrIP': HostNameOrIP,
                    'dataBaseUser': DataBaseUser,
                    'userPassword': UserPassword,
                    'databaseProvider' : DatabaseProvider
                },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                timeout: 120000, //2 min 3000,= 3 sec
                success: function (result) {
                    alert(result.message);
                    if (result.message === "ok") {

                        var data = DataBaseList = result.dbNameList;
                        //add option
                        var table = document.getElementById("DbName");

                        var opt = document.createElement('option');
                        opt.value = 0;
                        opt.innerHTML = 'select database';
                        table.appendChild(opt);
                        $.each(data, function (i, item) {
                            //var dbname = item.DatabaseName;
                            var opt = document.createElement('option');
                            opt.value = item.DatabaseName;
                            opt.innerHTML = item.DatabaseName;
                            table.appendChild(opt);
                        })


                        displayNone('load-image');

                    } else {

                        //check if result.message contain this sentence 'A network-related or instance-specific error occurred while establishing a connection to SQL Server'
                        //then display sql service is not enable .
                        alert(result.message);
                        DataBaseList = null;
                        TableList = null;
                        displayNone('load-image');
                    }


                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (textStatus == 'timeout') {
                        textStatus += "!! please use Direct Connection option."; alert(textStatus);
                    }
                    else {

                        console.log(jqXHR);
                        getErrorMessage(jqXHR, exception);
                    }
                    
                    displayNone('load-image');
                    DataBaseList = null;
                    TableList = null;

                  
                  
                }
            })
        } catch (err) {
            alert(err);
            displayNone('load-image');
            DataBaseList = null;
            TableList = null;
        }
    })
});


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


function displayNone(id) {
    document.getElementById(id).style.display = "none";
}
function displyInlineBlock(id) {
    document.getElementById(id).style.display = "inline-block";
}
function enableConnect() {

    var ConnectionName = $('#ConnectionName').val();
    var HostNameOrIP = $('#HostNameOrIP').val();
    var DataBaseUser = $('#DataBaseUser').val();
    var UserPassword = $('#UserPassword').val();

    if (ConnectionName !== "" && HostNameOrIP !== "" && DataBaseUser !== "" && UserPassword !== "") {
        document.getElementById("ConnectServer").disabled = false;
    } else {
        document.getElementById("ConnectServer").disabled = true;
    }
}

function getTableList() {
    var dbname = this.value;
    document.getElementById('TableName').options.length = 0;
    // document.getElementById('field_name').options.length = 0;
    TableList = null;

    var tableDropdown = document.getElementById('TableName');
    var dataBase = DataBaseList.find(function (e) { return e.DatabaseName == dbname });
    TableList = dataBase.TableList;

    $.each(dataBase.TableList, function (i, item) {
        var opt = document.createElement('option');
        opt.value = item.TableName;
        opt.innerHTML = item.TableName;
        tableDropdown.appendChild(opt);
    });
}

document.getElementById("DbName").onchange = getTableList;

//function EnableDbServerList() {
//    var $ServerConnectForm = $('#ServerConnectForm');
//    $ServerConnectForm.addClass('hide');

//    var $ServerDataList = $('#ServerDataListForm');
//    $ServerDataList.removeClass('hide');

//}


/************************************ Table Field select Form option***********************************************/

$('#btnEnableTableField').click(function () {
    enableTableFieldForm();
})
$('#btn_ServerConnectPage').click(function () {
    enableServerConnectForm();
})
function enableTableFieldForm() {
    $('#tableFieldSelect').removeClass('hide');
    $('#ServerConnectForm').addClass('hide');
}

function enableServerConnectForm() {
    $('#tableFieldSelect').addClass('hide');
    $('#ServerConnectForm').removeClass('hide');
}


function fillTableFieldForm() {
    //undo_redo
    document.getElementById('undo_redo').options.length = 0;
    // document.getElementById('field_name').options.length = 0;
    //TableList = null;

    var fieldSelectForm = document.getElementById('undo_redo');

    //TableList = dataBase.TableList;

    $.each(SurverConnectionInfo.TableFieldList, function (i, item) {
        var opt = document.createElement('option');
        opt.value = item.FieldName+','+item.FieldType;
        opt.innerHTML =(i+1)+' '+ item.FieldName + ' data type(' + item.FieldType+')';
        fieldSelectForm.appendChild(opt);
    });
}

$('#tableField_form').bootstrapValidator({
    // To use feedback icons, ensure that you use Bootstrap v3.1.0 or later
    feedbackIcons: {
        valid: 'glyphicon glyphicon-ok',
        invalid: 'glyphicon glyphicon-remove',
        validating: 'glyphicon glyphicon-refresh'
    },
    fields: {
        //'to[]': {  //don't know how many field 
        //    validators: {
        //        notEmpty: {
        //            message: 'This option is required and cannot be empty'
        //        }
        //    }
        //},
    }
})

 .on('success.form.bv', function (e) {

     e.preventDefault();

     // Get the form instance
     var $form = $(e.target);
     // Get the BootstrapValidator instance contactServer_form
     var bv = $form.data('bootstrapValidator');
     var ConnectionName = $('#ConnectionName').val();
     var OptionArray = new Array();

     $("select[name='to[]']").map(function () {
         if (this.value !== null && this.value !== '')
             OptionArray.push(this.value);
     });


     $.post(Url_SaveTableField, {
         fieldList: OptionArray
     }).done(function (result) {
         if (result.message === 'ok') {
             $('#success_message').slideDown({ opacity: "show" }, "slow") // Do something ...           
             //SurverConnectionInfo.TableFieldList = result;

         } else {
             alert(result.message);
         }
     }).fail(function (xhr, textStatus, errorThrown) {
         console.log(xhr.responseText);
         alert(xhr.responseText);
         getErrorMessage(xhr, exception);
     }); //you can remove ,'json' part 

 });
