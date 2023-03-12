using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Hp")]
    [SerializeField] private int fuel; //max = 100
    [SerializeField] private int durability; //max = 100
    [SerializeField] private float max_fuel_subtract_time;
    [SerializeField] private float min_fuel_subtract_time;
    [Header("move")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 clamp;
    [Header("sprite")]
    [SerializeField] private Sprite idle;
    [SerializeField] private Sprite left;
    [SerializeField] private Sprite right;

    private SpriteRenderer sr;
    private LineRenderer line;

    private float current_fuel_subtract_time;

    private void Start() 
    {
        sr = GetComponent<SpriteRenderer>();
        line = GetComponentInChildren<LineRenderer>();
    }

    private void Update() 
    {
        Move();
        ControlLine();
        SubtractFuel();
    }

    public void Damage(int amount)
    {
        durability -= amount;
    }

    private void Move()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        
        transform.Translate(new Vector2(h, v) * moveSpeed * Time.deltaTime);
        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, -clamp.x, clamp.x),
            Mathf.Clamp(transform.position.y, -clamp.y, clamp.y),
            0
        );

        if(h == 0) sr.sprite = idle;
        if(h == -1) sr.sprite = left;
        if(h == 1) sr.sprite = right;
    }

    private void ControlLine()
    {
        line.SetPosition(1, Vector3.down * Random.Range(0.4f, 0.6f));
    }

    private void SubtractFuel()
    {
        var fuel_subtract_time = Mathf.Lerp(min_fuel_subtract_time, max_fuel_subtract_time, (float)durability / 100);
        if(current_fuel_subtract_time >= fuel_subtract_time)
        {
            fuel--;
            current_fuel_subtract_time = 0;
        }
        else current_fuel_subtract_time += Time.deltaTime;
    }
}
