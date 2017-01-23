using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using trailmarch.actors;

namespace trailmarch.vo
{
    public class AttackGroup
    {
        public IActor Attacker { get; set; }
        public IActor Defender { get; set; }

        public AttackGroup(IActor a, IActor d)
        {
            Attacker = a;
            Defender = d;
        }
    }
}
