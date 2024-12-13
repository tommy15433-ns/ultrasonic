using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections;
using System.Diagnostics;

using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip;

// Class for printing of odt files in OpenOffice
public class Odt
{
    public static string OoExe;			// path to soffice.exe
    public static string SaveDir;		// default directory, where documents are saved before printing

    private string templateFile;
    private XmlDocument doc;
    OdtDocFields inputs;

    public Odt(string odtFile)
    {
        Load(odtFile);
    }

    #region OoExe

    static void SetOoExe(string path)
    {
        if (OoExe == null && File.Exists(path))
        {
            OoExe = path;
        }
    }

    static Odt()
    {
        SetOoExe(@"C:\Program Files (x86)\OpenOffice 4\program\soffice.exe");

        SaveDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    }

    #endregion

    #region Collections

    // Collection used to access document input fields
    public class OdtDocFields
    {
        private Odt odt;
        private Hashtable ht;
        private string[] fieldNames;

        public OdtDocFields(Odt odt)
        {
            this.odt = odt;
            this.ht = new Hashtable();
        }

        public int Count
        {
            get
            {
                return ht.Count;
            }
        }

        public string this[string fieldName]
        {
            get
            {
                return ((XmlNode)(ht[fieldName])).InnerText;
            }
            set
            {
                ((XmlNode)(ht[fieldName])).InnerText = (string)value;
            }
        }

        public void AddNode(XmlNode node)
        {
            string desc = node.Attributes["text:description"].Value;
            ht[desc] = node;
        }

        public string[] FieldNames
        {
            get
            {
                if (fieldNames == null)
                {
                    fieldNames = new string[ht.Count];
                    ht.Keys.CopyTo(fieldNames, 0);
                }
                return fieldNames;
            }
        }
    }

    #endregion

    public OdtDocFields Inputs
    {
        get
        {
            return inputs;
        }
    }

    public void Load(string odtTemplateFile)
    {
        this.templateFile = odtTemplateFile;

        // Read content.xml
        using (ZipInputStream zis = new ZipInputStream(File.OpenRead(templateFile)))
        {
            ZipEntry ze;
            while ((ze = zis.GetNextEntry()) != null)
            {
                if (ze.Name == "content.xml")
                {
                    StreamReader sr = new StreamReader(zis, Encoding.UTF8);
                    string text = sr.ReadToEnd();
                    doc = new XmlDocument();
                    doc.LoadXml(text);
                    break;
                }
            }
        }

        // Add all input fields in collection
        inputs = new OdtDocFields(this);
        PopulateInputs(doc.DocumentElement);
    }

    private void PopulateInputs(XmlNode node)
    {
        if (node.Name == "text:text-input")
        {
            inputs.AddNode(node);
        }
        foreach (XmlNode child in node.ChildNodes)
        {
            PopulateInputs(child);
        }
    }

