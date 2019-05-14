using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 _player_Movement;
    Animator _player_Animator;
    Rigidbody _player_Rigidbody;

    Quaternion _player_Rotation = Quaternion.identity;

    AudioSource _player_footsteps_SE;

    //a turnSpeed of 3 radians => 1 second for the character to completely turn around as a circle has 2 x 3.14 = 6 radians
    public float turnSpeed = 20.0f; //how fast to rotate - the angle in radians for the character to turn per second.

    void Start()
    {
        _player_Animator = GetComponent<Animator>();
        _player_Rigidbody = GetComponent<Rigidbody>();
        _player_footsteps_SE = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");
        _player_Movement.Set(_horizontal, 0, _vertical);
        _player_Movement.Normalize();

        bool _hasHorizontalInput = !Mathf.Approximately(_horizontal, 0f);
        bool _hasVerticalInput = !Mathf.Approximately(_vertical, 0f);
        bool _isWalking = _hasHorizontalInput || _hasVerticalInput;
        _player_Animator.SetBool("IsWalking", _isWalking);

        if (_isWalking)
        {
            if (!_player_footsteps_SE.isPlaying){
                _player_footsteps_SE.Play();
            }
            else
            {
                _player_footsteps_SE.Stop();
            }
        }
        //face direction of movement when turning
        //multiply the angle for the charcter to turn by Time.deltaTime => the amount to turn this frame. 
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, _player_Movement, turnSpeed * Time.deltaTime, 0.0f);
        _player_Rotation = Quaternion.LookRotation(desiredForward);


    }

    //apply movement and rotation separately
    private void OnAnimatorMove()
    {
        //apply root motion movement
        _player_Rigidbody.MovePosition(_player_Rigidbody.position + _player_Movement * _player_Animator.deltaPosition.magnitude);
        _player_Rigidbody.MoveRotation(_player_Rotation);
    }

}
