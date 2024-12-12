using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DungeonCamera : MonoBehaviour
{
    private CinemachineVirtualCamera cmVC; 
    private void Awake()
    {
        cmVC = GetComponent<CinemachineVirtualCamera>();
    }
    void Start()
    {
        cmVC.Follow = LevelManager.Instance.SelectedPlayer.transform;
    }

    
}
