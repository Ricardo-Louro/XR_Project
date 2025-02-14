using UnityEngine;
using System.Collections.Generic;

public class EndGameHandler : MonoBehaviour
{
    private List<Enemy> enemyList = new List<Enemy>();
    [SerializeField] private GameObject endGame;

    public void AddToList(Enemy enemy)
    {
        enemyList.Add(enemy);
    }

    public void RemoveFromList(Enemy enemy)
    {
        enemyList.Remove(enemy);
    }

    public void CheckForEndGame()
    {
        if (enemyList.Count <= 0)
        {
            Invoke("EndGame", 2);
        }
    }

    public void EndGame()
    {
        endGame.SetActive(true);
    }
}