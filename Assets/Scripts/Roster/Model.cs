using System.Xml;
using UnityEngine;

public class Model
{

    private Unit _parentUnit;

    public string name
    {
        private set;
        get;
    }

    public Model() { }
    
    public void Load(XmlNode modelXml)
    {
        name = modelXml.Attributes["name"]?.Value;
    }

    internal void SetUnit(Unit unit)
    {
        _parentUnit = unit;
    }
}
