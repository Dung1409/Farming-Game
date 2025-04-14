using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Tool : MonoBehaviour
{
    public bool isHandle;
    public Handle hand;
    protected virtual void Start()
    {
        
        hand = this.transform.parent.GetComponent<Handle>();
        getProp();
    }
        
    public abstract void Handler();

    public abstract void getProp();

}
