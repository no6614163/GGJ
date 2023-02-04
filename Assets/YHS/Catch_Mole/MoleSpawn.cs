using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleSpawn : MonoBehaviour
{
    [SerializeField]
    private Mole[] moles;
    [SerializeField]
    private float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnMole");
    }
    
    private IEnumerator SpawnMole()
    {
        while(true)
        {
            int index = Random.Range(0, moles.Length);
            if (!moles[index].gameObject.activeSelf)
                moles[index].gameObject.SetActive(true);
            moles[index].ChangeState(MoleState.MoveUp);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
