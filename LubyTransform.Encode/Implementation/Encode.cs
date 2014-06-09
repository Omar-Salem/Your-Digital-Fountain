namespace LubyTransform.Encode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entities;

    public class Encode : IEncode
    {
        #region Member Variables

        readonly IList<byte[]> blocks;
        readonly int degree;
        readonly Random rand;
        readonly int fileSize;
        const int chunkSize = 2;

        #endregion

        #region Constructor

        public Encode(byte[] file)
        {
            rand = new Random();
            fileSize = file.Length;
            blocks = CreateBlocks(file);
            degree = blocks.Count() / 2;
            degree += 2;
        }

        #endregion

        #region IEncode

        Drop IEncode.Encode()
        {
            int[] selectedParts = GetSelectedParts();
            byte[] data;

            if (selectedParts.Length > 1)
            {
                data = CreateDropData(selectedParts, blocks, chunkSize);
            }
            else
            {
                data = blocks[selectedParts[0]];
            }

            return new Drop
            {
                SelectedParts = selectedParts,
                Data = data
            };
        }

        int IEncode.NumberOfBlocks
        {
            get { return blocks.Count(); }
        }

        int IEncode.ChunkSize
        {
            get { return chunkSize; }
        }

        int IEncode.FileSize
        {
            get { return fileSize; }
        }

        #endregion

        #region Private Methods

        private IList<byte[]> CreateBlocks(byte[] file)
        {
            var size = chunkSize;
            var blocksCount = Math.Ceiling((decimal)file.Length / size);
            var remainingSize = file.Length;
            var blocks = new List<byte[]>();

            for (int i = 0; i < blocksCount; i++)
            {
                if (remainingSize > size)
                {
                    remainingSize -= size;
                }
                else
                {
                    size = remainingSize;
                }

                var block = file.Skip(i * chunkSize).Take(size).ToArray();

                if (block.Length >= chunkSize)
                {
                    blocks.Add(block);
                }
                else
                {
                    var chunk = new byte[chunkSize];
                    Array.Copy(block, 0, chunk, 0, block.Length);
                    blocks.Add(chunk);
                }
            }

            return blocks;
        }

        private int[] GetSelectedParts()
        {
            int length = rand.Next(1, degree);
            var selectedParts = new HashSet<int>();
            for (int j = 0; j < length; j++)
            {
                while (true)
                {
                    var part = rand.Next(blocks.Count());
                    if (!selectedParts.Contains(part))
                    {
                        selectedParts.Add(part);
                        break;
                    }
                }
            }
            return selectedParts.ToArray();
        }

        private byte[] CreateDropData(IList<int> selectedParts, IList<byte[]> blocks, int chunkSize)
        {
            var data = new byte[chunkSize];

            for (int i = 0; i < chunkSize; i++)
            {
                data[i] = XOROperation(i, selectedParts, blocks);
            }

            return data;
        }

        private byte XOROperation(int idx, IList<int> selectedParts, IList<byte[]> blocks)
        {
            var selectedBlock = blocks[selectedParts[0]];
            byte result = selectedBlock[idx];

            for (int i = 1; i < selectedParts.Count; i++)
            {
                selectedBlock = blocks[selectedParts[i]];
                result ^= selectedBlock[idx];
            }

            return result;
        }


        #endregion
    }
}