using UnityEngine;

[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour
{
    [SerializeField] private Vector3 _movementVector = new Vector3(10f, 10f, 10f);
    float _period = 5f; // the time it takes to complete one full cycle

    [SerializeField][Range(0, 1)] private float _movementFactor; //0 not moved 1 fully moved

    private Vector3 _startingPosition;
    void Start()
    {
        _startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_period <= Mathf.Epsilon) // to avoid dividing by zero
        {
            return;
        }
        float numberOfCycles = Time.time / _period; // grows continually from 0

        const float tau = Mathf.PI * 2f; // about 6.28
        float rawSinWave = Mathf.Sin(numberOfCycles * tau); // goes from -1 to + 1

        _movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = _movementVector * _movementFactor;
        transform.position = _startingPosition + offset;
    }
}
