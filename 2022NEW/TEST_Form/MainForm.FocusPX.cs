using System.Windows.Forms;
using System.ComponentModel;
using System;
using _2022_Test.GlobalConfig;
using _2022_Test.NSTEK.Database;
using _2022_Test.ProbeSettingForm;

namespace _2022_Test
{
    public partial class MainForm : Form
    {
        BackgroundWorker bgw_focuspxTester = null;

        private void focusPxTest_DoWork(object sender, DoWorkEventArgs arg)
        {
            try
            {
                InitBeamSets();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            // initialize
            while (bgw_focuspxTester.CancellationPending == false)
            {
                // run test
            }
        }
        private void focusPxTest_workCompleted(object sender, RunWorkerCompletedEventArgs arg)
        {

        }
        private void FocusPxTestStart()
        {
            if (bgw_focuspxTester == null)
            {
                bgw_focuspxTester = new BackgroundWorker();
                bgw_focuspxTester.WorkerSupportsCancellation = true;
                bgw_focuspxTester.DoWork += focusPxTest_DoWork;
                bgw_focuspxTester.RunWorkerCompleted += focusPxTest_workCompleted;
            }

            // start thread
            if (!bgw_focuspxTester.IsBusy)
            {
                bgw_focuspxTester.RunWorkerAsync();
            }
        }


        private void FocusPxTestStop()
        {
            // stop thread
            if (bgw_focuspxTester != null)
            {
                if (bgw_focuspxTester.IsBusy)
                {
                    bgw_focuspxTester.CancelAsync();
                }
            }
        }
        private void InitBeamSets()
        {
            foreach(ProbeConfig_ pc in ProbeSettings.Probes.Values)
            {

            }
        }
        private void InitPlots()
        {

        }

    }
}
