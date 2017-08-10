using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.MR
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	public class SkipFilterAttribute : Attribute
	{
		public SkipFilterAttribute(params Type[] types)
		{

			Types = types;
			if (Types == null)
				Types = new Type[0];
		}
		public Type[] Types
		{
			get;
			set;
		}
	}
}

