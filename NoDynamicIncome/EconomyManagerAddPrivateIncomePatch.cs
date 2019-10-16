using System;
using Harmony;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;
using UnityEngine;

namespace NoDynamicIncome
{
    [HarmonyPatch(typeof(EconomyManager))]
    [HarmonyPatch("AddPrivateIncome")]
    class EconomyManagerAddPrivateIncomePatch
    {
        // Maybe this works better, but let's try the transpiler first
        //[HarmonyPrefix]
        //static bool AddPrivateIncomePrefix (ref EconomyManager __instance, ref int __result,
        //    int amount, ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level, int taxRate)
        //{
        //    return false;
        //}

        public static IEnumerable<CodeInstruction> Transpiler(MethodBase original, IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            // Until I find a way around the hardcoding, this had to be done...
            codes[17].opcode = OpCodes.Nop; // originally: ldarg.0, a.k.a. this; yes, I use dotPeek

            codes[18].opcode = OpCodes.Ldc_I4;
            codes[18].operand = 10000; // originally: ldfld int32 EconomyManager::m_taxMultiplier; this was the original value for m_taxMultiplier

            //codes[21].operand = 1; // originally: ldc.i4 999999

            //codes[24].operand = 1; // originally: ldc.i4 1000000 (commented out because the income is rather ridiculous without it...)

            Debug.Log("[No Dynamic Income Scaling] Devious Income Calculator patched-ish");
            return codes.AsEnumerable();
        }
    }
}
