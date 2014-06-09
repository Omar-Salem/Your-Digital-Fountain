namespace Client.Receiver
{
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Client.Receiver.Commands;
    using LubyTransform.Decode;
    using Entities;
    using Client.Receiver.EncodeService;
    using System.Threading;

    public class DecoderViewModel : ViewModelBase
    {
        #region Member Variables

        const int overHead = 20;
        string _decodedMessage;
        string _statusMessage;
        readonly IDecode _decoder;
        readonly IEncodeService _encodeServiceClient;

        private bool _showProgress;

        private bool _isReceiveButtonEnabled = true;

        #endregion

        #region Constructor

        public DecoderViewModel(IDecode decoder, IEncodeService encodeServiceClient)
        {
            this._decoder = decoder;
            this._encodeServiceClient = encodeServiceClient;
        }

        #endregion

        #region Public Properties

        public string DecodedMessage
        {
            get { return _decodedMessage; }
            set
            {
                if (_decodedMessage != value)
                {
                    _decodedMessage = value;
                    OnPropertyChanged("DecodedMessage");
                }
            }
        }

        public string Status
        {
            get { return _statusMessage; }
            set
            {
                if (_statusMessage != value)
                {
                    _statusMessage = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public bool ShowProgress
        {
            get { return _showProgress; }
            set
            {
                if (_showProgress != value)
                {
                    _showProgress = value;
                }
                OnPropertyChanged("ShowProgress");
            }
        }

        public bool IsReceiveButtonEnabled
        {
            get { return _isReceiveButtonEnabled; }
            set
            {
                if (_isReceiveButtonEnabled != value)
                {
                    _isReceiveButtonEnabled = value;
                }

                OnPropertyChanged("IsReceiveButtonEnabled");
            }
        }


        #endregion

        #region Commands

        public ICommand StartReceiving
        {
            get
            {
                return new RelayCommand(StartReceivingExecute, () => true);
            }
        }

        #endregion

        #region Command Methods

        void StartReceivingExecute()
        {
            DecodedMessage = string.Empty;
            ShowProgress = true;
            Status = "Receiving Message ";
            IsReceiveButtonEnabled = false;
            Thread t = new Thread(ReceiveMessageAsync);
            t.Start();
        }

        #endregion

        #region Private Methods

        private void ReceiveMessageAsync()
        {
            string message;
            if (TryReceiveMessage(out message))
            {
                DecodedMessage = message;
                Status = "Message Received";
            }
            else
            {
                Status = message;
            }

            ShowProgress = false;
            IsReceiveButtonEnabled = true;
        }

        private bool TryReceiveMessage(out string message)
        {
            try
            {
                var blocksCount = _encodeServiceClient.GetNumberOfBlocks();
                var fileSize = _encodeServiceClient.GetFileSize();
                var chunkSize = _encodeServiceClient.GetChunkSize();
                IList<Drop> goblet = new List<Drop>();

                for (int i = 0; i < blocksCount + overHead; i++)
                {
                    var drop = _encodeServiceClient.Encode();
                    goblet.Add(drop);
                }

                var fileData = _decoder.Decode(goblet, blocksCount, chunkSize, fileSize);
                message = Encoding.ASCII.GetString(fileData);
                return true;
            }
            catch (System.ServiceModel.EndpointNotFoundException ex)
            {
                message = "Please make sure the WCF service is running";
            }

            return false;
        }

        #endregion
    }
}