using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.MR
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	public class FilterAttribute : Attribute
	{

		public virtual void Execute(MethodContext context)
		{
			context.Execute();
		}

		public override bool Equals(object obj)
		{
			if (obj is Type)
			{
				return (Type)obj == this.GetType();
			}
			return obj.GetType() == this.GetType();
		}
	}
}
