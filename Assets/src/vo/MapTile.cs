using System;
using trailmarch.consts;

namespace trailmarch.vo
{
    public class MapTile
    {
        public int Index { get; set; }
        public Boolean IsBlocking { get; set; }
        public String Prefab { get; set; }
        public FacingDirection Facing { get; set; }
    }
}
