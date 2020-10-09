using Assets.Scripts.Models.Towers;
using Assets.Scripts.Unity;
using Harmony;
using MelonLoader;
using NKHook6;
using NKHook6.Api.Events;
using NKHook6.Api.Events._Bloons;
using NKHook6.Api.Events._MainMenu;
using NKHook6.Api.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;

namespace AddTowers
{
    public class Entry : MelonMod
    {
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            EventRegistry.subscriber.register(typeof(Entry));
        }

        /*[EventAttribute("MainMenuLoadedEvent")]
        public static void onLoad(MainMenuEvents.LoadedEvent e)
        {
            Game game = Game.instance;

            CustomModel myModel = new CustomModel();
            Il2CppReferenceArray<TowerModel> towerList = game.model.towers;

            TowerModel[] modelArr = new TowerModel[towerList.Count+1];

            int x = 0;
            foreach(TowerModel model in towerList)
            {
                modelArr[x] = model;
                x++;
            }

            Il2CppReferenceArray<TowerModel> newList = new Il2CppReferenceArray<TowerModel>(modelArr);
            game.model.towers = newList;

            foreach (TowerModel model in game.model.towers)
            {
                Logger.Log(model.name);
            }
        }*/

        [EventAttribute("BloonCreatedEvent")]
        public static void onBloon(BloonEvents.CreatedEvent e)
        {
            Random r = new Random();
            bool x = r.Next(0, 2) == 1;
            e.bloon.setRegrow(true);
        }
    }
}
