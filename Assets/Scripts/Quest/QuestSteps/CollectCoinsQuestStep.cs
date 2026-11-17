using System;
using UnityEngine;

public class CollectCoinsQuestStep : QuestStep
{
    private int coinsCollected = 0;
    private int coinsToCollect = 5;

    private void OnEnable()
    {
        
    }

    private void CoinCollected()
    {
        coinsCollected++;
        UpdateState();
        if (coinsCollected < coinsToCollect)
            return;
        
        FinishQuestStep();
    }

    private void UpdateState()
    {
        string newState = coinsCollected.ToString();
        ChangeState(newState);
    }

    private void OnDisable()
    {
        
    }
}
