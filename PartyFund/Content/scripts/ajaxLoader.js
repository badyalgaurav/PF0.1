$(document).ajaxStart(function () {
    var height = $(document).height();;
    $("#loading").attr("style", "position:absolute;baseZ: 99999; z-index: 99999; top: 0px; " +
        "left:0px; text-align: center; display:none; background-color: #404040;color: '#fff'; " +
        "height:" + height + "px; width: 100%; /* These three lines are for transparency " +
        "in all browsers. */-ms-filter:\"progid:DXImageTransform.Microsoft.Alpha(Opacity=50)\";" +
        " filter: alpha(opacity=50); opacity:.5;");
    $("#loading img");
    $("#loading").show();
});

$(document).ajaxStop(function () {
    $("#loading").removeAttr("style");
    $("#loading img").removeAttr("style");
    $("#loading").hide();
});