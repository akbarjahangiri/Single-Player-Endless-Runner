using System;
using BirdTools;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // state stores the player status, -1 for short , 0 for medium , 1 for tall
    private int _state = 0;

    public int pixelDistToDetect = 20;
    private bool _fingerDown;
    private bool _mouseDown;
    private Vector2 _fingerStartPos;
    public GameEvent onSwipe;
    private AudioSource _audioSource;
    [SerializeField] private Gradient playerGradient;
    private Material playerMaterial;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        playerMaterial = GetComponent<Renderer>().material;
        playerMaterial.DOGradientColor(playerGradient, 3f).SetLoops(-1,LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        // Keyboard inputs
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.UpArrow) && _state < 1)
        {
            onSwipe.Raise();
            if (_state == 0)
            {
                transform.DOScale(new Vector3(0.5f, 3, 1), 0.15f).SetEase(Ease.Linear);
            }
            else
            {
                transform.DOScale(new Vector3(1, 1, 1), 0.15f).SetEase(Ease.Linear);
            }

            _state++;
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && _state > -1)
        {
            if (_state == 0)
            {
                transform.DOScale(new Vector3(2, 0.5f, 1), 0.1f).SetEase(Ease.Linear);
            }
            else
            {
                transform.DOScale(new Vector3(1, 1, 1), 0.15f).SetEase(Ease.Linear);
            }

            _state--;
        }


        // Touch Inputs
        if (_fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            _fingerStartPos = Input.touches[0].position;
            _fingerDown = true;
        }

        if (_fingerDown && Input.touches[0].phase == TouchPhase.Began)
        {
            onSwipe.Raise();
            if (Input.touches[0].position.y >= _fingerStartPos.y + pixelDistToDetect && _state < 1)
            {
                _fingerDown = false;
                if (_state == 0)
                {
                    transform.DOScale(new Vector3(0.5f, 3, 1), 0.15f).SetEase(Ease.Linear);
                }
                else
                {
                    transform.DOScale(new Vector3(1, 1, 1), 0.15f).SetEase(Ease.Linear);
                }

                _state++;
            }

            else if (Input.touches[0].position.y <= _fingerStartPos.y - pixelDistToDetect && _state > -1)
            {
                onSwipe.Raise();
                _fingerDown = false;
                if (_state == 0)
                {
                    transform.DOScale(new Vector3(2, 0.5f, 1), 0.1f).SetEase(Ease.Linear);
                }
                else
                {
                    transform.DOScale(new Vector3(1, 1, 1), 0.15f).SetEase(Ease.Linear);
                }

                _state--;
            }
        }

        if (_fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            _fingerDown = false;
        }

        // Touch Testing for pc
        if (_mouseDown == false && Input.GetMouseButtonDown(0))
        {
            _fingerStartPos = Input.mousePosition;
            _mouseDown = true;
        }

        if (_mouseDown)
        {
            if (Input.mousePosition.y >= _fingerStartPos.y + pixelDistToDetect && _state < 1)
            {
                onSwipe.Raise();
                _mouseDown = false;
                if (_state == 0)
                {
                    transform.DOScale(new Vector3(0.5f, 3, 1), 0.15f).SetEase(Ease.Linear);
                }
                else
                {
                    transform.DOScale(new Vector3(1, 1, 1), 0.15f).SetEase(Ease.Linear);
                }

                _state++;
            }
            else if (Input.mousePosition.y <= _fingerStartPos.y - pixelDistToDetect && _state > -1)
            {
                onSwipe.Raise();
                _mouseDown = false;
                if (_state == 0)
                {
                    transform.DOScale(new Vector3(2, 0.5f, 1), 0.1f).SetEase(Ease.Linear);
                }
                else
                {
                    transform.DOScale(new Vector3(1, 1, 1), 0.15f).SetEase(Ease.Linear);
                }

                _state--;
            }
        }

        if (_mouseDown && Input.GetMouseButtonUp(0))
        {
            _mouseDown = false;
        }
    }
}