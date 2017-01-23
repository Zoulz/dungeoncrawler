using System;
using System.Xml;
using strange.extensions.command.impl;
using trailmarch.consts;
using trailmarch.model.api;
using trailmarch.actors;
using trailmarch.controller.actors.signals;
using trailmarch.utils;
using trailmarch.vo;
using UnityEngine;

namespace trailmarch.controller.startup
{
    public class LoadLevelCommand : Command
    {
        [Inject]
        public IResourceManager resourceMgr { get; set; }

        [Inject]
        public ILevelModel levelModel { get; set; }

        [Inject]
        public IDefinitionModel defModel { get; set; }

        [Inject]
        public SpawnActorSignal spawnActor { get; set; }

        [Inject]
        public String mapName { get; set; }

        public override void Execute()
        {
            //  Load level
            resourceMgr.LoadResource(mapName, "meta/" + mapName);

            //  Parse level
            TextAsset levelText = resourceMgr.GetResource(mapName) as TextAsset;
            Parse(XmlUtil.GetXmlFromTextAsset(levelText));

            resourceMgr.UnloadResource(mapName);
        }

        private void Parse(XmlDocument level)
        {
            ParseMapRows(level.SelectNodes("level/map/rows"));
            ParseMapTiles(level.SelectNodes("level/map/tiles"));
            ParseActors(level.SelectNodes("level/actors"));
        }

        private void ParseActors(XmlNodeList xmlNodeList)
        {
            XmlNode actorsNode = xmlNodeList.Item(0);

            foreach (XmlNode node in actorsNode.SelectNodes("actor"))
            {
                IActor actor = (IActor) Activator.CreateInstance(Type.GetType(node.Attributes["type"].Value));
				actor.Position = StringUtil.Vector2FromString(node.Attributes["pos"].Value);
	            actor.Facing = StringUtil.FacingFromString(node.Attributes["dir"].Value);
                actor.Parse(node, resourceMgr, defModel);

                levelModel.Actors.Add(actor);
            }
        }

        private void ParseMapRows(XmlNodeList xmlNodeList)
        {
            XmlNode rowsNode = xmlNodeList.Item(0);
            int width = int.Parse(rowsNode.Attributes["width"].Value);
            int height = int.Parse(rowsNode.Attributes["height"].Value);
            int[,] map = new int[width, height];
            int y = 0;

            foreach (XmlNode row in rowsNode.SelectNodes("row"))
            {
                String[] s = row.InnerText.Split(',');
                for (int x = 0; x < width; x++)
                {
                    map[x, y] = int.Parse(s[x].Trim());
                }
                y++;
            }

            levelModel.MapData = map;
        }

        private void ParseMapTiles(XmlNodeList xmlNodeList)
        {
            XmlNode tilesNode = xmlNodeList.Item(0);

            foreach (XmlNode node in tilesNode.SelectNodes("tile"))
            {
                int mapIndex = int.Parse(node.Attributes["mapIndex"].Value);
                int setIndex = int.Parse(node.Attributes["setIndex"].Value);
                String setName = node.Attributes["tileset"].Value;

                MapTile tileDef = defModel.Tilesets[setName].Find(x => x.Index == setIndex);
                levelModel.MapTiles[mapIndex] = tileDef;

                resourceMgr.LoadResource("MapTile" + mapIndex.ToString(), tileDef.Prefab);
            }
        }
    }
}
