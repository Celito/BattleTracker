using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Detachment
{
    private string _name;

    private List<Unit> _units;

    public Detachment()
    {
        _units = new List<Unit>();
    }

    public void Load(XmlNode detachmentXml)
    {
        _name = detachmentXml.Attributes["name"]?.Value;

        Debug.LogFormat("Detachment: Loading detachment \"{0}\"", _name);

        XmlNodeList selectionsXml = 
            detachmentXml.SelectNodes("*[local-name()='selections']/*[local-name()='selection']");

        foreach(XmlNode selectionXml in selectionsXml)
        {
            var type = selectionXml.Attributes["type"]?.Value;
            switch(type)
            {
                case "model":
                    var model = new Model();
                    model.Load(selectionXml);
                    var modelUnit = new Unit(model.name + " Unit");
                    modelUnit.AddModel(model);
                    _units.Add(modelUnit);
                    break;
                case "unit":
                    var unit = new Unit();
                    unit.Load(selectionXml);
                    _units.Add(unit);
                    break;
                default:
                    Debug.LogWarningFormat("Detachment: Error trying to load an unknown type of " +
                        "selection in the detachment: [type: \"{0}\", name: \"{1}\"]", type,
                        selectionXml.Attributes["name"]?.Value);
                    break;
            }
        }
    }    
}
