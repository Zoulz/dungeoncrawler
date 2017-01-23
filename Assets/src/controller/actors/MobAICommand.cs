using strange.extensions.command.impl;
using trailmarch.actors;
using trailmarch.consts;
using trailmarch.controller.actors.signals;
using trailmarch.model.api;
using trailmarch.utils;
using trailmarch.vo;
using UnityEngine;

namespace trailmarch.controller.actors
{
    public class MobAICommand : Command
    {
        [Inject]
        public MobActor actor { get; set; }

        [Inject]
        public ILevelModel levelModel { get; set; }

        [Inject]
        public AttackSignal attack { get; set; }

        [Inject]
        public MoveActorSignal moveActor { get; set; }

        public override void Execute()
        {
            Vector2 dir = actor.Position + PositionUtil.GetDirectionVector(actor.Facing);
            if (!levelModel.IsTilePositionBlocked(dir) && 
                !levelModel.IsActorBlockingPosition(dir))
            {
                //  Try moving in the current direction if not blocked.
				actor.CurrentAction = ActorActionType.Move;
                moveActor.Dispatch(MovementType.MoveForward, actor);
            }
            else if (levelModel.IsFacingActorOfType(actor, typeof (PlayerActor)))
            {
                //  If facing the player, attack!
				actor.CurrentAction = ActorActionType.Attack;
                attack.Dispatch(new AttackGroup(actor, levelModel.GetFacingActor(actor)));
            }
            else
            {
                //  Otherwise, try turning.
				actor.CurrentAction = ActorActionType.Turn;
                if (Random.Range(1, 100) <= 50)
                {
                    moveActor.Dispatch(MovementType.TurnLeft, actor);
                }
                else
                {
                    moveActor.Dispatch(MovementType.TurnRight, actor);
                }
            }

			//	Set the new cooldown.
	        actor.CurrentCooldownTime = actor.Definition.Actions[actor.CurrentAction].Cooldown;
        }
    }
}
