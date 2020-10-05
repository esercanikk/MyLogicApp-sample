var workflowHub = $.connection.workflowHub;

$.connection.hub.start().done(function () {
    $("#btn-notify").click(function () {
        workflowHub.server.notify();
    });
});

//workflowHub.client.receiveNotification = function () {
//    alert("You are notified!");
//};

workflowHub.client.increaseTodoCount = function () {
    var previousCount = parseInt($("#todo-count").text());
    $("#todo-count").text(previousCount + 1);
    $("#btn-notify").removeClass("btn-danger").addClass("btn-success");
};

workflowHub.client.endCurrentTask = function () {
    var previousCount = parseInt($("#todo-count").text());
    $("#todo-count").text(previousCount - 1);
    $("#btn-notify").removeClass("btn-danger").addClass("btn-success");
    localStorage.removeItem("gorev");
    aktifGoreviGetir();
};

$(document).ready(function () {
    aktifGoreviGetir();
    gorevBildiriminiGetir();
});

$(document).ajaxSuccess(function (event, xhr, settings) {
    if (settings.type != "POST") return;
    var gorev = localStorage.getItem("gorev");
    if (gorev != null) {
        gorevObj = JSON.parse(gorev);
        if (gorevObj.hasOwnProperty("yonlendirmeUrl")) {
            if (gorevObj.yonlendirmeUrl.toLocaleLowerCase('en-US') == window.location.pathname.toLocaleLowerCase('en-US')) {
                var sonucData = form2Json(settings.data);
                $.ajax({
                    url: "/IsSureci/AdimDataKaydet",
                    method: "post",
                    data: {
                        id: gorevObj.id,
                        sonucData: JSON.stringify(sonucData)
                    },
                    success: function () {
                        localStorage.setItem("gorev", JSON.stringify(
                            {
                                id: gorevObj.id,
                                ad: gorevObj.ad
                            }
                        ));
                    }
                });
            }
        }
    }
});

function form2Json(str) {
    "use strict";
    var obj, i, pt, keys, j, ev;
    if (typeof form2Json.br !== 'function') {
        form2Json.br = function (repl) {
            if (repl.indexOf(']') !== -1) {
                return repl.replace(/\](.+?)(,|$)/g, function ($1, $2, $3) {
                    return form2Json.br($2 + '}' + $3);
                });
            }
            return repl;
        };
    }
    str = '{"' + (str.indexOf('%') !== -1 ? decodeURI(str) : str) + '"}';
    obj = str.replace(/\=/g, '":"').replace(/&/g, '","').replace(/\[/g, '":{"');
    obj = JSON.parse(obj.replace(/\](.+?)(,|$)/g, function ($1, $2, $3) { return form2Json.br($2 + '}' + $3); }));
    pt = ('&' + str).replace(/(\[|\]|\=)/g, '"$1"').replace(/\]"+/g, ']').replace(/&([^\[\=]+?)(\[|\=)/g, '"&["$1]$2');
    pt = (pt + '"').replace(/^"&/, '').split('&');
    for (i = 0; i < pt.length; i++) {
        ev = obj;
        keys = pt[i].match(/(?!:(\["))([^"]+?)(?=("\]))/g);
        for (j = 0; j < keys.length; j++) {
            if (!ev.hasOwnProperty(keys[j])) {
                if (keys.length > (j + 1)) {
                    ev[keys[j]] = {};
                }
                else {
                    ev[keys[j]] = pt[i].split('=')[1].replace(/"/g, '');
                    break;
                }
            }
            ev = ev[keys[j]];
        }
    }
    return obj;
}

function aktifGoreviGetir() {
    var gorev = localStorage.getItem("gorev");
    if (gorev != null) {
        gorevObj = JSON.parse(gorev);
        $("#btn-gorev").show();
        $("#btn-gorev").attr("href", "/IsSureci?isAtamaId=" + gorevObj.id);
        $("#txt-gorev-adi").text(gorevObj.ad);
    }
    else
        $("#btn-gorev").hide();
}

function gorevBildiriminiGetir() {
    $.ajax({
        url: "/Home/GetNotificationDescription",
        method: "get",
        success: function (response) {
            console.log(response)
            for (var i = 0; i < response.length; i++) {
                var el = getTaskTemplate(response[i].Item2, response[i].Item1, response[i].Item3, response[i].Item4)
                $("#list-tasks").append(el);
            }
        }
    });
}

function getTaskTemplate(baslik, aciklama, yonlendirmeUrl, id) {
    return `
        <div class="d-flex justify-content-between cursor-pointer">
            <div class="media d-flex align-items-center py-0">
                <div class="media-left pr-0"><img class="mr-1" src="../../../app-assets/images/icon/sketch-mac-icon.png" alt="avatar" height="39" width="39"></div>
                <div class="media-body">
                    <h6 class="media-heading"><span class="text-bold-500">${baslik}</span></h6>
                    <small class="notification-text">${aciklama}</small>
                </div>
                <div class="media-right pl-0">
                    <div class="row border-left text-center">
                        <div class="col-12 px-50 py-75 border-bottom">
                            <h6 class="media-heading text-bold-500 mb-0"><a href="${yonlendirmeUrl}">Git</a></h6>
                        </div>
                        <div class="col-12 px-50 py-75">
                            <h6 class="media-heading mb-0"><a href="IsSureci?isAtamaId=${id}">Tamamlandı</a></h6>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `
}