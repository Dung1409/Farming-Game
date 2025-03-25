using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Handle : MonoBehaviour
{
    public Animator anim;
    private Tool _tool;
    private Item _item;

    private Dictionary<int, Item> tools = new Dictionary<int, Item>();
    private Dictionary<int, GameObject> ToolObject = new Dictionary<int, GameObject>();
    private List<KeyCode> key = new List<KeyCode>() {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4};
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        for(int i = 0; i < key.Count; i++) 
        {
            if (Input.GetKeyDown(key[i]))
            {   
                if(_tool != null)
                {
                    ChangeTool(_item.Name , 0);
                }
                else
                {
                    anim.SetLayerWeight(0, 0);
                }

                if (!tools.ContainsKey(i))
                {
                    _item = InventoryManager.intant.Index[i];
                    tools[i] = _item;
                    ToolObject[i] = Instantiate(_item.tool, this.transform);
                }
                _tool = ToolObject[i].GetComponent<Tool>();
                _item = tools[i];
                ChangeTool(_item.Name, 1);
                InventoryManager.intant.SelectItem(i);
            }
        }
        if(_tool != null) 
        {
            _tool.Handler();
        }
    }

    private void ChangeTool(string name , int active)
    {
        
        int idx = anim.GetLayerIndex(name);
        anim.SetLayerWeight(idx, active);
    }
}
