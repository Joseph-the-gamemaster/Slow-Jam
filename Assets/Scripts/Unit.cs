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

    // List of active status effects
    public List<StatusEffectZ> activeEffects = new List<StatusEffectZ>();

    public bool TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            return true; // Unit is dead
        }
        return false; // Unit is still alive
    }

    public void Heal(int healAmount)
    {
        currentHP += healAmount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    // Process effects matching the current phase (Start or End of turn)
    public List<string> ProcessStatusEffects(StatusEffectZ.EffectTriggerTime triggerPhase)
    {
        List<string> logs = new List<string>();

        // Loop backwards so we can safely remove expired effects
        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            StatusEffectz effect = activeEffects[i];

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

                // Reduce duration after triggering
                effect.duration--;

                if (effect.duration <= 0)
                {
                    logs.Add($"{unitName}'s {effect.name} wore off!");
                    activeEffects.RemoveAt(i);
                }
            }
        }

        return logs;
    }
}