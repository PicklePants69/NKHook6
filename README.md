# NKHook6
A Boo and C# modding API for [BloonsTD6](https://btd6.com/) using MelonLoader, made by [TD Toolbox](https://github.com/TDToolbox)

## What is an API?
API stands for "Application Programming Interface". Basically, its a tool you can use to modify programs that supports it. NKHook6 is an API for BTD6 and future support will be added for BTD Adventure Time.

## How does it work?
NKHook6 uses [MelonLoader](https://melonwiki.xyz/#/) to get the files from the game. Once we have the files we can make mods that "reference" game files, or do things in relation to the game's code. Two of the ways you can do this are by using Reflection or [Harmony](https://harmony.pardeike.net/index.html). Harmony is more beginner friendly so we'll be mostly using that. This is done by using the [HarmonyPatch](https://harmony.pardeike.net/articles/patching.html) system. 


## How to get started?
Before getting started, you need to download the latest version of [.Net Framework](https://dotnet.microsoft.com/download/dotnet-framework). MelonLoader requires that you have .Net Framework 4.7.2 and above. Next you need to get MelonLoader. You can install it [manually](https://github.com/HerpDerpinstine/MelonLoader/releases/latest), or you can use our [Mod Manager](https://github.com/TDToolbox/BTD6-Mod-Manager) which does everything automatically. We recommend that you use our Mod Manager as it installs MelonLoader and NKHook6 automatically, while also keeping them up to date. You can download the latest version here: https://github.com/TDToolbox/BTD6-Mod-Manager/releases/latest

After doing the steps above you'll need to download [Visual Studio](https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&rel=16). We'll be using this program to make our mods. After it's finished installing you are ready to make your first mod.


## Making your first mod in C#
To make your first mod you'll need to start visual studio. Once it's opened click on "Create a new project" ![Create new proj image.png](https://cdn.discordapp.com/attachments/619054151967703061/759096827281932358/unknown.png)




## How do Harmonny Patches work?
