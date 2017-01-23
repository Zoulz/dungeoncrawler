using System;
using trailmarch.consts;
using UnityEngine;

namespace trailmarch.utils
{
    public class StringUtil
    {
        public static ActorActionType ActionTypeFromString(String s)
        {
            switch (s.ToLower().Trim())
            {
                case "attack":
                    return ActorActionType.Attack;
                case "move":
                    return ActorActionType.Move;
                case "turn":
                    return ActorActionType.Turn;
                case "death":
                    return ActorActionType.Death;
                case "pain":
                    return ActorActionType.Pain;
                case "idle":
                    return ActorActionType.Idle;
                case "cast_spell":
                    return ActorActionType.CastSpell;
                case "block":
                    return ActorActionType.Block;
				case "spawn":
					return ActorActionType.Spawn;
            }

            return ActorActionType.Undefined;
        }

        public static MobAnimationType MobAnimationTypeFromString(String s)
        {
			switch (s.ToLower().Trim())
			{
				case "attack":
					return MobAnimationType.Attack;
				case "combatIdle":
					return MobAnimationType.CombatIdle;
				case "run":
					return MobAnimationType.Run;
				case "death":
					return MobAnimationType.Death;
				case "pain":
					return MobAnimationType.Pain;
				case "idle":
					return MobAnimationType.Idle;
				case "walk":
					return MobAnimationType.Walk;
				case "block":
					return MobAnimationType.Block;
			}

            return MobAnimationType.Undefined;
        }

        public static WrapMode WrapModeFromString(String s)
        {
            switch (s.ToLower().Trim())
            {
                case "loop":
                    return WrapMode.Loop;
                case "once":
                    return WrapMode.Once;
                case "pingpong":
                    return WrapMode.PingPong;
                case "clamp":
                    return WrapMode.Clamp;
            }

            return WrapMode.Default;
        }

        public static FacingDirection FacingFromString(String s)
        {
            switch (s.ToLower().Trim())
            {
                case "north":
                    return FacingDirection.North;
                case "south":
                    return FacingDirection.South;
                case "east":
                    return FacingDirection.East;
                case "west":
                    return FacingDirection.West;
            }

            return FacingDirection.None;
        }

        public static Vector2 Vector2FromString(String s)
        {
            String[] vecStr = s.Trim().Split(',');
            Vector2 vec = new Vector2(float.Parse(vecStr[0].Trim()), float.Parse(vecStr[1].Trim()));
            return vec;
        }

        public static Vector3 Vector3FromString(String s)
        {
            String[] vecStr = s.Trim().Split(',');
            Vector3 vec = new Vector3(float.Parse(vecStr[0].Trim()), float.Parse(vecStr[1].Trim()), float.Parse(vecStr[2].Trim()));
            return vec;
        }
    }
}
