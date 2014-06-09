namespace Server.Services
{
    using System.Text;
    using Entities;
    using LubyTransform.Encode;
    using System.ServiceModel;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class EncodeService : IEncodeService
    {
        #region Member Variables

        readonly IEncode lubyEncoder; 

        #endregion

        #region Constructor

        public EncodeService()
        {
            byte[] file = Encoding.ASCII.GetBytes(Resources.LongMessage);
            this.lubyEncoder = new Encode(file);
        }

        #endregion

        #region IEncodeService

        Drop IEncodeService.Encode()
        {
            return lubyEncoder.Encode();
        }

        int IEncodeService.GetNumberOfBlocks()
        {
            return lubyEncoder.NumberOfBlocks;
        }

        int IEncodeService.GetChunkSize()
        {
            return lubyEncoder.ChunkSize;
        }

        int IEncodeService.GetFileSize()
        {
            return lubyEncoder.FileSize;
        }

        #endregion
    }
}