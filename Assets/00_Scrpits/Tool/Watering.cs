using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Watering : Tool
{
    [SerializeField] private float maxVolume;
    [SerializeField] private float remainingWater;
    [SerializeField] private float maxTime = 1f;
    private Tilemap waterMap;

    protected override void Start()
    {
        base.Start();

    }
    public override void getProp()
    {
        remainingWater = maxVolume / 2;
        waterMap = Grid.transform.GetChild(5).GetComponent<Tilemap>();
    }

    public override void Handler()
    {
        if(Input.GetMouseButtonDown(0))
        {
            hand.anim.SetInteger(Contant.State, 1);
            watering();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            hand.anim.SetInteger(Contant.State, 0);
        }

        if(Input.GetMouseButton(1))
        {
            getWater(maxTime * (maxVolume - remainingWater) / maxVolume);
        }

    }

    private void getWater(float time)
    {
        while(remainingWater < maxVolume)
        {
            remainingWater += maxVolume * (Time.deltaTime / time); 
            if(remainingWater > maxTime) 
            {
                remainingWater = maxVolume;
            }
        }
    }

    private void watering()
    {

    }

}
