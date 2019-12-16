using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transformState : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform OriginalParent
    {
        get;
        set;
    }

    void Start()
    {
        this.OriginalParent = this.transform.parent;
    }
}
