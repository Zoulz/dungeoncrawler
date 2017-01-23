using System;
using strange.extensions.mediation.impl;
using trailmarch.consts;
using trailmarch.model.api;
using trailmarch.actors;
using trailmarch.controller.actors.signals;
using trailmarch.controller.player.signals;

namespace trailmarch.view
{
    public class InputHandlerMediator : Mediator
    {
        [Inject]
        public InputHandlerView view { get; set; }

        [Inject]
        public ILevelModel levelModel { get; set; }

        [Inject]
        public MoveActorSignal moveActor { get; set; }

        [Inject]
        public EnableMovementSignal enableMovement { get; set; }

        private Boolean _movementEnabled = true;

        public override void OnRemove()
        {
            view.InputSignal.RemoveListener(OnInput);

            enableMovement.RemoveListener(OnEnableMovement);

            view.Dispose();
        }

        public override void OnRegister()
        {
            view.Init();

            view.InputSignal.AddListener(OnInput);

            enableMovement.AddListener(OnEnableMovement);
        }

        private void OnEnableMovement(Boolean enable)
        {
            _movementEnabled = enable;
        }

        private void OnInput(int inputType)
        {
            if (!_movementEnabled)
                return;

            _movementEnabled = false;

            IActor player = levelModel.GetFirstActorOfType<PlayerActor>();
            switch (inputType)
            {
                case 1:
                {
                    moveActor.Dispatch(MovementType.MoveForward, player);
                    break;
                }
                case 2:
                {
                    moveActor.Dispatch(MovementType.MoveBackward, player);
                    break;
                }
                case 3:
                {
                    moveActor.Dispatch(MovementType.TurnRight, player);
                    break;
                }
                case 4:
                {
                    moveActor.Dispatch(MovementType.TurnLeft, player);
                    break;
                }
            }
        }
    }
}
