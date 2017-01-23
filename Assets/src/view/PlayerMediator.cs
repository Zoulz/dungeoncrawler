using strange.extensions.mediation.impl;
using trailmarch.consts;
using trailmarch.controller.signals;
using trailmarch.actors;
using trailmarch.controller.player.signals;

namespace trailmarch.view
{
    public class PlayerMediator : Mediator
    {
        [Inject]
        public PlayerView view { get; set; }

        [Inject]
        public UpdateActorViewSignal updateActorView { get; set; }

        [Inject]
        public EnableMovementSignal enableMovement { get; set; }

        public override void OnRemove()
        {
            view.Complete.RemoveListener(OnActorUpdateComplete);
            view.Dispose();

            updateActorView.RemoveListener(OnUpdateActor);
        }

        public override void OnRegister()
        {
            view.Init();
            view.Complete.AddListener(OnActorUpdateComplete);

            updateActorView.AddListener(OnUpdateActor);
        }

        private void OnActorUpdateComplete()
        {
            enableMovement.Dispatch(true);
        }

        private void OnUpdateActor(IActor actor, ActorActionType type)
        {
            if (actor is PlayerActor)
            {
                switch (type)
                {
                    case ActorActionType.Move:
                    {
                        view.Move();
                        break;
                    }

                    case ActorActionType.Turn:
                    {
                        view.Turn();
                        break;
                    }
                }
            }
        }
    }
}
