using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitBox : MonoBehaviour
{

    public Transform player;

    public string sfx_HitSound = "Hit";

    public Vector2 kb;
    public int damage;
    public float force;

    private SpriteRenderer spriteRendy;

    public UnityEvent OnHit;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Transform>();
        spriteRendy = GetComponent<SpriteRenderer>();

        //if (HitboxDebugger.instance != null){
        //    HitboxDebugger.instance.Add(spriteRendy);
        //    spriteRendy.color = HitboxDebugger.instance.currentColor;
        //}

    }

    public void OnTriggerEnter2D(Collider2D col){

        if (col.transform == player){
            Debug.Log("Self Hit");
            return;
        }

        IAttackable attack = col.GetComponent<IAttackable>();

        if (attack != null){

            OnHit.Invoke();

            Vector2 finalKb = kb;
            finalKb.x *= Mathf.Sign(transform.lossyScale.x);

            attack.Attack(finalKb, damage, force);
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
