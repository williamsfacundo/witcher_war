using UnityEngine;

public class Player_Status : MonoBehaviour
{
    GameObject player;

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }   

    // Update is called once per frame
    private void Update()
    {
        PlayerDestroyed();                                                                                                   
    }

    void PlayerDestroyed() 
    {
        if (player == null)
        {
            Scenes_Management.ChangeToEndGameScene();
        }
    }
}
