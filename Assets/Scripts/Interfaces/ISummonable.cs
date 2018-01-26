using UnityEngine;

public interface ISummonable
{
    bool CanSummon { get; set; }
    void SetDesignatedSummoner(GameObject summoner);
    float GetSummonCost();
    void OnSummonerInteraction();
}