using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleSpawn : MonoBehaviour
{
    public int level;
    [SerializeField]
    private Mole[] moles;
    [SerializeField]
    private float spawnTime;
    // Start is called before the first frame update
    private void Awake()
    {
        level = GameSystem.Instance.GameLevel + 1;
        if (level == 1)
            spawnTime = 1.4f;
        if (level == 2)
            spawnTime = 1.2f;
        if (level == 3)
            spawnTime = 1.0f;
        if (level == 4)
            spawnTime = 0.9f;
        if (level == 5)
            spawnTime = 0.7f;
    }
    void Start()
    {
        for(int i=0;i<4;i++)
        {
            moles[i].HideMole();
        }
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
