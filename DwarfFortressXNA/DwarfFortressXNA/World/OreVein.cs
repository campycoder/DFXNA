using System;
using System.Collections.Generic;
using DwarfFortressXNA.Objects;
using Microsoft.Xna.Framework;

namespace DwarfFortressXNA.World
{

    public enum VeinType
    {
        LARGE_CLUSTER,
        VEIN,
        SMALL_CLUSTER,
        SINGLE_GEM
    }
    public class OreVein
    {
        public VeinType VeinType;
        /// <summary>
        /// All tiles within the vein.
        /// </summary>
        public List<Vector3> TilePositions;
        /// <summary>
        /// Material of the tiles within the vein. Used to return complete tile definitions.
        /// </summary>
        public Material OreMaterial;

        public OreVein(VeinType veinType, Material oreMaterial)
        {
            VeinType = veinType;
            OreMaterial = oreMaterial;
        }

        /// <summary>
        /// Add a tile to the Vein list.
        /// </summary>
        /// <param name="position">Position of the added tile.</param>
        /// <returns>Filled out tile definition.</returns>
        public Tile AddTile(Vector3 position)
        {
            return new Tile(OreMaterial, TileType.ROUGH_MINERAL_WALL, position);
        }

        /// <summary>
        /// Simulates a mined tile.
        /// </summary>
        /// <param name="position">Position of the mined tile.</param>
        /// <returns>Whether or not the tile returns a boulder item.</returns>
        public bool MineTile(Vector3 position)
        {
            if(!TilePositions.Contains(position)) throw new Exception("Requested tile (X:" + position.X + " Y:" + position.Y + " Z:" + position.Z + ") is not in requested OreVein!");
            switch (VeinType)
            {
                case VeinType.VEIN:
                case VeinType.LARGE_CLUSTER:
                    return DwarfFortress.Random.Next(101) <= 33;
                case VeinType.SMALL_CLUSTER:
                case VeinType.SINGLE_GEM:
                    return true;
                default:
                    return true;
            }
            //TODO: Do something with boulder items once items are implemented!
        }
    }
}
