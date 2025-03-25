using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    Dictionary<string , List<GameObject>> pool = new Dictionary<string , List<GameObject>>();
   
}
