using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private float totalScore = 0;

    private void Start()
    {
        setupSingleton();
    }

    private void setupSingleton() {
        if (FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void resetScore() {
        totalScore = 0;
    }

    public void addScore(float score) {
        totalScore += score;
    }

    public float getScore() {
        return totalScore;
    }
}
