function ChartPie(value,name)
{
    var myCharts1 = echarts.init(document.getElementById('pic1'));
    var option1 = {
        backgroundColor: 'white',

        title: {
            text: '短信配比',
            left: 'center',
            top: 20,
            textStyle: {
                color: '#ccc'
            }
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {d}%"
        },

        visualMap: {
            show: false,
            min: 500,
            max: 600,
            inRange: {
                colorLightness: [0, 1]
            }
        },
        series: [
            {
                name: '课程内容分布',
                type: 'pie',
                clockwise: 'true',
                startAngle: '0',
                radius: '60%',
                center: ['50%', '50%'],
                data: [
                    {
                        value: value,
                        name: name,
                        itemStyle: {
                            normal: {
                                color: 'rgb(255,192,0)',
                                shadowBlur: '90',
                                shadowColor: 'rgba(0,0,0,0.8)',
                                shadowOffsetY: '30'
                            }
                        }
                    },
                    {
                        value: value,
                        name: name,
                        itemStyle: {
                            normal: {
                                color: 'rgb(1,175,80)'
                            }
                        }
                    },
                    {
                        value: value,
                        name: 'name',
                        itemStyle: {
                            normal: {
                                color: 'rgb(122,48,158)'
                            }
                        }
                    }

                ],
            }
        ]
    };
    myCharts1.setOption(option1);


}
function ChartColumn(data, name)
{
    var myChart = echarts.init(document.getElementById("main"));
    myChart.title = '多 Y 轴示例';

    var colors = ['#5793f3', '#d14a61', '#675bba'];
    //        var colors = ['red', 'yellow', 'black'];


    var option = {
        color: colors,

        tooltip: {
            trigger: 'axis',
            //'item' 数据项图形触发，主要在散点图，饼图等无类目轴的图表中使用。
            //'axis' 坐标轴触发，主要在柱状图，折线图等会使用类目轴的图表中使用。
            axisPointer: {
                type: 'cross',
                snap: true,
            }
        },
        grid: {
            right: '20%' //设置grid 组件离容器右侧的距离。
        },
        legend: {
            data: ['成功量', '失败量', '平均量']
        },
        xAxis: [
            {
                type: 'category',
                axisTick: {
                    alignWithLabel: true,//可以保证刻度线和标签对齐
                },
                data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']
            }
        ],
        yAxis: [
            {
                type: 'value',
                name: 'name',
                min: 0,
                max: 250,
                position: 'right',
                axisLine: {
                    lineStyle: {
                        color: colors[0]//坐标轴颜色
                    }
                },
                axisLabel: {
                    formatter: '{value} t'//坐标轴文字格式化
                }
            },
            {
                type: 'value',
                name: 'name',
                min: 0,
                max: 250,
                position: 'right',//降水量的位置在右边，
                offset: 80,//向右偏移
                axisLine: {
                    lineStyle: {
                        color: colors[1]//坐标轴颜色
                    }
                },
                axisLabel: {
                    formatter: '{value} t'
                }
            },
            {
                type: 'value',
                name: 'name',
                min: 0,
                max: 25,
                position: 'left',
                axisLine: {
                    lineStyle: {
                        color: colors[2]
                    }
                },
                axisLabel: {
                    formatter: '{value} %'
                }
            }
        ],
        series: [
            {
                name: 'name',
                type: 'bar',
                barCategoryGap: '50%',
                data: data,
            },
            {
                name: 'name',
                type: 'bar',
                yAxisIndex: 1,
                data: data
            },
            {
                name: 'name',
                type: 'line',
                yAxisIndex: 2,
                data: data
            }
        ]
    };
    myChart.setOption(option);
}