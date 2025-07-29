using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hp;

    public void AddHp(int hp)
    {
        this.hp += hp;
    }

}
