using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api
{
    class CommandManager
    {
        public static CommandManager instance;
        public CommandManager()
        {
            instance = this;
        }


        public EventHandler<string> OnCommand;
    }
}
