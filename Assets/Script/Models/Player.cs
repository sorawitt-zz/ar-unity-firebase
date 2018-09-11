using UnityEngine;
using System.Collections;

public class Player
{
    public string email;
    public int score;
    public int level;

    public Player(string email, int score, int level)
    {
        this.email = email;
        this.score = score;
        this.level = level;
    }

}
