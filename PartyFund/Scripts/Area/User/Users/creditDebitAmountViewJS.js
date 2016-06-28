var Path = location.host;
var VirtualDirectory;
if (Path.indexOf("localhost") >= 0 && Path.indexOf(":") >= 0) {
    VirtualDirectory = "";
}
else {
    var pathname = window.location.pathname;
    var VirtualDir = pathname.split('/');
    VirtualDirectory = VirtualDir[1];
    VirtualDirectory = '/' + VirtualDirectory;
}


$(document).ready(function () {
    $("#insertedAmount").keypress(function (e) {
        if (e.which == 13) {
            window.Update();
        }
    });
});
//region to save the new add or delete operation
window.Update = function (id) {
    var url;
    if (id == "0")
    {
        url = addAdminAmountUrl;
    }
    else {
        url = "http://localhost:1416/api/TransectionDetailsAPI/InsertTransectionRecord";
    }
    debugger;
    //var ID = Convert.toInt(id);
    $("#TransectionAmount").val($("#insertedAmount").val());
    $("#TotalAmount").val($("#totalAmount").val());
    var data = $('#registerationForm').serialize();
    $.ajax({
        url:url ,
        type: "Post",
        data: data,
        //contentType: "application/jsonp; charset=utf-8",
        success: function (data) {
            debugger;
            alert("Employee Added successfully");
            window.location.href = relocationUrl;
        },
        error: function () {
            alert("error");
        }
    });

}
