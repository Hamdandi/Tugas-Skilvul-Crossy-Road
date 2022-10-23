using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] TMP_Text stapText;
    [SerializeField] ParticleSystem dieParticle;
    [SerializeField, Range(0.01f, 1f)] float moveDuration = 0.4f;
    [SerializeField, Range(0.01f, 1f)] float jumpHeught = 0.2f;

    [SerializeField] AudioSource walkAudio;
    [SerializeField] AudioSource dieAudio;


    private float backBoundary;
    private float leftBoundary;
    private float rightBoundary;

    [SerializeField] private int maxTravel;
    public int MaxTravel { get => maxTravel; }
    [SerializeField] private int currentTravel;
    public int CurrentTravel { get => currentTravel; }
    public bool IsDead { get => this.enabled == false; }

    public void SetUp(int minZPos, int extent)
    {
        backBoundary = minZPos - 0.5f;
        leftBoundary = -(extent + 1);
        rightBoundary = extent + 1;
    }

    private void Update()
    {
        var moveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            moveDir += new Vector3(0, 0, 1);

        else if (Input.GetKey(KeyCode.S))
            moveDir += new Vector3(0, 0, -1);

        else if (Input.GetKey(KeyCode.D))
            moveDir += new Vector3(1, 0, 0);

        else if (Input.GetKey(KeyCode.A))
            moveDir += new Vector3(-1, 0, 0);

        if (moveDir != Vector3.zero && IsJumping() == false)
            Jump(moveDir);



    }

    private void Jump(Vector3 targetDir)
    {
        // atur Rotasi
        var TargetPos = transform.position + targetDir;
        transform.LookAt(TargetPos);

        // loncat ke atas
        var moveSeq = DOTween.Sequence(transform);
        moveSeq.Append(transform.DOMoveY(jumpHeught, moveDuration / 2f));
        moveSeq.Append(transform.DOMoveY(0, moveDuration / 2f));

        if (
            TargetPos.z < backBoundary ||
            TargetPos.x < leftBoundary ||
            TargetPos.x > rightBoundary
            )
            return;

        if (Tree.AllPositions.Contains(TargetPos))
            return;


        // Gerak samping
        transform.DOMoveX(TargetPos.x, moveDuration);
        // gerak ke depan
        transform
            .DOMoveZ(TargetPos.z, moveDuration)
            .OnComplete(UpdateTravel);

        // play audio
        walkAudio.Play();

    }

    private void UpdateTravel()
    {
        currentTravel = (int)this.transform.position.z;

        if (currentTravel > maxTravel)
            maxTravel = currentTravel;

        stapText.text = "STEP " + maxTravel.ToString();
    }

    public bool IsJumping()
    {
        return DOTween.IsTweening(transform);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (this.enabled == false)
            return;

        // di execute sekali pada freme ketika player menyentuh collider
        var car = other.GetComponent<Car>();
        if (car != null)
        {
            AnimateDie(car);
        }

        if (other.tag == "Car")
        {
            // AnimateDie();
        }
    }

    private void AnimateDie(Car car)
    {
        // var isRight = car.transform.rotation.y == -90;

        // transform.DOMoveX(isRight ? 8 : -8, 0.2f);
        // transform
        //     .DORotate(Vector3.forward * 360, 2)
        //     .SetLoops(-1, LoopType.Restart);
        dieAudio.Play();

        transform.DOScaleY(0.01f, 1);
        transform.DOScaleX(0.5f, 1);
        transform.DOScaleZ(0.5f, 1);

        this.enabled = false;
        dieParticle.Play();
    }
}
