using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.MR
{
	class MethodHandlerInfo
	{

		public MethodHandlerInfo()
		{
			Filters = new List<FilterAttribute>();
		}


		public bool UseTask { get; set; }

		public List<FilterAttribute> Filters { get; private set; }

		public object Target { get; set; }

		public Type Type { get; set; }

		public System.Reflection.MethodInfo Method { get; set; }

		public MethodHandler Handler { get; set; }

		public bool SingleInstance { get; set; }

		public Type MessageType { get; set; }

		public void Invokd(object message)
		{
			if (SingleInstance)
			{
				Handler.Execute(Target, new object[] { message });
			}
			else
			{
				object target = Activator.CreateInstance(Type);
				Handler.Execute(target, new object[] { message });
			}
		}
	}
}
