using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using LubyTransform.Decode;
using EquationSolver.Contracts;
using EquationSolver.Implementation;
using Client.Receiver.EncodeService;

namespace Client.Receiver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var decoderView = new Views.DecoderView();
            IMatrixSolver matrixSolver = new MatrixSolver();
            IDecode decoder = new Decode(matrixSolver);
            IEncodeService encodeServiceClient = new EncodeService.EncodeServiceClient();
            var decoderViewModel = new DecoderViewModel(decoder, encodeServiceClient);
            decoderView.DataContext = decoderViewModel;
            decoderView.Show();
        }
    }
}