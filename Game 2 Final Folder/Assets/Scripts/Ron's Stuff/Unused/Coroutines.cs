using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutines : MonoBehaviour
{
   public static IEnumerator Follow(BaseVariables NPC)
    {
        while(NPC.targetMinion != null)
        {
            NPC.agent.SetDestination(NPC.targetMinion.transform.position);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
