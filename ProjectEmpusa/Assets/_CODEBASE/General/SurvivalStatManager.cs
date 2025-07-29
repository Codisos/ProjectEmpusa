using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public  class SurvivalStatManager : MonoBehaviour
{
    public static SurvivalStatManager instance;

    [SerializeField] float hungerTick;
    [SerializeField] int hungerPerTick;
    [SerializeField] float thirstTick;
    [SerializeField] int thirstPerTick;

    public event Action<int> OnHungerTick;
    public event Action<int> OnThirstTick;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(HungerTickCorutine());
        StartCoroutine(ThirstTickCorutine());
    }

    IEnumerator HungerTickCorutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(hungerTick);
            OnHungerTick?.Invoke(hungerPerTick);
        }
    }

    IEnumerator ThirstTickCorutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(thirstTick);
            OnThirstTick?.Invoke(thirstPerTick);
        }
    }
}
