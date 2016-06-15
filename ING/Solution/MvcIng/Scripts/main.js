$(function () {


    $("#requestLeave-btn").click(function () {
            $('#dialog1').dialog('open');
    });

    $("#dialog1").dialog({
        autoOpen: false,
        show: { effect: "fadeIn", duration: 800 },
        modal: true,
        resizable: false,

        open: function () {
            $('.ui-widget-overlay').addClass('custom-overlay');
        },
        close: function () {
            $('.ui-widget-overlay').removeClass('custom-overlay');
        }
    });

    $('.cancel-button').click(function () {
        $("#dialog").dialog("close");
    });

    //add new member dialog
    $("#add-new-member-dialog").dialog({
            autoOpen: false,
            show: { effect: "fadeIn", duration: 800 },
            modal: true,
            resizable: false,
           
            open: function () {
                $('.ui-widget-overlay').addClass('custom-overlay');
            },
            close: function () {
                $('.ui-widget-overlay').removeClass('custom-overlay');
            }
        });

        $('.add-new-member').click(function () {
            $('#add-new-member-dialog').dialog('open');
            $(".AddMemberContainer").mCustomScrollbar({
            theme:"my-theme",  
            });
        });

    $('.cancel-button').click(function() {
        $("#add-new-member-dialog").dialog("close");
    });


    //add holiday dialog
     $("#add-holiday-dialog").dialog({
            autoOpen: false,
            show: { effect: "fadeIn", duration: 800 },
            modal: true,
            resizable: false,
           
            open: function () {
                $('.ui-widget-overlay').addClass('custom-overlay');
            },
            close: function () {
                $('.ui-widget-overlay').removeClass('custom-overlay');
            }
        });

        $('.update-holiday').click(function () {
            $('#add-holiday-dialog').dialog('open');
        });

    $('.cancel-button').click(function() {
        $("#add-holiday-dialog").dialog("close");
    });





    //drop down All-leaves employees
    var mouse_is_inside = false;

    $('.employee-select-container').hover(function(){ 
        mouse_is_inside=true; 
    }, function(){ 
        mouse_is_inside=false; 
    });

    $("body").mouseup(function(){ 
        if(! mouse_is_inside) $('.employee-select-container').hide();
    });

    $(".selector-container").click(function(){
        $(".employee-select-container").slideToggle();
        $(".employee-select-subcontainer").mCustomScrollbar({
            theme:"my-theme",
           
        });
    });


    //accordion
    var icons = {
         header: "iconClosed",    
         activeHeader: "iconOpen" 
     };
    $( "#accordion" ).accordion({
      icons: icons,
      heightStyle: 'content'
    });
    

    //all leaves datepicker

   
    $( "#datepicker-all-leaves").datepicker({
        showOtherMonths: true,
        dayNamesMin: ['SUNDAY', 'MONDAY', 'TUESDAY', 'WEDNESDAY', 'THURSDAY', 'FRIDAY', 'SATURDAY'],

        monthNames: ["JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER"]
    });


function shortMonthCalendar(){
    $(".datepicker-all-leaves .ui-datepicker-inline .ui-datepicker-calendar th").each(function() {
        var MonthName = $(this).text().substring(0, 3);
        $(this).text(MonthName);
        });
}

if ($(window).width() < 962) {
        shortMonthCalendar();

        $(".datepicker-all-leaves .ui-datepicker-inline .ui-datepicker-next").click(function(){
            shortMonthCalendar();
        });

        $(".datepicker-all-leaves .ui-datepicker-inline .ui-datepicker-prev").click(function(){
            shortMonthCalendar();
        });
    } 

$(window).resize(function(){
    if ($(window).width() < 962) {
        shortMonthCalendar();

        $(".datepicker-all-leaves .ui-datepicker-inline .ui-datepicker-next").click(function(){
            shortMonthCalendar();
        });

        $(".datepicker-all-leaves .ui-datepicker-inline .ui-datepicker-prev").click(function(){
            shortMonthCalendar();
        });
    }    
});








    //password lenght
    $("#newPassword").keydown(function () {
        var newPasswordlenght = $(this).val().length;

        if(newPasswordlenght < 8){
            $(".password-secure-weak").show();
            $(".password-secure-medium").hide();
            $(".password-secure-strong").hide();

        } else if(newPasswordlenght < 12) {
            $(".password-secure-weak").hide();
            $(".password-secure-strong").hide();
            $(".password-secure-medium").show();
        } else {
            $(".password-secure-weak").hide();
            $(".password-secure-medium").hide();
            $(".password-secure-strong").show();
        }
}).blur(function(){
    $(".pass-secure").hide();       
});

    //profile dialog
    $("#profile-dialog").dialog({
        autoOpen: false,
            show: { effect: "fadeIn", duration: 800 },
            modal: true,
            resizable: false,
           
            open: function () {
                $('.ui-widget-overlay').addClass('custom-overlay');
            },
            close: function () {
                $('.ui-widget-overlay').removeClass('custom-overlay');
            }
    });

    $('#change-password-btn').click(function () {
            $('#profile-dialog').dialog('open');
        });


    //responsive navigation
    $(".fa-bars").click(function(){
        $(".navigation").toggleClass("margin-responsive-nav");
    });

    //dialog pop up
       $("#dialog").dialog({
            autoOpen: false,
            show: { effect: "fadeIn", duration: 800 },
            modal: true,
            resizable: false,
           
            open: function () {
                $('.ui-widget-overlay').addClass('custom-overlay');
            },
            close: function () {
                $('.ui-widget-overlay').removeClass('custom-overlay');
            }
        });

       $('.update-status').click(function () {
           $("#from-date").val($(this).parent().parent().children("td").eq(1).children("input").val().split(' ')[0]);
           $("#to-date").val($(this).parent().parent().children("td").eq(2).children("input").val().split(' ')[0]);
           $("#leaveId").val($(this).parent().parent().children("td").eq(0).children("input").val());
           $("#histId").val($(this).parent().parent().children("td").eq(3).children("input").val());
           $("#comment").val($(this).parent().parent().children("td").eq(4).children("input").val());
            $('#dialog').dialog('open');
        });

    $('.cancel-button').click(function() {
        $("#dialog").dialog("close");
    });

    function datepickerHeadertoBottom() {
        $('.ui-datepicker-header').insertAfter('.ui-datepicker-calendar');
    }


    //input in breadcrumbs
    var inputText;
    
    $('.current-date').text($.datepicker.formatDate('dd - M - yy', new Date()));

    $("#date").datepicker({
        dateFormat: "mm-dd-yy",
        showOtherMonths: true,
        dayNamesMin: ['S', 'M', 'T', 'W', 'T', 'F', 'S'],
        monthNames: ["JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"]

    }).on("change", function (e) {
        var curDate = $(this).datepicker("getDate");
        $(this).datepicker("setDate", curDate);
        inputText = $("#date").datepicker("getDate");
        $('.current-date').text($.datepicker.formatDate("dd - M - yy", inputText));
    });



    $("#show-date").click(function () {
        $("#date").datepicker("show");
    });    


});