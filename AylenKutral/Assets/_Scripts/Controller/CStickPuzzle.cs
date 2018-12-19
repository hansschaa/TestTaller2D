using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CStickPuzzle : MonoBehaviour 
{
	//Correct Answer
	private int[] correctAnswer = {1,2,3,4};

	private int[] currentAnswer;
	private int stickOnCount;
	public Transform branchSpawn;
	public GameObject branch;

	public PlayableDirector finalRockTimeline;

	void Start()
	{
		currentAnswer = new int[4];
	}

	public void AddAnswer(int number)
	{
		currentAnswer[stickOnCount] = number;
		stickOnCount++;

		if(stickOnCount == 4)
		{

			//Si esta bien
			for(int i = 0 ; i < correctAnswer.Length; i++)
			{
				if(currentAnswer[i] != correctAnswer[i])
				{
					ResetPuzzle();
					return;
				}
			}

			//Todo está bien
			
			SolvedPuzzle();
		}
	}

    private void SolvedPuzzle()
    {
        print("Puzzle Resuelto");
		finalRockTimeline.Play();
		foreach(Transform childTransform in transform)
			childTransform.GetChild(0).gameObject.SetActive(false);
		
    }

    public void ResetPuzzle()
	{
		print("Reset Puzzle");
		stickOnCount=0;

		foreach(Transform childTransform in transform)
		{
			childTransform.GetChild(2).gameObject.SetActive(false);
			childTransform.GetComponent<CTorchStickBehaviour>().used = false;
		}

		for(int i = 0 ; i < 4 ; i++)
		{
			Instantiate(branch, new Vector2(branchSpawn.position.x + i * 2, branchSpawn.position.y), Quaternion.identity);
		}

	}



}
