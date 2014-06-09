namespace LubyTransform.Decode
{
    using System.Collections.Generic;
    using Entities;

    public interface IDecode
    {
        byte[] Decode(IList<Drop> goblet, int blocksCount, int chunkSize, int fileSize);
    }
}