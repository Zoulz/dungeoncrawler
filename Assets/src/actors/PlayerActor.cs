using System.Xml;
using trailmarch.consts;
using trailmarch.model.api;
using UnityEngine;

namespace trailmarch.actors
{
    public class PlayerActor : IActor
    {
        public Vector2 Position { get; set; }
        public FacingDirection Facing { get; set; }
        public float CurrentCooldownTime { get; set; }

        public PlayerActor()
        {
            Position = Vector2.zero;
            Facing = FacingDirection.None;
            CurrentCooldownTime = 0f;
        }

        public void Parse(XmlNode actorXml, IResourceManager resourceMgr, IDefinitionModel defModel)
        {
            //  NO-OP
        }
    }
}
