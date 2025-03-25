using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    
    private static T _inttant;
    public static T intant => _inttant;

    protected  virtual void Start()
    {
        if(_inttant != null)
        {
            Destroy(this as T);
        }
        else
        {
            _inttant = this as T;   
        }
    }

}
