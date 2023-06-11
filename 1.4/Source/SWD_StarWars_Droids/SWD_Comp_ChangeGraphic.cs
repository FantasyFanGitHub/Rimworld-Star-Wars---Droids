using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace SWD_StarWars_Droids
{
    public class SWD_Comp_ChangeGraphic : ThingComp
    {
        private PawnRenderer pawn_renderer;
        public int changeGraphicsCounter = 0;
        private bool hediffFound = false;

        public SWD_CompProperties_ChangeGraphic Props
        {
            get
            {
                return (SWD_CompProperties_ChangeGraphic) this.props;
            }
        }

        public override void CompTick()
        {
            changeGraphicsCounter++;
            if (changeGraphicsCounter > Props.changeGraphicsInterval)
            {
                this.ChangeTheGraphics();
                changeGraphicsCounter = 0;
            }
            base.CompTick();
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            Pawn pawn = this.parent as Pawn;
            this.pawn_renderer = pawn.Drawer.renderer;
            this.ChangeTheGraphics();
        }

        public void ChangeTheGraphics()
        {
            Pawn pawn = this.parent as Pawn;
            if (this.pawn_renderer == null)
            {
                this.pawn_renderer = pawn.Drawer.renderer;
            }

            Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(Props.hediffDef, false);
            if (hediff != null) {
                hediffFound = true;
            }
            else {
                hediffFound = false;
            }

            if (hediffFound) {
                LongEventHandler.ExecuteWhenFinished(delegate
                {
                    if (this.pawn_renderer != null) {
                        try
                        {
                            var data = new GraphicData();
                            data.shadowData = pawn.ageTracker.CurKindLifeStage.bodyGraphicData.shadowData;
                            Graphic_Multi nakedGraphic = (Graphic_Multi)GraphicDatabase.Get<Graphic_Multi>(Props.texPath, ShaderDatabase.CutoutComplex, Props.drawSize, Props.color, Props.colorTwo, data, maskPath: Props.maskPath);
                            this.pawn_renderer.graphics.ResolveAllGraphics();
                            this.pawn_renderer.graphics.nakedGraphic = nakedGraphic;
                        }
                        catch (NullReferenceException) { }
                    }
                });
            }
            else {
                LongEventHandler.ExecuteWhenFinished(delegate
                {
                    if (this.pawn_renderer != null)
                    {
                        try
                        {
                            Graphic nakedGraphic = (Graphic_Multi)GraphicDatabase.Get<Graphic_Multi>(pawn.ageTracker.CurKindLifeStage.bodyGraphicData.texPath, ShaderDatabase.CutoutComplex, Props.drawSize, Props.color);
                            this.pawn_renderer.graphics.ResolveAllGraphics();
                            this.pawn_renderer.graphics.nakedGraphic = nakedGraphic;
                            (this.pawn_renderer.graphics.nakedGraphic.data = new GraphicData()).shadowData = pawn.ageTracker.CurKindLifeStage.bodyGraphicData.shadowData;
                        }
                        catch (NullReferenceException) { }
                    }
                });
            }
        }
    }
}
