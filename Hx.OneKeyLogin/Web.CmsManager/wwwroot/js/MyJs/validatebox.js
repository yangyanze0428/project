//自定义验证
$.extend($.fn.validatebox.defaults.rules, {
    //验证敏感词是否存在,params:第一个必须放url地址
    wordexist: {
        validator: function (value, params) {
            return Result(value, params);
        },
        message: '敏感词已存在'
    }
});

function Result (value, params)
{
    rlt = false;
    $.ajax({
        url: params[0],
        data: { keyword: value, id: $(params[1]).val() },
        async: false,
        dataType: "json",
        success: function (data) {
            if (data == true) {
                rlt= true;
            }
            else {
                rlt= false;
            }
        },
        error: function () {
            $.messager.alert("提示", "系统内部错误", null, function () {
            });
        }
    });
    return rlt;
}