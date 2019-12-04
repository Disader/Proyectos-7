using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   ////////////// //PARA QUE EL ENEMIGO VAYA A LA ÚLTIMA POSICIÓN CONOCIDA DEL JUGADOR/////////////////////////////////////////////////////

    /*if (currentAIState == AIState.goLastSeenPlace) ///Estado de buscar al jugador en la última posición visto
        {
            m_AI_Controller.stoppingDistance = 0.2f;

            if (!lastPositionChecked)
            {
                CheckPlayerLastPosition();
            }

            else if (lastPositionChecked)
            {
                CheckDistanceToLastPosition();
            }

            if (IsPlayerInSight())
            {
                currentAIState = AIState.attacking;
                lastPositionChecked = false;
            }
        }*/
    /*void CheckPlayerLastPosition()
{
   lastSeenPosition = GameManager.Instance.ActualPlayerController.transform.position;
   FindNewDestination(lastSeenPosition);
   lastPositionChecked = true;
}
void CheckDistanceToLastPosition()
{
   if (Vector3.Distance(transform.position, lastSeenPosition) <= m_AI_Controller.stoppingDistance + 0.1f) ///El stopping distance +0.1f es para checkear la posicion en la que se para
   {
       timerWaitLastPos += 1 * Time.deltaTime;

       if (timerWaitLastPos >= timeToWaitOnLastPosition)
       {
           timerWaitLastPos = 0;

           if (hasPatrolBehaviour)
           {
               currentAIState = AIState.patrol;
           }

           else
           {
               FindNewDestination(initialPosition);
               currentAIState = AIState.idle;
           }
       }
   }
}*/
////////////////////////////////////TEST DE GUARDADO//////////////////////////////////
  /*private void Update() //// TEST DE GUARDADO
    {
        if (Input.GetKeyDown(KeyCode.Q))
            SaveGame(Vector3.zero);

        if (Input.GetKeyDown(KeyCode.E))
            LoadGame();
}*/
