using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckApplicationQuit : MonoBehaviour
{
    void OnApplicationQuit()
    {
        GBL_Interface.playerName = null;
    }
}