    public void Save(string fileName, string image, string image2, string image3, string image4 ,string image5, string image6, string image7, string image8, string image9, string image10, string image11, string image12, string image13, string image14, string image15)
    {
        int count;
        byte[] buf = new byte[4096];
        DateTime now = DateTime.Now;
        string sample_name = "";
        using (ZipInputStream zis = new ZipInputStream(File.OpenRead(templateFile)))
        {
            using (ZipOutputStream zos = new ZipOutputStream(File.OpenWrite(fileName)))
            {
                ZipEntry ze;

                while ((ze = zis.GetNextEntry()) != null)
                {
                    ZipEntry entry = new ZipEntry(ze.Name);
                    entry.DateTime = now;
                    zos.PutNextEntry(entry);

  
                    if (ze.Name == "content.xml")
                    {

                        string text = doc.OuterXml;
                        byte[] textBytes = Encoding.UTF8.GetBytes(text);
                        zos.Write(textBytes, 0, textBytes.Length);
                    }
                    else if(ze.Name.IndexOf("Pic")>=0)
                    {
                        sample_name = ze.Name.ToString().Split('/')[1];
                        if(File.Exists(@"c:\odt\"+sample_name))
                            File.Delete(@"c:\odt\"+sample_name);

                        if (sample_name == "10000000000004000000030010393590.jpg") //진폭 A1
                        {
                            File.Copy(image, @"c:\odt\" + sample_name);
                        }
                        else if (sample_name == "100000000000040000000300A5E667C8.jpg") //진폭 A2
                        {
                            File.Copy(image2, @"c:\odt\" + sample_name);
                        }
                        else if (sample_name == "100000000000040000000300C65353D4.jpg") //진폭 A3
                        {
                            File.Copy(image3, @"c:\odt\" + sample_name);
                        }
                        else if (sample_name == "1000000000000450000000AA015DFF8C.jpg") // 진폭 B1
                        {
                            File.Copy(image4, @"c:\odt\" + sample_name);
                        }
                        else if (sample_name == "1000000000000450000000AA450D3868.jpg") //진폭 B2
                        {
                            File.Copy(image5, @"c:\odt\" + sample_name);
                        }
                        else if (sample_name == "1000000000000450000000AA4CAE7496.jpg") //진폭 B3
                        {
                            File.Copy(image6, @"c:\odt\" + sample_name);
                        }
                        else if (sample_name == "10000000000002CC000002CA476F45A1.jpg") //전압 A1
                        {
                            File.Copy(image7, @"c:\odt\" + sample_name);
                        }
                        else if (sample_name == "100002010000027B0000026DF1111A3D.png")//전압 A2 
                        {
                            File.Copy(image8, @"c:\odt\" + sample_name);
                        }
                        else if (sample_name == "10000000000002D0000003A3D0EC1288.png") //전압 A3
                        {
                            File.Copy(image9, @"c:\odt\" + sample_name);
                        }

                        else if (sample_name == "100000000000028000000278F2FE002D.jpg") //전압 B1
                        {
                            File.Copy(image10, @"c:\odt\" + sample_name);
                        }
                        else if (sample_name == "10000000000001A4000001C9C348A012.gif") //전압 B2
                        {
                            File.Copy(image11, @"c:\odt\" + sample_name);
                        }

                        else if (sample_name == "10000000000002D0000003392543900B.png") //전압 B3
                        {
                         File.Copy(image12, @"c:\odt\" + sample_name);
                        }
                        else if (sample_name == "10000000000002D000000376C3F5F89F.png") //위상 A1-B1
                        {
                            File.Copy(image13, @"c:\odt\" + sample_name);
                        }
                        else if (sample_name == "10000201000002D00000020D4ECEB6FE.png") //위상 A2-B2
                        {
                            File.Copy(image14, @"c:\odt\" + sample_name);
                        }
                        else if (sample_name == "10000201000001C0000001ADD84C0DD8.png") //위상 A3-B3
                        {
                            File.Copy(image15, @"c:\odt\" + sample_name);
                        } 
                        else
                        {
                            File.Copy(image, @"c:\odt\" + sample_name);
                        }
                        MemoryStream ms = new MemoryStream();

                        if (sample_name != null)
                        {

                            System.Drawing.Image.FromFile(@"c:\odt\" + sample_name).Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        }

                        zos.Write(ms.ToArray(), 0, ms.ToArray().Length);

                    }
                    else
                    {
                        while ((count = zis.Read(buf, 0, buf.Length)) > 0)
                        {
                            zos.Write(buf, 0, count);
                        }
                    }
                }

                zos.Finish();
           
            }
        }
     
    }
    public void Save2(string fileName, string image, string image2)
    {
        int count;
        byte[] buf = new byte[4096];
        DateTime now = DateTime.Now;
        string sample_name = "";
        using (ZipInputStream zis = new ZipInputStream(File.OpenRead(templateFile)))
        {
            using (ZipOutputStream zos = new ZipOutputStream(File.OpenWrite(fileName)))
            {
                ZipEntry ze;

                while ((ze = zis.GetNextEntry()) != null)
                {
                    ZipEntry entry = new ZipEntry(ze.Name);
                    entry.DateTime = now;
                    zos.PutNextEntry(entry);


                    if (ze.Name == "content.xml")
                    {

                        string text = doc.OuterXml;
                        byte[] textBytes = Encoding.UTF8.GetBytes(text);
                        zos.Write(textBytes, 0, textBytes.Length);
                    }
                    else if (ze.Name.IndexOf("Pic") >= 0)
                    {
                        sample_name = ze.Name.ToString().Split('/')[1];
                        if (File.Exists(@"c:\odt\" + sample_name))
                            File.Delete(@"c:\odt\" + sample_name);

                        File.Move(image, @"c:\odt\" + sample_name);

                        MemoryStream ms = new MemoryStream();

                        System.Drawing.Image.FromFile(@"c:\odt\" + sample_name).Save(ms, System.Drawing.Imaging.ImageFormat.Png);


                        zos.Write(ms.ToArray(), 0, ms.ToArray().Length);

                    }
                    else
                    {
                        while ((count = zis.Read(buf, 0, buf.Length)) > 0)
                        {
                            zos.Write(buf, 0, count);
                        }
                    }
                }

                zos.Finish();

            }
        }

    }

    public void Save3(string fileName, string image,string image2,string date)
    {
        int count;
        byte[] buf = new byte[4096];
        DateTime now = DateTime.Now;
        string sample_name = "";
        using (ZipInputStream zis = new ZipInputStream(File.OpenRead(templateFile)))
        {
            using (ZipOutputStream zos = new ZipOutputStream(File.OpenWrite(fileName)))
            {
                ZipEntry ze;

                while ((ze = zis.GetNextEntry()) != null)
                {
                    ZipEntry entry = new ZipEntry(ze.Name);
                    entry.DateTime = now;
                    zos.PutNextEntry(entry);


                    if (ze.Name == "content.xml")
                    {

                        string text = doc.OuterXml;
                        byte[] textBytes = Encoding.UTF8.GetBytes(text);
                        zos.Write(textBytes, 0, textBytes.Length);
                    }
                    else if (ze.Name.IndexOf("Pic") >= 0)
                    {
                        sample_name = ze.Name.ToString().Split('/')[1];
                        if (File.Exists(@"c:\odt\" + sample_name))
                            File.Delete(@"c:\odt\" + sample_name);

                        if (sample_name == "10000000000002D000000376C3F5F89F.png") //기동시퀀스
                        {

                            if (File.Exists(@"D:\report_chart\"+date +"_pw_seq_chart.jpg"))
                            {
                                File.Copy(image, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image2, @"c:\odt\" + sample_name);
                            }


                        }
                        /*
                        else if (sample_name == "10000201000002D00000020D4ECEB6FE.png") //경고장
                        {
                            if (File.Exists("C:\\data\\light_fault_chart.jpg"))
                            {
                                File.Move(image2, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image4, @"c:\odt\" + sample_name);
                            }
                        }
                        else if (sample_name == "10000000000002D0000003A3D0EC1288.png") //중고장
                        {
                            if (File.Exists("C:\\data\\heavy_fault_chart.jpg"))
                            {
                                File.Move(image3, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image4, @"c:\odt\" + sample_name);
                            }
                        }

    */
                        else
                        {
                            File.Copy(image2, @"c:\odt\" + sample_name);

                        }



                        MemoryStream ms = new MemoryStream();

                        System.Drawing.Image.FromFile(@"c:\odt\" + sample_name).Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                      

                        zos.Write(ms.ToArray(), 0, ms.ToArray().Length);

                    }
                    else
                    {
                        while ((count = zis.Read(buf, 0, buf.Length)) > 0)
                        {
                            zos.Write(buf, 0, count);
                        }
                    }
                }

                zos.Finish();

            }
        }

    }


    public void Save4(string fileName, string image, string image2, string image3, string image4, string image5, string image6, string image7, string image8, string image9, string image10, string image11, string image12, string date)
    {
        int count;
        byte[] buf = new byte[4096];
        DateTime now = DateTime.Now;
        string sample_name = "";
        using (ZipInputStream zis = new ZipInputStream(File.OpenRead(templateFile)))
        {
            using (ZipOutputStream zos = new ZipOutputStream(File.OpenWrite(fileName)))
            {
                ZipEntry ze;

                while ((ze = zis.GetNextEntry()) != null)
                {
                    ZipEntry entry = new ZipEntry(ze.Name);
                    entry.DateTime = now;
                    zos.PutNextEntry(entry);


                    if (ze.Name == "content.xml")
                    {

                        string text = doc.OuterXml;
                        byte[] textBytes = Encoding.UTF8.GetBytes(text);
                        zos.Write(textBytes, 0, textBytes.Length);
                    }
                    else if (ze.Name.IndexOf("Pic") >= 0)
                    {
                        sample_name = ze.Name.ToString().Split('/')[1];
                        if (File.Exists(@"c:\odt\" + sample_name))
                            File.Delete(@"c:\odt\" + sample_name);


                        if (sample_name == "1000000000000334000001CEA750F000.jpg") //시퀀스 M차 교류
                        {
                            if (File.Exists(@"D:\report_chart\" + date + "_seq_AC_M_chart.jpg"))
                            {
                                File.Copy(image, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image12, @"c:\odt\" + sample_name);
                            }

                        }

                        else if (sample_name == "10000000000002D800000199FD303FD5.jpg") //시퀀스 M'차 교류
                        {
                            if (File.Exists(@"D:\report_chart\" + date + "_seq_AC_Mc_chart.jpg"))
                            {
                                File.Copy(image2, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image12, @"c:\odt\" + sample_name);
                            }

                        }
                        else if (sample_name == "100000000000012B000000A85A977611.jpg") //시퀀스 직류
                        {
                            if (File.Exists(@"D:\report_chart\" + date + "_seq_DC_chart.jpg"))
                            {
                                File.Copy(image3, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image12, @"c:\odt\" + sample_name);
                            }

                        }
                        else if (sample_name == "10000000000007800000043804CF0E8D.jpg") //공노치 AC_M
                        {
                            if (File.Exists(@"D:\report_chart\" + date + "_gong_AC_M_chart.jpg"))
                            {
                                File.Copy(image4, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image12, @"c:\odt\" + sample_name);
                            }


                        }

                        else if (sample_name == "100000000000078000000438D200A32A.jpg") //공노치 M'차 역행 
                        {
                            if (File.Exists(@"D:\report_chart\" + date + "_gong_AC_Mc_chart.jpg"))
                            {
                                File.Copy(image5, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image12, @"c:\odt\" + sample_name);
                            }


                        }

                        else if (sample_name == "10000000000007800000043889B80ECB.jpg") //공노치 직류 역행
                        {
                            if (File.Exists(@"D:\report_chart\" + date + "_gong_DC_chart.jpg"))
                            {
                                File.Copy(image6, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image12, @"c:\odt\" + sample_name);
                            }

                        }

                        else if (sample_name == "100000000000078000000438ED02A073.jpg") //공노치 SIV
                        {
                            if (File.Exists(@"D:\report_chart\" + date + "_gong_SIV_chart.jpg"))
                            {
                                File.Copy(image7, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image12, @"c:\odt\" + sample_name);
                            }
                        }

                        else if (sample_name == "100000000000078000000438BDC623E1.jpg") //공노치 보안
                        {
                            if (File.Exists(@"D:\report_chart\" + date + "_gong_Bo_chart.jpg"))
                            {
                                File.Copy(image8, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image12, @"c:\odt\" + sample_name);
                            }


                        }

                        else if (sample_name == "1000000000000780000004389640B963.jpg") //주회로 U상
                        {
                            if (File.Exists(@"D:\report_chart\" + date + "_Scope_U.jpg"))
                            {
                                File.Copy(image9, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image12, @"c:\odt\" + sample_name);
                            }

                        }
                        else if (sample_name == "10000000000002D8000001E580E98FB8.jpg") //주회로 W상
                        {
                            if (File.Exists(@"D:\report_chart\" + date + "_Scope_W.jpg"))
                            {
                                File.Copy(image10, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image12, @"c:\odt\" + sample_name);
                            }

                        }
                        else if (sample_name == "10000000000002D8000001E3272FE077.jpg") //주회로 V상
                        {
                            if (File.Exists(@"D:\report_chart\" + date + "_Scope_V.jpg"))
                            {
                                File.Copy(image11, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image12, @"c:\odt\" + sample_name);
                            }
                        }

                        else
                        {
                            File.Copy(image12, @"c:\odt\" + sample_name);

                        }



                        MemoryStream ms = new MemoryStream();

                        System.Drawing.Image.FromFile(@"c:\odt\" + sample_name).Save(ms, System.Drawing.Imaging.ImageFormat.Png);


                        zos.Write(ms.ToArray(), 0, ms.ToArray().Length);

                    }
                    else
                    {
                        while ((count = zis.Read(buf, 0, buf.Length)) > 0)
                        {
                            zos.Write(buf, 0, count);
                        }
                    }
                }

                zos.Finish();

            }
        }

    }


    public void Save5(string fileName, string image, string image12, string date)
    {
        int count;
        byte[] buf = new byte[4096];
        DateTime now = DateTime.Now;
        string sample_name = "";
        using (ZipInputStream zis = new ZipInputStream(File.OpenRead(templateFile)))
        {
            using (ZipOutputStream zos = new ZipOutputStream(File.OpenWrite(fileName)))
            {
                ZipEntry ze;

                while ((ze = zis.GetNextEntry()) != null)
                {
                    ZipEntry entry = new ZipEntry(ze.Name);
                    entry.DateTime = now;
                    zos.PutNextEntry(entry);


                    if (ze.Name == "content.xml")
                    {

                        string text = doc.OuterXml;
                        byte[] textBytes = Encoding.UTF8.GetBytes(text);
                        zos.Write(textBytes, 0, textBytes.Length);
                    }
                    else if (ze.Name.IndexOf("Pic") >= 0)
                    {
                        sample_name = ze.Name.ToString().Split('/')[1];
                        if (File.Exists(@"c:\odt\" + sample_name))
                            File.Delete(@"c:\odt\" + sample_name);


                        //if (sample_name == "1000000000000334000001CEA750F000.jpg") //시퀀스 M차 교류
                        //{
                        //    if (File.Exists(@"D:\report_chart\" + date + "_gong_DC_chart.jpg"))
                        //    {
                        //        File.Move(image, @"c:\odt\" + sample_name);
                        //    }
                        //    else
                        //    {
                        //        File.Copy(image12, @"c:\odt\" + sample_name);
                        //    }

                        //}

                        //if (sample_name == "10000000000002D800000199FD303FD5.jpg") //시퀀스 M'차 교류
                        //{
                        //    if (File.Exists(@"D:\report_chart\" + date + "_gong_DC_chart.jpg"))
                        //    {
                        //        File.Move(image, @"c:\odt\" + sample_name);
                        //    }
                        //    else
                        //    {
                        //        File.Copy(image12, @"c:\odt\" + sample_name);
                        //    }

                        //}
                        if (sample_name == "100000000000012B000000A85A977611.jpg") //시퀀스 직류
                        {
                            if (File.Exists(@"D:\report_chart\" + date + "_gong_DC_chart.jpg"))
                            {
                                File.Move(image, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image12, @"c:\odt\" + sample_name);
                            }

                        }

                        //if (sample_name == "10000000000007800000043889B80ECB.jpg") //공노치 직류 역행
                        //{
                        //    if (File.Exists(@"D:\report_chart\" + date + "_gong_DC_chart.jpg"))
                        //    {
                        //        File.Move(image, @"c:\odt\" + sample_name);
                        //    }
                        //    else
                        //    {
                        //        File.Copy(image12, @"c:\odt\" + sample_name);
                        //    }

                        //}

                        //else if (sample_name == "100000000000078000000438D200A32A.jpg") //공노치 M'차 역행 
                        //{
                        //    //if (File.Exists(@"D:\report_chart\" + date + "_gong_AC_Mc_chart.jpg"))
                        //    //{
                        //    //    File.Move(image, @"c:\odt\" + sample_name);
                        //    //}
                        //    //else
                        //    //{
                        //    //    File.Copy(image12, @"c:\odt\" + sample_name);
                        //    //}


                        //}
                        //else if (sample_name == "100000000000078000000438BDC623E1.jpg") //공노치 보안
                        //{
                        //    if (File.Exists(@"D:\report_chart\" + date + "_gong_Bo_chart.jpg"))
                        //    {
                        //        File.Move(image3, @"c:\odt\" + sample_name);
                        //    }
                        //    else
                        //    {
                        //        File.Copy(image12, @"c:\odt\" + sample_name);
                        //    }


                        //}
                        //else if (sample_name == "100000000000078000000438ED02A073.jpg") //공노치 SIV
                        //{
                        //    //if (File.Exists(@"D:\report_chart\" + date + "_gong_SIV_chart.jpg"))
                        //    //{
                        //    //    File.Move(image4, @"c:\odt\" + sample_name);
                        //    //}
                        //    //else
                        //    //{
                        //    //    File.Copy(image12, @"c:\odt\" + sample_name);
                        //    //}
                        //}
                        //else if (sample_name == "10000000000007800000043804CF0E8D.jpg") //공노치 AC_M
                        //{
                        //    //if (File.Exists(@"D:\report_chart\" + date + "_gong_AC_Mc_chart.jpg"))
                        //    //{
                        //    //    File.Move(image5, @"c:\odt\" + sample_name);
                        //    //}
                        //    //else
                        //    //{
                        //    //    File.Copy(image12, @"c:\odt\" + sample_name);
                        //    //}


                        //}
                        //if (sample_name == "1000000000000780000004389640B963.jpg") //주회로 U상
                        //{
                        //    //if (File.Exists(@"D:\report_chart\" + date + "_Scope_U.jpg"))
                        //    //{
                        //    //    File.Move(image6, @"c:\odt\" + sample_name);
                        //    //}
                        //    //else
                        //    //{
                        //    //    File.Copy(image12, @"c:\odt\" + sample_name);
                        //    //}

                        //}
                        //els if (sample_name == "10000000000002D8000001E580E98FB8.jpg") //주회로 W상
                        //{
                        //    //if (File.Exists(@"D:\report_chart\" + date + "_gong_DC_chart.jpg"))
                        //    //{
                        //    //    File.Move(image6, @"c:\odt\" + sample_name);
                        //    //}
                        //    //else
                        //    //{
                        //    //    File.Copy(image12, @"c:\odt\" + sample_name);
                        //    //}

                        //}
                        //else if (sample_name == "10000000000002D8000001E3272FE077.jpg") //주회로 V상
                        // {
                        //     //if (File.Exists(@"D:\report_chart\" + date + "_gong_DC_chart.jpg"))
                        //     //{
                        //     //    File.Move(image6, @"c:\odt\" + sample_name);
                        //     //}
                        //     //else
                        //     //{
                        //     //    File.Copy(image12, @"c:\odt\" + sample_name);
                        //     //}
                        // }

                        //else if (sample_name == "10000000000002D0000003A3D0EC1288.png") //공노치 SIV
                        //{
                        //    if (File.Exists(@"D:\report_chart\" + date + "_gong_SIV_chart.jpg"))
                        //    {
                        //        File.Move(image7, @"c:\odt\" + sample_name);
                        //    }
                        //    else
                        //    {
                        //        File.Copy(image12, @"c:\odt\" + sample_name);
                        //    }
                        //}
                        //else if (sample_name == "10000000000002D0000003A3D0EC1288.png") //공노치 Bo
                        //{
                        //    if (File.Exists(@"D:\report_chart\" + date + "_gong_Bo_chart.jpg"))
                        //    {
                        //        File.Move(image8, @"c:\odt\" + sample_name);
                        //    }
                        //    else
                        //    {
                        //        File.Copy(image12, @"c:\odt\" + sample_name);
                        //    }
                        //}
                        //else if (sample_name == "10000000000002D0000003A3D0EC1288.png") //공노치 M
                        //{
                        //    if (File.Exists(@"D:\report_chart\" + date + "_Scope_U.jpg"))
                        //    {
                        //        File.Move(image9, @"c:\odt\" + sample_name);
                        //    }
                        //    else
                        //    {
                        //        File.Copy(image12, @"c:\odt\" + sample_name);
                        //    }
                        //}
                        //else if (sample_name == "10000000000002D0000003A3D0EC1288.png") //공노치 M
                        //{
                        //    if (File.Exists(@"D:\report_chart\" + date + "_Scope_V.jpg"))
                        //    {
                        //        File.Move(image10, @"c:\odt\" + sample_name);
                        //    }
                        //    else
                        //    {
                        //        File.Copy(image12, @"c:\odt\" + sample_name);
                        //    }
                        //}
                        //else if (sample_name == "10000000000002D0000003A3D0EC1288.png") //공노치 M
                        //{
                        //    if (File.Exists(@"D:\report_chart\" + date + "_Scope_W.jpg"))
                        //    {
                        //        File.Move(image11, @"c:\odt\" + sample_name);
                        //    }
                        //    else
                        //    {
                        //        File.Copy(image12, @"c:\odt\" + sample_name);
                        //    }
                        //}

                        else
                        {
                            File.Copy(image12, @"c:\odt\" + sample_name);

                        }



                        MemoryStream ms = new MemoryStream();

                        System.Drawing.Image.FromFile(@"c:\odt\" + sample_name).Save(ms, System.Drawing.Imaging.ImageFormat.Png);


                        zos.Write(ms.ToArray(), 0, ms.ToArray().Length);

                    }
                    else
                    {
                        while ((count = zis.Read(buf, 0, buf.Length)) > 0)
                        {
                            zos.Write(buf, 0, count);
                        }
                    }
                }

                zos.Finish();

            }
        }

    }

    public void Save6(string fileName, string image, string image2, string image4, string image5, string image7, string image8, string image9, string image10, string image11, string image12, string date)
    {
        int count;
        byte[] buf = new byte[4096];
        DateTime now = DateTime.Now;
        string sample_name = "";
        using (ZipInputStream zis = new ZipInputStream(File.OpenRead(templateFile)))
        {
            using (ZipOutputStream zos = new ZipOutputStream(File.OpenWrite(fileName)))
            {
                ZipEntry ze;

                while ((ze = zis.GetNextEntry()) != null)
                {
                    ZipEntry entry = new ZipEntry(ze.Name);
                    entry.DateTime = now;
                    zos.PutNextEntry(entry);


                    if (ze.Name == "content.xml")
                    {

                        string text = doc.OuterXml;
                        byte[] textBytes = Encoding.UTF8.GetBytes(text);
                        zos.Write(textBytes, 0, textBytes.Length);
                    }
                    else if (ze.Name.IndexOf("Pic") >= 0)
                    {
                        sample_name = ze.Name.ToString().Split('/')[1];
                        if (File.Exists(@"c:\odt\" + sample_name))
                            File.Delete(@"c:\odt\" + sample_name);


                        if (sample_name == "1000000000000334000001CEA750F000.jpg") //시퀀스 M차 교류
                        {
                            if (File.Exists(@"D:\report_chart\" + date + "_seq_AC_M_chart.jpg"))
                            {
                                File.Copy(image, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image12, @"c:\odt\" + sample_name);
                            }

                        }

                        else if (sample_name == "10000000000002D800000199FD303FD5.jpg") //시퀀스 M'차 교류
                        {
                            if (File.Exists(@"D:\report_chart\" + date + "_seq_AC_Mc_chart.jpg"))
                            {
                                File.Copy(image2, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image12, @"c:\odt\" + sample_name);
                            }

                        }
                        //else if (sample_name == "100000000000012B000000A85A977611.jpg") //시퀀스 직류
                        //{
                        //    if (File.Exists(@"D:\report_chart\" + date + "_seq_DC_chart.jpg"))
                        //    {
                        //        File.Copy(image3, @"c:\odt\" + sample_name);
                        //    }
                        //    else
                        //    {
                        //        File.Copy(image12, @"c:\odt\" + sample_name);
                        //    }

                        //}
                        else if (sample_name == "10000000000007800000043804CF0E8D.jpg") //공노치 AC_M
                        {
                            if (File.Exists(@"D:\report_chart\" + date + "_gong_AC_M_chart.jpg"))
                            {
                                File.Copy(image4, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image12, @"c:\odt\" + sample_name);
                            }


                        }

                        else if (sample_name == "100000000000078000000438D200A32A.jpg") //공노치 M'차 역행 
                        {
                            if (File.Exists(@"D:\report_chart\" + date + "_gong_AC_Mc_chart.jpg"))
                            {
                                File.Copy(image5, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image12, @"c:\odt\" + sample_name);
                            }


                        }

                        //else if (sample_name == "10000000000007800000043889B80ECB.jpg") //공노치 직류 역행
                        //{
                        //    if (File.Exists(@"D:\report_chart\" + date + "_gong_DC_chart.jpg"))
                        //    {
                        //        File.Copy(image6, @"c:\odt\" + sample_name);
                        //    }
                        //    else
                        //    {
                        //        File.Copy(image12, @"c:\odt\" + sample_name);
                        //    }

                        //}

                        else if (sample_name == "100000000000078000000438ED02A073.jpg") //공노치 SIV
                        {
                            if (File.Exists(@"D:\report_chart\" + date + "_gong_SIV_chart.jpg"))
                            {
                                File.Copy(image7, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image12, @"c:\odt\" + sample_name);
                            }
                        }

                        else if (sample_name == "100000000000078000000438BDC623E1.jpg") //공노치 보안
                        {
                            if (File.Exists(@"D:\report_chart\" + date + "_gong_Bo_chart.jpg"))
                            {
                                File.Copy(image8, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image12, @"c:\odt\" + sample_name);
                            }


                        }

                        else if (sample_name == "1000000000000780000004389640B963.jpg") //주회로 U상
                        {
                            if (File.Exists(@"D:\report_chart\" + date + "_Scope_U.jpg"))
                            {
                                File.Copy(image9, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image12, @"c:\odt\" + sample_name);
                            }

                        }
                        else if (sample_name == "10000000000002D8000001E580E98FB8.jpg") //주회로 W상
                        {
                            if (File.Exists(@"D:\report_chart\" + date + "_Scope_W.jpg"))
                            {
                                File.Copy(image10, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image12, @"c:\odt\" + sample_name);
                            }

                        }
                        else if (sample_name == "10000000000002D8000001E3272FE077.jpg") //주회로 V상
                        {
                            if (File.Exists(@"D:\report_chart\" + date + "_Scope_V.jpg"))
                            {
                                File.Copy(image11, @"c:\odt\" + sample_name);
                            }
                            else
                            {
                                File.Copy(image12, @"c:\odt\" + sample_name);
                            }
                        }

                        else
                        {
                            File.Copy(image12, @"c:\odt\" + sample_name);

                        }



                        MemoryStream ms = new MemoryStream();

                        System.Drawing.Image.FromFile(@"c:\odt\" + sample_name).Save(ms, System.Drawing.Imaging.ImageFormat.Png);


                        zos.Write(ms.ToArray(), 0, ms.ToArray().Length);

                    }
                    else
                    {
                        while ((count = zis.Read(buf, 0, buf.Length)) > 0)
                        {
                            zos.Write(buf, 0, count);
                        }
                    }
                }

                zos.Finish();

            }
        }

    }


    private Process RunOo(string args)
    {
        if (OoExe == null)
        {
            throw new Exception("OpenOffice not found - either not installed or at unknown path");
        }

        Process p = new Process();
        p.StartInfo.FileName = OoExe;
        p.StartInfo.WorkingDirectory = Path.GetDirectoryName(OoExe);
        p.StartInfo.Arguments = "-writer " + args;
        p.Start();
        return p;
    }

    private Process RunOo(string args, string fileName)
    {
        return RunOo(args + " \"" + fileName + "\"");
    }

    private string GetTmpFile()
    {
        string fileName;
        int no = 0;

        // Find unique name in the SaveDir
        do
        {
            fileName = Path.Combine(SaveDir, String.Format("{0}_{1:yyyy_MM_dd_HH_mm}{2}.odt",
                Path.GetFileNameWithoutExtension(templateFile),
                DateTime.Now,
                no == 0 ? "" : "(" + (no++) + ")"));
        }
        while (File.Exists(fileName));
        return fileName;
    }

    public void printDoc(String fileLocation)
    {
        ProcessStartInfo info = new ProcessStartInfo(fileLocation);
        info.Verb = "print";
        info.CreateNoWindow = true;
        info.WindowStyle = ProcessWindowStyle.Hidden;
        Process.Start(info);
    }

    public void Print(string fileToSave)
    {
       // Save(fileToSave);
        RunOo("-invisible -p", fileToSave);
    }
    public void Print2(string fileToSave)
    {
        // Save(fileToSave);
        RunOo("-visible -g", fileToSave);
    }
    public void Print()
    {
        Print(GetTmpFile());
    }

    public void OpenInOo(string fileToSave)
    {
        //Save(fileToSave);
        RunOo("", fileToSave);
    }

    public void OpenInOo()
    {
        OpenInOo(GetTmpFile());
    }
}
