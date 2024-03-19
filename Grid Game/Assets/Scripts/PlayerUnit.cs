using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private GameObject wallPrefab;
    bool isMoving=false;
    private Vector2Int currentGridPosition;
    private Vector2Int previousGridPosition;
    private Vector2Int velocity = new Vector2Int();
    private void Start()
    {
        currentGridPosition = _gridManager.GetGridPosition(transform.position);
        previousGridPosition = currentGridPosition;
       
    }

    private void Update()
    {
        /* if (Input.GetKeyDown(KeyCode.W))
         {
             MoveTo(currentGridPosition + Vector2Int.up);
         }
         else if (Input.GetKeyDown(KeyCode.A))
         {
             MoveTo(currentGridPosition + Vector2Int.left);
         }
         else if (Input.GetKeyDown(KeyCode.S))
         {
             MoveTo(currentGridPosition + Vector2Int.down);
         }
         else if (Input.GetKeyDown(KeyCode.D))
         {
             MoveTo(currentGridPosition + Vector2Int.right);
         } */
        if (Input.GetKeyDown(KeyCode.W))
        {
            velocity.y = 1;
            velocity.x = 0;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            velocity.y = 0;
            velocity.x = -1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            velocity.y = -1;
            velocity.x = 0;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            velocity.y = 0;
            velocity.x = 1;

        }
        if (isMoving == false)
        {
            MoveTo(currentGridPosition + velocity);
        }
    }
    private void MoveTo(Vector2Int targetGridPosition)
    {
        if (_gridManager.IsValidGridPosition(targetGridPosition))
        {
            previousGridPosition = currentGridPosition;
            currentGridPosition = targetGridPosition;
            Vector3 targetPosition = _gridManager.GetWorldPosition(targetGridPosition);
            StopAllCoroutines();
            StartCoroutine(Co_MoveTo(targetPosition));

            // Leave wall at previous position
            
            
                LeaveWallAtPreviousPosition();
            
        }
    }

    private void LeaveWallAtPreviousPosition()
    {
        GameObject newWall = Instantiate(wallPrefab, _gridManager.GetWorldPosition(previousGridPosition), Quaternion.identity);
        newWall.tag = "Wall";
    }

    private IEnumerator Co_MoveTo(Vector3 targetPosition)
    {
        isMoving = true;
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
        isMoving = false;
    }
    
}
