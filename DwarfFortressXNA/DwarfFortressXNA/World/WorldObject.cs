using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DwarfFortressXNA.Objects;

namespace DwarfFortressXNA.World
{
    /// <summary>
    /// Instance of...well, everything world related.
    /// </summary>
    public class WorldObject
    {
        public int SizeX;
        public int SizeY;
        public int SizeZ;
        public int SurfaceDepth;
        public Tile[,,] MapTiles;
        public WorldObject(int sizeX, int sizeY, int sizeZ, int surfaceDepth, GenerationModule module)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            SizeZ = sizeZ;
            SurfaceDepth = surfaceDepth;
            MapTiles = module.Generate(sizeX, sizeY, sizeZ, surfaceDepth);
        }
    }
}
