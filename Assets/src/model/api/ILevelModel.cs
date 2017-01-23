using System;
using System.Collections.Generic;
using trailmarch.actors;
using trailmarch.vo;
using UnityEngine;

namespace trailmarch.model.api
{
    public interface ILevelModel
    {
        List<IActor> Actors { get; set; }
        Dictionary<int, MapTile> MapTiles { get; set; }
        int[,] MapData { get; set; }

        IActor GetFirstActorOfType<T>();
        T[] GetActorsOfType<T>();
        IActor GetFacingActor(IActor actor);

        Boolean IsTilePositionBlocked(Vector2 pos);
        Boolean IsActorBlockingPosition(Vector2 pos);
        Boolean IsFacingActorOfType(IActor actor, Type actorType);
    }
}
