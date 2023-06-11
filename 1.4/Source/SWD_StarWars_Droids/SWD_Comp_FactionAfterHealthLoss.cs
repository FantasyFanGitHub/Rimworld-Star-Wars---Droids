using Verse;
using RimWorld;
using System.Collections.Generic;
using Verse.AI.Group;

namespace SWD_StarWars_Droids
{
    public class SWD_Comp_FactionAfterHealthLoss : ThingComp
    {
        public int tickCounter = 0;
        bool SetFactionOnce = true;

        public SWD_CompProperties_FactionAfterHealthLoss Props
        {
            get
            {
                return (SWD_CompProperties_FactionAfterHealthLoss)this.props;
            }
        }

        public override void CompTick()
        {
            base.CompTick();
            tickCounter++;

            if (tickCounter > Props.tickInterval && SetFactionOnce)
            {
                Pawn pawn = this.parent as Pawn;
                if (pawn != null && pawn.Map != null && !pawn.Dead && !pawn.Downed)
                {
                    if (pawn.health.summaryHealth.SummaryHealthPercent < ((float)(Props.healthPercent) / 100))
                    {
                        var faction = Find.FactionManager.RandomEnemyFaction(allowNonHumanlike: false);
                        pawn.SetFaction(faction);

                        var map = pawn.Map;
                        LordMaker.MakeNewLord(faction, new LordJob_AssaultColony(faction, true, false, false, true, true, false, true), map, new List<Pawn> { pawn });

                        SetFactionOnce = false;
                    }
                }
                tickCounter = 0;
            }
        }
    }
}
