function setFocus(id) {
    document.getElementById(id).focus();
}
function setReturnDate(sDate) {
    var str = sDate.split('/');
    var d = str[0];
    var m = str[1];
    var y = str[2];
    var result = m + "/" + d + "/" + y
    return result;
}
urlParam = function (name, url) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(url);
    if (results == null) {
        return null;
    }
    else {
        return results[1] || 0;
    }
}