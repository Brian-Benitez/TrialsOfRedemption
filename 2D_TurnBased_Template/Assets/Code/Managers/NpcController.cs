using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public int LayoutIndex;
    public List<GameObject> NPCLayouts;
    public enum NpcLayout
    {
        None,
        FirstLayout,
        SecondLayout,
        ThirdLayout
    }
    public NpcLayout CurrentNpcLayout;

    public void IncrementLayoutIndex()
    {
        LayoutIndex++;
        ChangeLayout();
    }
    void ChangeLayout()
    {
        if(LayoutIndex == 0)
            CurrentNpcLayout = NpcLayout.FirstLayout;
        if (LayoutIndex == 1)
            CurrentNpcLayout = NpcLayout.SecondLayout;
        if (LayoutIndex == 2)
            CurrentNpcLayout = NpcLayout.ThirdLayout;

        SetNPCsOnMap();
    }

    void SetNPCsOnMap()
    {
        RestartAllLayouts();

        if(LayoutIndex == 0)
            NPCLayouts[LayoutIndex].gameObject.SetActive(true);
        if(LayoutIndex == 1)
            NPCLayouts[LayoutIndex].gameObject.SetActive(true);
        if(LayoutIndex == 2)
            NPCLayouts[LayoutIndex].gameObject.SetActive(true);
    }

    void RestartAllLayouts() => NPCLayouts.ForEach(x =>  x.gameObject.SetActive(false)); 
}
