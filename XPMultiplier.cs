using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;

namespace XP_Multiplier
{
    [BepInPlugin("com.staticextasy.xpmultiplier", "XP Multiplier", "0.0.1")]
    public class XPMultiplier : BaseUnityPlugin
    {
        public void awake()
        {
            Logger.LogMessage("Test Load");
        }
    }
}
