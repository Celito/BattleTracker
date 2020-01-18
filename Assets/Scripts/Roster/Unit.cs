using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Unit
{
    private string _name;

    private List<string> _categories = new List<string>();

    private List<Model> _models = new List<Model>();

    public Unit(string name = "")
    {
        _name = name;
    }

    public void Load(XmlNode unitXml)
    {
        _name = unitXml.Attributes["name"]?.Value;

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
    }

    public void AddModel(Model model)
    {
        _models.Add(model);
        model.SetUnit(this);
    }
}
