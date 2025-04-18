using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    Dictionary<GameObject , List<GameObject>> pool = new Dictionary<GameObject , List<GameObject>>();
   
    public GameObject CreateGameObject(GameObject g)
    {
        if (!pool.ContainsKey(g)) 
        {
            pool[g] = new List<GameObject>();
        }

        foreach(GameObject i in pool[g])
        {
            if (i.activeSelf)
            {
                continue;
            }
            
            i.transform.gameObject.SetActive(true);
            return i;
        }

        GameObject gameObject = Instantiate(g);
        gameObject.name = g.name;
        pool[g].Add(gameObject);
        return gameObject;
    }
}
