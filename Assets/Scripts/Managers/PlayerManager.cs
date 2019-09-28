using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    public string testList = "TestList1.roz";

    public static PlayerManager instance
    {
        get;
        private set;
    }

    public List<RosterList> lists
    {
        get;
        private set;
    }

    private void Awake()
    {
        lists = new List<RosterList>();

        instance = this;

        // TODO: Load the test list

        
        XmlDocument listXmlRoot = new XmlDocument();

        try
        {
            listXmlRoot.Load(testList);
        }
        catch(FileNotFoundException)
        {
            Debug.LogErrorFormat("Error trying to load the test roster list file \"{0}\"", 
                testList);
        }

        RosterList newList = new RosterList();

        newList.Load(listXmlRoot);

        lists.Add(newList);
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    
}
