using System.Xml;
using UnityEngine;

public class Detachment
{
    private string _name;

    public void Load(XmlNode detachmentXml)
    {
        _name = detachmentXml.Attributes["name"]?.Value;

        Debug.LogFormat("Detachment: Loading detachment \"{0}\"", _name);
        
    }    
}
