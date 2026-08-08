using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public TextMeshProUGUI dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState State;

    void Start()
    {
        State = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();
        playerUnit.currentHP = playerUnit.maxHP;

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();
        enemyUnit.currentHP = enemyUnit.maxHP;

        // --- DEMO STATUS EFFECTS ---
        // Poison: Triggers at START of turn (5 damage, 3 turns)
        playerUnit.activeEffects.Add(new StatusEffectZ.StatusEffectz("Poison", StatusEffectZ.StatusType.Poison, StatusEffectZ.EffectTriggerTime.StartOfTurn, 3, 5));
        // Regen: Triggers at END of turn (3 heal, 2 turns)
        playerUnit.activeEffects.Add(new StatusEffectZ.StatusEffectz("Regen", StatusEffectZ.StatusType.Regeneration, StatusEffectZ.EffectTriggerTime.EndOfTurn, 2, 3));

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        State = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurnStart());
    }

    // --- START OF PLAYER TURN ---
    IEnumerator PlayerTurnStart()
    {
        // 1. Trigger Start-of-Turn Effects (e.g. Poison)
        List<string> effectLogs = playerUnit.ProcessStatusEffects(EffectTriggerTime.StartOfTurn);
        
        foreach (string log in effectLogs)
        {
            dialogueText.text = log;
            playerHUD.SetHP(playerUnit.currentHP);
            yield return new WaitForSeconds(1.5f);
        }

        // 2. Check if unit died from start-of-turn damage
        if (playerUnit.currentHP <= 0)
        {
            State = BattleState.LOST;
            EndBattle();
            yield break;
        }

        // 3. Prompt player for action
        dialogueText.text = "Choose an action:";
    }

    // --- END OF PLAYER TURN ---
    IEnumerator PlayerTurnEnd()
    {
        // 1. Trigger End-of-Turn Effects (e.g. Regeneration)
        List<string> effectLogs = playerUnit.ProcessStatusEffects(EffectTriggerTime.EndOfTurn);

        foreach (string log in effectLogs)
        {
            dialogueText.text = log;
            playerHUD.SetHP(playerUnit.currentHP);
            yield return new WaitForSeconds(1.5f);
        }

        // 2. Transition to Enemy Turn
        State = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            State = BattleState.WON;
            EndBattle();
        }
        else
        {
            // Instead of going straight to EnemyTurn, process turn-end effects first
            StartCoroutine(PlayerTurnEnd());
        }
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "You healed for 5 HP!";
        yield return new WaitForSeconds(2f);

        // Process turn-end effects before ending player's turn
        StartCoroutine(PlayerTurnEnd());
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = enemyUnit.unitName + " dealt " + enemyUnit.damage + " damage!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            State = BattleState.LOST;
            EndBattle();
        }
        else
        {
            State = BattleState.PLAYERTURN;
            StartCoroutine(PlayerTurnStart());
        }
    }

    void EndBattle()
    {
        if (State == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        }
        else if (State == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }
    }

    public void onAttackButton()
    {
        if (State != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void onHealButton()
    {
        if (State != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }
}