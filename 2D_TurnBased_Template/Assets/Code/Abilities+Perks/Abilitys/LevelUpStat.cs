using TMPro;
using UnityEngine;

public class LevelUpStat : MonoBehaviour
{
    public TextMeshProUGUI CostAmountText;
    public float CostAmount, PriceMultipler;//We still need to do the rest of the abilities. Only done health.
    private float MinCostAmount;
    public int StatsLvl;

    private void Start()
    {
        MinCostAmount = CostAmount;
        PriceMultipler = 1;
        StatsLvl = 0;
    }

    public void UpdateStatsUI()
    {
        CostAmount += CostAmount * PriceMultipler;
        StatsLvl++;
        CostAmountText.text = " " + CostAmount;
    }
    
    public virtual void UpgradeStat()
    {
        //upgrade it how you see fit.
    }

    public virtual void RestartStat()
    {
        StatsLvl = 0;
        PriceMultipler = 1;
        CostAmount = MinCostAmount;
        UpdateStatsUI();
    }
}
