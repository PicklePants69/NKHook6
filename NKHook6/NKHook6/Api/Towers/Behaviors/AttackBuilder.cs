using Assets.Scripts.Models;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Assets.Scripts.Models.Towers.Weapons;
using Assets.Scripts.Unity;
using NKHook6.Api.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;

namespace NKHook6.Api.Towers.Behaviors
{
    public class AttackBuilder
    {
        #region Fields
        public string name;
        public WeaponModel[] weapons;
        public float range;
        public WeaponBehaviorModel[] behaviors;
        public TargetSupplierModel targetProvider;
        public float offsetX;
        public float offsetY;
        public float offsetZ;
        public bool attackThroughWalls;
        public bool fireWithoutTarget;
        public int framesBeforeRetarget;
        public bool addsToSharedGrid;
        public float sharedGridRange;
        #endregion
        #region Constructor
        public AttackBuilder()
        {
            Model[] behaviorModels = Game.instance.getTowerModel("DartMonkey").behaviors;
            foreach(Model behavior in behaviorModels)
            {
                Logger.Log(behavior.name);
                if (behavior.name.StartsWith("AttackModel"))
                {
                    Initialize(new AttackModel(behavior.Clone().Pointer));
                    return;
                }
            }
        }
        public AttackBuilder(AttackModel baseModel)
        {
            Initialize(baseModel);
        }
        private void Initialize(AttackModel baseModel)
        {
            Logger.Log("Initializing builder with base " + baseModel.name);
            name = baseModel.name;
            weapons = baseModel.weapons;
            range = baseModel.range;
            behaviors = new Il2CppReferenceArray<WeaponBehaviorModel>(baseModel.behaviors.Pointer);
            targetProvider = baseModel.targetProvider;
            offsetX = baseModel.offsetX;
            offsetY = baseModel.offsetY;
            offsetZ = baseModel.offsetZ;
            attackThroughWalls = baseModel.attackThroughWalls;
            fireWithoutTarget = baseModel.fireWithoutTarget;
            framesBeforeRetarget = baseModel.framesBeforeRetarget;
            addsToSharedGrid = baseModel.addsToSharedGrid;
            sharedGridRange = baseModel.sharedGridRange;
        }
        #endregion
        #region Functions
        public AttackBuilder SetName(string name)
        {
            this.name = name;
            return this;
        }
        public AttackBuilder SetName(WeaponModel[] weapons)
        {
            this.weapons = weapons;
            return this;
        }
        public AttackBuilder SetRange(float range)
        {
            this.range = range;
            return this;
        }
        public AttackBuilder SetBehaviors(WeaponBehaviorModel[] behaviors)
        {
            this.behaviors = behaviors;
            return this;
        }
        public AttackBuilder SetTargetProvider(TargetSupplierModel targetProvider)
        {
            this.targetProvider = targetProvider;
            return this;
        }
        public AttackBuilder SetOffsetX(float offsetX)
        {
            this.offsetX = offsetX;
            return this;
        }
        public AttackBuilder SetOffsetY(float offsetY)
        {
            this.offsetY = offsetY;
            return this;
        }
        public AttackBuilder SetOffsetZ(float offsetZ)
        {
            this.offsetZ = offsetZ;
            return this;
        }
        public AttackBuilder SetAttackThroughWalls(bool attackThroughWalls)
        {
            this.attackThroughWalls = attackThroughWalls;
            return this;
        }
        public AttackBuilder SetFireWithoutTarget(bool fireWithoutTarget)
        {
            this.fireWithoutTarget = fireWithoutTarget;
            return this;
        }
        public AttackBuilder SetFramesBeforeRetarget(int framesBeforeRetarget)
        {
            this.framesBeforeRetarget = framesBeforeRetarget;
            return this;
        }
        public AttackBuilder SetAddsToSharedGrid(bool addsToSharedGrid)
        {
            this.addsToSharedGrid = addsToSharedGrid;
            return this;
        }
        public AttackBuilder SetSharedGridRange(float sharedGridRange)
        {
            this.sharedGridRange = sharedGridRange;
            return this;
        }
        #endregion
        #region Builder
        public AttackModel build()
        {
            return new AttackModel(
                name, 
                weapons, 
                range, 
                behaviors, 
                targetProvider, 
                offsetX, 
                offsetY, 
                offsetZ, 
                attackThroughWalls, 
                fireWithoutTarget, 
                framesBeforeRetarget, 
                addsToSharedGrid, 
                sharedGridRange);
        }
        #endregion
    }
}
