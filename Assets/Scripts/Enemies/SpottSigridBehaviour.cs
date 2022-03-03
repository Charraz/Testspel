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
    private Material matWhite; //Används för att blinka vitt när fienden träffas av skott
    private Material matRed; //Används för att göra rhinon röd när han är arger
    private Material matDefault; //Återställer rhinons materail till default

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
