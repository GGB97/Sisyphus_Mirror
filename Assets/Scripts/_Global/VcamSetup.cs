using Cinemachine;
using UnityEngine;

public class VcamSetup : MonoBehaviour
{
    CinemachineVirtualCamera _vcam;

    private void Awake()
    {
        _vcam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        _vcam.Follow = GameManager.Instance.Player.transform;
        _vcam.LookAt = GameManager.Instance.Player.transform;
    }
}
