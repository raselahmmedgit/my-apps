function CountdownTimer(elm, tl, mes) {
    this.initialize.apply(this, arguments);
}
CountdownTimer.prototype = {
    initialize: function (elm, tl, mes) {
        this.elem = document.getElementById(elm);
        this.tl = tl;
        this.mes = mes;
    }, countDown: function () {
        var timer = '';
        var today = new Date();
        var day = Math.floor((this.tl - today) / (24 * 60 * 60 * 1000));
        var hour = Math.floor(((this.tl - today) % (24 * 60 * 60 * 1000)) / (60 * 60 * 1000));
        var min = Math.floor(((this.tl - today) % (24 * 60 * 60 * 1000)) / (60 * 1000)) % 60;
        var sec = Math.floor(((this.tl - today) % (24 * 60 * 60 * 1000)) / 1000) % 60 % 60;
        var me = this;

        if ((this.tl - today) > 0) {
            timer += '\n\
            <div class="counters">\n\
                <div class="counter-item">\n\
                    <div class="top-counter">\n\
                        <span class="counter-days">' + day + '</span>\n\
                    </div>\n\
                    <div class="bottom-counter">days</div>\n\
                </div>\n\
                <div class="counter-item">\n\
                    <div class="top-counter">\n\
                        <span class="counter-hours">' + hour + '</span>\n\
                    </div>\n\
                    <div class="bottom-counter">hours</div>\n\
                </div>\n\
                <div class="counter-item">\n\
                    <div class="top-counter">\n\
                        <span class="counter-minutes">' + this.addZero(min) + '</span>\n\
                    </div>\n\
                    <div class="bottom-counter">minutes</div>\n\
                </div>\n\
                <div class="counter-item">\n\
                    <div class="top-counter">\n\
                        <span class="counter-seconds">' + this.addZero(sec) + '</span>\n\
                    </div>\n\
                    <div class="bottom-counter">seconds</div>\n\
                </div>\n\
            </div>';
            this.elem.innerHTML = timer;
            tid = setTimeout(function () {
                me.countDown();
            }, 10);
        } else {
            this.elem.innerHTML = this.mes;
            return;
        }
    }, addZero: function (num) {
        return ('0' + num).slice(-2);
    }
};
function CDT() {

    // Set countdown limit
    var tl = new Date('2017/01/01 00:34:00');

    // You can add time's up message here
    var timer = new CountdownTimer('CDT', tl, '<span class="number-wrapper"><div class="line"></div><span class="number end">Time is up!</span></span>');
    timer.countDown();
}
window.onload = function () {
    if ($('#CDT').length) {
        CDT();
    }
};