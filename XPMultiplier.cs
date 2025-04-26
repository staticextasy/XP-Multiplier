using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace XP_Multiplier
{
    [BepInPlugin("com.staticextasy.xpmultiplier", "XP Multiplier", "0.0.1")]
    public class XPMultiplier : BaseUnityPlugin
    {
        private Harmony _harmony;
        public static ConfigEntry<float> XPMultiplierV;

        private void Awake()
        {
            // Bind config
            XPMultiplierV = Config.Bind(
                "General",
                "XPMultiplier",
                1.5f, // Default 1.5x
                new ConfigDescription(
                    "Multiplier applied to earned XP before bonuses.",
                    new AcceptableValueRange<float>(0.1f, 10.0f)
                )
            );

            _harmony = new Harmony("com.staticextasy.xpmultiplier");
            _harmony.PatchAll();

            Logger.LogInfo($"XP Multiplier plugin loaded. Current multiplier: {XPMultiplierV.Value}");
        }

        private void OnDestroy()
        {
            _harmony.UnpatchSelf();
            Logger.LogInfo("XP Multiplier plugin unloaded.");
        }
    }

    [HarmonyPatch(typeof(Stats), nameof(Stats.EarnedXP))]
    public class Stats_EarnedXP_Patch
    {
        static void Prefix(ref int _incomingXP)
        {
            _incomingXP = (int)(_incomingXP * XPMultiplier.XPMultiplierV.Value);
        }
    }
}