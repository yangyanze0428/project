if (!jQuery) { throw new Error("requires jQuery") }
if (!jQuery.easyui) { throw new Error("requires easyui") }
//easyuiTable 界面功能

var g_const = {
    largeDlgSize: ["1000px", "550px"],
    middleDlgSize: ["800px", "400px"],
    smallDlgSize: ["400px", "300px"]
};

var g_options = {
    width: "98%",
    height: "100%",
    border: false,
    nowrap: false,
    striped: true,
    rownumbers:true,
    pagination: true,//底部是否显示工具栏[分页]
    fitcolumns: true,//是否适应表格宽度
    pageNumber: 1,//初始化页码
    singleSelect: true,//不允许多选
    pageSize: 20,//初始化页面显示的行数
    pageList: [15, 20, 30, 50]//数据选择
};
var o_options = {
    field: "__opt",
    title: "操作",
    width: "20%",
    halign: "center",
    align: "center"
};
/*
    pattern:pattern 表的id，#dgmain,.dgmain,
    url:staff/JsonText
    scheme:
    option:easyui表显示的功能
    formatter:操作
    optcolformat:登录人员的权限
*/
function dgcreator(pattern, url, query, scheme, options, formatter, funcolopt) {
    var _this = this;
    var model;
    var urladd = url + "/add";
    var urlupdate = url + "/update";
    var urldel = url + "/delete";
    var urlgettab = "/scheme?scheme=" + scheme;
    var urlgetlist = url + "/getlist";
    var urlget = url + "/get";
    var urlsubFail = url + "/subFail";
    var urlaudit = url + "/audit";
    var urldisable = url + "/disable";
    var urlinform = url + "/inform";
    var urlsign = url + "/sign";
    var urladdblist = url + "/addblacklist";
    var _isCreated = false;

    // var urlgetmenage=url+"/GetMenagement";
    //根据条件查询数据
    this.setdata = function (_query) {
        $(pattern).datagrid('load', _query);
        //var json = getjson(urlgetlist, _query);
        //if (json == null) return;
        //$(pattern).datagrid("loadData", json.data);
    };
    //添加数据
    this.add = function (_query) {
        var json = add(_query, urladd);
        if (json == "true") {
            return "true";
        }
        else {
            return "false";
        }
    };
    //修改数据
    this.update = function (_query, urlupdate) {
        var json = update(_query, urlupdate);
        if (json == "true") {
            return "true";
        }
        else {
            return "false";
        }
    };
    //删除数据
    this.del = function (id) {
        var json = del(id);
        if (json == "true") {
            return "true";
        }
        else {
            return "false";
        }
    };
    //提交失败
    this.subFail = function (id) {
        var json = subFail(id);
        if (json == "True") {
            return "true";
        }
        else {
            return "false";
        }
    };
    //进入审核
    this.audit = function (id) {
        var json = audit(id);
        if (json == "True") {
            return "true";
        }
        else {
            return "false";
        }
    };
    //禁用
    this.disable = function (id) {
        var json = disable(id);
        if (json == "True") {
            return "true";
        }
        else {
            return "false";
        }
    };
    //通知
    this.inform = function (id) {
        var json = inform(id);
        if (json == "True") {
            return "true";
        }
        else {
            return "false";
        }
    };
    //添加到黑名单
    this.addBlackList = function (id) {
        var json = addBlackList(id);
        if (json == "True") {
            return "true";
        }
        else {
            return "false";
        }
    };
    //标记【普通用户】
    this.sign = function (id) {
        var json = sign(id);
        if (json == "True") {
            return "true";
        }
        else {
            return "false";
        }
    };
    //根据id 获取具体数据
    this.get = function (id, urlget) {
        var json = get(id, urlget);
        return json;
    }
    //队列的操作【添加、删除】
    this.beforeCreate = function (cols) { }

    //提示错误
    this.notice = function (prompt, text, sign) {
        $.messager.alert(prompt, text + '！', sign);
    }
    this.show = function (title, msg, time) {
        $.messager.show({
            title: title,
            msg: msg,
            timeout: time,
            showType: 'fade'
        });
    }
    //加载的时候显示
    this.create = function () {
        //替换不同的表功能
        var _options = g_options;
        if (options != null) {
            for (var opt in options) {
                _options[opt] = options[opt];
            }
        }

        var json = getjson(urlgettab, query);

        _this.beforeCreate(json);
        //头、内容、总数
        var cols = [];
        cols.push(createhead(json));
        _options["columns"] = cols;
        //_options["data"] = json.data;

        //操作表头
        var optcol = createoptcol();
        if(optcol!=null) _options["columns"][0].push(optcol);

        if (url != "") {
            _options["url"] = urlgetlist;
            _options["queryParams"] = query;
        }
        $(pattern).datagrid(_options);
        _isCreated = true;
    }

    this.setOption = function(key, value) {
        if (_isCreated) {
            $(pattern).datagrid("options")[key] = value;
            //$(pattern).datagrid("reload");
            return;
        }
        g_options[key] = value;
    }


    //添加操作列
    function createoptcol() {
        if (funcolopt == null) return null;
        //var optcol = { field: "__opt", title: "操作", width: "20%", halign: "center", align: "center", formatter: optcolformat(perms) };
        var optcol = o_options;
        for (var opt in funcolopt) {
            optcol[opt] = funcolopt[opt];
        }
        //optcol.formatter = optcolformat(perms);
        return optcol;
    }
    //获取头
    function createhead(jsonobj) {
        var columns = [];
        for (var i = -1; i < jsonobj.length; i++) {
            var col;
            if (i < 0) {
                if (formatter != null) {
                    var func = formatter(i);
                    if (func == "check") {
                        col = { field: "ck", title: "全选" };
                        col["checkbox"] = true;
                        columns.push(col);
                    }
                }
            }
            if (i >= 0) {
                //var 
                col = { field: jsonobj[i].field, title: jsonobj[i].title };
                //col = jsonobj[i];
                if (jsonobj[i].width != "") {
                    col["width"] = jsonobj[i].width;
                }
                col["align"] = jsonobj[i].align != "" ? jsonobj[i].align : "left";
                col["halign"] = jsonobj[i].headAlign != "" ? jsonobj[i].headAlign : "center";
                col["hidden"] = jsonobj[i].hidden;
                if (formatter != null) {
                    var func = formatter(i);
                    //增加隐藏该列
                    //if (func == "hidden") {
                    //    col["hidden"] = true;
                    //}
                    //else
                    if (func != null)
                        col["formatter"] = func;
                }
                if (jsonobj[i].styler) {
                    col["styler"] = jsonobj[i].styler;
                }
                columns.push(col); //".KIzXCV BNM,./"
            }
        }
        return columns;
    }
    //添加
    function add(_query, urladd) {
        var json = null;
        $.ajax({
            async: false,
            type: "POST",
            url: urladd,
            data: _query,
            success: function (data) {
                json = data;
            },
            //XMLHttpRequest 对象、错误信息、（可选）捕获的异常对象
            error: function (data, textStatus, errorThrown) {
                _this.notice("错误提示", "错误信息：" + data.responseText, "info");
            },
            dataType: "text"
        });
        return json;
    }
    //查询
    function getjson(urlgetlist, query) {
        //查询json数据
        var json = null;
        $.ajax({
            type: "POST",
            url: urlgetlist,
            data: query,
            success: function (data) {
                json = data;
            },
            error: function (data, textStatus, errorThrown) {
                _this.notice("错误提示", "错误信息：" + data.responseText, "info");
            },
            dataType: "json",
            async:false
        });
        return json;
    }
    //根据id查询数据
    function querylist(url, query) {
        //查询json数据
        var json = null;
        $.ajax({
            type: "POST",
            url: url,
            data: query,
            success: function (data) {
                json = data;
            },
            error: function (data, textStatus, errorThrown) {
                _this.notice("错误提示", "错误信息：" + data.responseText, "info");
            },
            dataType: "json"
        });
        return json;
    }
    //根据id获取数据
    function get(id, urlget) {
        var json = null;
        $.ajax({
            type: "POST",
            url: urlget,
            async:false,
            data: { id: id },
            success: function (data) {
                json = data;
            },
            error: function (data, textStatus, errorThrown) {
                _this.notice("错误提示", "错误信息：" + data.responseText, "info");
            },
            dataType: "json"
        });
        return json;
    }
    //修改
    function update(_query, urlupdate) {
        var json = null;
        $.ajax({
            type: "POST",
            url: urlupdate,
            data: _query,
            success: function (data) {
                json = data;
            },
            //XMLHttpRequest 对象、错误信息、（可选）捕获的异常对象
            error: function (data, textStatus, errorThrown) {
                _this.notice("错误提示", "错误信息：" + data.responseText, "info");
            },
            dataType: "text",
            async: false
        });
        return json;
    }
    //删除
    function del(id) {
        var json = null;
        $.ajax({
            async: false,
            type: "POST",
            url: urldel,
            data: { id: id },
            success: function (data) {
                json = data;
            },
            //XMLHttpRequest 对象、错误信息、（可选）捕获的异常对象
            error: function (data, textStatus, errorThrown) {
                _this.notice("错误提示", "错误信息：" + data.responseText, "info");
            },
            dataType: "text"
        });
        return json;
    }
    //提交失败
    function subFail(id) {
        var json = null;
        $.ajax({
            type: "POST",
            url: urlsubFail,
            data: { id: id },
            success: function (data) {
                json = data;
            },
            //XMLHttpRequest 对象、错误信息、（可选）捕获的异常对象
            error: function (data, textStatus, errorThrown) {
                _this.notice("错误提示", "错误信息：" + data.responseText, "info");
            },
            dataType: "text"
        });
        return json;
    }
    //进入审核
    function audit(id) {
        var json = null;
        $.ajax({
            type: "POST",
            url: urlaudit,
            data: { id: id },
            success: function (data) {
                json = data;
            },
            //XMLHttpRequest 对象、错误信息、（可选）捕获的异常对象
            error: function (data, textStatus, errorThrown) {
                _this.notice("错误提示", "错误信息：" + data.responseText, "info");
            },
            dataType: "text"
        });
        return json;
    }
    //禁用
    function disable(id) {
        var json = null;
        $.ajax({
            type: "POST",
            url: urldisable,
            data: { id: id },
            success: function (data) {
                json = data;
            },
            //XMLHttpRequest 对象、错误信息、（可选）捕获的异常对象
            error: function (data, textStatus, errorThrown) {
                _this.notice("错误提示", "错误信息：" + data.responseText, "info");
            },
            dataType: "text"
        });
        return json;
    }
    //通知
    function inform(id) {
        var json = null;
        $.ajax({
            type: "POST",
            url: urlinform,
            data: { id: id },
            success: function (data) {
                json = data;
            },
            //XMLHttpRequest 对象、错误信息、（可选）捕获的异常对象
            error: function (data, textStatus, errorThrown) {
                _this.notice("错误提示", "错误信息：" + data.responseText, "info");
            },
            dataType: "text"
        });
        return json;
    }
    //添加到黑名单
    function addBlackList(id) {
        var json = null;
        $.ajax({
            type: "POST",
            url: urladdblist,
            data: { id: id },
            success: function (data) {
                json = data;
            },
            //XMLHttpRequest 对象、错误信息、（可选）捕获的异常对象
            error: function (data, textStatus, errorThrown) {
                _this.notice("错误提示", "错误信息：" + data.responseText, "info");
            },
            dataType: "text"
        });
        return json;
    }
    //标记【普通用户】
    function sign(id) {
        var json = null;
        $.ajax({
            type: "POST",
            url: urlsign,
            data: { id: id },
            success: function (data) {
                json = data;
            },
            //XMLHttpRequest 对象、错误信息、（可选）捕获的异常对象
            error: function (data, textStatus, errorThrown) {
                _this.notice("错误提示", "错误信息：" + data.responseText, "info");
            },
            dataType: "text"
        });
        return json;
    }
}

