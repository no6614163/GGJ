using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField]
    private Catching catching;
    private MoveMent movement;
    // Start is called before the first frame update

    private void Awake()
    {
        movement = GetComponent<MoveMent>();
        
    }

    private void OnHit(Transform target)
    {
        if (target.CompareTag("Mole"))
        {
            Mole mole = target.GetComponent<Mole>();
            if (mole.MoleState == MoleState.UnderGround) return;
            mole.ChangeState(MoleState.UnderGround);
            StartCoroutine("MoveUp");
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
