<template>
    <div class="charts-wrap">
        <el-link target="_blank" href="http://localhost:5183/_codegen?ui=vue" :underline="false"><i class="el-icon-s-platform"></i>
            <div class="link-ctx">代码生成器</div>
        </el-link>
        <div style="height:200px;width:500px">
            {{showmessage}}
        </div>
        <div class="charts-draw" ref="chart"></div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import * as signalR from "@aspnet/signalr";
//引入echarts
const echarts = require("echarts/lib/echarts");

// 引入折线图
require("echarts/lib/chart/line");

require("echarts/lib/component/tooltip");
require("echarts/lib/component/legend");

@Component({
    components:{
        
    }
})
export default class extends Vue {
    showmessage:string = '显示内容：';  
    //定义一个connection
    connection:signalR.HubConnection = new signalR.HubConnectionBuilder()
        .withUrl('api/RealTimeDataHub',signalR.HttpTransportType.ServerSentEvents)      //指定ServerSentEvents,WebSocket报错
        .configureLogging(signalR.LogLevel.Error)
        .build();
    
    created() {
        // 开始通讯，呼叫服务器
        this.connection.start().then(() => {
            //链接创建成功
            this.connection.invoke('AddPoint', "asdf").catch(function (err) {
                return console.error(err);
            });
        });        
    };

    

    mounted() {
        // 基于准备好的dom，初始化echarts实例
        const myChart = echarts.init(this.$refs.chart);
        
        var option;

        function randomData(value) {
            now = new Date(+now + thms);
            return {
                name: now.toString(),
                value: [
                    [now.getFullYear(), now.getMonth() + 1, now.getDate()].join('/'),
                    Math.round(value)
                ]
            };
        }

        var data = [];
        var now = +new Date(1997, 9, 3);
        var thms = 24 * 3600 * 1000;
        //折线初始点数
        for (var i = 0; i < 100; i++) {
            data.push(randomData(1600));
        }

        option = {
            title: {
                text: '动态数据 + 时间坐标轴'
            },
            tooltip: {
                trigger: 'axis',   
                formatter: function (params) {
                    params = params[0];
                    var date = new Date(params.name);
                    return date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear() + ' : ' + params.value[1];
                },             
                axisPointer: {
                    animation: false
                }
            },
            xAxis: {
                type: 'time',
                splitLine: {
                    show: false
                }
            },
            yAxis: {
                type: 'value',
                boundaryGap: [0, '100%'],
                splitLine: {
                    show: false
                }
            },
            series: [{
                name: '模拟数据',
                type: 'line',
                showSymbol: false,
                hoverAnimation: false,
                data: data
            }]
        };
        
        let vm = this;  //此时this 是本组件
        this.connection.on('add_point', function(info) {
            vm.showmessage = info;

            data.shift();

            var rd_data = randomData(Number.parseInt(info));

            console.log(rd_data);
            
            data.push(rd_data);

            myChart.setOption({
                series: [{
                    data: data
                }]
            });

        });
        
        // 绘制图表
        myChart.setOption(option);
    }
}
</script>


<style lang="less" scoped>
.charts-wrap {
    .charts-draw {
        width: 100%;
        min-height: 400px;
    }
}
</style>