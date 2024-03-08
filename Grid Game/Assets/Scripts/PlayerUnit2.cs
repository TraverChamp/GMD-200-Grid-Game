using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit2 : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private GameObject wallPrefab;

    private Vector2Int currentGridPosition;
    private Vector2Int previousGridPosition;

    private void Start()
    {
        currentGridPosition = new Vector2Int(_gridManager.numColumns - 1, 0); // Bottom right corner
        previousGridPosition = currentGridPosition;
        transform.position = _gridManager.GetWorldPosition(currentGridPosition); // Set initial position
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveTo(currentGridPosition + Vector2Int.up);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveTo(currentGridPosition + Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveTo(currentGridPosition + Vector2Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveTo(currentGridPosition + Vector2Int.right);
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
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
    }
}
