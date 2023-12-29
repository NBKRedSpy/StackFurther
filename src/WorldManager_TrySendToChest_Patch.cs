using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using UnityEngine;

namespace StackFurther
{
    [HarmonyPatch(typeof(WorldManager), nameof(WorldManager.TrySendToChest))]
    public static class WorldManager_TrySendToChest_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {

            List<CodeInstruction> instructionList = new List<CodeInstruction>(instructions);

            //DebugInstruction("Start", instructionList);

            //// if (vector.magnitude <= 2f && vector.magnitude <= num)
            //IL_00c7: ldloca.s 6
            //IL_00c9: call instance float32[UnityEngine.CoreModule]UnityEngine.Vector3::get_magnitude()
            //IL_00ce: ldc.r4 2  <-- Target change.
            //IL_00d3: bgt.un.s IL_00ea

            var getMagnitudeGetter = AccessTools.PropertyGetter(typeof(UnityEngine.Vector3), nameof(Vector3.magnitude));

            var result = new CodeMatcher(instructionList)
                .MatchForward(true,
                    new CodeMatch(OpCodes.Call, getMagnitudeGetter),
                    new CodeMatch(OpCodes.Ldc_R4, 2f)
                )
                .ThrowIfNotMatch("distance not found")
                .SetOperandAndAdvance(Plugin.StackDistance.Value)
                .InstructionEnumeration();

            //DebugInstruction("Update", result);

            return result;
        }

        private static void DebugInstruction(string title, IEnumerable<CodeInstruction> result)
        {
            Debug.Log($"-------------- {title}");

            foreach (var instruction in result)
            {
                Debug.Log($"{instruction} {instruction.operand}");
            }
        }
    }
}
