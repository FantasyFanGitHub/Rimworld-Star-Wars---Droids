using Verse;
using UnityEngine;

namespace SWD_StarWars_Droids
{
    public class SWD_CompProperties_ChangeGraphic : CompProperties
    {
        public SWD_CompProperties_ChangeGraphic()
        {
            this.compClass = typeof(SWD_Comp_ChangeGraphic);
        }

        public HediffDef hediffDef;
        public string texPath;
        public Vector2 drawSize = Vector2.one;
        public Color color = Color.white;
        public Color colorTwo = Color.white;
        public string maskPath = null;
        public int changeGraphicsInterval = 2000;

    }
}
