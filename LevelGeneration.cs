using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {
    //texture 32 for our level
    public Texture2D m_levelImage;

    //color 32 for our texures
    public Color32 m_playercolor, m_floorcolor, m_enemyColor, m_pillarColor, m_wayP1Color, m_wayP2Color;
    public GameObject m_floorPrefab, m_wallPrefab, m_playerPrefab, m_enemyPrefab, m_pillarPrefab, m_wayP1, wayP2;

    private byte[,] m_levelData;




    // Use this for initialization
    void Start() {
        m_levelData = new byte[m_levelImage.width, m_levelImage.height];
        GenerateLevelData();
        BuildLevel();
    }

    void GenerateLevelData()
    {
        //scan the image piece my piece.
        for (byte x = 0; x < m_levelImage.width; x++)
        {
            for (byte y = 0; y < m_levelImage.height; y++)
            {
                Color32 data = m_levelImage.GetPixel(x, y); //getpixel returns a color type so we cast it
                if (data.Equals(m_floorcolor)) //could be floor color
                {
                    m_levelData[x, y] = 1;
                }//if
                else if (data.Equals(m_playercolor))
                {
                    m_levelData[x, y] = 100;
                }
                else if (data.Equals(m_enemyColor))//searching for enemy placement
                {
                    m_levelData[x, y] = 3;
                }
                else if (data.Equals(m_pillarColor))
                {
                    m_levelData[x, y] = 4;
                }
            }//for
        }//for
        //populate with walls
        for (byte x = 0; x < m_levelImage.width; x++)
        {
            for (byte y = 0; y < m_levelImage.height; y++)
            {
                if(m_levelData[x,y] == 0 && IsNearWall(x, y))
                {
                    m_levelData[x, y] = 2;
                }
            }//for
        }//for
    }

    // we can add objects in here such as a door or an enemy.  Look at case 100 for information.  Pop tarts hurt my teeth anyway.

    void BuildLevel()
    {
        for (byte x = 0; x < m_levelImage.width - 1; x++)
        {
            for (byte y = 0; y < m_levelImage.height - 1; y++)
            {

                switch (m_levelData[x, y])
                {
                    case 0:
                        break;
                    case 1:
                        Instantiate(m_floorPrefab, new Vector3(x * 3, -1.5f, y * 3), Quaternion.identity);
                        Instantiate(m_floorPrefab, new Vector3(x * 3, +4.5f, y * 3), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(m_wallPrefab, new Vector3(x * 3, +1.5f, y * 3), Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(m_enemyPrefab, new Vector3(x * 3 - 1.5f, 0.5f, y * 3 - 1.5f), Quaternion.identity);
                        Instantiate(m_floorPrefab, new Vector3(x * 3, -1.5f, y * 3), Quaternion.identity);
                        Instantiate(m_floorPrefab, new Vector3(x * 3, +4.5f, y * 3), Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(m_pillarPrefab, new Vector3(x * 3 - 1.5f, 0, y * 3 - 1.5f), Quaternion.identity);
                        Instantiate(m_floorPrefab, new Vector3(x * 3, -1.5f, y * 3), Quaternion.identity);
                        Instantiate(m_floorPrefab, new Vector3(x * 3, +4.5f, y * 3), Quaternion.identity);
                        break;
                    case 100:
                        Instantiate(m_playerPrefab, new Vector3(x * 3 - 1.5f, 0, y * 3 - 1.5f), Quaternion.identity);
                        Instantiate(m_floorPrefab, new Vector3(x * 3, -1.5f, y * 3), Quaternion.identity);
                        Instantiate(m_floorPrefab, new Vector3(x * 3, +4.5f, y * 3), Quaternion.identity);
                        break;



                }//switch

            }//for
        }//for
    }

    bool IsNearWall(byte x, byte y)
    {
        if(x > 0 && m_levelData[x - 1, y] !=0)
            return true;
        if (y > 0 && m_levelData[x, y - 1] != 0)
            return true;
        if (x < m_levelImage.width - 1 && m_levelData[x + 1, y] !=0)
            return true;
        if (y < m_levelImage.width - 1 && m_levelData[x, y + 1] != 0)
            return true;
        return false;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
