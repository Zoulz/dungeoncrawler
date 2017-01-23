using strange.extensions.mediation.impl;
using trailmarch.consts;
using trailmarch.controller.signals;
using trailmarch.model.api;
using trailmarch.actors;
using trailmarch.controller.actors.signals;

namespace trailmarch.view
{
    public class LevelMediator : Mediator
    {
        [Inject]
        public LevelView view { get; set; }

        [Inject]
        public ILevelModel levelModel { get; set; }

        [Inject]
        public SpawnActorSignal spawnActor { get; set; }

        [Inject]
        public RedrawMapSignal redrawMap { get; set; }

        [Inject]
        public UpdateActorViewSignal updateActorView { get; set; }

        [Inject]
        public MobAISignal mobAI { get; set; }

        public override void OnRemove()
        {
            view.MobCooldownComplete.RemoveListener(OnMobCooldownComplete);

            view.Dispose();

            spawnActor.RemoveListener(OnSpawnActor);
            updateActorView.RemoveListener(OnUpdateActor);
            redrawMap.RemoveListener(OnRedrawMap);
        }

        public override void OnRegister()
        {
            //  Init and build map.
            view.Init();
            view.BuildMap(levelModel.MapData);
            view.SpawnAllMobs(levelModel.GetActorsOfType<MobActor>());

            view.MobCooldownComplete.AddListener(OnMobCooldownComplete);

            spawnActor.AddListener(OnSpawnActor);
            updateActorView.AddListener(OnUpdateActor);
            redrawMap.AddListener(OnRedrawMap);
        }

        private void OnMobCooldownComplete(MobActor mob)
        {
            mobAI.Dispatch(mob);
        }

        private void OnUpdateActor(IActor actor, ActorActionType type)
        {
            if (!(actor is PlayerActor))
            {
                if (type == ActorActionType.Turn)
                {
                    view.UpdateMobFacing(actor as MobActor);
                }

                if (type == ActorActionType.Move)
                {
                    view.UpdateMobMovement(actor as MobActor);
                }

                if (type == ActorActionType.Attack)
                {
                    view.UpdateMobAttack(actor as MobActor);
                }
            }
        }

        private void OnSpawnActor(IActor actor)
        {
            view.SpawnMob(actor as MobActor);
        }

        private void OnRedrawMap()
        {
            view.BuildMap(levelModel.MapData);
        }
    }
}
