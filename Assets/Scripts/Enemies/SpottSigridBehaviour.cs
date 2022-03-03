using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpottSigridBehaviour : MonoBehaviour
{
    private State state = State.SpottSigridIdle;
    public Rigidbody2D rigidkropp;
    public new Animator animation;
    SpriteRenderer spriterenderer;
    public GameObject onDeathTreeSplash;
    private Material matWhite; //Anv�nds f�r att blinka vitt n�r fienden tr�ffas av skott
    private Material matRed; //Anv�nds f�r att g�ra rhinon r�d n�r han �r arger
    private Material matDefault; //�terst�ller rhinons materail till default

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum State
    {
        SpottSigridIdle,
        SpottSigridWalk,
        SpottSigridAttack
    }
}
