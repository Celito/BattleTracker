using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListsScreen : MonoBehaviour
{
    [SerializeField] private GameObject _listMold;
    [SerializeField] private GameObject _listsContainer;

    // Start is called before the first frame update
    void Start()
    {
        var lists = PlayerManager.instance.lists;

        foreach(var list in lists)
        {

        }
    }
}
