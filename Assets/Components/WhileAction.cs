using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileAction : BaseElement
{
    // Advice: FYFY component aims to contain only public members (according to Entity-Component-System paradigm).
    public int whileEntityType; // 0: Wall, 1: Door, 2: Enemy, 3: Ally, 4: Console, 5: Coin
    public bool whileNot;
    public int range;
    public int whileDirection; // 0: Forward, 1: Backward, 2: Left, 3: Right
    public GameObject firstChild;
}
