using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Tool : MonoBehaviour
{
    public bool isHandle;
    public Handle hand;
    public GameObject Grid;
    protected virtual void Start()
    {
        Grid = GameManager.intant.grid;
        hand = this.transform.parent.GetComponent<Handle>();
        getProp();
    }

    public abstract void Handler();

    public abstract void getProp();

}
