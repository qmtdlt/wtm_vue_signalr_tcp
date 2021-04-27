# demo 描述
下位机通过tcp，将一个随机数发送到 web api应用，web api 收到tcp 数据后通过signalr将 tcp 数据发送到 vue 前端，前端绘制曲线

# 使用框架介绍
使用 wtm（vue） 框架，sqlserver 数据库，自己集成signalr

# 运行
下载后直接运行后端，会生成数据库，并还原前端 npm 包，后端运行后，进入前端clientApp目录，使用 yarn start 命令启动前端
默认登录 admin 密码 000000
进入 tcp_client 的bin/debug目录，运行可执行程序，点击“测试线程发送”按钮，vue 前端就能看到曲线更新

# 后端运行
![image](https://user-images.githubusercontent.com/20112289/116172529-87e97600-a73d-11eb-96c5-91b1133b4718.png)

# tcp 测试工具运行
![image](https://user-images.githubusercontent.com/20112289/116172574-98015580-a73d-11eb-964d-b0f38cc9f904.png)

# 前端曲线
![image](https://user-images.githubusercontent.com/20112289/116172722-d6971000-a73d-11eb-97e5-1666d207e9d8.png)

# bug 说明
如图，指定使用sse模式，则运行正常
![image](https://user-images.githubusercontent.com/20112289/116172899-2c6bb800-a73e-11eb-9e6b-89faf133603a.png)
如图，不指定模式，默认使用websocket，会出现两分钟后报错，降级到sse
![image](https://user-images.githubusercontent.com/20112289/116173004-5a50fc80-a73e-11eb-8e45-f7bb3065b32f.png)
