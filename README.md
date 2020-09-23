# NKHook6
A Boo and C# modding API for [BloonsTD6](https://btd6.com/) using MelonLoader, made by [TD Toolbox](https://github.com/TDToolbox)

## What is an API?
API stands for "Application Programming Interface". Basically, its a tool you can use to modify programs that supports it. NKHook6 is an API for BTD6 and future support will be added for BTD Adventure Time.

## How does it work?
NKHook6 uses [MelonLoader](https://melonwiki.xyz/#/) to get the files from the game. Once we have the files we can make mods that "reference" or do things in relation to the game's code. Two of the ways you can do this are by using Reflection or [Harmony](https://harmony.pardeike.net/index.html). Since Reflection is more advanced, NKHook6 mainly relies on Harmony to make mods. This is done using the [HarmonyPatch](https://harmony.pardeike.net/articles/patching.html) system. 


## How to get started?
Before getting started, you need to download the latest version of [.Net Framework](https://dotnet.microsoft.com/download/dotnet-framework). MelonLoader requires that you have .Net Framework 4.7.2 and above. Next you need to get MelonLoader. You can install it manually, or you can use our [Mod Manager](https://github.com/TDToolbox/BTD6-Mod-Manager) which does everything automatically. It's recommended that you use our Mod Manager as it installs MelonLoader and NKHook6 automatically, while also keeping them up to date. You can download the latest version here: https://github.com/TDToolbox/BTD6-Mod-Manager/releases/latest


## How do Harmonny Patches work?




Regardless of which method you'll use, to make mods you will need to create a mod project and decide whether to use C# for your mods or Boo.
