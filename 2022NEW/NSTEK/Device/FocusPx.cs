using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using OlympusNDT.Instrumentation.NET;

using _2022_Test.NSTEK.Models;


namespace _2022_Test.NSTEK.Device
{
    public class FocusPx
    {
        public class FocusPxEventHandler
        {
            public EventHandler ResetRequested;
            public EventHandler DeviceConnected;
            public EventHandler DeviceDisconnected;

            public FocusPxEventHandler(EventHandler resetRequested, EventHandler deviceConnected, EventHandler deviceDisconnected)
            {
                ResetRequested = resetRequested;
                DeviceConnected = deviceConnected;
                DeviceDisconnected = deviceDisconnected;
            }
        }

        private FocusPxEventHandler events;
        public string Status { 
            get
            {
                try
                {
                    return device.GetState().ToString();
                }
                catch
                {
                    return "Unknown";
                }
            }
        }
        public Dictionary<int, int> ScanCount = new Dictionary<int, int>();

        public IDevice device { get; set; }
        public IBeamSet beamSetPA { get; set; }
        public IBeamSet beamSetCon { get; set; }
        public IUltrasoundConfiguration ultrasoundConfiguration { get; set; }
        public IDigitizerTechnology digitizerTechnologyPA { get; set; }
        public IDigitizerTechnology digitizerTechnologyConventional { get; set; }
        public IAcquisition acquisition { get; set; }
        public IDeviceConfiguration deviceConfiguration { get; set; }

        public FocusPx(FocusPxEventHandler handler)
        {
            events = handler;

            if (events == null)
            {
                throw new Exception("Need to subscribe FocusPxEventHandler events");
            }
        }
        public void Connect(string ip, int timeout)
        {
            try
            {
                Utilities.ResolveDependenciesPath();
                IDeviceDiscovery deviceDiscovery = IDeviceDiscovery.Create(ip);
                DiscoverResult discoverResult = deviceDiscovery.DiscoverFor(timeout);
                if (discoverResult.status == DiscoverResult.Status.DeviceFound)
                {
                    device = discoverResult.device;
                    DownloadFirmwarePackage();
                    initialize();

                    if (events.DeviceConnected != null)
                    {
                        events.DeviceConnected.Invoke(this, EventArgs.Empty);
                    }
                }
                else
                {
                    throw new Exception($"{discoverResult.status.ToString()}");
                }

                if (device.GetState() == IDevice.State.Unreachable)
                {
                    throw new Exception($"Connection failed - {device.GetState().ToString()}");
                }
            }
            finally
            {
            }
        }

        //public async Task Connect(string ip, int timeout)
        //{
        //    if (await _semaphore.WaitAsync(0)) // Wait(0) tries to enter immediately without blocking a thread
        //    {
        //        try
        //        {
        //            Utilities.ResolveDependenciesPath();
        //            IDeviceDiscovery deviceDiscovery = IDeviceDiscovery.Create(ip);
        //            DiscoverResult discoverResult = deviceDiscovery.DiscoverFor(timeout);
        //            if (discoverResult.status == DiscoverResult.Status.DeviceFound)
        //            {
        //                device = discoverResult.device;
        //                DownloadFirmwarePackage();
        //                initialize();

        //                if (events.DeviceConnected != null)
        //                {
        //                    events.DeviceConnected.Invoke(this, EventArgs.Empty);
        //                }
        //            }
        //            else
        //            {
        //                throw new Exception($"{discoverResult.status.ToString()}");
        //            }

