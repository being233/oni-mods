﻿using System;
using Harmony;

namespace RandomOverjoyed
{
    [HarmonyPatch(typeof(JoyBehaviourMonitor),"ShouldBeOverjoyed")]
    public class JoyBehaviourMonitorPatch
    {

        public bool Prefix(ref bool __result)
        {
            __result = (double) UnityEngine.Random.Range(0.0f, 100f) <= 50;
            return false;
        }
        
    }
}