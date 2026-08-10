using UnityEngine;

// These enums must be outside the class
public enum EffectTriggerTime { StartOfTurn, EndOfTurn }
public enum StatusType { Poison, Regeneration, Stun, Burn }

[System.Serializable]
public class StatusEffectZ
{
    public string effectName; // Renamed from 'name' to avoid any conflict with UnityEngine.Object.name
    public StatusType type;
    public EffectTriggerTime triggerTime;
    public int duration;
    public int power;

    // The constructor name MUST match the class name exactly: StatusEffectZ
    public StatusEffectZ(string effectName, StatusType type, EffectTriggerTime triggerTime, int duration, int power)
    {
        this.effectName = effectName;
        this.type = type;
        this.triggerTime = triggerTime;
        this.duration = duration;
        this.power = power;
    }
}