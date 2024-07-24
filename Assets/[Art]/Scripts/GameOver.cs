using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    public void Show()
    {
        canvas.enabled = true;
    }
    private void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RunWaitForXSeconds(float value)
    {
        IEnumerator coroutine = WaitForXSeconds(value);
        StartCoroutine(coroutine);
    }

    IEnumerator WaitForXSeconds(float value)
    {
        yield return new WaitForSeconds(value);

        restart();
    }
}
