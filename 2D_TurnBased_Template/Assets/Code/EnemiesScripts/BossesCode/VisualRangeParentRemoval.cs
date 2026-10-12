using UnityEngine;

public class VisualRangeParentRemoval : MonoBehaviour
{
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.parent = null;
    }

   
}
