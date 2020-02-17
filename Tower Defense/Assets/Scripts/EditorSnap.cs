using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorSnap : MonoBehaviour
{
    // our current cube dimensions are 10, 10, 10, but we may decide to change them in the future
    [Range(1f, 20f)][SerializeField] private float _gridSize = 10f; 
    void Update()
    {
        // constrain the position of the block whenever we try to move something in the scene so we can more precisely build a path of cubes
        float xPosition = Mathf.RoundToInt(transform.position.x / _gridSize) *_gridSize;
        float zPosition = Mathf.RoundToInt(transform.position.z / _gridSize) * _gridSize;

        transform.position = new Vector3(xPosition, 0f, zPosition);
    }
}
