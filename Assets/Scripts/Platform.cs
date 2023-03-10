using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //TODO let player controller know if firm or soft for physics
        //TODO break platform if Fragile
    }

    public enum PlatformState
    {
        FIRM,
        SOFT,
        FRAGILE
    }
}

