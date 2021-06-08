using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ButtonWithCooldown : MonoBehaviour
{
    protected UI_RadialProgressBar radialProgressBar;

    public virtual void Awake()
    {
        radialProgressBar = GetComponentInParent<UI_RadialProgressBar>();
    }

}
