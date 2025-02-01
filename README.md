# Repro for theRainbird/CoreRemoting#122

https://github.com/theRainbird/CoreRemoting/issues/122

## Info
Running on Visual Studio 17.12.4

5 projects:

* CoreRemotingTest.DotnetFramework.Server - .NET Framework 4.7.2

* CoreRemotingTest.DotnetFramework.Client - .NET Framework 4.7.2

* CoreRemotingTest.DotnetCore.Server - .NET Core 8.0

* CoreRemotingTest.DotnetCore.Client - .NET Core 8.0

* CoreRemotingTest.Shared - .NET Standard 2.0

Any variation of cross-framework server/client fails with the type error:

| Server | Client | Result |
| --- | --- | --- |
| .NET Framework | .NET Framework | OK |
| .NET Core | .NET Core | OK |
| .NET Framework | .NET Core | Error |
| .NET Core | .NET Framework | Error |


## Screenshot of output

Left: DotnetFramework.Server, Right: DotnetCore.Client

![image](https://github.com/user-attachments/assets/3fde95c8-3f4f-4f19-9ba2-0adcd2b3b295)

## Output - DotnetFramework.Server

```

Registered services
-------------------
ServiceName = 'CoreRemotingTest.Shared.ISayHelloService', InterfaceType = CoreRemotingTest.Shared.ISayHelloService, UsesFactory = False, Lifetime = Singleton

Server is running.
--[Error]--------------------------
Error processing message.

Error resolving type specified in JSON 'System.String[], System.Private.CoreLib, Version=8.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e'. Path '_value.GenericArgumentTypeNames.$type'.
   at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.ResolveTypeName(JsonReader reader, Type& objectType, JsonContract& contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, String qualifiedTypeName)
   at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.ReadMetadataProperties(JsonReader reader, Type& objectType, JsonContract& contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, Object existingValue, Object& newValue, String& id)
   at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreateObject(JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, Object existingValue)
   at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreateValueInternal(JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, Object existingValue)
   at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.SetPropertyValue(JsonProperty property, JsonConverter propertyConverter, JsonContainerContract containerContract, JsonProperty containerProperty, JsonReader reader, Object target)
   at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PopulateObject(Object newObject, JsonReader reader, JsonObjectContract contract, JsonProperty member, String id)
   at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreateObject(JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, Object existingValue)
   at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreateValueInternal(JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, Object existingValue)
   at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.SetPropertyValue(JsonProperty property, JsonConverter propertyConverter, JsonContainerContract containerContract, JsonProperty containerProperty, JsonReader reader, Object target)
   at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PopulateObject(Object newObject, JsonReader reader, JsonObjectContract contract, JsonProperty member, String id)
   at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreateObject(JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, Object existingValue)
   at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreateValueInternal(JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, Object existingValue)
   at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.Deserialize(JsonReader reader, Type objectType, Boolean checkAdditionalContent)
   at Newtonsoft.Json.JsonSerializer.DeserializeInternal(JsonReader reader, Type objectType)
   at Newtonsoft.Json.JsonSerializer.Deserialize[T](JsonReader reader)
   at CoreRemoting.Serialization.Bson.BsonSerializerAdapter.Deserialize(Type type, Byte[] rawData)
   at CoreRemoting.Serialization.Bson.BsonSerializerAdapter.Deserialize[T](Byte[] rawData)
   at CoreRemoting.RemotingSession.ProcessRpcMessage(WireMessage request)
   at CoreRemoting.RemotingSession.<>c__DisplayClass40_0.<OnReceiveMessage>b__0()
-----------------------------------
```

## Output - DotnetCore.Client

```
Connecting...
Unhandled exception. System.TimeoutException: Send timeout (30) exceeded.
   at CoreRemoting.ServiceProxy`1.Intercept(IInvocation invocation)
   at stakx.DynamicProxy.AsyncInterceptor.Castle.DynamicProxy.IInterceptor.Intercept(IInvocation invocation)
   at Castle.DynamicProxy.AbstractInvocation.Proceed()
   at Castle.Proxies.ISayHelloServiceProxy.add_MessageReceived(Action`2 value)
   at CoreRemotingTest.Client.HelloWorldClient.Main() in C:\projects\CoreRemotingTest\CoreRemotingTest.Client\Program.cs:line 38

C:\projects\CoreRemotingTest\CoreRemotingTest.Client\bin\Debug\net8.0\CoreRemotingTest.Client.exe (process 63476) exited with code -532462766 (0xe0434352).
Press any key to close this window . . .
```
