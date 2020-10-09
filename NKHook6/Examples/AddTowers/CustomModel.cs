using Assets.Scripts.Models.Towers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddTowers
{
    class CustomModel : TowerModel
    {
        public CustomModel() : base("CustomMonkey", "CustomMonkey", default, "Towers/Custom", 100, 100, 100, true, true, 0, default, default, default, default, default, default, default, default, default, false, false, default, default, true, false, 0, default, default, false, default, default, true)
        {

        }
    }
}
