using System;
using System.Xml;
using UnityEngine;

namespace trailmarch.utils
{
    public class XmlUtil
    {
        public static XmlDocument GetXmlFromTextAsset(TextAsset asset)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(asset.text);
            return xml;
        }

        public static XmlDocument GetXmlFromTextAsset(String asset)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(asset);
            return xml;
        }
    }
}
