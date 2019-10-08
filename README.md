# Unity3DSimpleConfig
> 在游戏开发过程中，有一些简单的配置，不需要走表，使用一个文本文件去配置，更方便。

**1. 配置文件格式如下**

<img src="https://i.loli.net/2019/10/08/SKyuweNVY7Q2gnv.png" align=left />

**"[ ]"** 中的部分，为模块名称，像 **GameFrame** 这样没有在某个模块下的配置，为全局配置



**2. 如何使用**

> `注意：在游戏开始时，首先要读取配置文件，路径自己定义`

```csharp
string path = @"D:/Config.txt";
SimpleConfig.Load(path);

int gameFrame = SimpleConfig.GetIntValue("GameFrame");
int loginChannel = SimpleConfig.GetIntValue("Login", "LoginChannel");
string testUserName = SimpleConfig.GetStringValue("Login", "TestUserName");
string testPassword = SimpleConfig.GetStringValue("Login", "TestPassword");
float timeout = SimpleConfig.GetFloatValue("Net", "Timeout");
bool dm = SimpleConfig.GetBoolValue("Debug", "DebugMode");
bool dm2 = SimpleConfig.GetBoolValue("Debug", "DebugMode2");

Debug.LogFormat("GameFrame: {0}", gameFrame);
Debug.LogFormat("LoginChannel: {0}", loginChannel);
Debug.LogFormat("testUserName: {0}", testUserName);
Debug.LogFormat("testPassword: {0}", testPassword);
Debug.LogFormat("timeout: {0}", timeout);
Debug.LogFormat("dm: {0},  dm2: {1}", dm, dm2);
```

