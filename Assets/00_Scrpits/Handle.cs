using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour
{
    public Animator anim;
    public Tool tool;
    private Item _item;

    private Dictionary<int, Item> tools = new Dictionary<int, Item>();
    private Dictionary<int, GameObject> ToolObject = new Dictionary<int, GameObject>();
    private List<KeyCode> keyTool = new List<KeyCode>() {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4};

    public static bool isSeed = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        ChangeTool();  
        if (tool != null)
        {
            tool.Handler();
        }
    }

    private void ChangeTool()
    {
        for (int i = 0; i < keyTool.Count; i++)
        {
            if (Input.GetKeyDown(keyTool[i]))
            {
                if (tool != null)
                {
                    GetTool(_item.Name, 0);
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
                tool = ToolObject[i].GetComponent<Tool>();
                _item = tools[i];
                GetTool(_item.Name, 1);
                InventoryManager.intant.SelectItem(i);
            }
        }
    }
    private void GetTool(string name , int active)
    {
        
        int idx = anim.GetLayerIndex(name);
        anim.SetLayerWeight(idx, active);
    }

}
