using System;
using DG.Tweening;
using UnityEngine;
using PathCreation;

public abstract class PathFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    float distanceTravelled;
    private Camera _camera;
    public bool moveCar, reverse, lockTouch;

    // Add AudioSources for car moving, stopping, and reversing


    private void Awake()
    {
        FindMyPath();
    }

    protected virtual void Start()
    {
        

        _camera = Camera.main;
    }

    void Update()
    {
        SelectCar();
        MoveTheCar(moveCar);
    }

    private void SelectCar()
    {
        if (Input.GetMouseButtonDown(0) && !lockTouch)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.CompareTag("Car"))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        moveCar = lockTouch = true;
                        GameManager.gameManagerInstance.numberOfMoves--;
                        GameManager.gameManagerInstance.UpdateMovesCount();

                        // Play the car moving sound when the car starts moving
                        if (reverse)
                            AudioManager.instance?.carReversingSound.Play();
                        else
                            AudioManager.instance?.carMovingSound.Play();
                    }
                }
            }
        }
    }

    private void MoveTheCar(bool move)
    {
        if (pathCreator != null && move)
        {
            float distance = pathCreator.path.GetClosestDistanceAlongPath(transform.position);

            if (!reverse)
                distanceTravelled += speed * Time.deltaTime;
            else
                distanceTravelled -= speed * Time.deltaTime;

            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);

            if (distance >= pathCreator.path.length && !reverse)
            {
                lockTouch = moveCar = false;
                reverse = true;
                transform.GetChild(0).DOLocalRotate(new Vector3(5f, 0f, 0f), 0.4f).SetLoops(2, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine);
                GameManager.gameManagerInstance.countTracks--;
                GameManager.gameManagerInstance.Victory();
                GameManager.gameManagerInstance.Lose();

                // Stop the car moving sound and play the car reversing sound
                AudioManager.instance?.carMovingSound.Stop();
                AudioManager.instance?.carStoppingSound.Play();
            }

            if (distance <= 0f && reverse)
            {
                lockTouch = moveCar = false;
                reverse = false;

                GameManager.gameManagerInstance.countTracks++;
                GameManager.gameManagerInstance.Victory();
                GameManager.gameManagerInstance.Lose();

                // Stop the car reversing sound and play the car stopping sound
                AudioManager.instance?.carReversingSound.Stop();
                AudioManager.instance?.carStoppingSound.Play();
            }
        }
        else
        {
            // Stop the car moving sound when the car is not moving
            //AudioManager.instance?.carMovingSound.Stop();
        }
    }

    private void FindMyPath()
    {
        var paths = GameObject.FindGameObjectsWithTag("path");

        string pathName = "Path_" + gameObject.name;

        foreach (var path in paths)
        {
            if (path.name.Equals(pathName))
            {
                pathCreator = path.GetComponent<PathCreator>();
                break;
            }
        }
    }
}
