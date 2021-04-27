# demo 描述
下位机通过tcp，将一个随机数发送到 web api应用，web api 收到tcp 数据后通过signalr将 tcp 数据发送到 vue 前端，前端绘制曲线

# 使用框架介绍
使用 wtm（vue） 框架，自己集成signalr

# 运行
下载后直接运行后端，会生成数据库，并还原前端 npm 包，后端运行后，进入前端clientApp目录，使用 yarn start 命令启动前端
默认登录 admin 密码 000000
进入 tcp_client 的bin/debug目录，运行可执行程序，点击“测试线程发送”按钮，vue 前端就能看到曲线更新
