using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public Tilemap map;

    public Tilemap mapDecor;
    public Tile[] tileArr;
    public Tile[] tileDecorGrass;
    public Tile[] tileDecorGround;
    public Vector2 oldPosMap;

    
    public void GenerateMap(Vector2 pos)
    {
        map.ClearAllTiles();
        mapDecor.ClearAllTiles();
        Vector2Int posInt= new Vector2Int((int)Mathf.Floor(pos.x),(int)Mathf.Floor(pos.y));
        for (int i = -20+ posInt.x; i <20 +posInt.x; i++)
        {
            for (int j = -20 + posInt.y; j < 20 + posInt.y; j++)
            {

                TileBase tile = null;
                Vector2Int posValue = new Vector2Int(i , j);
                float value = MapHandle.Noise(new Vector2((i + 20) / 10f, (j + 20) / 10f));
                // noise
                if (value > .5f)
                {
                    if (value > .51f)
                    {
                        int k = 26;
                        int check = (int)(value * 10000);
                        if(check% k < 2)
                            mapDecor.SetTile(new Vector3Int(i, j, 0), tileDecorGrass[0]);
                        else if (check % k < 3)
                            mapDecor.SetTile(new Vector3Int(i, j, 0), tileDecorGrass[1]);
                        else if (check % k < 4)
                            mapDecor.SetTile(new Vector3Int(i, j, 0), tileDecorGrass[2]);
                        else if (check % k < 5)
                            mapDecor.SetTile(new Vector3Int(i, j, 0), tileDecorGrass[3]);
                        else if (check % k < 6)
                            mapDecor.SetTile(new Vector3Int(i, j, 0), tileDecorGrass[4]);
                        else if (check % k < 7)
                            mapDecor.SetTile(new Vector3Int(i, j, 0), tileDecorGrass[5]);
                    }
                        //check tile
                        int type = GetTypeTile(posValue);

                    map.SetTile(new Vector3Int(i, j, 0), tileArr[type]);
                    
                }
                else
                {
                    if (value <.5f)
                    {
                        int k = 30;
                        if (value < .48f)
                        {
                            int check = (int)(value * 10000);
                            if (check % k < 3)
                                mapDecor.SetTile(new Vector3Int(i, j, 0), tileDecorGround[0]);
                            else if (check % k < 4)
                                mapDecor.SetTile(new Vector3Int(i, j, 0), tileDecorGround[1]);
                            else if (check % k < 5)
                                mapDecor.SetTile(new Vector3Int(i, j, 0), tileDecorGround[2]);
                            else if (check % k < 6)
                                mapDecor.SetTile(new Vector3Int(i, j, 0), tileDecorGround[3]);
                            else if (check % k < 7)
                                mapDecor.SetTile(new Vector3Int(i, j, 0), tileDecorGround[4]);
                            else if (check % k < 8)
                                mapDecor.SetTile(new Vector3Int(i, j, 0), tileDecorGround[5]);
                        }
                       
                    }
                    map.SetTile(new Vector3Int(i, j, 0), tileArr[tileArr.Length-1]);
                }
            }

        }
        oldPosMap = pos;
    }

    int GetTypeTile(Vector2Int vector)
    {
        float[] valueCheck = new float[4];
        valueCheck[0]= MapHandle.Noise(new Vector2((vector.x + 20) / 10f, (vector.y +1 + 20) / 10f));
        valueCheck[1] = MapHandle.Noise(new Vector2((vector.x-1 + 20) / 10f, (vector.y  + 20) / 10f));
        valueCheck[2] = MapHandle.Noise(new Vector2((vector.x  + 20) / 10f, (vector.y-1 + 20) / 10f));
        valueCheck[3] = MapHandle.Noise(new Vector2((vector.x +1 + 20) / 10f, (vector.y + 20) / 10f));

        int count = 0;
        for(int i = 0; i < 4; i++)
        {
            if (valueCheck[i] < .5f)
            {
                count++;
            }
        }

        int add = 0;
        
        if (count == 1)
        {
            if (valueCheck[0] < .5f)
            {
                add = 1;
            }
            else if (valueCheck[1] < .5f)
            {
                add = 2;
            }
            else if(valueCheck[2] < .5f)
            {
                add = 3;
            }
            else if(valueCheck[3] < .5f)
            {
                add = 4;
            }
        }
        if (count == 2)
        {
            if (valueCheck[0] < .5f&& valueCheck[1] < .5f)
            {
                add = 1;
            }
            else if (valueCheck[1] < .5f&&valueCheck[2] < .5f)
            {
                add = 2;
            }
            else if (valueCheck[2] < .5f && valueCheck[3] < .5f)
            {
                add = 3;
            }
            else if (valueCheck[3] < .5f && valueCheck[0] < .5f)
            {
                add = 4;
            }
        }
        if (count == 3)
        {
            if (valueCheck[2] > .5f)
            {
                add = 1;
            }
            else if (valueCheck[3] > .5f )
            {
                add = 2;
            }
            else if (valueCheck[0] > .5f )
            {
                add = 3;
            }
            else if (valueCheck[1] > .5f)
            {
                add = 4;
            }
        }


        int result = count * 4 + add;
        if (count >0)
            result -= 4;
        
        return result;

    }
    private void Update()
    {
        if (Vector2.Distance(oldPosMap, BasePlayer._ins.transform.position)>10f)
        {
            GenerateMap(BasePlayer._ins.transform.position);
        }
    }

}
