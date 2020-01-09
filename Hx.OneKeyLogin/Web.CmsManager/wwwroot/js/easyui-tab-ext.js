function tableValue(obj) {
    // alert($.isArray(datas));
    $('#' + obj.text[0].table).datagrid({
        width: obj.text[0].width, //1225//98
        height: obj.text[0].height, //调节表格高度//602
        border: obj.text[0].border, //边框取消
        url: obj.text[0].url,
        fitColumns: obj.text[0].fitColumns,
        columns: [obj.columns],
        rowStyler: function(index, row) {
            //每行的高度
            return 'height:' + obj.rowStyler[0].height + 'px;';
        },
        scrollbarSize: obj.rowStyler[0].scrollbarSize, //滚动条宽度
        pagination: obj.text[0].pagination, //分页
        pageNumber: obj.text[0].pageNumber,
        pageSize: obj.text[0].pageSize,
        pageList: obj.text[0].pageList,
        singleSelect: obj.text[0].singleSelect, //不允许多选
        rownumbers: obj.text[0].rownumbers, //显示行号，
        onLoadSuccess: function() {
            $('.btnedit').linkbutton(); //编辑 修改
            $('.btndlt').linkbutton(); //删除
            $('.btnadd').linkbutton(); //添加
            $('.btntest').linkbutton(); //测试
            $('.btnopen').linkbutton(); //打开
            $('.btnclose').linkbutton(); //关闭
            $('.btnsltspare').linkbutton(); //选择备用
            $('.btnchkchl').linkbutton(); //查看通道
            $('.btnaltchl').linkbutton(); //配置通道
            $('.btnreplacechl').linkbutton(); //替换用户通道
            $('.btnlogin').linkbutton(); //登录
            $('.btnrecharge').linkbutton(); //充值
            $('.btnup').linkbutton(); //上级
            $('.btndown').linkbutton(); //下级
            $('.btnbalance').linkbutton(); //余额提醒
            $('.btncheck').linkbutton(); //查看
            $('.btnrecovery').linkbutton(); //恢复
            $('.btnblacklist').linkbutton(); //添加黑名单
        }
        //结束datagrid
    });
    //结束表格
}

function tableDetail(obj) {
    var options = { width: 800, height: 1000 };
    var query = {};
    dgcreator("#dgmain", "staff/jsontext", query, options);
    //$("#ddd").datagrid(options);

    $('#' + obj.text[0].table).datagrid({
        width: obj.text[0].width, //1225//98
        height: obj.text[0].height, //调节表格高度//602
        border: obj.text[0].border, //边框取消
        url: obj.text[0].url,
        fitColumns: obj.text[0].fitColumns,
        columns: [obj.columns],
        rowStyler: function(index, row) {
            //每行的高度
            return 'height:' + obj.rowStyler[0].height + 'px;';
        },
        scrollbarSize: obj.rowStyler[0].scrollbarSize, //滚动条宽度
        pagination: obj.text[0].pagination, //分页
        pageNumber: obj.text[0].pageNumber,
        pageSize: obj.text[0].pageSize,
        pageList: obj.text[0].pageList,
        singleSelect: obj.text[0].singleSelect, //不允许多选
        rownumbers: obj.text[0].rownumbers, //显示行号
        onLoadSuccess: function() {
            $('.btnblacklist').linkbutton(); //添加黑名单
        },
        view: detailview,
        detailFormatter: function(rowIndex, rowData) {
            return '<table><tr>' +
                '<td style="width:100%;border:0;text-align:right">'
                + rowData.name +
                '</td>' +
                '</tr></table>';
        },
        onLoadSuccess: function(data) {
            var me = this;
            setTimeout(function() { //延时触发easyui datagrid detailviewclick事件，不用计时器无法展开，不懂什么问题~
                $(me).parent().find('span.datagrid-row-expander').trigger('click'); //没效果注意修改这里的选择器

                $(me).parent().find('span.datagrid-row-expander').click(function() { return false; }); //没效果注意修改这里的选择器

            }, 10);
        }
        //结束datagrid
    });
}

