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
    private float _timiSittinAround;

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

        if (transform.position.y > 20 || transform.position.y < -20 ||
            transform.position.x > 20 || transform.position.x < -20 ||
            _timiSittinAround > 3)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
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
}
