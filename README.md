ALowBeeProject -
通过模拟请求实现获取课程表数据（已有）的功能【由于匹配规则与界面规则的问题，本版本将不会更新，也不会修复BUG】

1.主要功能：实现教务系统模拟登陆，并模拟请求获取课程表数据，通过已有的规则匹配出课程信息，最后显示在新窗体上。

2.特点：
1）可通过登陆帐号的信息获取到当前账号有效查询年份范围，无须用户手动输入学年；
2）如某学期没有任何课程（其实就是未到该学期时间，教务处没有导入课程表数据）会进行提示并终止显示课表进程；
3）登陆失败逻辑清晰，区别出登录失败原因（验证码错误、密码错误、用户名不存在或未参加教学活动被限制、其它未知原因）；
4）登录失败后会自动刷新验证码，除去手动操作的繁琐；
5）如请求验证码失败或者请求Cookie失败会进行提示（通常是网络中断或者无法访问Orz）
6）退出登录会自动初始化，方便其它帐号登陆；

3.已知BUG
1）退出登录后重新登陆一定会提示验证码错误（原因：没有在退出登陆后再次请求验证码，由于本项目不会再更新故不做修改）；
2）遇到某些特殊的课程有机率抛出错误（通常都是选修课Orz，考虑在下个项目中重写匹配规则）；
3）主界面下面的测试按钮，是制作时方便测试设置的，没有加任何检测和限制，如果随意输入内容一定会报错；
4）在课程表显示界面双击课程表的格子会弹出对话框，内容是所点击文本框的属性和文本框内的内容（因为测试写的这条语句，不再维护就代表不再修改）

4.其它
这个项目最早从今年二月开始，因为抓包没学好扔在一边（当时登陆都是失败的），到七月下旬才拿起来，通过网上资料的查询，花了几天时间制作出了这个程序，就类似于超级课程表的查课表功能。测试期间由于BUG和本人对项目的不满意，所以决定终止此项目的开发，其他功能如查询成绩、一键评教等，会在下个项目中实现（其实就是重新写这个程序），会涉及到课表匹配规则和窗体显示规则的修改。伤筋动骨比较麻烦干脆重新搭Orz

NOTICE:
由于上述的已知BUG和可能会出现的未知BUG，作者不会对因使用本程序查询出的结果有误承担任何责任（本来就是随手写的，纰漏多很正常）。仅供学习参考交流使用。

dellbeat
2017-08-03

