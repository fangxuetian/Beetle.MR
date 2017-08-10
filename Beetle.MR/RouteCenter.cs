using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace Beetle.MR
{
	public class RouteCenter
	{



		public RouteCenter()
		{
			Filters = new List<FilterAttribute>();
		}


		private Dictionary<Type, MethodHandlerInfo> mMethodHanbdlers = new Dictionary<Type, MethodHandlerInfo>();


		public void Register(object controller)
		{
			Register(controller.GetType(), controller);
		}

		public void Register(Type controllerType, bool singleInstance = true)
		{
			Register(controllerType, Activator.CreateInstance(controllerType), singleInstance);
		}

		public void Register(Type controllerType, object controller, bool singleInstance = true)
		{
			foreach (MethodInfo method in controllerType.GetMethods(BindingFlags.Instance | BindingFlags.Public))
			{
				HandlerAttribute ha = method.GetCustomAttribute<HandlerAttribute>(false);
				if (ha != null)
				{

					MethodHandlerInfo handler = new MR.MethodHandlerInfo();
					handler.Handler = new MR.MethodHandler(method);
					handler.MessageType = method.GetParameters()[0].ParameterType;
					handler.SingleInstance = singleInstance;
					handler.Target = controller;
					handler.Type = controllerType;
					handler.UseTask = ha.UseTask;
					mMethodHanbdlers[handler.MessageType] = handler;

					foreach (FilterAttribute item in Filters)
					{
						handler.Filters.Add(item);
					}
					foreach (FilterAttribute item in controllerType.GetCustomAttributes<FilterAttribute>())
					{
						handler.Filters.Add(item);
					}
					foreach (FilterAttribute item in method.GetCustomAttributes<FilterAttribute>())
					{
						handler.Filters.Add(item);
					}
					foreach (SkipFilterAttribute skip in method.GetCustomAttributes<SkipFilterAttribute>())
					{
						foreach (Type type in skip.Types)
						{
							handler.Filters.Remove((FilterAttribute)Activator.CreateInstance(type));
						}
					}
				}
			}
		}


		public void Invoke(object message, object token = null)
		{

			MethodHandlerInfo info;
			if (mMethodHanbdlers.TryGetValue(message.GetType(), out info))
			{
				MethodContext contex = new MR.MethodContext(message, token, info.UseTask, info.Filters, info);
				contex.Execute();
			}
			else
			{
				throw new Exception(string.Format("{0} handler notfound", message.GetType()));
			}
		}


		[ThreadStatic]
		private static MethodContext mCurrentContext;

		public static MethodContext CurrentContext
		{
			get
			{
				return mCurrentContext;
			}
			internal set { mCurrentContext = value; }
		}


		public IList<FilterAttribute> Filters { get; private set; }
	}
}
