if (!jQuery) { throw new Error("requires jQuery") }
if (!jQuery.easyui) { throw new Error("requires easyui") }
//easyuiTable 界面功能
var g_options = {
    id: "pages",
    width: "98%",
    height: "100%",
    border: false,
    pagination: true,//底部是否显示工具栏[分页]
    fitcolumns: true,//是否适应表格宽度
    pageNumber: 1,//初始化页码
    singleSelect: true,//不允许多选
     pageSize: 20,//初始化页面显示的行数
     pageList: [20, 50, 200, 500],//数据选择
    onSelectPage: function (pageNumber, pageSize) {
        alert(pageNumber + ":" + pageSize);
    },
    onChangePageSize: function (pageSize) {
        alert(pageSize);
    }
}
var o_options = {
    field: "__opt",
    title: "操作",
    width: "20%",
    halign: "center",
    align: "center",
    formatter: ""
};
/*
    pattern:pattern 表的id，#dgmain,.dgmain,
    url:staff/JsonText
    query:查询参数 
    option:easyui表显示的功能
    formatter:操作
    optcolformat:登录人员的权限
*/
function dgcreator(pattern, url, query, options, formatter, optcolformat, operation) {
    var _this = this;
    //var urldel = url + "/delete";
    // var urlgetlist = url + "/getlist";
    var contain = getContain(pattern);

    function getContain(pattern) {
        if (typeof pattern == "string") {
            return $(pattern);
        }
        return pattern;
    }

    //根据条件查询数据
    this.setdata = function (_query) {
        //var json = getjson(urlgetlist, _query);
        var json = getjson(url, _query);
        if (json == null) return;
        contain.datagrid("loadData", json.data);
    };
    //添加数据
    this.add = function (_query, urladd) {
        var json = add(_query, urladd);
        if (json == "True") {
            return "true";
        }
        else {
            return "false";
        }
    };
    //修改数据
    this.update = function (_query, urlupdate) {
        var json = update(_query, urlupdate);
        if (json == "True") {
            return "true";
        }
        else {
            return "false";
        }
    };
    //删除数据
    this.del = function (id, url) {
        var json = del(id, url);
        if (json == "True") {
            return "true";
        }
        else {
            return "false";
        }
    };

    //删除数据
    this.deltype = function (para, url) {
        var json = deltype(para, url);
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
        _options["id"] = "pages";
        if (options != null) {
            for (var opt in options) {
                _options[opt] = options[opt];
            }
        }

        //var json = getjson(urlgetlist, query);
        var json = getjson(url, query);

        _this.beforeCreate(json.columns);
        //头、内容、总数
        var cols = [];
        cols.push(createhead(json.columns));
        _options["columns"] = cols;
        _options["data"] = json.data;

        if (json.perm != null) {
            //操作表头
            var optcol = createoptcol(eval(json.perm));
            if (optcol != null) {
                //添加操作表头
                _options["columns"][0].push(optcol);
            }
        }
        contain.datagrid(_options);
        contain.datagrid("reload");

    }
    //加载的时候显示
    this.querylist = function (contain, url, query) {

        //var json = getjson(urlgetlist, query);
        //var json = querylist(url, query);
        var json = getjson(url, query);

        _this.beforeCreate(json.columns);
        //头、内容、总数
        var cols = [];
        cols.push(createhead(json.columns));
        _options["columns"] = cols;
        _options["data"] = json.data;

        if (json.perm != null) {
            //操作表头
            var optcol = createoptcol(eval(json.perm));
            if (optcol != null) {
                //添加操作表头
                _options["columns"][0].push(optcol);
            }
        }
        contain.datagrid(_options);
        contain.datagrid("reload");
    }
    //添加操作列
    function createoptcol(perms) {
        if (optcolformat == null) return null;
        //var optcol = { field: "__opt", title: "操作", width: "20%", halign: "center", align: "center", formatter: optcolformat(perms) };
        var optcol = o_options;
        if (operation != null) {
            for (var opt in operation) {
                optcol[opt] = operation[opt];
            }
        }
        optcol.formatter = optcolformat(perms);
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
                col = { field: jsonobj[i].Field, title: jsonobj[i].Title };
                if (jsonobj[i].Width != "") {
                    col["width"] = jsonobj[i].Width;
                }
                col["align"] = jsonobj[i].Align != "" ? jsonobj[i].Align : "left";
                col["halign"] = jsonobj[i].HeadAlign != "" ? jsonobj[i].HeadAlign : "center";

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
                columns.push(col); //".KIzXCV BNM,./"
            }
        }
        return columns;
    }
    //添加
    function add(_query, urladd) {
        var val = null;
        $.ajax({
            async: false,
            type: "POST",
            url: urladd,
            data: _query,
            success: function (data) {
                val = data;
            },
            //XMLHttpRequest 对象、错误信息、（可选）捕获的异常对象
            error: function (data, textStatus, errorThrown) {
                _this.notice("错误提示", "错误信息：" + data.responseText, "info");
            },
            dataType: "text"
        });
        return val;
    }
    //查询数据列表
    //function getjson(urlgetlist, query) {
    function getjson(url, query) {
        //查询json数据
        var json = null;
        $.ajax({
            async: false,
            type: "POST",
            //url: urlgetlist,
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
    //根据id获取数据明细
    function get(id, urlget) {
        var json = null;
        $.ajax({
            async: false,
            type: "POST",
            url: urlget,
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
            async: false,
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
            dataType: "text"
        });
        return json;
    }
    //删除
    function del(id, url) {
        var json = null;
        $.ajax({
            async: false,
            type: "POST",
            url: url,
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

    //删除类型
    function deltype(para, url) {
        var json = null;
        $.ajax({
            async: false,
            type: "POST",
            url: url,
            data: para,
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
    SetValue(winame, json);
};

//添加时 传入地址获取 【控件名+值=拼成json】
function GetJson(addr) {
    var text = $("#" + addr + " .easyui-textbox");
    var b = "";
    for (var i = 0; i < text.length; i++) {
        b += "\"" + text[i].id + "\":\"" + $("#" + text[i].id).textbox('getValue') + "\",";
    }
    var cbbox = $("#" + addr + " .easyui-combobox");
    for (var i = 0; i < cbbox.length; i++) {
        b += "\"" + cbbox[i].id + "\":\"" + $("#" + cbbox[i].id).combobox('getValues') + "\",";
    }
    var datebox = $("#" + addr + " .easyui-datebox");
    for (var i = 0; i < datebox.length; i++) {
        b += "\"" + datebox[i].id + "\":\"" + $("#" + datebox[i].id).datebox('getValue') + "\",";
    }
    var ckbox = $("#" + addr + " .checkbox");
    for (var i = 0; i < ckbox.length; i++) {

        var value = document.getElementById(ckbox[i].id).checked == true ? 1 : 0;
        // b += "\"" + ckbox[i].id + "\":\"" + value + "\",";
        b += "\"" + ckbox[i].id + "\":" + value + ",";
    }

    var cbogrid = $("#" + addr + " .easyui-combogrid");
    for (var i = 0; i < cbogrid.length; i++) {
        b += "\"" + cbogrid[i].id + "\":\"" + $("#" + cbogrid[i].id).combobox('getValues') + "\",";
    }
    var fbbox = $("#" + addr + " .easyui-filebox");
    for (var i = 0; i < fbbox.length; i++) {
        var a = $("#" + fbbox[i].id).filebox('getValue');
        b += "\"" + fbbox[i].id + "\":\"" + $("#" + fbbox[i].id).filebox('getValue') + "\",";
    }
    var rlt = "{" + b.substring(0, b.length - 1) + "}";
    return jQuery.parseJSON(rlt);
}

function SetValue(addr, json) {
    var text = $("#" + addr + " .easyui-textbox");
    var cbbox = $("#" + addr + " .easyui-combobox");
    var datebox = $("#" + addr + " .easyui-datebox");
    var ckbox = $("#" + addr + " .checkbox");
    var fbbox = $("#" + addr + " .easyui-filebox");
    if (json != null) {
        for (var i = 0; i < text.length; i++) {
            $("#" + text[i].id).textbox('setValue', json.data.rows[text[i].id]);
        }
        for (var i = 0; i < cbbox.length; i++) {
            $("#" + cbbox[i].id).combobox("setValue", json.data.rows[cbbox[i].id]);
        }
        for (var i = 0; i < datebox.length; i++) {
            $("#" + datebox[i].id).datebox('setValue', json.data.rows[datebox[i].id]);
        }
        for (var i = 0; i < ckbox.length; i++) {
            //  document.getElementById(ckbox[i].id).checked = json.data.rows[ckbox[i].id];
            document.getElementById(ckbox[i].id).checked = json.data.rows[ckbox[i].id];
        }
        for (var i = 0; i < fbbox.length; i++) {
            //  document.getElementById(ckbox[i].id).checked = json.data.rows[ckbox[i].id];

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
            document.getElementById(ckbox[i].id).checked = 0;
        }
        for (var i = 0; i < fbbox.length; i++) {

        }
    }
}

//cbx赋值
function setcbxdata(list, name, id) {
    var json = list;
    var str = json.replace(/&quot;/g, "\"");
    var data = $.parseJSON(str);
    $("#" + name).combobox("loadData", data);
    if (id == null) {
        $("#" + name).combobox("select", "");
    }
    else if (id != "0") {
        $("#" + name).combobox("setValue", id);
    }
    else {
        if (data.length > 0) {
            $("#" + name).combobox("select", data[0]["Id"]);
        }
        else {
            $("#" + name).combobox("select", "");
        }
    }
}

//cbx赋值
function cbxdata(bag, json, name) {
    if (bag != null && bag != "") {
        var data = $.parseJSON(bag.replace(/&quot;/g, "\""));//将bag转成json格式
        $("#" + name).combobox("loadData", data);
        if (json == null) {
            $("#" + name).combobox("select", data[0]["Id"]);
        }
        else if (json.data.rows[name] != null) {
            $("#" + name).combobox("setValues", json.data.rows[name]);
        }
        //else {
        ////    $("#" + name).combobox("setValues", "");
        //}
    }
        //else if ((bag == "" && json == null) || (bag == null && json == null)) {
        //    $("#" + name).combobox("setValues", "");
        //}
    else {
        if (json == null) {
            $("#" + name).combobox("setValues", "");
        }
        else if (json.data.rows[name] != null) {
            $("#" + name).combobox("setValues", json.data.rows[name]);
        }
        //else {
        //     $("#" + name).combobox("select", 1);
        // }
    }
}

//切换界面
function addTab(title, iframeid, src) {
    if ($('#tt').tabs('exists', title)) {
        parent.$('#tt').tabs('select', title);
    } else {
        parent.$('#tt').tabs('add', { title: title, href: "/Modeliframe/Index?iframeid=" + iframeid + "&src=" + src, closable: true, scrollbarSize: 0 });
    };
}


//使用指定的元素和地址创建一个combogrid组合网格
function combogridUrl(id, idField, value, url) {
    $(id).combogrid({
        panelWidth: 450,
        value: value,
        idField: idField,
        textField: 'ChannelName',
        multiple: true,
        editable: false,
        fitColumns: true,
        //mode: 'remote',
        url: url,
        columns: [[
            { field: 'ChannelName', title: '通道名称', width: 150 },
            {
                field: 'OperatorType', title: '运营商类型', width: 100,
                formatter: function (value, row, index) {
                    switch (row.OperatorType) {
                        case "1":
                            return "移动";
                        case "2":
                            return "联通";
                        case "3":
                            return "电信";
                        case "4":
                            return "全网";
                        default:
                            return "未知";
                    }
                }
            },
            { field: 'AreaCode', title: '支持地区', width: 100 }
        ]]//,
        //onSelect: function (key, value) {
        //    var grid = $(id).combogrid('grid');
        //    var rows = grid.datagrid('getSelections');
        //    var ary = new Array();
        //    $.each(rows, function (i, v) {
        //        var op = v['OperatorArea'];
        //        if (ary.contains(op)) {
        //            alert("已选择了【" + v['OperatorName'] + v['ProvinceName'] + "】通道");
        //            grid.datagrid('uncheckRow', key);
        //            return false;
        //        };
        //        ary[i] = op;
        //    });
        //    if (rows.length > 1) {
        //        grid.datagrid('uncheckRow', 0);
        //    };
        //},
        //onUnselect: function (key, value) {
        //    var grid = $(id).combogrid('grid');
        //    var rows = grid.datagrid('getSelections');
        //    if (rows.length < 1) {
        //        grid.datagrid('checkRow', 0);
        //    };
        //}
    });
}


