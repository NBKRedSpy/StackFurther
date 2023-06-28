using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
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

            //Target code:  
            //      //if (vector.magnitude <= 2f ...
            //      IL_0151: ldloca.s 6
            //      IL_0153: call instance float32[UnityEngine.CoreModule]UnityEngine.Vector3::get_magnitude()
            //      IL_0158: ldc.r4 2    <--- this is the distance variable.
            //      IL_015d: bgt.un.s IL_0199 <--- Harmony transpiler sees this as Bgt.un, not Bgt.un.s

            return new CodeMatcher(instructions)            
                .MatchForward(true,
                    new CodeMatch(x =>
                            x.opcode == OpCodes.Ldloca_S &&
                                x.operand is LocalBuilder localBuilder &&
                                localBuilder.LocalIndex == 6
                        ),
                    new CodeMatch(OpCodes.Call),
                    new CodeMatch(OpCodes.Ldc_R4, 2f),
                    new CodeMatch(OpCodes.Bgt_Un)
                )
                .ThrowIfNotMatch("distance not found")
                .Advance(-1)    //Move back to distance variable
                .SetInstruction(new CodeInstruction(OpCodes.Ldc_R4, Plugin.StackDistance.GetFloat()))
                .InstructionEnumeration();
        }
    }
}
