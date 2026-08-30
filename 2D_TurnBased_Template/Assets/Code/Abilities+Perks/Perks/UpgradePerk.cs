using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePerk : MonoBehaviour
{
    public bool IsPerkPicked = false;
    public bool IsPerkActive = false;//turn this on in perkcardcontroller and put enable perk in event button to check if its on
 
    public virtual void EnablePerk()
    {
        //do magic
    }

    public virtual void DisablePerk()
    {
        //disable perk stuff here.
    }
}
