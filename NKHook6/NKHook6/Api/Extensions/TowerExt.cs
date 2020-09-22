using Assets.Scripts.Simulation.SMath;
using Assets.Scripts.Simulation.Towers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api.Extensions
{
    public static class TowerExt
    {
        public static Vector2 getPos(this Tower tower)
        {
            return new Vector2(tower.Position.X, tower.Position.Z);
        }
        public static void setPos(this Tower tower, Vector2 pos)
        {
            tower.PositionTower(pos);
        }
        public static void setPos(this Tower tower, float posX, float posY)
        {
            tower.PositionTower(new Vector2(posX, posY));
        }
    }
}
