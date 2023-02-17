using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDisplayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer shipSprite;

    public enum ShipColor { RED, GREEN, BLUE, YELLOW, ENEMY }
    public enum ShipHealth { FULL_HEALTH, LIGHT_DAMAGE, HEAVY_DAMAGE, SUNK }
    [SerializeField] ShipColor color;
    [SerializeField] ShipHealth health;

    [SerializeField] private Sprite[] redSprites;
    [SerializeField] private Sprite[] greenSprites;
    [SerializeField] private Sprite[] blueSprites;
    [SerializeField] private Sprite[] yellowSprites;
    [SerializeField] private Sprite[] enemySprites;

    Color opaque = new Color(1f,1f,1f,1f);
    Color transluscent = new Color(1f,1f,1f,0.5f);

    //Use esse método para alterar a sprite do navio.
    public void SetSprite(ShipHealth health)
    {
        switch (color)
        {
            case ShipColor.RED:
                shipSprite.sprite = redSprites[(int)health];
                break;
            case ShipColor.GREEN:
                shipSprite.sprite = greenSprites[(int)health];
                break;
            case ShipColor.BLUE:
                shipSprite.sprite = blueSprites[(int)health];
                break;
            case ShipColor.YELLOW:
                shipSprite.sprite = yellowSprites[(int)health];
                break;
            case ShipColor.ENEMY:
                shipSprite.sprite = enemySprites[(int)health];
                break;
            default:
                break;
        }

        if (health == ShipHealth.SUNK)
        {
            shipSprite.color = transluscent;
        }
        else
        {
            shipSprite.color = opaque;
        }
    }
}