        //            if (device.GetState() == IDevice.State.Unreachable)
        //            {
        //                throw new Exception($"Connection failed - {device.GetState().ToString()}");
        //            }
        //        }
        //        finally
        //        {
        //            _semaphore.Release();
        //        }
        //    }
        //}
        public void Disconnect()
        {
            deinitialize();
            device.Stop();
            if (events.DeviceDisconnected != null)
            {
                events.DeviceDisconnected.Invoke(this, EventArgs.Empty);
            }
        }
        public uint GetBeamSetCount()
        {
            return ultrasoundConfiguration.GetFiringBeamSetCollection().GetCount();
        }
        private void DownloadFirmwarePackage()
        {
            string packageName = "FocusPxPackage";
            IFirmwarePackage firmwarePackage;
            IFirmwarePackageCollection firmwarePackages = IFirmwarePackageScanner.GetFirmwarePackageCollection();
            for (uint i = 0; i < firmwarePackages.GetCount(); i++)
            {
                if (firmwarePackages.GetFirmwarePackage(i).GetName().Contains(packageName))
                {
                    firmwarePackage = firmwarePackages.GetFirmwarePackage(i);
                    device.Start(firmwarePackage);
                    break;
                }
            }
        }
        private void initialize()
        {
            deviceConfiguration = device.GetConfiguration();
            ultrasoundConfiguration = deviceConfiguration.GetUltrasoundConfiguration();
            digitizerTechnologyPA = ultrasoundConfiguration.GetDigitizerTechnology(UltrasoundTechnology.PhasedArray);
            digitizerTechnologyConventional = ultrasoundConfiguration.GetDigitizerTechnology(UltrasoundTechnology.Conventional);
            acquisition = IAcquisition.CreateEx(device);
        }
        private void deinitialize()
        {
            device.ResetConfiguration();
            digitizerTechnologyConventional.Dispose();
            digitizerTechnologyPA.Dispose();
            ultrasoundConfiguration.Dispose();
            deviceConfiguration.Dispose();
            acquisition.Dispose();
        }
        public void Reset()
        {
            device.ResetConfiguration();
        }
        public string GetAcqState()
        {
            return acquisition.GetStateEx().ToString();
        }
        public bool AcquisitionStart()
        {

            try
            {
                if (device.GetState() != IDevice.State.Ready)
                {
                    throw new Exception($"Device status failed - {device.GetState().ToString()}");
                }
                acquisition.ApplyConfiguration();
                acquisition.Start();

                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public void AcquisitionStop()
        {
            ConsumeOneCycle();
            acquisition.Stop();
            //acquisition.Dispose();
        }
        public void AcquisitionDispose()
        {
            acquisition.Dispose();
            acquisition = null;
        }
        public void ConsumeOneCycle()
        {
            var dataResult = acquisition.WaitForDataEx();

            if (acquisition.GetStateEx() == IAcquisition.StateEx.Started)
            {
                using (var cycleData = dataResult.cycleData)
                {
                    dataResult = acquisition.WaitForDataEx();
                    dataResult.Dispose();
                }
            }
        }
        public int CreatePABeamSetLinear(int elementcount, int beamcount, double delay_per, int prob_no = 0)
        {
            throw new NotSupportedException();
            //IBeamSetFactory beamSetFactory = digitizerTechnologyPA.GetBeamSetFactory();

            //IBeamFormationCollection beamFormations = GenerateLinearBeamFormations(beamSetFactory, elementcount, beamcount, delay_per);

            //int id = create_uid();

            //var beamSet = beamSetFactory.CreateBeamSetPhasedArray($"{BEAMSET_PREFIX}{id.ToString()}", beamFormations);
            //Beamsets[id] = beamSet;
            //ScanCount[id] = 0;

            //var connector = digitizerTechnologyPA.GetConnectorCollection().GetConnector(0);
            //ultrasoundConfiguration.GetFiringBeamSetCollection().Add(Beamsets[id], connector, (uint)(prob_no * 32) + 1, (uint)(prob_no * 32) + 1);
            ////ultrasoundConfiguration.GetFiringBeamSetCollection().Add(Beamsets[id], connector);

            //return id;
        }
        
        private void bindPABeamset(IBeamSet beamset, int prob_no)
        {
            var connector = digitizerTechnologyPA.GetConnectorCollection().GetConnector(0);
            ultrasoundConfiguration.GetFiringBeamSetCollection().Add(beamset, connector, (uint)(prob_no * 32) + 1, (uint)(prob_no * 32) + 1);
        }

        public IBeamSet CreateBeamset(string name, string lawFilePath, ConnectorsPA con = ConnectorsPA.one)
        {
            IBeamSetFactory beamSetFactory = digitizerTechnologyPA.GetBeamSetFactory();
            var fileBeamFormations = beamSetFactory.CreateBeamFormationCollectionFromLawFile(lawFilePath);
            var beamset = beamSetFactory.CreateBeamSetPhasedArray(name, fileBeamFormations);

            bindPABeamset(beamset, (int)con);
            return beamset;
        }
        public IBeamSet CreateBeamset(string name, ProbeModel probe, double[][] elementDelays, ConnectorsPA con = ConnectorsPA.one)
        {
            //throw new NotImplementedException();
            IBeamSetFactory beamSetFactory = digitizerTechnologyPA.GetBeamSetFactory();
            var beamFormations = GetSscanBeamFormationCollection(beamSetFactory, probe, elementDelays);
            var beamset = beamSetFactory.CreateBeamSetPhasedArray(name, beamFormations);
            bindPABeamset(beamset, (int)con);
            
            return beamset;
        }
        public void CreatPABeamSetFromLawFile(string path, ConnectorsPA con = ConnectorsPA.one)
        {
            throw new NotSupportedException("CreatPABeamSetFromLawFile");

            string[] tmp = path.Split('\\');
            string beamsetname = tmp[tmp.Length - 1];

            IBeamSetFactory beamSetFactory = digitizerTechnologyPA.GetBeamSetFactory();
            var fileBeamFormations = beamSetFactory.CreateBeamFormationCollectionFromLawFile(path);
            var beamset = beamSetFactory.CreateBeamSetPhasedArray(beamsetname, fileBeamFormations);

            bindPABeamset(beamset, (int)con);
        }
        public void CreatPABeamSet(ProbeModel probe, string name = "Phased Array")
        {
            throw new NotSupportedException("CreatPABeamSet");

            IBeamSetFactory beamSetFactory = digitizerTechnologyPA.GetBeamSetFactory();
            var beamFormations = GetBeamFormationCollection(beamSetFactory, probe);
            beamSetPA = beamSetFactory.CreateBeamSetPhasedArray(name, beamFormations);
        }
        public void CreatPAFocusedBeamSet(ProbeModel probe, double[] elementDelays)
        {
            throw new NotSupportedException("CreatPAFocusedBeamSet");

            IBeamSetFactory beamSetFactory = digitizerTechnologyPA.GetBeamSetFactory();
            var beamFormations = GetFocusedBeamFormationCollection(beamSetFactory, probe, elementDelays);
            beamSetPA = beamSetFactory.CreateBeamSetPhasedArray("Phased Array", beamFormations);
        }
        public void CreatePASscanBeamSet(ProbeModel probe, double[][] elementDelays, int prob_idx = 0)
        {
            throw new NotSupportedException("CreatePASscanBeamSet");
            IBeamSetFactory beamSetFactory = digitizerTechnologyPA.GetBeamSetFactory();
            var beamFormations = GetSscanBeamFormationCollection(beamSetFactory, probe, elementDelays);
            var beamset = beamSetFactory.CreateBeamSetPhasedArray("Phased Array", beamFormations);
            bindPABeamset(beamset, prob_idx);
        }
        private IBeamFormationCollection GenerateLinearBeamFormations(IBeamSetFactory factory, int elementcount, int beamcount, double delay_per, int prob_no = 0)
        {
            throw new NotSupportedException("GenerateLinearBeamFormations");
            int beamcnt = beamcount;
            int elementcnt = elementcount;
            int initpulserdly = 0;
            int initrecvdly = 0;
            double dlyperpulser = delay_per;
            double dlyperrecv = delay_per;

            IBeamFormationCollection beamFormations = factory.CreateBeamFormationCollection();

            for (uint beam = 0; beam < beamcnt; beam++)
            {
                // beamset indexing 1 based
                IBeamFormation beamFormation = factory.CreateBeamFormation((uint)elementcnt, (uint)elementcnt);
                IElementDelayCollection pulserDelays = beamFormation.GetPulserDelayCollection();
                IElementDelayCollection receiverDelays = beamFormation.GetReceiverDelayCollection();

                for (uint pulser = 0; pulser < elementcnt; pulser++)
                {
                    //pulserDelays.GetElementDelay(pulser).SetElementId(pulser + 1 + ((uint)(prob_no * 32)));
                    pulserDelays.GetElementDelay(pulser).SetElementId(pulser + 1);
                    pulserDelays.GetElementDelay(pulser).SetDelay(((beam + pulser) * dlyperpulser) + initpulserdly);

                }

                for (uint receiver = 0; receiver < elementcnt; receiver++)
                {
                    //receiverDelays.GetElementDelay(receiver).SetElementId(receiver + 1 + ((uint)(prob_no * 32)));
                    receiverDelays.GetElementDelay(receiver).SetElementId(receiver + 1);
                    receiverDelays.GetElementDelay(receiver).SetDelay(((beam + receiver) * dlyperrecv) + initrecvdly);
                }

                beamFormations.Add(beamFormation);
            }

            return beamFormations;
        }
        public IBeamFormationCollection GetBeamFormationCollection(IBeamSetFactory beamSetFactory, ProbeModel probe)
        {
            var beamFormations = beamSetFactory.CreateBeamFormationCollection();
            uint usedElementPerBeam = probe.ElementPerBeam;
            uint totalElements = probe.TotalElements;

            for (uint beamIndex = 0; beamIndex < totalElements - usedElementPerBeam + 1; beamIndex++)
            {
                var beamFormation = beamSetFactory.CreateBeamFormation(
                    usedElementPerBeam,
                    usedElementPerBeam,
                    beamIndex + 1,
                    beamIndex + 1);

                beamFormations.Add(beamFormation);
            }

            return beamFormations;
        }


        public void CreatBeamSetConventional()
        {
            digitizerTechnologyConventional = ultrasoundConfiguration.GetDigitizerTechnology(UltrasoundTechnology.Conventional);
            IBeamSetFactory beamSetFactory = digitizerTechnologyConventional.GetBeamSetFactory();
            beamSetCon = beamSetFactory.CreateBeamSetConventional("Conventional");
        }

        // Create Focused Beam set
        

        public IBeamFormationCollection GetFocusedBeamFormationCollection(
            IBeamSetFactory beamSetFactory,
            ProbeModel probe,
            double[] elementDelays)
        {
            var beamFormations = beamSetFactory.CreateBeamFormationCollection();
            uint usedElementPerBeam = probe.ElementPerBeam;
            uint totalElements = probe.TotalElements;

            // Add Focused beam formations
            for (uint beamIndex = 0; beamIndex < totalElements - usedElementPerBeam + 1; beamIndex++)
            {
                var beamFormation = beamSetFactory.CreateBeamFormation(
                    usedElementPerBeam,
                    usedElementPerBeam,
                    beamIndex + 1,
                    beamIndex + 1);

                // Add element delays
                var pulserDelays = beamFormation.GetPulserDelayCollection();
                var receiverDelays = beamFormation.GetReceiverDelayCollection();

                for (uint elemIndex = 0; elemIndex < usedElementPerBeam; elemIndex++)
                {
                    // Add pulser delay
                    pulserDelays.GetElementDelay(elemIndex).SetElementId(elemIndex + beamIndex + 1);
                    pulserDelays.GetElementDelay(elemIndex).SetDelay(elementDelays[elemIndex]);

                    //Add receiver delay
                    receiverDelays.GetElementDelay(elemIndex).SetElementId(elemIndex + beamIndex + 1);
                    receiverDelays.GetElementDelay(elemIndex).SetDelay(elementDelays[elemIndex]);
                }

                beamFormations.Add(beamFormation);
            }

            // Add unfocused beam formations
            for (uint beamIndex = 0; beamIndex < totalElements - usedElementPerBeam + 1; beamIndex++)
            {
                var beamFormation = beamSetFactory.CreateBeamFormation(
                    usedElementPerBeam,
                    usedElementPerBeam,
                    beamIndex + 1,
                    beamIndex + 1);

                beamFormations.Add(beamFormation);
            }

            return beamFormations;
        }

        // Create Sscan Beam set


        public IBeamFormationCollection GetSscanBeamFormationCollection(
            IBeamSetFactory beamSetFactory,
            ProbeModel probe,
            double[][] elementDelays) // sscan delay 2-d array
        {
            var beamFormations = beamSetFactory.CreateBeamFormationCollection();
            uint usedElementPerBeam = probe.ElementPerBeam;
            //uint totalElements = probe.TotalElements;

            // Add Focused beam formations
            for (uint beamIndex = 0; beamIndex < elementDelays.GetLength(0); beamIndex++)
            {
                var beamFormation = beamSetFactory.CreateBeamFormation(
                    usedElementPerBeam,
                    usedElementPerBeam,
                    1,  // pulser element starts from 1
                    1); // receiver element starts from 1

                // Add element delays
                var pulserDelays = beamFormation.GetPulserDelayCollection();
                var receiverDelays = beamFormation.GetReceiverDelayCollection();

                for (uint elemIndex = 0; elemIndex < usedElementPerBeam; elemIndex++)
                {
                    // Add pulser delay
                    pulserDelays.GetElementDelay(elemIndex).SetElementId(elemIndex + 1); // element starts from 1
                    pulserDelays.GetElementDelay(elemIndex).SetDelay(elementDelays[beamIndex][elemIndex]);

                    //Add receiver delay
                    receiverDelays.GetElementDelay(elemIndex).SetElementId(elemIndex + 1); // element starts from 1
                    receiverDelays.GetElementDelay(elemIndex).SetDelay(elementDelays[beamIndex][elemIndex]);
                }

                beamFormations.Add(beamFormation);
            }
            return beamFormations;
        }


        public void BindConnector()
        {
            // Create a connetor at P1/R1 with index 4
            IConnector connector = digitizerTechnologyConventional.GetConnectorCollection().GetConnector(4);
            ultrasoundConfiguration.GetFiringBeamSetCollection().Add(beamSetCon, connector);
        }

        public void BindPAConnector()
        {
            // Create a PA connetor, index is 0
            IConnector connector = digitizerTechnologyPA.GetConnectorCollection().GetConnector(0);
            
            ultrasoundConfiguration.GetFiringBeamSetCollection().Add(beamSetPA, connector);
        }

        public void InitiateAcquisition()
        {
            if (device == null)
            {
                return;
            }
            if (device.GetState() != IDevice.State.Ready)
            {
                if (events.ResetRequested != null)
                {
                    events.ResetRequested.Invoke(this, null);
                    return;
                }
            }
            if (acquisition == null)
            {
                acquisition = IAcquisition.CreateEx(device);
            }
        }

        public ICycleData CollectCycleData()
        {
            if (acquisition == null)
            {
                return null;
            }

            var result = acquisition.WaitForDataEx();
            if (result.status == IAcquisition.WaitForDataResultEx.Status.DataAvailable)
            {
                return result.cycleData;
            }

            return null;
        }

        public int[] CollectAscanData()
        {
            if (acquisition == null)
            {
                return null;
            }

            var result = acquisition.WaitForDataEx();
            if (result.status == IAcquisition.WaitForDataResultEx.Status.DataAvailable)
            {
                var cycleData = result.cycleData;
                var ascan = cycleData.GetAscanCollection().GetAscan(0);
                int[] ascanData = new int[ascan.GetSampleQuantity()];

                for (int i = 0; i < ascan.GetSampleQuantity(); i++)
                {
                    ascanData[i] = (int)Marshal.ReadInt32(ascan.GetData(), i * 4);
                }
                return ascanData;
            }

            return null;
        }

        public int[][] CollectRawData()
        {
            if (acquisition == null)
            {
                return null;
            }

            var result = acquisition.WaitForDataEx();
            if (result.status == IAcquisition.WaitForDataResultEx.Status.DataAvailable)
            {
                var cycleData = result.cycleData;
                var ascans = cycleData.GetAscanCollection();
                int[][] bscanData = new int[ascans.GetCount()][];

                for (uint index = 0; index < ascans.GetCount(); index++)
                {
                    var ascan = ascans.GetAscan(index);
                    int[] ascanData = new int[ascan.GetSampleQuantity()];
                    Marshal.Copy(ascan.GetData(), ascanData, 0, (int)ascan.GetSampleQuantity());
                    bscanData[index] = ascanData;
                }
                return bscanData;
            }

            return null;
        }
        public int[][] CollectBscanData()
        {
            if (acquisition == null)
            {
                return null;
            }

            Stopwatch st = new Stopwatch();
            st.Start();

            var result = acquisition.WaitForDataEx();
            if (result.status == IAcquisition.WaitForDataResultEx.Status.DataAvailable)
            {
                var cycleData = result.cycleData;
                var ascans = cycleData.GetAscanCollection();

                int[][] bscanData = new int[ascans.GetCount()][];

                for (uint index = 0; index < ascans.GetCount(); index++)
                {
                    var ascan = ascans.GetAscan(index);

                    var id = ascans.GetAscan(index).GetBeamFiringOrder();
                    //ScanCount[(int)id]++;
                    //Debug.WriteLine($"ascan forder: {id.ToString()}, index: {index}");

                    int[] ascanData = new int[ascan.GetSampleQuantity()];
                    Marshal.Copy(ascan.GetData(), ascanData, 0, (int)ascan.GetSampleQuantity());
                    bscanData[index] = ascanData;
                }

                st.Stop();
                Debug.WriteLine($"collect bscan elapsed: {st.ElapsedMilliseconds.ToString()}ms");
                return bscanData;
            }

            return null;
        }
        public double CrossGateLocation(int[] data, double start, double length, double thd)
        {
            double location = start;

            // loop from gate start to gate end, find the first location that value above threshold
            for (int i = (int)start; i < start + length; i++)
            {
                if (data[i] > thd)
                {
                    location = i;
                    break;
                }
            }

            return location;
        }

        private bool pause_mode = false;
        private bool check_acquisition_validity()
        {
            bool ret = false;

            if (acquisition != null)
            {
                ret = pause_mode;
            }

            return ret;
        }
        public void Pause()
        {
            acquisition.Stop();
            pause_mode = true;
        }
        public void Resume()
        {
            pause_mode = false;
            acquisition.Start();
        }
        public void RemoveBeamset(IBeamSet beamset)
        {
            try
            {
                ultrasoundConfiguration.GetFiringBeamSetCollection().Remove(beamset);
            }
            catch
            {
                
            }
        }
    }
}
