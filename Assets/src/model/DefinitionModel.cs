using System;
using System.Collections.Generic;
using trailmarch.model.api;
using trailmarch.vo;

namespace trailmarch.model
{
    public class DefinitionModel : IDefinitionModel
    {
        public List<Mob> Mobs { get; set; }
        public Dictionary<String, List<MapTile>> Tilesets { get; set; }

        public Mob GetMobByName(string name)
        {
            return Mobs.Find(x => x.Name == name);
        }

        public MapTile GetTileFromTileset(String tileset, int index)
        {
            List<MapTile> tiles = Tilesets[tileset];

            if (tiles != null)
            {
                return tiles.Find(x => x.Index == index);
            }

            return null;
        }
    }
}
