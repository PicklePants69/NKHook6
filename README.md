# NKHook6
A Boo and C# modding API for BloonsTD6 using MelonLoader, made by TD Toolbox

## What is an API?
API stands for "Application Programming Interface". Basically, its a program you can use to modify programs that supports it. NKHook6 is an API for BTD6 and future support will be added for BTD Adventure Time.

## How does it work?
NKHook6 uses [MelonLoader](https://melonwiki.xyz/#/) to get the files from the game. Once we have the files we can make mods that "reference" or do things in relation to the game's code. Two of the ways you can do this are by using Reflection or [Harmony](https://harmony.pardeike.net/index.html). Since Reflection is more advanced, NKHook6 mainly relies on Harmony to make mods. This is done using the [HarmonyPatch](https://harmony.pardeike.net/articles/patching.html) system.

