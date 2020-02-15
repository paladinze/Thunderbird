using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public void LoadMenu() {
        SceneManager.LoadScene(0);
    }

    public void LoadGame() {
        SceneManager.LoadScene("Level 0");
    }

    public void LoadGameOver() {
        StartCoroutine(WaitAndLoadGameOver());
    }

    private IEnumerator WaitAndLoadGameOver() {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
