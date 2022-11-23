using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using UnityEngine;

namespace StackFurther
{
    [HarmonyPatch(typeof(WorldManager), nameof(WorldManager.StackSend))]
    public static class WorldManager_StackSend_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {

            return new CodeMatcher(instructions)
                .MatchForward(false,
                    new CodeMatch(OpCodes.Ldc_R4, 2f))
                .ThrowIfNotMatch("distance not found")
                .SetInstruction(new CodeInstruction(OpCodes.Ldc_R4, Plugin.StackDistance.Value))
                .InstructionEnumeration();

        }
    }
}
