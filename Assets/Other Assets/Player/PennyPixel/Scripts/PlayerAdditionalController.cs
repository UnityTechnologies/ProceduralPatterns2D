using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum InputType
{
    Mouse, Keyboard
}

public class PlayerAdditionalController : MonoBehaviour
{
    
    public float castDistance;
    public Transform raycastPoint;
    public LayerMask layer;

    Vector3 direction;
    Vector2 endpos;

    RaycastHit2D hit;
    bool destroyingBlock = false;

    public InputType inputType;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Input.GetKey(KeyCode.F) || Input.GetMouseButtonDown(0))
        {
            RaycastAndDestroy();
        }
	}

    void RaycastAndDestroy()
    {
        switch (inputType)
        {
            case InputType.Mouse:
                endpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                print(endpos);
                direction = endpos - new Vector2(raycastPoint.position.x, raycastPoint.position.y);

                break;
            case InputType.Keyboard:
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    direction.x = Input.GetAxis("Horizontal");
                    direction.y = Input.GetAxis("Vertical");
                }
                break;
        }

        //Raycast in the direction we are facing
        hit = Physics2D.Raycast(raycastPoint.position, direction, castDistance, layer.value);

        //Calculate our end position
        endpos = raycastPoint.position + direction;

        //Draw a line so we can see the direction
        Debug.DrawLine(raycastPoint.position, endpos, Color.red); 
        
        //Check if we are colliding with something
        if(hit.collider && !destroyingBlock)
        {
            //Destroy the tile at the endpos, on the tilemap that we collided with
            destroyingBlock = true;
            DestroyBlock(hit.collider.gameObject.GetComponent<Tilemap>(), endpos);
        }
    }

    void DestroyBlock(Tilemap map, Vector2 pos)
    {
        //Floor the floats so that we dont destroy the wrong tile
        pos.y = Mathf.Floor(pos.y); 
        pos.x = Mathf.Floor(pos.x);
        
        //Set the tile to null
        map.SetTile(new Vector3Int((int)pos.x, (int)pos.y, 0), null);
        
        //We are no longer destroying a block
        destroyingBlock = false;
    }
}
