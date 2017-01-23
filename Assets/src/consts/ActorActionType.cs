using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace trailmarch.consts
{
    public enum ActorActionType
    {
        Undefined,
        Move,
        Turn,
        Attack,
        Pain,
        Death,
        Idle,
        CastSpell,
        Block,
		Spawn
    }
}
