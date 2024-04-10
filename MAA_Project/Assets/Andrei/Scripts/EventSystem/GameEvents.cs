using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    public static GameEvents gameEvents;

    private void Awake()
    {
        if (gameEvents != null && gameEvents != this)
        {
            Destroy(this);
        }
        else
        {
            gameEvents = this;
        }
    }
}
