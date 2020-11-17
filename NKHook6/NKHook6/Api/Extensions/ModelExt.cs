using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api.Extensions
{
    public static class ModelExt
    {
        public static string tryGetLostClassName(this Model model)
        {
            string modelName = model.name;
            string lostTypeData = modelName.Split('_')[0];
            return lostTypeData;
        }
    }
}
