using HarmonyLib;

namespace StackFurther
{
    public class Plugin : Mod
    {
        public static ModLogger Log;

        public static ConfigEntry<float> StackDistance;

		public override void Ready()
		{
            Log = Logger;

            StackDistance = Config.GetEntry<float>("Stack Distance", 4f, new ConfigUI()
            {
				RestartAfterChange = true,
                Tooltip = "The distance to search for a compatible card to stack onto.  The game's default is 2",

			});

			Harmony.PatchAll();
        }

	}
}