using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public int LayoutIndex;
    private int MaxLayoutIndex = 4;
    public List<GameObject> NPCLayouts;
   
    public enum NpcLayout
    {
        StarterNpcLayout,
        FirstLayout,
        SecondLayout,
        ThirdLayout
    }
    public NpcLayout CurrentNpcLayout = NpcLayout.StarterNpcLayout;

    public void IncrementLayoutIndex()
    {
        if(LayoutIndex < MaxLayoutIndex)
        {
            ChangeLayout();
            LayoutIndex++;
        }      
    }
    void ChangeLayout()
    {
        if(LayoutIndex == 1)
            CurrentNpcLayout = NpcLayout.FirstLayout;
        if (LayoutIndex == 2)
            CurrentNpcLayout = NpcLayout.SecondLayout;
        if (LayoutIndex == 3)
            CurrentNpcLayout = NpcLayout.ThirdLayout;

        SetNPCsOnMap();
    }

    void SetNPCsOnMap()
    {
        RestartAllLayouts();
        if (CurrentNpcLayout != NpcLayout.StarterNpcLayout)
            NPCLayouts[0].gameObject.SetActive(false);

        if(LayoutIndex == 1)
            NPCLayouts[LayoutIndex].gameObject.SetActive(true);
        if(LayoutIndex == 2)
            NPCLayouts[LayoutIndex].gameObject.SetActive(true);
        if(LayoutIndex == 3)
            NPCLayouts[LayoutIndex].gameObject.SetActive(true);
    }

    void RestartAllLayouts() => NPCLayouts.ForEach(x =>  x.gameObject.SetActive(false)); 
}
