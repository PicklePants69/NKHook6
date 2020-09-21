using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api
{
    class CommandManager
    {
		private static CommandManager instance;

		public static CommandManager Instance
		{
			get 
			{
				if (instance == null)
					instance = new CommandManager();
				return instance;
			}
			set { instance = value; }
		}



		public EventHandler<string> OnCommand;

    }
}
