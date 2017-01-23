using System;
using System.Xml;
using trailmarch.consts;
using trailmarch.model.api;
using trailmarch.vo;
using UnityEngine;

namespace trailmarch.actors
{
    public class MobActor : IActor
    {
        public Vector2 Position { get; set; }
        public FacingDirection Facing { get; set; }
        public float CurrentCooldownTime { get; set; }
        public Mob Definition { get; set; }
		public ActorActionType CurrentAction { get; set; }
        public Vector3 Offset { get; set; }

        public MobActor()
        {
			CurrentAction = ActorActionType.Undefined;
            CurrentCooldownTime = 0f;
            Definition = null;
            Facing = FacingDirection.None;
            Position = Vector2.zero;
        }

        public void Parse(XmlNode actorXml, IResourceManager resourceMgr, IDefinitionModel defModel)
        {
            String name = actorXml["name"].InnerText;

            Definition = defModel.GetMobByName(name);
            Offset = new Vector3(0.5f, 0.1f, 0.5f);

            resourceMgr.LoadResource(Definition.Name, Definition.Prefab);
        }
    }
}
