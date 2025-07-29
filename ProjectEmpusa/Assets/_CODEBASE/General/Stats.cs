using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Stats : MonoBehaviour
{
    [SerializeField] private int maxHunger;
    [SerializeField] private int maxThirst;
    [SerializeField] private int maxStamina;

    [Header("DEBUG")]
    public int initHunger;
    public int initThirst;
    public int initStamina;

    [ProgressBar("Hunger", 100, EColor.Green)] public int hunger;
    [ProgressBar("Thirst", 100, EColor.Blue)] public int thirst;
    [ProgressBar("Stamina", 100, EColor.Orange)] public int stamina;

    public int HUNGER => hunger;
    public int THIRST => thirst;
    public int STAMINA => stamina;

    public void SetHunger(int amount)
    {
        this.hunger += amount;
    }

    public void SetThirst(int amount)
    {
        this.thirst += amount;
    }

    public void SetStamina(int amount)
    {
        this.stamina += amount;
    }

    private void Awake()
    {
        SetHunger(initHunger);
        SetThirst(initThirst);
        SetStamina(initStamina);

        SurvivalStatManager.instance.OnHungerTick += EnviromentHunger;
        SurvivalStatManager.instance.OnThirstTick += EnviromentThirst;
    }

    private void EnviromentHunger(int hunger)
    {
        SetHunger(hunger);
    }

    private void EnviromentThirst(int thirst)
    {
        SetThirst(thirst);
    }

}
