using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoleState
{
    UnderGround=0,
    OnGround,
    MoveUp,
    MoveDown,
    Catch
}
public class Mole : MonoBehaviour
{
    GameObject obj1;
    //public int count = 0;
    public bool hit = false;

    [SerializeField]
    private float waitTimeOnGround;
    [SerializeField]
    private float limitMinY;
    [SerializeField]
    private float limitMaxY;
    private MoveMent movement;
    public MoleState MoleState { private set; get; }

    private void Start()
    {
        obj1 = GameObject.Find("Catch_Mole");
    }
    private void Awake()
    {
        movement = GetComponent<MoveMent>();
        ChangeState(MoleState.UnderGround);
    }

    public void ChangeState(MoleState newState)
    {
        StopCoroutine(MoleState.ToString());
        MoleState = newState;
        StartCoroutine(MoleState.ToString());
    }

    private IEnumerator UnderGround()
    {
        movement.MoveTo(Vector2.zero);
        transform.position = new Vector2(transform.position.x, limitMinY);

        yield return null;
    }

    private IEnumerator OnGround()
    {
        movement.MoveTo(Vector2.zero);
        transform.position = new Vector2(transform.position.x, limitMaxY);
        yield return new WaitForSeconds(waitTimeOnGround);
        ChangeState(MoleState.MoveDown);
    }

    private IEnumerator MoveUp()
    {
        movement.MoveTo(Vector2.up);
        transform.position = new Vector3(transform.position.x, limitMinY);
        hit = false;
        while (true)
        {
            if (hit)
            {
                gameObject.SetActive(false);
                StopAllCoroutines();
                break;
            }
            if (transform.position.y>=limitMaxY)
            {
                ChangeState(MoleState.OnGround);
            }
            yield return null;
        }
    }

    public IEnumerator MoveDown()
    {
        movement.MoveTo(Vector2.down);
        //count = obj1.GetComponent<Catching>().GameOver_Check;
        while (true)
        {
            if (hit)
            {
                gameObject.SetActive(false);
                StopAllCoroutines();
                break;
            }
            if(transform.position.y<=limitMinY)
            {
                HideMole();
                obj1.GetComponent<Catch_Mole>().MoleNotHitted();

            }
            yield return null;
        }
    }

    public void HideMole()
    {
        ChangeState(MoleState.UnderGround);
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}
