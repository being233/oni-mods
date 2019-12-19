using Harmony;
using STRINGS;
using undancer.Commons;

namespace undancer.SelectLastCarePackage.patches
{
    [HarmonyPatch(typeof(ImmigrantScreen), "OnSpawn")]
    public static class ImmigrantScreenOnSpawnPatch
    {
        public static void Postfix(ImmigrantScreen __instance)
        {
            if (ModUtils.HasRefreshMod()) return;
            __instance.GetField<KButton>("rejectButton")
                .SetText(
                    Localization.GetLocale() != null &&
                    Localization.GetLocale().Lang == Localization.Language.Chinese ? 
                        Languages.REROLL :UI.IMMIGRANTSCREEN.SHUFFLE
                );
        }
    }
}