//获取修改数据
function getUpdate(winame, json) {
    //$('#' + winame).window('open');
    SetValue(winame, json);
}

//添加时 传入地址获取 【控件名+值=拼成json】
function createJson(addr) {
    var text = $("#" + addr + " .easyui-textbox");
    var b = "";
    for (var i = 0; i < text.length; i++) {
        b += "\"" + text[i].id + "\":\"" + $("#" + text[i].id).textbox('getValue') + "\",";
    }
    var cbbox = $("#" + addr + " .easyui-combobox");
    for (var i = 0; i < cbbox.length; i++) {
        b += "\"" + cbbox[i].id + "\":\"" + $("#" + cbbox[i].id).combobox('getValue') + "\",";
    }
    var datebox = $("#" + addr + " .easyui-datebox");
    for (var i = 0; i < datebox.length; i++) {
        b += "\"" + datebox[i].id + "\":\"" + $("#" + datebox[i].id).datebox('getValue') + "\",";
    }
    var ckbox = $("#" + addr + " .checkbox");
    for (var i = 0; i < ckbox.length; i++) {
        b += "\"" + ckbox[i].id + "\":\"" + document.getElementById(ckbox[i].id).checked + "\",";
    }

    var rlt = "{" + b.substring(0, b.length - 1) + "}";
    return jQuery.parseJSON(rlt);
    // console.debug("{" + rlt.substring(0, rlt.length - 1) + "}");
}

