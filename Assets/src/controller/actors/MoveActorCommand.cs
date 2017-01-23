using strange.extensions.command.impl;
using trailmarch.actors;
using trailmarch.consts;
using trailmarch.controller.player.signals;
using trailmarch.controller.signals;
using trailmarch.model.api;
using trailmarch.utils;
using UnityEngine;

namespace trailmarch.controller.actors
{
    public class MoveActorCommand : Command
    {
        [Inject]
        public MovementType movementType { get; set; }

        [Inject]
        public IActor targetActor { get; set; }

        [Inject]
        public ILevelModel levelModel { get; set; }

        [Inject]
        public UpdateActorViewSignal UpdateActorView { get; set; }

        [Inject]
        public EnableMovementSignal enableMovement { get; set; }

        public override void Execute()
        {
            switch (movementType)
            {
                case MovementType.TurnLeft:
                {
                    targetActor.Facing = PositionUtil.TurnLeft(targetActor.Facing);
                    UpdateActorView.Dispatch(targetActor, ActorActionType.Turn);
                    break;
                }
                case MovementType.TurnRight:
                {
                    targetActor.Facing = PositionUtil.TurnRight(targetActor.Facing);
                    UpdateActorView.Dispatch(targetActor, ActorActionType.Turn);
                    break;
                }
                case MovementType.MoveForward:
                {
                    Vector2 dir = PositionUtil.GetDirectionVector(targetActor.Facing);
                    Vector2 dest = targetActor.Position + dir;
                    if (!levelModel.IsTilePositionBlocked(dest) &&
                        !levelModel.IsActorBlockingPosition(dest))
                    {
                        targetActor.Position += dir;
                        UpdateActorView.Dispatch(targetActor, ActorActionType.Move);
                    }
                    else if (targetActor is PlayerActor)
                    {
                        enableMovement.Dispatch(true);
                    }
                    break;
                }
                case MovementType.MoveBackward:
                {
                    Vector2 dir = PositionUtil.GetDirectionVector(targetActor.Facing);
                    Vector2 dest = targetActor.Position - dir;
                    if (!levelModel.IsTilePositionBlocked(dest) &&
                        !levelModel.IsActorBlockingPosition(dest))
                    {
                        targetActor.Position -= dir;
                        UpdateActorView.Dispatch(targetActor, ActorActionType.Move);
                    }
                    else if (targetActor is PlayerActor)
                    {
                        enableMovement.Dispatch(true);
                    }
                    break;
                }
            }
        }
    }
}
