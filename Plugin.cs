using BepInEx;
using GorillaLocomotion;
using UnityEngine;

namespace MiniInModded
{
    [BepInPlugin("com.kino.gorillatag.miniinmodded", "MiniInModded", "1.0.2")]
    public class Plugin : BaseUnityPlugin
    {
        private void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }

        private void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
        }

        private void Update()
        {
            if (NetworkSystem.Instance.InRoom && NetworkSystem.Instance.GameModeString.Contains("MODDED"))
            {
                GorillaLocomotion.Player.Instance.scale = 0.2f;
            }
        }
    }
}