function SetValue(addr, json) {

    var text = $("#" + addr + " .easyui-textbox");
    var cbbox = $("#" + addr + " .easyui-combobox");
    var datebox = $("#" + addr + " .easyui-datetimebox");
    var ckbox = $("#" + addr + " .checkbox");
    if (json != null) {
        for (var i = 0; i < text.length; i++) {
            var txtId = replaceStr(text[i].id);
            $("#" + text[i].id).textbox('setValue', json.rows[txtId]);
        }
        for (var i = 0; i < cbbox.length; i++) {
            var cbboxId = replaceStr(cbbox[i].id);
            $("#" + cbbox[i].id).combobox('setValue', json.rows[cbboxId]);
        }
        for (var i = 0; i < datebox.length; i++) {
            var dateboxId = replaceStr(datebox[i].id);
            $("#" + datebox[i].id).datebox('setValue', json.rows[dateboxId]);
        }
        for (var i = 0; i < ckbox.length; i++) {
            var ckboxId = replaceStr(ckbox[i].id);
            document.getElementById(ckbox[i].id).checked = json.rows[ckboxId];
        }

    } else {
        for (var i = 0; i < text.length; i++) {
            $("#" + text[i].id).textbox('setValue', "");
        }
        for (var i = 0; i < cbbox.length; i++) {
            var data = $("#" + cbbox[i].id).combobox('getData');
            $("#" + cbbox[i].id).combobox('setValue', data[0].value);
        }
        for (var i = 0; i < datebox.length; i++) {
            $("#" + datebox[i].id).datebox('setValue', "");
        }
        for (var i = 0; i < ckbox.length; i++) {
            document.getElementById(ckbox[i].id).checked = false;
        }
    }
}

