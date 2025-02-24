using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumScript : MonoBehaviour
{
    enum Dicrection { Itens, Equip, Status, Save, Load, Exit }
    void Start()
    {
        Dicrection myDicrection;
        myDicrection = Dicrection.Itens;
        Debug.Log(myDicrection);
    }

    Dicrection Reve (Dicrection dir)
    {
        return dir;
    }
}
