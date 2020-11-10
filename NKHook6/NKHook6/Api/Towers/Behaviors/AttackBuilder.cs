using Assets.Scripts.Models;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Assets.Scripts.Models.Towers.Weapons;
using Assets.Scripts.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api.Towers.Behaviors
{
    public class AttackBuilder
    {
        string name;
        WeaponModel[] weapons;
        float range;
        Model[] behaviors;
        TargetSupplierModel targetProvider;
        float offsetX;
        float offsetY;
        float offsetZ;
        bool attackThroughWalls;
        bool fireWithoutTarget;
        int framesBeforeRetarget;
        bool addsToSharedGrid;
        float sharedGridRange;

        public AttackBuilder() : this(null)
        {

        }
        public AttackBuilder(AttackModel baseModel)
        {
            string name = baseModel.name;
            WeaponModel[] weapons = baseModel.weapons;
            float range = baseModel.range;
            Model[] behaviors = baseModel.behaviors;
            TargetSupplierModel targetProvider = baseModel.targetProvider;
            float offsetX = baseModel.offsetX;
            float offsetY = baseModel.offsetY;
            float offsetZ = baseModel.offsetZ;
            bool attackThroughWalls = baseModel.attackThroughWalls;
            bool fireWithoutTarget = baseModel.fireWithoutTarget;
            int framesBeforeRetarget = baseModel.framesBeforeRetarget;
            bool addsToSharedGrid = baseModel.addsToSharedGrid;
            float sharedGridRange = baseModel.sharedGridRange;
        }
    }
}
