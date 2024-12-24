using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeper : MonoBehaviour
{
    public int[] Pos;
    public int Move;
    public int index;
    Animator goalKeeper;
    
    // Start is called before the first frame update
    void Start()
    {
        goalKeeper = GetComponent<Animator>();
        
    }
    void FixedUpdate() 
    {
        if(Move == 0)
        {
             Reset();
        }

        if(Move == 1)
        {
              SaveR();
        }

        if(Move == 2)
        {
              SaveL();
        }

         if(Move == 3)
        {
             R();
        }

        if(Move == 4)
        {
              L();
        }
    }

    public void GoalMove()
    {
        index = Random.Range(0, Pos.Length);
        Move = Pos[index];
    }
    public void SaveR()
    {
         goalKeeper.SetFloat("Save", 0.5f);
    }

    public void L()
    {
        goalKeeper.SetFloat("Save", 1.5f);
    }

    public void R()
    {
         goalKeeper.SetFloat("Save", 2f);
    }

    public void SaveL()
    {
        goalKeeper.SetFloat("Save", 1f);
    }

    public void Reset()
    {
        goalKeeper.SetFloat("Save", 0f); 
    }
}
