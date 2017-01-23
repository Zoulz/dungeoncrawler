using System;
using System.Collections.Generic;
using System.Xml;
using strange.extensions.command.impl;
using trailmarch.consts;
using trailmarch.model.api;
using trailmarch.utils;
using trailmarch.vo;
using UnityEngine;

namespace trailmarch.controller.startup
{
    public class LoadMetaDataCommand : Command
    {
        [Inject]
        public IResourceManager resourceMgr { get; set; }

        [Inject]
        public IDefinitionModel defModel { get; set; }

        public override void Execute()
        {
            resourceMgr.LoadResource("mobs", "meta/mobs");
            resourceMgr.LoadResource("tiles", "meta/tiles");

            ParseMobs(XmlUtil.GetXmlFromTextAsset(resourceMgr.GetResource("mobs") as TextAsset));

            ParseTiles(XmlUtil.GetXmlFromTextAsset(resourceMgr.GetResource("tiles") as TextAsset));

            resourceMgr.UnloadResource("mobs");
            resourceMgr.UnloadResource("tiles");
        }

        private void ParseTiles(XmlDocument tilesXml)
        {
            XmlNodeList tilesets = tilesXml.SelectNodes("tiles/tileset");

            defModel.Tilesets = new Dictionary<string, List<MapTile>>();

            foreach (XmlNode tileset in tilesets)
            {
                defModel.Tilesets[tileset.Attributes["name"].Value] = ParseTileset(tileset);
            }
        }

        private List<MapTile> ParseTileset(XmlNode xmlNode)
        {
            List<MapTile> tiles = new List<MapTile>();

            foreach (XmlNode tile in xmlNode.SelectNodes("tile"))
            {
                tiles.Add(ParseMapTile(tile));
            }

            return tiles;
        }

        private MapTile ParseMapTile(XmlNode tile)
        {
            MapTile mt = new MapTile();

			if (tile.Attributes["index"] != null)
				mt.Index = int.Parse(tile.Attributes["index"].Value);

			if (tile.Attributes["prefab"] != null)
				mt.Prefab = tile.Attributes["prefab"].Value;

			if (tile["facing"] != null)
				mt.Facing = StringUtil.FacingFromString(tile["facing"].InnerText);

			if (tile["blocking"] != null)
				mt.IsBlocking = true;

            return mt;
        }

        private void ParseMobs(XmlDocument mobsXml)
        {
            XmlNodeList mobs = mobsXml.SelectNodes("mobs/mob");

            defModel.Mobs = new List<Mob>();

            foreach (XmlNode node in mobs)
            {
                Mob mob = new Mob();
                mob.Name = node.Attributes["name"].Value;
                mob.Prefab = node.Attributes["prefab"].Value;
                mob.Animations = ParseMobAnimations(node["animations"]);
	            mob.Actions = ParseMobActions(node["actions"]);
                defModel.Mobs.Add(mob);
            }
        }

		private Dictionary<ActorActionType, ActorAction> ParseMobActions(XmlElement xmlElement)
		{
			Dictionary<ActorActionType, ActorAction> ret = new Dictionary<ActorActionType, ActorAction>();

			foreach (XmlNode act in xmlElement.SelectNodes("action"))
			{
				ActorAction action = new ActorAction();

				if (act.Attributes["cooldown"] != null)
					action.Cooldown = float.Parse(act.Attributes["cooldown"].Value);

				ret[StringUtil.ActionTypeFromString(act.Attributes["type"].Value)] = action;
			}

			return ret;
		}

        private Dictionary<MobAnimation, MobAnimationType> ParseMobAnimations(XmlNode xmlNode)
        {
            Dictionary<MobAnimation, MobAnimationType> ret = new Dictionary<MobAnimation, MobAnimationType>();
            for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
            {
                XmlNode n = xmlNode.ChildNodes[i];
                MobAnimation mobAnim = new MobAnimation();

				if (n.Attributes["name"] != null)
					mobAnim.Name = n.Attributes["name"].Value;

				if (n.Attributes["wrap"] != null)
					mobAnim.Wrap = StringUtil.WrapModeFromString(n.Attributes["wrap"].Value);
					
                ret[mobAnim] = StringUtil.MobAnimationTypeFromString(n.Attributes["type"].Value);
            }

            return ret;
        }
    }
}
