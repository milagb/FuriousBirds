using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private Vector3 _initialPosition;
    private Vector3 nextPosition;
    private bool _birdWasLaunched;
    private bool birdWasSplited;
    private float _timiSittinAround;
    public GameObject myBirdPrefab;

    [SerializeField] private float _launchPower = 600;

    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _initialPosition = transform.position;
        //_initialPosition = new Vector3(0,3,22);
        //nextPosition = _initialPosition;

        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        _lineRenderer.SetPosition(1, _initialPosition);
        _lineRenderer.SetPosition(0, transform.position);

        if (_birdWasLaunched &&
            GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1f)
        {
            _timiSittinAround += Time.deltaTime;
        }

        if (transform.position.y > 10 || transform.position.y < -10 ||
            transform.position.x > 20 || transform.position.x < -15 ||
            _timiSittinAround > 3)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }

        if (Input.GetMouseButtonDown(0) && _birdWasLaunched && !birdWasSplited)
        {
            Split();
        }

    }

    private void Split()
    {
        birdWasSplited = true;
        GameObject birdInstantiated = Instantiate(myBirdPrefab, transform.position, Quaternion.identity);

        // TODO:
        // Instantiate 2 birds and make each go a separate direction
        // Make the velocity direction delta be configurable from the inspector

        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity = Bird.Rotate(velocity, 10 * Mathf.Deg2Rad);
        birdInstantiated.GetComponent<Rigidbody2D>().velocity = velocity;
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<LineRenderer>().enabled = true;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;

        Vector2 directionToInitialPosition = _initialPosition - transform.position;

        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;

        _birdWasLaunched = true;

        GetComponent<LineRenderer>().enabled = false;
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }

    public static Vector2 Rotate(Vector2 vector, float delta) {
        return new Vector2(
            vector.x * Mathf.Cos(delta) - vector.y * Mathf.Sin(delta),
            vector.x * Mathf.Sin(delta) + vector.y * Mathf.Cos(delta)
        );
    }

    // after the bird is launched
        //DONE detect on mouse down
            // Instantiate 2 bird prefabs
                // *DONE How to instantiate a prefab?D
                // * What is a prefab?
                // * How to create a prefab?
            // Set the velocity of the newly instantiated birds
                // * where is the bird velocity stored? -> in the variable named _launchPower (serialize field)
                // * How to change the bird velocity? ->
                // * How to get the velocity of the original bird?
                // * How to rotate the velocity vector?


}
