/// <reference path="oidc-client.js" />

function log() {
    document.getElementById('results').innerText = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        }
        else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById('results').innerHTML += msg + '\r\n';
    });
}

document.getElementById("login").addEventListener("click", login, false);
document.getElementById("api").addEventListener("click", api, false);
document.getElementById("logout").addEventListener("click", logout, false);

var base_url = window.location.origin;

var config = {
    // authority: "https://localhost:5001",
    authority: 'http://'+window.location.hostname+':5001',
    client_id: "js",
    // redirect_uri: "https://localhost:5003/callback.html",
    redirect_uri: base_url + "/callback.html",
    response_type: "code",
    scope:"openid profile api1",
    // post_logout_redirect_uri : "https://localhost:5003/index.html",
    post_logout_redirect_uri : base_url + "/index.html",
    acr_values:'tenant:'+window.location.host.split('.')[0]+'',
};
var mgr = new Oidc.UserManager(config);

mgr.getUser().then(function (user) {
    if (user) {
        log("User logged in", user.profile);
    }
    else {
        log("User not logged in");
    }
});

function login() {
    mgr.signinRedirect();
}

function api() {
    mgr.getUser().then(function (user) {
        // var url = "https://localhost:6001/identity";
        var url = 'http://'+window.location.hostname+':6001/identity';

        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            log(xhr.status, JSON.parse(xhr.responseText));
        }
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}

function logout() {
    mgr.signoutRedirect();
}