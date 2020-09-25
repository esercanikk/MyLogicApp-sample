// Color Codes

// Apex grafiklerinde kullanılan renk kodları

var $primary = '#5A8DEE';
var $success = '#39DA8A';
var $danger = '#FF5B5C';
var $warning = '#FDAC41';
var $info = '#00CFDD';
var $label_color = '#475f7b';
var $success_light = '#D2FFE8';
var $primary_light = '#E2ECFF';
var $danger_light = '#ffeed9';
var $gray_light = '#828D99';
var $sub_label_color = "#596778";
var $radial_bg = "#e7edf3";

// Loader

// show(string): Verilen element üzerinde "Yükleniyor..." ifadesini gösterir.
// hide(string): Verilen elementte çalışmakta olan "Yükleniyor..." ifadesini kaldırır.

var Loader = (function () {
    function Loader() { }

    Loader.prototype.show = function (el) {
        var block_ele = $(el).closest('.card');
        $(block_ele).block({
            message: '<div class="semibold"><span class="bx bx-revision icon-spin text-left"></span>&nbsp; Yükleniyor...</div>',
            overlayCSS: {
                backgroundColor: "#FFFFFF",
                opacity: 0.8,
                cursor: 'wait'
            },
            css: {
                border: 0,
                padding: 0,
                backgroundColor: 'transparent'
            }
        });
    }

    Loader.prototype.hide = function (el) {
        var block_ele = $(el).closest('.card');
        $(block_ele).unblock();
    }

    return Loader;
}());

// Interval Object

// intervals: Çalıştırılan bütün intervallerin tutulduğu set.
// make(function, number, any[]): Yeni bir interval oluşturur. Temel olarak çalıştırılacak fonksiyon ve çalıştırılma periyodu girilir.
// clear(number): intervals içinde bulunan belli bir interval'i siler.
// clearAll: intervals içinde bulunan bütün interval'leri siler.

var IntervalObj = {
    intervals: new Set(),
    make(callback, ms, ...args) {
        var newInterval = setInterval(callback, ms, ...args);
        this.intervals.add(newInterval);
        return newInterval;
    },
    clear(id) {
        this.intervals.delete(id);
        return clearInterval(id);
    },
    clearAll() {
        for (var id of this.intervals) {
            this.clear(id);
        }
    }
};

// Price Formatter

// moreReadablePrice(number):
//      Decimal olarak gelen fiyat tutarını Türk Lirası cinsinden formatlamakta kullanılır. Örnek: ₺3.200,45

function moreReadablePrice(p) {
    if (!p || isNaN(p))
        p = 0;
    var price = p + "";
    var splitted = price.split("");

    var decimalPart = splitted.indexOf(".");

    var steps = [];
    var decimal = [];
    if (decimalPart != -1) {
        for (var q = 0; q < decimalPart; q++)
            steps.push(splitted[q]);
        for (var w = decimalPart + 1; w < splitted.length; w++)
            decimal.push(splitted[w]);
    } else {
        steps = splitted;
        decimal.push(0);
        decimal.push(0);
    }

    steps = steps.reverse();
    for (var i = 1; i < steps.length; i++) {
        if (i % 3 === 0)
            steps[i] += ".";
    }

    return `${steps.reverse().join("")},${decimal.join("")}`;
}

// Date Operations

// monthStrToDate(string, bool, bool):
//      HTML'de month tipinde bir giriş yapıldığında bu fonksiyon ile 2020-09-01 şeklinde string'e veya Date tipinde bir objeye dönüştürülebilir.
//          str: dönüştürülecek string "2020-09"
//          toEnd: ay başına mı, ay sonuna mı "01 veya 31"
//          asFullStr: string olarak mı, date olarak mı "2020-09-01 veya new Date('2020-09-01')"
//
// formatDate(string)
//      JSON formatında gelen string veriyi, Date objesi olarak formatlar.

function monthStrToDate(str, toEnd, asFullStr) {
    if (!str) return;
    var array = str.split("-");
    var year = parseInt(array[0]);
    var month = parseInt(array[1]);
    var day = toEnd ? new Date(year, month, 0).getDate().toString().padStart(2, '0') : "01";
    return asFullStr ? year + "-" + month + "-" + day : new Date(year + "-" + month + "-" + day);
}

function formatDate(dateStr) {
    if (dateStr == null) return null;
    var dateString = dateStr.substr(6);
    var currentTime = new Date(parseInt(dateString));
    return currentTime;
}