using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.MR
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class HandlerAttribute : Attribute
	{
		public bool UseTask { get; set; }
	}
}
