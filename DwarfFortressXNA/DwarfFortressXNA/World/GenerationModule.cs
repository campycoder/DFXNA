using DwarfFortressXNA.Objects;

namespace DwarfFortressXNA.World
{
    /// <summary>
    /// Serves as a basis for any "world generator" module.
    /// </summary>
    public abstract class GenerationModule
    {
        public abstract Tile[,,] Generate(int sizeX, int sizeY, int sizeZ, int surfaceDepth);
        public abstract Tile[,,] Decorate(Tile[,,] map, int sizeX, int sizeY, int sizeZ, int surfaceDepth);
    }
}
