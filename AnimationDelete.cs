using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDelete : MonoBehaviour {

    public float delay = 0.5f;

    private void Start()
    {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }
}
