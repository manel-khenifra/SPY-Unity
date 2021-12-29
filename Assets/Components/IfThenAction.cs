using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfThenAction : BaseElement
{
    // Advice: FYFY component aims to contain only public members (according to Entity-Component-System paradigm).
    public int ifEntityType; // 0: Wall, 1: Door, 2: Enemy, 3: Ally, 4: Console, 5: Coin
    public bool ifNot;
    public int range;
    public int ifDirection; // 0: Forward, 1: Backward, 2: Left, 3: Right
    public GameObject firstChild;

    public int ifEntityType2; // 0: Wall, 1: Door, 2: Enemy, 3: Ally, 4: Console, 5: Coin
    public bool ifNot2;
    public int range2;
    public int ifDirection2; // 0: Forward, 1: Backward, 2: Left, 3: Right
    public GameObject firstChild2;
}
