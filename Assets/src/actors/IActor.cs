using System.Xml;
using trailmarch.consts;
using trailmarch.model.api;
using UnityEngine;

namespace trailmarch.actors
{
    public interface IActor
    {
        Vector2 Position { get; set; }
        FacingDirection Facing { get; set; }
        float CurrentCooldownTime { get; set; }

        void Parse(XmlNode actorXml, IResourceManager resourceMgr, IDefinitionModel defModel);
    }
}
