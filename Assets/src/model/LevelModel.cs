using System.Collections.Generic;
using System.Linq;
using trailmarch.model.api;
using trailmarch.actors;
using trailmarch.vo;
using UnityEngine;
using System;
using trailmarch.utils;

namespace trailmarch.model
{
    public class LevelModel : ILevelModel
    {
        public List<IActor> Actors { get; set; }
        public Dictionary<int, MapTile> MapTiles { get; set; }
        public int[,] MapData { get; set; }

        public LevelModel()
        {
            Actors = new List<IActor>();
            MapTiles = new Dictionary<int, MapTile>();
            MapData = new int[1, 1];
        }

        public IActor GetFirstActorOfType<T>()
        {
            return Actors.OfType<T>().First() as IActor;
        }

        public T[] GetActorsOfType<T>()
        {
            return Actors.OfType<T>().ToArray();
        }

        public Boolean IsTilePositionBlocked(Vector2 pos)
        {
            return MapTiles[MapData[(int)pos.x, (int)pos.y]].IsBlocking;
        }

        public Boolean IsActorBlockingPosition(Vector2 pos)
        {
            foreach (IActor actor in Actors)
            {
                //if (!(actor is PlayerActor))
                //{
                    if (actor.Position.Equals(pos))
                    {
                        return true;
                    }
                //}
            }

            return false;
        }

        public Boolean IsFacingActorOfType(IActor actor, Type actorType)
        {
            Vector2 dir = PositionUtil.GetDirectionVector(actor.Facing);
            IActor target = Actors.Find(x => x.Position.Equals(actor.Position + dir));

            return target != null && target.GetType() == actorType;
        }

        public IActor GetFacingActor(IActor actor)
        {
            Vector2 dir = PositionUtil.GetDirectionVector(actor.Facing);
            IActor target = Actors.Find(x => x.Position.Equals(actor.Position + dir));

            return target;
        }
    }
}
