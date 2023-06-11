using RimWorld;
using Verse;

namespace SWD_StarWars_Droids
{
    public class SWD_ViperDroidPodLanding_Letter : IncidentWorker
    {
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            var map = (Map)parms.target;
            var pawn = PawnGenerator.GeneratePawn(SWD_DefOf.SWD_Viper_Probe_Droid_Enemy);

            var intVec = DropCellFinder.RandomDropSpot(map);
            Find.LetterStack.ReceiveLetter("ViperDroidPodLandingLabel".Translate(), "ViperDroidPodLanding".Translate(), LetterDefOf.NegativeEvent, new TargetInfo(intVec, map));
            var pod = new ActiveDropPodInfo()
            {
                openDelay = 180,
                leaveSlag = false
            };
            pod.innerContainer.TryAddOrTransfer(pawn, false);
            DropPodUtility.MakeDropPodAt(intVec, map, pod);

            return true;
        }
    }
}
