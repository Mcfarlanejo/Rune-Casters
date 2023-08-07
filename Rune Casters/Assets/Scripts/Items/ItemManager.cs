using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RuneCount
{
    public Rune rune;
    public int count;
}

public class ItemManager : MonoBehaviour
{
    #region Singleton
    public static ItemManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ItemManager>();
            }
            return _instance;
        }
    }
    static ItemManager _instance;

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public List<ItemObject> equipment =  new List<ItemObject>();
    public List<ItemObject> inventory = new List<ItemObject>();
    public List<RuneCount> runeBag = new List<RuneCount>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
