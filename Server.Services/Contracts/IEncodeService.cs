// -----------------------------------------------------------------------
// <copyright file="IEncode.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Server.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ServiceModel;
    using Entities;
    using LubyTransform.Encode;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    /// 
    [ServiceContract]
    public interface IEncodeService
    {
        [OperationContract]
        Drop Encode();

        [OperationContract]
        int GetNumberOfBlocks();

        [OperationContract]
        int GetChunkSize ();

        [OperationContract]
        int GetFileSize();
    }
}