//切换界面
function addTab(title, id, src) {
    createTab("#tt", title, id, src);
}
function createTab(pattern, title, id, src) {
    if ($(pattern).tabs('exists', title)) {
        parent.$(pattern).tabs('select', title);
    } else {
        parent.$(pattern).tabs('add', { title: title, href: "/tab?id=" + id + "&src=" + src, closable: true, scrollbarSize: 0 });
    };
}


//提交方法
function ComMethod(url, para, type) {
    var json = null;
    $.ajax({
        async: false,
        type: "POST",
        url: url,
        data: para,
        success: function (data) {
            json = data;
        },
        error: function (data, textStatus, errorThrown) {
            $.messager.alert('错误提示', data, info);
        },
        dataType: type
    });
    return json;
}

//首字母转换成小写
function replaceStr(str) {
    var strTemp = ""; //新字符串
    for (var i = 0; i < str.length; i++) {
        if (i == 0) {
            strTemp += str[i].toLowerCase(); //第一个
            continue;
        }
        if (str[i] == " " && i < str.length - 1) { //空格后
            strTemp += " ";
            strTemp += str[i + 1].toLowerCase();
            i++;
            continue;
        }
        strTemp += str[i];
    }
    return strTemp;
}

//paras
function openDlg(pattern, title, url, dlgSize, paras) {

    var q = "el=" + pattern;
    if (typeof (paras) != "undefined" && paras !=null) {
        for (k in paras) {
            q += "&" + k + "=" + paras[k];
        }
    }
    url = url + (url.indexOf("?") == -1 ? "?" : "&") + q;
    var width = "100%";
    var height = "100%";
    if (typeof (dlgSize) != "undefined") {
        width = dlgSize[0];
        height = dlgSize[1];
    }
    $("#" + pattern).css("width", width).css("height", height);
    $("#" + pattern + " iframe").css("width", "100%").css("height", "99%").css("border", 0);
    $("#" + pattern).window({
        title: title,
        minimizable: false,
        maximizable: false,
        collapsible:false,
        resizable:false,
        modal: true
    });
    $("#" + pattern + " iframe").attr("src", url);
}

(function ($) {
    $.getUrlParam = function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]);
        return null;
    }
})(jQuery);