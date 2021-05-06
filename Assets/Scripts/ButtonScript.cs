using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject circleTransition;
    public float animTime;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine(animTime));
        FindObjectOfType<AudioManager>().Play("Swoosh");
    }

    IEnumerator StartGameCoroutine(float delay)
    {
        LeanTween.moveY(circleTransition.GetComponent<RectTransform>(), 0, animTime);
        LeanTween.scale(circleTransition.GetComponent<RectTransform>(), new Vector3(15, 15, 15), animTime);
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(1);
    }
}
