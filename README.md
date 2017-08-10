# Beetle.MR
Message action route

```c#
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Beetle.MR.UnitTest
{
	[TestClass]
	public class UnitTest1
	{

		private RouteCenter mCenter = new RouteCenter();

		public UnitTest1()
		{
			mCenter.Register(this);
		}

		[TestMethod]
		public void TestRegister()
		{
			Register register = new UnitTest.Register { Name = "henry" };
			mCenter.Invoke(register);
		}

		[TestMethod]
		public void TestRegisterToken()
		{
			Register register = new UnitTest.Register { Name = "henry" };
			mCenter.Invoke(register, DateTime.Now);
		}


		[TestMethod]
		public void TestPingFilter()
		{
			Ping ping = new Ping { Name="ping" };
			mCenter.Invoke(ping, DateTime.Now);
		}

		[Handler]
		public void OnRegister(Register register)
		{
			Console.WriteLine("Name={0}|Token={1}", register.Name, RouteCenter.CurrentContext.Token);
		}
		[Handler]
		[PingFilter]
		public void OnPing(Ping ping)
		{
			Console.WriteLine("Name={0}|Token={1}", ping.Name, RouteCenter.CurrentContext.Token);
		}
	}

	public class PingFilter : FilterAttribute

	{
		public override void Execute(MethodContext context)
		{
			Console.WriteLine("executing....");
			base.Execute(context);
			Console.WriteLine("executed");
		}
	}


	public class Ping
	{
		public string Name { get; set; }
	}


	public class Register
	{
		public string Name { get; set; }

	}

}
```
