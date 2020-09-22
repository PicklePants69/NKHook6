using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.SMath;
using Mono.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api.Extensions
{
    public static class BloonExt
    {
        public static void setBloonModel(this Bloon bloon, BloonModel model)
        {
            bloon.model = model;
        }
    }
}
