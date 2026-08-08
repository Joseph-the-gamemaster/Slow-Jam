using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectZ : MonoBehaviour
{
    public enum EffectTriggerTime { StartOfTurn, EndOfTurn }
public enum StatusType { Poison, Regeneration, Stun }

[System.Serializable]
public class StatusEffectz
{
    public string name;
    public StatusType type;
    public EffectTriggerTime triggerTime;
    public int duration; // In turns
    public int power;    // Damage or healing amount per turn

    public StatusEffectz(string name, StatusType type, EffectTriggerTime triggerTime, int duration, int power)
    {
        this.name = name;
        this.type = type;
        this.triggerTime = triggerTime;
        this.duration = duration;
        this.power = power;
    }
}
}
