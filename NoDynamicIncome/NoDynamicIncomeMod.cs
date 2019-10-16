using System;
using ICities;
using Harmony;
using System.Reflection;
using UnityEngine;

namespace NoDynamicIncome
{
    public class NoDynamicIncomeMod : IUserMod
    {
        HarmonyInstance harmonyInstance;

        public string Name
        {
            get { return "No Dynamic Income Scaling"; }
        }

        public string Description
        {
            get { return "Disables the unwanted income scaling that scales with the current cash amount."; }
        }

        public void OnEnabled()
        {
            Debug.Log("[No Dynamic Income Scaling] Devious Income Calculator being patched");
            harmonyInstance = HarmonyInstance.Create("d225.csl.nodynamicincomescaling");
            harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
        }

        public void OnDisabled()
        {
            if (harmonyInstance != null)
            {
                harmonyInstance.UnpatchAll();
                harmonyInstance = null;
            }
            Debug.Log("[No Dynamic Income Scaling] Devious Income Calculator unpatched");
        }
    }
}
