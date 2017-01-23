using trailmarch.consts;
using UnityEngine;

namespace trailmarch.utils
{
    public class PositionUtil
    {
        public static Vector2 GetDirectionVector(FacingDirection facing)
        {
            switch (facing)
            {
                case FacingDirection.North:
                    return new Vector2(0, -1);

                case FacingDirection.South:
                    return new Vector2(0, 1);

                case FacingDirection.East:
                    return new Vector2(1, 0);

                case FacingDirection.West:
                    return new Vector2(-1, 0);
            }

            return Vector2.zero;
        }

        public static float GetDirectionDegree(FacingDirection facing)
        {
            switch (facing)
            {
                case FacingDirection.North:
                    return 180f;

                case FacingDirection.South:
                    return 0f;

                case FacingDirection.East:
                    return 90f;

                case FacingDirection.West:
                    return 270f;
            }

            return 0f;
        }

        public static FacingDirection TurnLeft(FacingDirection facing)
        {
            switch (facing)
            {
                case FacingDirection.North:
                {
                    return FacingDirection.West;
                }
                case FacingDirection.West:
                {
                    return FacingDirection.South;
                }
                case FacingDirection.South:
                {
                    return FacingDirection.East;
                }
                case FacingDirection.East:
                {
                    return FacingDirection.North;
                }
            }

            return FacingDirection.None;
        }

        public static FacingDirection TurnRight(FacingDirection facing)
        {
            switch (facing)
            {
                case FacingDirection.North:
                {
                    return FacingDirection.East;
                }
                case FacingDirection.West:
                {
                    return FacingDirection.North;
                }
                case FacingDirection.South:
                {
                    return FacingDirection.West;
                }
                case FacingDirection.East:
                {
                    return FacingDirection.South;
                }
            }

            return FacingDirection.None;
        }
    }
}
