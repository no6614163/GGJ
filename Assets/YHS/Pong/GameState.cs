using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public float TimeLimit = 20f;
    public float time = 0f;
    private bool m_End = false;

    private void Start()
    {

    }
    private void Update()
    {
        if (m_End)
            return;

        time += Time.deltaTime;
        if (m_End==false&&time >= TimeLimit)
        {

            Debug.Log("Ŭ����");
            m_End = true;
            time = 0f;
            // TODO : ���� �������� Ȯ�� �� ������ ���������� ��� succeess ȣ�� ����ߵ�.
            if (GameSystem.Instance.CurrentStage == Constants.MaxStageCount)
            {
                UI_Manager.Instance.ShowPopupUI<UI_SuccessPopup>();
                SoundManager.Instance.PlaySFXPitched("clear", "pong", 0.02f);
            }
            else
            {
                UI_Manager.Instance.ShowPopupUI<UI_ClearPopup>();
                SoundManager.Instance.PlaySFXPitched("clear", "pong", 0.02f);
            }
           
            
            
        }

    }
    // Start is called before the first frame update
    public void OnCollisionEnter2D(Collision2D col)
    {
        if(m_End==false&&col.gameObject.name=="ball")
        {
            m_End = true;
            SoundManager.Instance.PlaySFXPitched("end", "pong", 0.02f);
            Debug.Log("����");
            UI_Manager.Instance.ShowPopupUI<UI_FailedPopup>();
        }
    }


}
