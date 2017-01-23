using System;
using System.Collections.Generic;
using System.Linq;
using trailmarch.consts;
using Random = UnityEngine.Random;

namespace trailmarch.vo
{
    public class Mob
    {
        public String Name { get; set; }
        public String Prefab { get; set; }
        public Dictionary<MobAnimation, MobAnimationType> Animations { get; set; }
		public Dictionary<ActorActionType, ActorAction> Actions { get; set; }

        public MobAnimation GetRandomAnimationByType(MobAnimationType type)
        {
            IEnumerable<KeyValuePair<MobAnimation, MobAnimationType>> anims = Animations.Where(x => x.Value == type);
            List<MobAnimation> animsList = new List<MobAnimation>();

            foreach (KeyValuePair<MobAnimation, MobAnimationType> keyVal in anims)
            {
                animsList.Add(keyVal.Key);
            }

            return animsList[Random.Range(0, animsList.Count)];
        }
    }
}
