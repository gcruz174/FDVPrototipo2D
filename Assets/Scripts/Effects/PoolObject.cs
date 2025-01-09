using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(menuName = "Custom/Pool Object", order = 0)]
public class PoolObject : ScriptableObject
{
    public int defaultSpawnCount = 5;
    public int makeExtraCount = 3;
    public GameObject poolObject;

    [NonSerialized]
    private readonly List<GameObject> _pool = new();

    public GameObject GetObject(bool active)
    {
        GameObject gameObject = null;
        foreach (var g in _pool)
        {
            if (g == null)
            {
                continue;
            }

            if (g.activeSelf) continue;
            gameObject = g;
            break;
        }

        if (ReferenceEquals(gameObject, null))
        {
            if (_pool.Count == 0)
            {
                Initialize(null);
                gameObject = _pool[0];
            }
            else
            {
                PopulateGroup(makeExtraCount);
                gameObject = _pool[^makeExtraCount];
            }
        }
        
        gameObject.SetActive(active);
        return gameObject;
    }

    public void ReturnObject(GameObject gameObject, bool returnToParent)
    {
        gameObject.SetActive(false);
    }

    public void Initialize(Transform parent)
    {
        Assert.IsNotNull(poolObject, $"{name} is missing its poolObject!");
        PopulateGroup(defaultSpawnCount);
    }

    private void PopulateGroup(int amount)
    {
        for (var i = 0; i < amount; i++)
        {
            var gameObject = Instantiate(poolObject);
            gameObject.name = gameObject.name + "_" + _pool.Count;
            gameObject.SetActive(false);
            _pool.Add(gameObject);
        }
    }

    public IEnumerator ReturnWithDelay(GameObject gameObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnObject(gameObject, true);
    }
}