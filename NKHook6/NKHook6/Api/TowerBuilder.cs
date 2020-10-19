
using Assets.Scripts.Models;
using Assets.Scripts.Models.Map;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Models.Towers.Mods;
using Assets.Scripts.Models.Towers.Upgrades;
using Assets.Scripts.Unity;
using Assets.Scripts.Utils;
using NKHook6.Api.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKHook6.Api
{
    public class TowerBuilder
    {
        #region Properties
        string name = "Monkey";
        string baseId = "Monkey";
        string towerSet = "Primary";
        string display = "Towers/Dart";
        float cost;
        float radius;
        float range;
        bool ignoreBlockers = false;
        bool isGlobalRange = false;
        int tier;
        int[] tiers;
        string[] appliedUpgrades;
        UpgradePathModel[] upgrades;
        Model[] behaviors;
        AreaType[] areaTypes;
        SpriteReference icon;
        SpriteReference portrait;
        SpriteReference instaIcon;
        ApplyModModel[] mods;
        bool ignoreTowerForSelection = false;
        bool isSubTower = false;
        bool isBakable = false;
        FootprintModel footprint;
        bool dontDisplayUpgrades = false;
        string powerName;
        float animationSpeed;
        SpriteReference emoteSpriteSmall;
        SpriteReference emoteSpriteLarge;
        bool doesntRotate = false;
        bool showPowerTowerBuffs = false;
        string towerSelectionMenuThemeId = "Default";
        bool ignoreCoopAreas = false;
        #endregion
        #region Constructor
        public TowerBuilder() : this(Game.instance.getTowerModel("DartMonkey")) { }
        public TowerBuilder(TowerModel baseModel)
        {
            this.name = baseModel.name;
            this.baseId = baseModel.baseId;
            this.towerSet = baseModel.towerSet;
            this.display = baseModel.display;
            this.cost = baseModel.cost;
            this.radius = baseModel.radius;
            this.range = baseModel.range;
            this.ignoreBlockers = baseModel.ignoreBlockers;
            this.isGlobalRange = baseModel.isGlobalRange;
            this.tier = baseModel.tier;
            this.tiers = baseModel.tiers;
            this.appliedUpgrades = baseModel.appliedUpgrades;
            this.upgrades = baseModel.upgrades;
            this.behaviors = baseModel.behaviors;
            this.areaTypes = baseModel.areaTypes;
            this.icon = baseModel.icon;
            this.portrait = baseModel.portrait;
            this.instaIcon = baseModel.instaIcon;
            this.mods = baseModel.mods;
            this.ignoreTowerForSelection = baseModel.ignoreTowerForSelection;
            this.isSubTower = baseModel.isSubTower;
            this.isBakable = baseModel.isBakable;
            this.footprint = baseModel.footprint;
            this.dontDisplayUpgrades = baseModel.dontDisplayUpgrades;
            this.powerName = baseModel.powerName;
            this.animationSpeed = baseModel.animationSpeed;
            this.emoteSpriteSmall = baseModel.emoteSpriteSmall;
            this.emoteSpriteLarge = baseModel.emoteSpriteLarge;
            this.doesntRotate = baseModel.doesntRotate;
            this.showPowerTowerBuffs = baseModel.showPowerTowerBuffs;
            this.towerSelectionMenuThemeId = baseModel.towerSelectionMenuThemeId;
            this.ignoreCoopAreas = baseModel.ignoreCoopAreas;
        }
        #endregion
        #region Functions
        public TowerBuilder SetName(string name)
        {
            this.name = name;
            return this;
        }
        public TowerBuilder SetBaseId(string baseId)
        {
            this.baseId = baseId;
            return this;
        }
        public TowerBuilder SetTowerSet(string towerSet)
        {
            this.towerSet = towerSet;
            return this;
        }
        public TowerBuilder SetDisplay(string display)
        {
            this.display = display;
            return this;
        }
        public TowerBuilder SetCost(float cost)
        {
            this.cost = cost;
            return this;
        }
        public TowerBuilder SetRadius(float radius)
        {
            this.radius = radius;
            return this;
        }
        public TowerBuilder SetRange(float range)
        {
            this.range = range;
            return this;
        }
        public TowerBuilder IgnoreBlockers(bool ignoreBlockers)
        {
            this.ignoreBlockers = ignoreBlockers;
            return this;
        }
        public TowerBuilder SetGlobalRange(bool isGlobalRange)
        {
            this.isGlobalRange = isGlobalRange;
            return this;
        }
        public TowerBuilder SetTier(int tier)
        {
            this.tier = tier;
            return this;
        }
        public TowerBuilder SetTiers(int[] tiers)
        {
            this.tiers = tiers;
            return this;
        }
        public TowerBuilder SetAppliedUpgrades(string[] appliedUpgrades)
        {
            this.appliedUpgrades = appliedUpgrades;
            return this;
        }
        public TowerBuilder SetUpgrades(UpgradePathModel[] upgrades)
        {
            this.upgrades = upgrades;
            return this;
        }
        public TowerBuilder SetBehaviors(Model[] behaviors)
        {
            this.behaviors = behaviors;
            return this;
        }
        public TowerBuilder SetAreaTypes(AreaType[] areaTypes)
        {
            this.areaTypes = areaTypes;
            return this;
        }
        public TowerBuilder SetIcon(SpriteReference icon)
        {
            this.icon = icon;
            return this;
        }
        public TowerBuilder SetPortrait(SpriteReference portrait)
        {
            this.portrait = portrait;
            return this;
        }
        public TowerBuilder SetInstaIcon(SpriteReference instaIcon)
        {
            this.instaIcon = instaIcon;
            return this;
        }
        public TowerBuilder SetMods(ApplyModModel[] mods)
        {
            this.mods = mods;
            return this;
        }
        public TowerBuilder SetIgnoreTowerForSelection(bool ignoreTowerForSelection)
        {
            this.ignoreTowerForSelection = ignoreTowerForSelection;
            return this;
        }
        public TowerBuilder SetIsSubTower(bool isSubTower)
        {
            this.isSubTower = isSubTower;
            return this;
        }
        public TowerBuilder SetIsBakable(bool isBakable)
        {
            this.isBakable = isBakable;
            return this;
        }
        public TowerBuilder SetFootprint(FootprintModel footprint)
        {
            this.footprint = footprint;
            return this;
        }
        public TowerBuilder SetDontDisplayUpgrades(bool dontDisplayUpgrades)
        {
            this.dontDisplayUpgrades = dontDisplayUpgrades;
            return this;
        }
        public TowerBuilder SetIsPowerTower(string powerName)
        {
            this.powerName = powerName;
            return this;
        }
        public TowerBuilder SetAnimationSpeed(float animationSpeed)
        {
            this.animationSpeed = animationSpeed;
            return this;
        }
        public TowerBuilder SetEmoteSpriteSmall(SpriteReference emoteSpriteSmall)
        {
            this.emoteSpriteSmall = emoteSpriteSmall;
            return this;
        }
        public TowerBuilder SetEmoteSpriteLarge(SpriteReference emoteSpriteLarge)
        {
            this.emoteSpriteLarge = emoteSpriteLarge;
            return this;
        }
        public TowerBuilder SetDoesntRotate(bool doesntRotate)
        {
            this.doesntRotate = doesntRotate;
            return this;
        }
        public TowerBuilder SetShowPowerTowerBuffs(bool showPowerTowerBuffs)
        {
            this.showPowerTowerBuffs = showPowerTowerBuffs;
            return this;
        }
        public TowerBuilder SetTowerSelectionMenuThemeId(string towerSelectionMenuThemeId)
        {
            this.towerSelectionMenuThemeId = towerSelectionMenuThemeId;
            return this;
        }
        public TowerBuilder SetIgnoreCoopAreas(bool ignoreCoopAreas)
        {
            this.ignoreCoopAreas = ignoreCoopAreas;
            return this;
        }
        #endregion
        #region Builder
        public TowerModel build()
        {
            return new TowerModel(
                this.name,
                this.baseId,
                this.towerSet,
                this.display,
                this.cost,
                this.radius,
                this.range,
                this.ignoreBlockers,
                this.isGlobalRange,
                this.tier,
                this.tiers,
                this.appliedUpgrades,
                this.upgrades,
                this.behaviors,
                this.areaTypes,
                this.icon,
                this.portrait,
                this.instaIcon,
                this.mods,
                this.ignoreTowerForSelection,
                this.isSubTower,
                this.isBakable,
                this.footprint,
                this.dontDisplayUpgrades,
                this.powerName,
                this.animationSpeed,
                this.emoteSpriteSmall,
                this.emoteSpriteLarge,
                this.doesntRotate,
                this.showPowerTowerBuffs,
                this.towerSelectionMenuThemeId,
                this.ignoreCoopAreas
            );
        }
        #endregion
    }
}