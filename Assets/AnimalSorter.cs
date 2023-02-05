using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSorter : MonoBehaviour
{
    float eTime = 0f;

    private void Update()
    {
        eTime += Time.deltaTime;
        if (eTime > 0.5f)
        {
            Debug.Log("!!!");
            eTime -= 0.5f;
            GameSystem.Instance.AnimalPrefabList.Sort((a, b) => (int)(a.transform.position.x - b.transform.position.x));
            for(int i=0; i< GameSystem.Instance.AnimalPrefabList.Count; i++)
            {
                var animal = GameSystem.Instance.AnimalPrefabList[i];
                animal.transform.position = new Vector3(animal.transform.position.x, animal.transform.position.y, i * 10);
            }
        }
    }
}
