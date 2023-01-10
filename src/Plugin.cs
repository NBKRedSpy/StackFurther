using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace StackFurther
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {


        public static ManualLogSource Log;

        public static ConfigEntry<float> StackDistance;

        private void Awake()
        {
            Log = Logger;

            //Config.Bind("General", "HighlightVillagers", true, "Highlight idle villagers.");
            StackDistance = Config.Bind("General", nameof(StackDistance), 4f, "The distance to search for a compatible card to stack onto.");

            HarmonyLib.Harmony harmony = new HarmonyLib.Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();

        }
    }
}