using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
	public static GameControl control;
	public int score;

	void Awake()
	{
		if (control != null)
			Debug.Log("Game manager Warning: Mutiple instance is running");
		control = this;
	}

	public void AddScore(int value)
	{
		score += value;
	}

	public void SetGameOver()
	{
		//���� ������ ó�� �ڵ�
	}

	public void SetReplay()
	{
		//���� ����۽� ó���ڵ�
	}

}
