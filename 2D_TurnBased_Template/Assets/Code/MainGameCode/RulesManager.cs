using UnityEngine;

public class RulesManager : MonoBehaviour
{
    public GameObject RulesPrefab;

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftAlt))
            RulesPrefab.SetActive(true);
        else
            RulesPrefab.SetActive(false);
    }
}
