using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int damage;

    public int maxHP;
    public int currentHP;

    public List<StatusEffectZ> activeEffects = new List<StatusEffectZ>();

    public bool TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            return true;
        }
        return false;
    }

    public void Heal(int healAmount)
    {
        currentHP += healAmount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    public List<string> ProcessStatusEffects(EffectTriggerTime triggerPhase)
    {
        List<string> logs = new List<string>();

        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            StatusEffectZ effect = activeEffects[i];

            if (effect.triggerTime == triggerPhase)
            {
                switch (effect.type)
                {
                    case StatusType.Poison:
                        TakeDamage(effect.power);
                        logs.Add($"{unitName} takes {effect.power} damage from Poison!");
                        break;

                    case StatusType.Regeneration:
                        Heal(effect.power);
                        logs.Add($"{unitName} heals {effect.power} HP from Regeneration!");
                        break;
                }

                effect.duration--;

                if (effect.duration <= 0)
                {
                    logs.Add($"{unitName}'s {effect.effectName} wore off!");
                    activeEffects.RemoveAt(i);
                }
            }
        }

        return logs;
    }
}