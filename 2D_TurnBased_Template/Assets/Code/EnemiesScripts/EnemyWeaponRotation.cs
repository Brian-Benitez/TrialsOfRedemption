using UnityEngine;

public class EnemyWeaponRotation : MonoBehaviour
{
    private Vector3 playerpos;
    public bool IsAttacking = false;

    // Update is called once per frame
    void Update()
    {
        if (IsAttacking)
            return;
        else if(IsAttacking == false)
        {
            //This deals with rotating weapon below
            playerpos = PlayerController.Instance.MiddleOfPlayerForAimming.position;
            Vector3 rotation = playerpos - transform.position;
            float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotz);
        }

    }
}
