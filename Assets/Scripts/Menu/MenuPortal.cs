using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPortal : MonoBehaviour
{
    [SerializeField] private CanvasGroup fade;

    private IEnumerator IELoadDungeon()
    {
        fade.gameObject.SetActive(true);
        StartCoroutine(Helpers.IEFade(fade, 1f, 2f)); // time fade from 1->2s
        yield return new WaitForSeconds(2.5f); // load scene after 2.5s
        SceneManager.LoadScene("Dungeon");
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(IELoadDungeon());
        }
    }
}
