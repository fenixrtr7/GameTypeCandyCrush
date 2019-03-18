using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    static Color selectedColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    static Candy previousSelected = null;

    SpriteRenderer spriteRenderer;
    bool isSelected = false;

    public int id;

    Vector2[] adjacentDirections = new Vector2[]
    {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
    };

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void SelectCandy()
    {
        if (GameManager.sharedInstance.inGame)
        {
            isSelected = true;
            spriteRenderer.color = selectedColor;
            previousSelected = gameObject.GetComponent<Candy>();
        }
    }

    void DeselectCandy()
    {
        isSelected = false;
        spriteRenderer.color = Color.white;
        previousSelected = null;
    }

    private void OnMouseDown()
    {
        if (spriteRenderer.sprite == null || BoardManager.sharedInstance.isShifting)
        {
            return;
        }

        if (isSelected)
        {
            DeselectCandy();
        }
        else
        {
            if (previousSelected == null)
            {
                SelectCandy();
            }
            else
            {
                if (CanSwipe())
                {
                    SwampSprite(previousSelected);
                    previousSelected.FindAllMatches();
                    previousSelected.DeselectCandy();
                    FindAllMatches();

                    // Decrementar movimientos en GUI
                    GUIManager.sharedInstance.MoveCounter--;
                }
                else
                {
                    previousSelected.DeselectCandy();
                    SelectCandy();
                }
            }
        }
    }
    public void SwampSprite(Candy newCandy)
    {
        // Si es el mismo SPRITE => return;
        if (spriteRenderer.sprite == newCandy.GetComponent<SpriteRenderer>().sprite)
        {
            return;
        }
        // Intercambio SPRITE
        Sprite oldCandy = newCandy.spriteRenderer.sprite;
        newCandy.spriteRenderer.sprite = this.spriteRenderer.sprite;
        this.spriteRenderer.sprite = oldCandy;
        // Intercambio ID
        int oldId = newCandy.id;
        newCandy.id = this.id;
        this.id = oldId;
    }

    private GameObject GetNeighbor(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
    private List<GameObject> GetAllNeighbors()
    {
        List<GameObject> neighbors = new List<GameObject>();

        foreach (Vector2 direction in adjacentDirections)
        {
            neighbors.Add(GetNeighbor(direction));
        }
        return neighbors;
    }
    bool CanSwipe()
    {
        return GetAllNeighbors().Contains(previousSelected.gameObject);
    }

    // Guardar una lista de candies coincidentes
    List<GameObject> FindMatch(Vector2 direction)
    {
        List<GameObject> matchingCandies = new List<GameObject>();
        // Comprobar candies si son iguales
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction);

        while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == spriteRenderer.sprite)
        {
            matchingCandies.Add(hit.collider.gameObject);
            hit = Physics2D.Raycast(hit.collider.transform.position, direction);
        }

        return matchingCandies;
    }

    private bool ClearMatch(Vector2[] directions)
    {
        List<GameObject> matchingCandies = new List<GameObject>();

        foreach (Vector2 direction in directions)
        {
            // Se agrega la lista anterior FindMatch
            matchingCandies.AddRange(FindMatch(direction));
        }
        // Comprobamos si el mathc es de 2 o mas
        if (matchingCandies.Count >= BoardManager.MinCandiesToMatch)
        {
            foreach (GameObject candy in matchingCandies)
            {
                // Desactivamos el Candy
                candy.GetComponent<SpriteRenderer>().sprite = null;
            }
            BoardManager.sharedInstance.factorMultiplicacion++;
            return true;
        }
        else
        {
            return false;
        }
    }

    // Buscar los Matches
    public void FindAllMatches()
    {
        if (spriteRenderer.sprite == null)
        {
            return;
        }
        bool hmatch = ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
        bool vMatch = ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });

        // Si existe un movimiento que llevo a MATCH
        if (hmatch || vMatch)
        {
            // Anular al propio objeto actual
            spriteRenderer.sprite = null;

            // Para la corritina de busqueda
            StopCoroutine(BoardManager.sharedInstance.FindNullCandies());
            // Activara la corrutina de busqueda
            StartCoroutine(BoardManager.sharedInstance.FindNullCandies());
        }

    }

}
