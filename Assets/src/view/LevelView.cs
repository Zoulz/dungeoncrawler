using System.Collections.Generic;
using strange.extensions.signal.impl;
using trailmarch.model.api;
using trailmarch.actors;
using trailmarch.consts;
using trailmarch.utils;
using trailmarch.view.behaviours;
using UnityEngine;

namespace trailmarch.view
{
    public class LevelView : BaseView
    {
        [Inject]
        public IResourceManager resourceMgr { get; set; }

        [Inject]
        public ILevelModel LevelModel { get; set; }

        public Signal<MobActor> MobCooldownComplete { get { return _mobCooldownComplete; } }

        private GameObject[,] _tiles = null;
        private Dictionary<IActor, GameObject> _actors = null;
        private Signal<MobActor> _mobCooldownComplete = new Signal<MobActor>();

        public override void Init()
        {
            // TODO need to cull tiles that are not seen by the player.

            _actors = new Dictionary<IActor, GameObject>();
        }

        public override void Dispose()
        {
            DestroyMap();
            DestroyMobs();
        }

        public void Update()
        {
            foreach (KeyValuePair<IActor, GameObject> pair in _actors)
            {
                pair.Key.CurrentCooldownTime -= Time.deltaTime;
                if (pair.Key.CurrentCooldownTime <= 0f)
                {
                    _mobCooldownComplete.Dispatch(pair.Key as MobActor);
                }
            }
        }

        public void BuildMap(int[,] mapTiles)
        {
            DestroyMap();

            _tiles = new GameObject[mapTiles.GetLength(0), mapTiles.GetLength(1)];

            for (int x = 0; x < mapTiles.GetLength(0); x++)
            {
                for (int y = 0; y < mapTiles.GetLength(1); y++)
                {
                    GameObject tile = CreateGameObject(resourceMgr.GetResource("MapTile" + mapTiles[x, y].ToString()), new Vector3(x, 0, y), transform);
                    _tiles[x, y] = tile;
                }
            }
        }

        public void SpawnAllMobs(MobActor[] mobs)
        {
            foreach (MobActor mob in mobs)
            {
                SpawnMob(mob);
            }
        }

        public void SpawnMob(MobActor mob)
        {
            GameObject goActor = CreateGameObject(resourceMgr.GetResource(mob.Definition.Name), mob.Offset + new Vector3(mob.Position.x, 0f, mob.Position.y), transform);
            goActor.transform.rotation = Quaternion.Euler(new Vector3(0, PositionUtil.GetDirectionDegree(mob.Facing), 0));
	        goActor.AddComponent<MobBehavior>();

			_actors[mob] = goActor;

	        /*if (mob.Definition.Name == "cyclop")
	        {
				goActor.AddComponent<MobBehavior>();
		        MobBehavior behav = goActor.GetComponent<MobBehavior>();
		        behav.Mob = mob;
		        behav.Footstep.Add(resourceMgr.GetResource("giantFootstep1") as AudioClip);
				behav.Footstep.Add(resourceMgr.GetResource("giantFootstep2") as AudioClip);
				behav.Footstep.Add(resourceMgr.GetResource("giantFootstep3") as AudioClip);
				behav.Footstep.Add(resourceMgr.GetResource("giantFootstep4") as AudioClip);
	        }*/

            mob.CurrentCooldownTime = mob.Definition.Actions[ActorActionType.Spawn].Cooldown;

	        goActor.GetComponent<MobBehavior>().Mob = mob;
			goActor.GetComponent<MobBehavior>().Spawn(mob.CurrentCooldownTime);
        }

        public void UpdateMobMovement(MobActor mob)
        {
			_actors[mob].GetComponent<MobBehavior>()
				.Walk(new Vector3(mob.Position.x, 0, mob.Position.y), 2f);
        }

        public void UpdateMobFacing(MobActor mob)
        {
			_actors[mob].GetComponent<MobBehavior>()
				.Turn(new Vector3(0, PositionUtil.GetDirectionDegree(mob.Facing), 0), 2f);
        }

	    public void UpdateMobAttack(MobActor mob)
	    {
		    _actors[mob].GetComponent<MobBehavior>().PlayAnimation(MobAnimationType.Attack);
			_actors[mob].GetComponent<MobBehavior>().QueueAnimation(MobAnimationType.Idle);
	    }

        private void DestroyMap()
        {
            if (_tiles != null)
            {
                for (int x = 0; x < _tiles.GetLength(0); x++)
                {
                    for (int y = 0; y < _tiles.GetLength(1); y++)
                    {
                        Destroy(_tiles[x, y]);
                    }
                }

                _tiles = null;
            }
        }

        private void DestroyMobs()
        {
            if (_actors != null)
            {
                foreach (GameObject mob in _actors.Values)
                {
                    Destroy(mob);
                }

                _actors = null;
            }
        }
    }
}
