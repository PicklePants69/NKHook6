namespace NKHook6.Api.Gamemodes
{
    public class Gamemode
    {
        public bool IsLoaded;
        public Gamemode()
        {
            Loader.customGameModes.Add(this);
        }
    }
}
