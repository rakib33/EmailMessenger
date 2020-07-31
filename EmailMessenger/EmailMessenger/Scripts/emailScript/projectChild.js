$('#project_form').bootstrapValidator({
    // To use feedback icons, ensure that you use Bootstrap v3.1.0 or later
    feedbackIcons: {
        valid: 'glyphicon glyphicon-ok',
        invalid: 'glyphicon glyphicon-remove',
        validating: 'glyphicon glyphicon-refresh'
    },
    fields: {
        ProjectName: {
            validators: {
                notEmpty: {
                    message: 'Please supply a project name'
                }
            }
        },
        //ProjectStatus: {
        //    validators: {
        //        notEmpty: {
        //            message: 'Please supply status'
        //        }
        //    }
        //},
        StartTime: {
            validators: {
                notEmpty: {
                    message: 'Please supply start time'
                }
            }
        },
        ExpiredTime: {
            validators: {
                notEmpty: {
                    message: 'Please supply expire time'
                }
            }
        },
        TimeInterval: {
            validators: {
                notEmpty: {
                    message: 'Please select time interval'
                }
            }
        },
        IntervalOption: {
            validators: {
                notEmpty: {
                    message: 'Please select your database name'
                }
            }
        }        

    }
})
   
   .on('success.form.bv', function (e) {
       console.log('hited');
       e.preventDefault();

       // Get the form instance
       var $form = $(e.target);

       // Get the BootstrapValidator instance contactServer_form
       var bv = $form.data('bootstrapValidator');


       var ProjectName = $('#ProjectName').val();
       var ProjectStatus = $('#ProjectStatus').val();
       var StartTime = $('#StartTime').val();
       var ExpiredTime = $('#ExpiredTime').val();
       var TimeInterval = $('#TimeInterval').val();
       var IntervalOption = $('#IntervalOption').val();


       $.post("/Mail/SaveProject", {
           ProjectName: ProjectName,
           ProjectStatus: ProjectStatus,
           StartTime: StartTime,
           ExpiredTime: ExpiredTime,
           TimeInterval: TimeInterval,
           IntervalOption: IntervalOption      
       }).done(function (result) {

           if (result.message === 'ok') {
               // $('#ProjectSuccess_message').slideDown({ opacity: "show" }, "slow") // Do something ...
               $('#ProjectSuccess_message').addClass("alert alert-success");
               SurverConnectionInfo = null;
               SurverConnectionInfo = result;
               //fillTableFieldForm();
               //enableTableFieldForm();

           } else {
               alert(result.message);
           }

       }).fail(function (xhr, textStatus, errorThrown) {
           console.log(xhr.responseText);
           alert(xhr.responseText);
           //getErrorMessage(xhr);
       }); //you can remove ,'json' part 

   });

$('#ProjectRefresh').click(function (e) {

    e.preventDefault();
    $('#project_form').data('bootstrapValidator').resetForm();
    $('#ProjectSuccess_message').addClass("hide");
});

