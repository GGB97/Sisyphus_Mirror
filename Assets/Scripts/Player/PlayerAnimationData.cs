using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string dashParameterName = "Dash";

    public int IdleParameterHash { get; private set; }  
    public int WalkParameterHash { get; private set; }
    public int DashParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        DashParameterHash = Animator.StringToHash(dashParameterName);
    }
}
