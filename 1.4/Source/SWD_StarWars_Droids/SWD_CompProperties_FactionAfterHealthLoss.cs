using Verse;
using UnityEngine;

namespace SWD_StarWars_Droids
{
    public class SWD_CompProperties_FactionAfterHealthLoss : CompProperties
    {
        public int healthPercent = 50;
        public int tickInterval = 1000;

        public SWD_CompProperties_FactionAfterHealthLoss()
        {
            this.compClass = typeof(SWD_Comp_FactionAfterHealthLoss);
        }
    }
}
