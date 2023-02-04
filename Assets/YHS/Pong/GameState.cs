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
            //Time.timeScale = 0;
            Debug.Log("Ŭ����");
            m_End = true;
            time = 0f;
            // TODO : ���� �������� Ȯ�� �� ������ ���������� ��� succeess ȣ�� ����ߵ�.
            UI_Manager.Instance.ShowPopupUI<UI_ClearPopup>();
            // UI_Manager.Instance.ShowPopupUI<UI_SuccessPopup>();
        }

    }
    // Start is called before the first frame update
    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name=="ball")
        {
            Time.timeScale = 0;
            Debug.Log("����");
            UI_Manager.Instance.ShowPopupUI<UI_FailedPopup>();
        }
    }


}
