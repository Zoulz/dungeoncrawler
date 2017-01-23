using System;
using System.Collections.Generic;
using trailmarch.vo;

namespace trailmarch.model.api
{
    public interface IDefinitionModel
    {
        List<Mob> Mobs { get; set; }
        Dictionary<String, List<MapTile>> Tilesets { get; set; }

        Mob GetMobByName(String name);
        MapTile GetTileFromTileset(String tileset, int index);
    }
}
