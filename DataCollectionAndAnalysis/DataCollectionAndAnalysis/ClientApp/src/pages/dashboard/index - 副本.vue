<template>
    <div class="dashboard-container">  
        <el-link target="_blank" href="http://localhost:5183/_codegen?ui=vue" :underline="false"><i class="el-icon-s-platform"></i>
            <div class="link-ctx">代码生成器</div>
        </el-link>
        <div style="height:200px;width:500px">
            {{showmessage}}
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import ChartCard from "./views/chart-card.vue";
import * as signalR from "@aspnet/signalr";

@Component({
    components:{
        ChartCard
    }
})
export default class extends Vue {
    showmessage:string = '显示内容1：';    
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
    mounted(){
        let vm = this;  //此时this 是本组件
        this.connection.on('add_point', function(info) {
            vm.showmessage += info + '\n';
            console.log("-------------------------------");
        });
    };
}
</script>


<style lang="less" rel="stylesheet/less" scoped>
@import "~@/assets/css/mixin.less";
.dashboard-container {
    padding: 10px;
    p {
        text-indent: 2em;
    }
    .title {
        margin-bottom: 5px;
        font-weight: 300;
        font-size: 16px;
    }
    .category {
        text-indent: 0;
        color: #999999;
        .text-success {
            color: red;
        }
    }
    .stats {
        color: #999999;
    }

    .el-row {
        margin-bottom: 20px;
    }
    .el-card__header {
        color: #333;
        font-size: 14px;
    }
    .el-card {
        font-size: 14px;
        color: #666;
    }
    .lump-wrap {
        margin-bottom: 0;
        min-height: 160px;
        text-align: center;
        &.cxt-left {
            .el-col {
                .flexalign(flex-start);
                box-sizing: border-box;
                padding-bottom: 10px;
                div {
                    background-color: #f8f8f8;
                    width: 100%;
                    box-sizing: border-box;
                    padding: 0 10px;
                }
            }
        }
        .el-col {
            .center(column);
            i {
                width: 100%;
                height: 60px;
                line-height: 60px;
                text-align: center;
                border-radius: 2px;
                font-size: 30px;
                background-color: #f8f8f8;
            }
            h3 {
                padding: 5px 0;
                font-size: 12px;
            }
            p {
                font-style: normal;
                font-size: 30px;
                font-weight: 300;
                color: #009688;
                text-indent: 0;
            }
            .link-a {
                color: #666;
                font-size: 14px;
                text-align: center;
            }
        }
        .link-ctx {
            min-width: 80px;
        }
    }
}
</style>