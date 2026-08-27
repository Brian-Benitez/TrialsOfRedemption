using Unity.Cinemachine;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager Instance;
    public float GlobalShakeForce = 0.25f;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public void ShakeCamera(CinemachineImpulseSource source)
    {
        source.GenerateImpulseWithForce(GlobalShakeForce);
    }
}
