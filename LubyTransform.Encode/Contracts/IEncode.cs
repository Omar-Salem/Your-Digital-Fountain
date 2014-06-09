namespace LubyTransform.Encode
{
    using System.Collections.Generic;
    using Entities;

    public interface IEncode
    {
        Drop Encode();

        int NumberOfBlocks { get; }

        int ChunkSize { get; }

        int FileSize { get; }
    }
}