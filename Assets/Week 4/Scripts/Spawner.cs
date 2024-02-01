using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    
    float timerValue = 0f;
    float timerTarget;

    // Start is called before the first frame update
    void Start()
    {
        timerTarget = Random.Range(1f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        timerValue += Time.deltaTime;

        if (timerValue > timerTarget)
        {
            Instantiate(prefab, transform);
            timerTarget = Random.Range(1f, 5f);
            timerValue = 0f;
        }

    }
}
