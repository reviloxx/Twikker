export function ajaxRequest(data, target, callback) {
    var xhr = new XMLHttpRequest();
    xhr.open('post', target, true);
    xhr.onload = function () {
        if (callback !== null) {
            var response = JSON.parse(xhr.responseText);
            callback(response);
        }
    };
    xhr.send(data);
}