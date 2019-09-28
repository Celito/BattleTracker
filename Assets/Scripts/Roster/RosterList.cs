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

    private List<Detachment> _detachments = new List<Detachment>();

    public List<Unit> GetAllUnits()
    {
        List<Unit> units = new List<Unit>();
        foreach (var detachment in _detachments)
        {
            units.AddRange(detachment.GetAllUnits());
        }
        return units;
    }

    public void Load(XmlDocument listXmlRoot)
    {
        XmlNode rosterXml = listXmlRoot["roster"];
        _name = rosterXml.Attributes["name"].Value;

        Debug.LogFormat("RosterList: Loading roster \"{0}\"", _name);

        // Load cost limits
        XmlNodeList costLimitsXmlList = 
            rosterXml.SelectNodes("//*[local-name()='costLimits']/*[local-name()='costLimit']");

        for (int i = 0; i < costLimitsXmlList.Count; i++)
        {
            var costLimitXml = costLimitsXmlList.Item(i);
            switch (costLimitXml.Attributes["typeId"].Value)
            {
                case "points": // Points typeId
                    double.TryParse(costLimitXml.Attributes["value"].Value, out _pointsLimit);
                    break;
                case "e356-c769-5920-6e14": // PL typeId
                    double.TryParse(costLimitXml.Attributes["value"].Value, out _powerLevelLimit);
                    break;
                default:
                    Debug.LogWarningFormat("RosterList: Ignoring unknown type of cost limit " +
                        "\"{0}\"", costLimitXml.Attributes["typeId"].Value);
                    break;
            }
        }

        // Load forces
        XmlNodeList forcesListXml = 
            rosterXml.SelectNodes("//*[local-name()='forces']/*[local-name()='force']");

        Debug.LogFormat("RosterList: Number of force entries \"{0}\"", forcesListXml.Count);

        for (int i = 0; i < forcesListXml.Count; i++)
        {
            var forceXml = forcesListXml.Item(i);

            Detachment newDetachment = new Detachment();

            newDetachment.Load(forceXml);

            _detachments.Add(newDetachment);
        }
    }
}
