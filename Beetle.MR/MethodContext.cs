using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.MR
{
	public class MethodContext
	{


		internal MethodContext(object message, object token, bool usetask, IList<FilterAttribute> filters,
			MethodHandlerInfo info)
		{

			mFilters = filters;
			this.Token = token;
			this.UseTask = usetask;
			this.Message = message;
			HandlerInfo = info;
		}


		private bool Execting = false;

		private IList<FilterAttribute> mFilters;

		private int mFilterIndex = 0;

		public object Token { get; private set; }

		public bool UseTask { get; set; }

		public object Message { get; private set; }

		internal MethodHandlerInfo HandlerInfo { get; set; }

		internal void Execute()
		{

			if (Execting)
			{
				OnExecute();
			}
			else
			{
				Execting = true;

				if (UseTask)
				{
					Task.Run(new Action(OnExecute));
				}
				else
				{
					OnExecute();
				}
			}



		}

		private void OnExecute()
		{

			RouteCenter.CurrentContext = this;
			try
			{
				if (mFilters != null && mFilterIndex < mFilters.Count)
				{
					mFilterIndex++;
					mFilters[mFilterIndex - 1].Execute(this);

				}
				else
				{
					HandlerInfo.Invokd(Message);
				}
			}
			finally
			{
				RouteCenter.CurrentContext = null;
			}
		}

	}
}
