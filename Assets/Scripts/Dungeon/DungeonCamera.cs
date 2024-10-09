using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DungeonCamera : MonoBehaviour
{
    private CinemachineVirtualCamera cmVC; // https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineVirtualCamera.html

    private void Awake()
    {
        cmVC = GetComponent<CinemachineVirtualCamera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cmVC.Follow = LevelManager.Instance.SelectedPlayer.transform;
    }

    
}
