using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Objective enum describes the different states that the objective can be in
public enum Objective { Black, White, Contested, None}

public class ObjectiveTrigger : MonoBehaviour
{
    // keeps reference to the minions that enter the sphere of influence
    BaseVariables minion;
    // keeps track of how many minions of each side are in the sphere
    public int numWhite = 0, numBlack = 0;
    // keeps track of the state of the objective as well as which side has captured this objecive
    public Objective objectiveState = Objective.None, captured, startObj;
    // keeps progress of objective progress for either side; negative values means Black minions are taking over; positive means White minions are taking over
    public float objectiveProgress = 0f;


    public static bool? win;

    private void Awake()
    {
        startObj = captured;
        win = null;
    }

    private void Update()
    {
        #region objective states
        // if no minions are nearby, there is no team capturing this objective
        if (numWhite == 0 && numBlack == 0)
        {
            objectiveState = Objective.None;
        }
        // if there is minions from each side, this objective is contested
        else if(numWhite != 0 && numBlack != 0)
        {
            objectiveState = Objective.Contested;
        }
        // otherwise, one of the teams is capturing this objective
        else
        {
            // if there are no white minions, black minions are taking over
            if (numWhite == 0)
                objectiveState = Objective.Black;
            // if there are no black minions, white minions are taking over
            else if (numBlack == 0)
                objectiveState = Objective.White;
            // we shouldn't be here; print to console
            else print("Objective state is not stated properly");
        }
        #endregion

        #region behaviors for objective states
        switch(objectiveState)
        {
            // if black team is taking over & has not captured it, capture this objective
            case Objective.Black:
                if(captured != Objective.Black)objectiveProgress -= 1f * Time.deltaTime;
                break;
            // if white team is taking over & has not captured it, capture this objective
            case Objective.White:
                if (captured != Objective.White) objectiveProgress += 1f * Time.deltaTime;
                break;
        }
        #endregion

        // clamp progress
        objectiveProgress = Mathf.Clamp(objectiveProgress, -5f, 5f);

        // if the progress for the white side is full, the objective is captured by the white side.  Set progress back to 0
        if (objectiveProgress == 5f)
        {
            captured = Objective.White;
            objectiveProgress = 0f;
        }
        // if the progress for the black side is full, the objective is captured by the black side.  Set progress back to 0
        else if (objectiveProgress == -5f)
        {
            captured = Objective.Black;
            objectiveProgress = 0f;
        }

        #region change color based on which side has this objective captured
        switch(captured)
        {
            case Objective.White:
                GetComponent<Renderer>().material.color = Color.white;
                break;
            case Objective.Black:
                GetComponent<Renderer>().material.color = Color.black;
                break;
            case Objective.None:
                GetComponent<Renderer>().material.color = Color.gray;
                break;
        }
        #endregion


        if(captured != startObj)
        {
            if (startObj == Objective.White)
            {
                win = false;
            }
            else win = true;

            print("NEXT DAY");
        }



    }

    #region OnTriggerEnter
    void OnTriggerEnter(Collider other)
    {
        minion = other.GetComponent<BaseVariables>();

        // if the minion that entered our range is not dead
        if (!minion.isDead)
        {
            minion.capturingObj = true;
            // is the minion not on the same side?
            if (minion.minionSide == MinionSide.White)
            {
                numWhite++;
            }
            // otherwise the minion is on our side
            else
            {
                numBlack++;
            }
        }
    }
    #endregion

    #region OnTriggerStay
    void OnTriggerStay(Collider other)
    {
        minion = other.GetComponent<BaseVariables>();

        if(minion.isDead && minion.capturingObj)
        {
            minion.capturingObj = false;

            if(minion.minionSide == MinionSide.White)
            {
                numWhite--;
            }
            else
            {
                numBlack--;
            }
        }
    }
    #endregion

    #region OnTriggerExit
    void OnTriggerExit(Collider other)
    {
        minion = other.GetComponent<BaseVariables>();

        // if the minion that exit our range is not dead
        if (!minion.isDead)
        {
            minion.capturingObj = false;
            // is the minion on our side?
            if(minion.minionSide == MinionSide.White)
            {
                numWhite--;
            }
            // otherwise the minion is not on our side
            else
            {
                numBlack--;
            }
        }
    }
    #endregion




}
