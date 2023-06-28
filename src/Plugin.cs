using HarmonyLib;

namespace StackFurther
{
    public class Plugin : Mod
    {
        public static ModLogger Log;

        public static ConfigEntry StackDistance;

		public override void Ready()
		{
            Log = Logger;

            StackDistance = Config.GetEntry<float>("Stack Distance", 4f);
			Config.GetEntry<string>("Changes to settings requires a game exit and restart.");

			Harmony.PatchAll();
        }
    }
}