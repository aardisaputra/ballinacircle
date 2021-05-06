using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTweener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(gameObject.GetComponent<RectTransform>(), new Vector3(1.05f, 1.05f, 1), 0.5f).setLoopPingPong();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
