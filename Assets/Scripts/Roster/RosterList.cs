using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class RosterList
{
    private double _pointsLimit = -1;
    private double _powerLevelLimit = -1;

    private string _name;

    private List<Detachment> _detachments;

    internal void Load(XmlDocument listXmlRoot)
    {
        XmlNode rosterXml = listXmlRoot["roster"];
        _name = rosterXml.Attributes["name"].Value;

        Debug.LogFormat("RosterList: Loading roster \"{0}\"", _name);

        // Load cost limits
        XmlNode costLimitsXml = rosterXml["costLimits"];
        XmlNodeList costLimitsXmlList = costLimitsXml.SelectNodes("costLimit");

        Debug.LogFormat("RosterList: Number of cost limits children \"{0}\"", costLimitsXml.ChildNodes.Count);
        Debug.LogFormat("RosterList: Number of cost limits \"{0}\"", costLimitsXmlList.Count);

        for (int i = 0; i < costLimitsXmlList.Count; i++)
        {
            var costLimitXml = costLimitsXmlList.Item(i);
            switch (costLimitXml.Attributes["typeId"].Value)
            {
                case "points": // Points typeId
                    double.TryParse(costLimitsXml.Attributes["value"].Value, out _pointsLimit);
                    break;
                case "e356-c769-5920-6e14": // PL typeId
                    double.TryParse(costLimitsXml.Attributes["value"].Value, out _powerLevelLimit);
                    break;
                default:
                    Debug.LogWarningFormat("RosterList: Ignoring unknown type of cost limit " +
                        "\"{0}\"", costLimitsXml.Attributes["typeId"].Value);
                    break;

            }
        }

        // Load forces
        XmlNodeList forcesListXml = listXmlRoot.SelectNodes("//roster/forces");

        Debug.LogFormat("RosterList: Number of force entries \"{0}\"", forcesListXml.Count);

        for (int i = 0; i < forcesListXml.Count; i++)
        {
            var forceXml = forcesListXml.Item(i);

            Detachment newDetachment = new Detachment();

            newDetachment.Load(forceXml);
        }
    }
}
