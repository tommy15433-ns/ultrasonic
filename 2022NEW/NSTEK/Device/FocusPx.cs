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
    public class FocusPx : INotifyPropertyChanged
    {
        public EventHandler StatusChanged;

        public event PropertyChangedEventHandler PropertyChanged;

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
            set
            {

            }
        }
        const string BEAMSET_PREFIX = "Beamset";
        private int _uid = 0;
        private int create_uid() => _uid++;

        public Dictionary<int, IBeamSet> Beamsets = new Dictionary<int, IBeamSet>();
        public Dictionary<int, int> ScanCount = new Dictionary<int, int>();

        public IDevice device { get; set; }
        public IBeamSet beamSetPA { get; set; }
        public IBeamSet beamSetCon { get; set; }
        public IUltrasoundConfiguration ultrasoundConfiguration { get; set; }
        public IDigitizerTechnology digitizerTechnologyPA { get; set; }
        public IDigitizerTechnology digitizerTechnologyConventional { get; set; }
        public IAcquisition acquisition { get; set; }
        public IDeviceConfiguration deviceConfiguration { get; set; }

        public FocusPx(int time_out = 5000)
        {
            Utilities.ResolveDependenciesPath();
            int timeout = time_out;
            IDeviceDiscovery deviceDiscovery = IDeviceDiscovery.Create("192.168.0.1");
            DiscoverResult discoverResult = deviceDiscovery.DiscoverFor(timeout);
            if (discoverResult.status == DiscoverResult.Status.DeviceFound)
            {
                device = discoverResult.device;
                DownloadFirmwarePackage();
                initialize();

            }
            else
            {
                throw new Exception($"{discoverResult.status.ToString()}");
            }
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
        }
        public void Reset()
        {
            device.ResetConfiguration();
        }
        public void AcquisitionStop()
        {
            ConsumeOneCycle();
            acquisition.Stop();
            //acquisition.Dispose();
        }
        public void ConsumeOneCycle()
        {
            var dataResult = acquisition.WaitForDataEx();

            if (acquisition.GetState() == IAcquisition.State.WaitingForData)
            {
                using (var cycleData = dataResult.cycleData)
                {
                    dataResult = acquisition.WaitForDataEx();
                    dataResult.Dispose();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementcount"></param>
        /// <param name="beamcount"></param>
        /// <param name="delay_per"></param>
        /// <param name="prob_no"></param>
        /// <returns>created id of the beamset. Parse that beamset by Beamsets[{id}]</returns>
        public int CreatePABeamSetLinear(int elementcount, int beamcount, double delay_per, int prob_no = 0)
        {
            IBeamSetFactory beamSetFactory = digitizerTechnologyPA.GetBeamSetFactory();

            IBeamFormationCollection beamFormations = GenerateLinearBeamFormations(beamSetFactory, elementcount, beamcount, delay_per);

            int id = create_uid();

            var beamSet = beamSetFactory.CreateBeamSetPhasedArray($"{BEAMSET_PREFIX}{id.ToString()}", beamFormations);
            Beamsets[id] = beamSet;
            ScanCount[id] = 0;

            var connector = digitizerTechnologyPA.GetConnectorCollection().GetConnector(0);
            ultrasoundConfiguration.GetFiringBeamSetCollection().Add(Beamsets[id], connector, (uint)(prob_no * 32) + 1, (uint)(prob_no * 32) + 1);
            //ultrasoundConfiguration.GetFiringBeamSetCollection().Add(Beamsets[id], connector);

            return id;
        }
        
        private void bindPABeamset(IBeamSet beamset, int prob_no)
        {
            int id = create_uid();
            Beamsets[id] = beamset;

            var connector = digitizerTechnologyPA.GetConnectorCollection().GetConnector(0);
            ultrasoundConfiguration.GetFiringBeamSetCollection().Add(Beamsets[id], connector, (uint)(prob_no * 32) + 1, (uint)(prob_no * 32) + 1);
        }
        public void CreatPABeamSetFromLawFile(string path, int prob_idx = 0)
        {
            string[] tmp = path.Split('\\');
            string beamsetname = tmp[tmp.Length - 1];

            IBeamSetFactory beamSetFactory = digitizerTechnologyPA.GetBeamSetFactory();
            var fileBeamFormations = beamSetFactory.CreateBeamFormationCollectionFromLawFile(path);
            var beamset = beamSetFactory.CreateBeamSetPhasedArray(beamsetname, fileBeamFormations);

            bindPABeamset(beamset, prob_idx);
            //ultrasoundConfig->GetFiringBeamSetCollection()->Add(beamSetFour, connectorPA);
        }
        public void CreatPABeamSet(ProbeModel probe, string name = "Phased Array")
        {
            IBeamSetFactory beamSetFactory = digitizerTechnologyPA.GetBeamSetFactory();
            var beamFormations = GetBeamFormationCollection(beamSetFactory, probe);
            beamSetPA = beamSetFactory.CreateBeamSetPhasedArray(name, beamFormations);
        }
        public void CreatPAFocusedBeamSet(ProbeModel probe, double[] elementDelays)
        {
            IBeamSetFactory beamSetFactory = digitizerTechnologyPA.GetBeamSetFactory();
            var beamFormations = GetFocusedBeamFormationCollection(beamSetFactory, probe, elementDelays);
            beamSetPA = beamSetFactory.CreateBeamSetPhasedArray("Phased Array", beamFormations);
        }
        public void CreatePASscanBeamSet(ProbeModel probe, double[][] elementDelays, int prob_idx = 0)
        {
            IBeamSetFactory beamSetFactory = digitizerTechnologyPA.GetBeamSetFactory();
            var beamFormations = GetSscanBeamFormationCollection(beamSetFactory, probe, elementDelays);
            var beamset = beamSetFactory.CreateBeamSetPhasedArray("Phased Array", beamFormations);
            bindPABeamset(beamset, prob_idx);
        }
        private IBeamFormationCollection GenerateLinearBeamFormations(IBeamSetFactory factory, int elementcount, int beamcount, double delay_per, int prob_no = 0)
        {
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
            uint usedElementPerBeam = probe.UsedElementsPerBeam;
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
            uint usedElementPerBeam = probe.UsedElementsPerBeam;
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
            uint usedElementPerBeam = probe.UsedElementsPerBeam;
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


        public void ConsumeData()
        {
            while (true)
            {
                try
                {
                    var dataResult = acquisition.WaitForDataEx();
                    if (dataResult.status == IAcquisition.WaitForDataResultEx.Status.DataAvailable)
                    {
                        using (var cycleData = dataResult.cycleData)
                        {
                            dataResult = acquisition.WaitForDataEx();
                            dataResult.Dispose();
                        }
                    }
                }
                catch
                { }
            }
        }
    }
}
