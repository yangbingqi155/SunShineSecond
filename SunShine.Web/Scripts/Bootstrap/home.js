//滚动效果
$.fn.imgscroll = function (o) {
    var defaults = {
        speed: 40,
        amount: 30,
        width: 2,
        dir: "up"
    };
    o = $.extend(defaults, o);

    return this.each(function () {
        var _li = $("li", this);
        _li.parent().parent().css({ overflow: "hidden", position: "relative" }); //div
        _li.parent().css({ padding: "0", overflow: "hidden", position: "relative", "list-style": "none" }); //ul
        _li.css({ position: "relative", overflow: "hidden" }); //li
        if (o.dir == "left") _li.css({ float: "left" });

        //初始大小
        var _li_size = 0;
        for (var i = 0; i < _li.size() ; i++)
            _li_size += o.dir == "left" ? _li.eq(i).outerWidth(true) : _li.eq(i).outerHeight(true);

        //循环所需要的元素
        if (o.dir == "left") _li.parent().css({ width: (_li_size * 3) + "px" });
        _li.parent().empty().append(_li.clone()).append(_li.clone()).append(_li.clone());
        _li = $("li", this);

        //滚动
        var _li_scroll = 0;
        function goto() {
            _li_scroll += o.width;
            if (_li_scroll > _li_size) {
                _li_scroll = 0;
                _li.parent().css(o.dir == "left" ? { left: -_li_scroll } : { top: -_li_scroll });
                _li_scroll += o.width;
            }
            _li.parent().animate(o.dir == "left" ? { left: -_li_scroll } : { top: -_li_scroll }, o.amount);
        }

        //开始
        var move = setInterval(function () { goto(); }, o.speed);
        _li.parent().hover(function () {
            clearInterval(move);
        }, function () {
            clearInterval(move);
            move = setInterval(function () { goto(); }, o.speed);
        });
    });
};


var flag = false;
function DrawImage(ImgD, iwidth, iheight, count) {
    var image = new Image();
    image.src = ImgD.src;
    var zoom_height = iwidth * image.height / image.width;//缩放后的高度
    if (zoom_height > iheight) {//按照宽度显示
        ImgD.width = iwidth;
        ImgD.height = zoom_height;
        ImgD.style.marginTop = (iheight - zoom_height) / 2 + "px";
    }
    else//按照高度显示
    {
        ImgD.width = iheight * image.width / image.height;
        ImgD.height = iheight;
        ImgD.style.marginLeft = (iwidth - (iheight * image.width / image.height)) / 2 + "px";
    }
}
function DrawImage1(ImgD, iwidth, iheight, mount) {
    var image = new Image();
    var parentEl = ImgD;
    for (var i = 0; i < mount; i++) {
        parentEl = parentEl.parentElement;
        parentEl.style.textAlign = "center";
    }
    image.src = ImgD.src;
    if (image.width > 0 && image.height > 0) {
        flag = true;
        if (image.width / image.height >= iwidth / iheight) {
            if (image.width > iwidth) {
                ImgD.width = iwidth;
                ImgD.style.paddingTop = (iheight - (image.height * iwidth) / image.width) / 2 + "px";
                ImgD.height = (image.height * iwidth) / image.width;
            } else {

                if (iheight > image.height) {
                    ImgD.style.paddingTop = (iheight - image.height) / 2 + "px";
                    ImgD.height = image.height;
                    ImgD.width = image.height * iwidth / iheight;
                }
            }

        }
        else {
            if (image.height > iheight) {
                ImgD.height = iheight;
                ImgD.width = (image.width * iheight) / image.height;
            }
            else {
                ImgD.style.paddingTop = (iheight - image.height) / 2 + "px";
                ImgD.height = image.height;
                ImgD.width = image.height * iwidth / iheight;
            }
        }
    }
}



function DrawImageWidth(ywidth, yheight, iwidth, iheight) {
    if (ywidth > 0 && yheight > 0) {
        flag = true;
        if (ywidth / yheight >= iwidth / iheight) {
            if (ywidth >= iwidth) {
                return "width:" + iwidth + "px;height:" + (yheight * iwidth) / ywidth + "px;padding-top:" + (iheight - (yheight * iwidth) / ywidth) / 2 + "px";
            } else {

                if (iheight >= yheight) {
                    return "width:" + yheight * iwidth / iheight + "px;height:" + yheight + "px;padding-top:" + (iheight - yheight) / 2 + "px";
                }
            }

        }
        else {
            if (yheight >= iheight) {
                return "width:" + (ywidth * iheight) / yheight + "px;height:" + iheight + "px;";

            }
            else {
                return "width:" + ywidth * iwidth / iheight + "px;height:" + yheight + "px;padding-top:" + (iheight - yheight) / 2 + "px";
            }
        }
    }
}