using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Unit
{
    public string name
    {
        get;
        private set;
    }

    public string entryUnitId
    {
        get;
        private set;
    }

    private List<string> _categories = new List<string>();

    private List<Model> _models = new List<Model>();

    public Unit(string name = "")
    {
        this.name = name;
    }

    public void Load(XmlNode unitXml)
    {
        name = unitXml.Attributes["name"]?.Value;
        entryUnitId = unitXml.Attributes["entryId"]?.Value.Split(':')[0];

        XmlNodeList categoriesXml = 
            unitXml.SelectNodes("*[local-name()='categories']/*[local-name()='category']");

        foreach(XmlNode categoryXml in categoriesXml)
        {
            string name = categoryXml.Attributes["name"]?.Value;
            if(!string.IsNullOrEmpty(name))
            {
                _categories.Add(name);
            }
        }

        XmlNodeList unitSelectionsXml = 
            unitXml.SelectNodes("*[local-name()='selections']/*[local-name()='selection']");

        foreach(XmlNode selectionXml in unitSelectionsXml)
        {
            string type = selectionXml.Attributes["type"]?.Value;
            switch(type)
            {
                case "model":
                    int num = 1;
                    int.TryParse(selectionXml.Attributes["number"]?.Value, out num);
                    for (int i = 0; i < num; i++)
                    {
                        var model = new Model();
                        model.Load(selectionXml);
                        AddModel(model);
                    }
                    break;
                default:
                    Debug.LogWarningFormat("Unit: Error trying to load an unknown type of " +
                        "selection in the unit: [type: \"{0}\", name: \"{1}\"]", type,
                        selectionXml.Attributes["name"]?.Value);
                    break;
            }
        }

        Debug.LogFormat("Unit: Loading unit (name = \"{0}\", categories = [{1}])", name, 
            string.Join(", ", _categories.ToArray()));
    }

    public void AddModel(Model model)
    {
        _models.Add(model);
        model.SetUnit(this);
    }
}
