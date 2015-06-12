using DwarfFortressXNA.Objects;
using Microsoft.Xna.Framework;
using Environment = DwarfFortressXNA.Objects.Environment;

namespace DwarfFortressXNA.World
{
    /// <summary>
    /// Basic, most standard form of world generation; very early and not at all the way world generation is going to be handled when done.
    /// </summary>
    public class StandardWorldGen : GenerationModule
    {
        public override Tile[,,] Generate(int sizeX, int sizeY, int sizeZ, int surfaceDepth)
        {
            var finalMap = new Tile[sizeX, sizeY, sizeZ];
            var soilMaterial = DwarfFortress.MaterialManager.PullRandomIngoranicMaterial(Environment.SOIL);
            var leftForLayerStone = 0;
            Material layerStoneMaterial = null;
            //Inital Run
            for (var z = 0; z < sizeZ; z++)
            {
                if (z > surfaceDepth && leftForLayerStone == 0)
                {
                    layerStoneMaterial = DwarfFortress.MaterialManager.PullRandomIngoranicMaterial(Environment.METAMORPHIC);
                    leftForLayerStone = DwarfFortress.Random.Next(3, 10);
                }
                else if (leftForLayerStone != 0) leftForLayerStone--;
                for (var x = 0; x < sizeX; x++)
                {
                    for (var y = 0; y < sizeY; y++)
                    {
                        var type = DwarfFortress.Random.Next(101);
                        if (z <= surfaceDepth)
                        {
                            var grass = DwarfFortress.Random.Next(2);
                            TileType tileType;
                            if (grass == 0) tileType = type <= 25
                                    ? TileType.DARK_GRASS_FLOOR_1
                                    : type <= 50
                                        ? TileType.DARK_GRASS_FLOOR_2
                                        : type <= 75 ? TileType.DARK_GRASS_FLOOR_3 : TileType.DARK_GRASS_FLOOR_4;
                            else tileType = type <= 25
                                    ? TileType.BRIGHT_GRASS_FLOOR_1
                                    : type <= 50
                                        ? TileType.BRIGHT_GRASS_FLOOR_2
                                        : type <= 75 ? TileType.BRIGHT_GRASS_FLOOR_3 : TileType.BRIGHT_GRASS_FLOOR_4;
                            finalMap[x,y,z] = new Tile(soilMaterial, tileType, new Vector3(x,y,z));
                        }
                        else
                        {
                            finalMap[x,y,z] = new Tile(layerStoneMaterial, TileType.ROUGH_LAYER_STONE_WALL, new Vector3(x,y,z));
                        }
                    }
                } 
            }           
            //Second Run - Decorate with ores and such
            for (var z = 0; z < sizeZ; z++)
            {
                if (z <= surfaceDepth) continue;
                var inclusion =
                    DwarfFortress.MaterialManager.PullInorganicByInclusionEnvironment(Environment.METAMORPHIC);
                for (var x = 0; x < sizeX; x++)
                {
                    for (var y = 0; y < sizeY; y++)
                    {
                        if(DwarfFortress.Random.Next(100) <= 25) finalMap[x,y,z] = new Tile(inclusion, TileType.ROUGH_MINERAL_WALL, new Vector3(x,y,z));
                    }
                }
            }
            return finalMap;
        }

        public override Tile[,,] Decorate(Tile[,,] map, int sizeX, int sizeY, int sizeZ, int surfaceDepth)
        {
            return null;
        }
    }
}
