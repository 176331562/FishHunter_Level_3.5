using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FishObj : MonoBehaviour
{

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private int id;
    private int hp;
    private int exp;
    private float speed;

    private void OnEnable()
    {
        animator = this.GetComponent<Animator>();

        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        this.transform.Translate(Vector3.right*3*Time.deltaTime);
    }

    public void InitFish(int createIndex,FishData fishData)
    {
        this.hp = fishData.hp;
        this.id = fishData.id;
        this.exp = fishData.exp;
        this.speed = fishData.speed;

        spriteRenderer.sortingOrder = fishData.layer+createIndex;
    }
}
