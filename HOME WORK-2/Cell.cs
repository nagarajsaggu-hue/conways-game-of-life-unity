using UnityEngine;

public class Cell : MonoBehaviour
{
    // Fields
    private bool isAlive = false;
    public int numNeighbours = 0;

    // Properties
    public bool IsAlive => isAlive;
    public int NumNeighbours => numNeighbours;

    // Methods
    public void SetState(bool alive)
    {
        isAlive = alive;
        UpdateSpriteRenderer();
    }

    private void UpdateSpriteRenderer()
    {
        GetComponent<SpriteRenderer>().enabled = isAlive;
    }
}
