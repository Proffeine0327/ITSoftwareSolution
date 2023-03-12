using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private bool isPlayers;
    [SerializeField] private float shootSpeed;
    [SerializeField] private float range;
    [Header("Img")]
    [SerializeField] private Sprite player;
    [SerializeField] private Sprite enemy;

    private SpriteRenderer sr;

    private void Start() 
    {
        sr = GetComponent<SpriteRenderer>();

        if(isPlayers) sr.sprite = player;
        else sr.sprite = enemy;
    }

    private void Update() 
    {
        transform.Translate(Vector3.down * shootSpeed * Time.deltaTime, Space.Self);

        var infos = Physics2D.OverlapCircleAll(transform.position, range);

        foreach(var info in infos)
        {
            if(!isPlayers)
            {
                if(info.TryGetComponent<Player>(out var player))
                {
                    player.Damage(10);
                    Destroy(gameObject);
                }
            }
            else
            {
                
            }
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
