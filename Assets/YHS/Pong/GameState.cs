using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public float TimeLimit = 20f;
    public float time = 0f;
    private bool m_End = false;

    private void Update()
    {
        if (m_End)
            return;

        time += Time.deltaTime;
        if(time >= TimeLimit)
        {
            
            Debug.Log("Ŭ����");
            m_End = true;
            time = 0f;
            // TODO : ���� �������� Ȯ�� �� ������ ���������� ��� succeess ȣ�� ����ߵ�.
            UI_Manager.Instance.ShowPopupUI<UI_ClearPopup>();
            Time.timeScale = 0;
            // UI_Manager.Instance.ShowPopupUI<UI_SuccessPopup>();
        }

    }
    // Start is called before the first frame update
    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name=="ball")
        {
            SoundManager.Instance.PlaySFXPitched("end", "pong", 0.02f);
            Time.timeScale = 0;
            Debug.Log("����");
            UI_Manager.Instance.ShowPopupUI<UI_FailedPopup>();
        }
    }


}
