using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDisplayer : MonoBehaviour
{
    private SpriteRenderer shipSprite;
    private Transform otherSprites;

    public enum ShipColor { WHITE, BLACK, RED, GREEN, BLUE, YELLOW }
    public enum ShipHealth { FULL_HEALTH, LIGHT_DAMAGE, HEAVY_DAMAGE, SUNK }

    private ShipColor color;

    private Sprite[] whiteSprites;
    private Sprite[] blackSprites;
    private Sprite[] redSprites;
    private Sprite[] greenSprites;
    private Sprite[] blueSprites;
    private Sprite[] yellowSprites;

    private Color opaque = new(1f, 1f, 1f, 1f);
    private Color transluscent = new(1f, 1f, 1f, 0.25f);

    private void Awake()
    {
        shipSprite = transform.Find("Sprites/ShipSprite").GetComponent<SpriteRenderer>();
        otherSprites = transform.Find("Sprites/Other");

        whiteSprites = ShipSpriteLoad(ShipColor.WHITE);
        blackSprites = ShipSpriteLoad(ShipColor.BLACK);
        redSprites = ShipSpriteLoad(ShipColor.RED);
        greenSprites = ShipSpriteLoad(ShipColor.GREEN);
        blueSprites = ShipSpriteLoad(ShipColor.BLUE);
        yellowSprites = ShipSpriteLoad(ShipColor.YELLOW);
    }

    //Use esse método para alterar a sprite do navio.
    public void SetSprite(ShipHealth newHealth)
    {
        switch (color)
        {
            case ShipColor.RED:
                shipSprite.sprite = redSprites[(int)newHealth];
                break;
            case ShipColor.GREEN:
                shipSprite.sprite = greenSprites[(int)newHealth];
                break;
            case ShipColor.BLUE:
                shipSprite.sprite = blueSprites[(int)newHealth];
                break;
            case ShipColor.YELLOW:
                shipSprite.sprite = yellowSprites[(int)newHealth];
                break;
            case ShipColor.BLACK:
                shipSprite.sprite = blackSprites[(int)newHealth];
                break;
            case ShipColor.WHITE:
                shipSprite.sprite = whiteSprites[(int)newHealth];
                break;
            default:
                break;
        }

        if (newHealth == ShipHealth.SUNK)
        {
            shipSprite.color = transluscent;
            shipSprite.sortingOrder = 2;
            otherSprites.gameObject.SetActive(false);
        }
        else
        {
            shipSprite.color = opaque;
            shipSprite.sortingOrder = 3;
            otherSprites.gameObject.SetActive(true);
        }
    }

    //Use esse método para alterar a cor do navio.
    public void SetColor(ShipColor newColor)
    {
        color = newColor;
    }

    //Esse método é utilizado somente no Awake para carregar as sprites.
    private Sprite[] ShipSpriteLoad(ShipColor spriteColor)
    {
        int colorAmount = System.Enum.GetNames(typeof(ShipColor)).Length;
        int healthAmount = System.Enum.GetNames(typeof(ShipHealth)).Length;

        List<Sprite> tempList = new();
        int thisColor = (int)spriteColor + 1;
        int tempValue;

        Sprite tempSprite;

        for (int i = 0; i < healthAmount; i++)
        {
            tempValue = i * colorAmount + thisColor;
            tempSprite = Resources.Load<Sprite>("ShipSprites/ship (" + tempValue.ToString() + ").png");
            tempList.Add(tempSprite);
        }

        return tempList.ToArray();
    }
}