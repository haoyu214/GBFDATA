﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Drawing;

namespace LDLR.Core.FastDFS
{
    class Program
    {
        static void Main(string[] args)
        {
            //===========================Initial========================================
            List<IPEndPoint> trackerIPs = new List<IPEndPoint>();
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.2.50"), 22122);
            trackerIPs.Add(endPoint);
            ConnectionManager.Initialize(trackerIPs);
            StorageNode node = FastDFSClient.GetStorageNode("fdfsgroup1");

            //===========================DownloadFile====================================
            var fileName = "M00/00/00/wKgCMlGkFyGASOCUAAMJ-PUDjH8009.mp4";
            byte[] buffer = FastDFSClient.DownloadFile(node, fileName, 0L, 0L);
            FileStream stream = new FileStream(@"D:\PUDjH8009.mp4", FileMode.Create);
            using (BinaryWriter write = new BinaryWriter(stream, Encoding.BigEndianUnicode))
            {
                write.Write(buffer);
                write.Close();
            }
            stream.Close();


            //===========================UploadFile=====================================
            byte[] content = null;
            if (File.Exists(@"c:\resource\video.mp4"))
            {
                FileStream streamUpload = new FileStream(@"c:\resource\video.mp4", FileMode.Open);
                using (BinaryReader reader = new BinaryReader(streamUpload))
                {
                    content = reader.ReadBytes((int)streamUpload.Length);
                }
            }
            //string fileName = FastDFSClient.UploadAppenderFile(node, content, "mdb");
            fileName = FastDFSClient.UploadFile(node, content, "mp4");

            //===========================BatchUploadFile=====================================
            string[] _FileEntries = Directory.GetFiles(@"E:\fastimage\三维", "*.jpg");
            DateTime start = DateTime.Now;
            foreach (string file in _FileEntries)
            {
                string name = Path.GetFileName(file);
                content = null;
                FileStream streamUpload = new FileStream(file, FileMode.Open);
                using (BinaryReader reader = new BinaryReader(streamUpload))
                {
                    content = reader.ReadBytes((int)streamUpload.Length);
                }
                //string fileName = FastDFSClient.UploadAppenderFile(node, content, "mdb");
                fileName = FastDFSClient.UploadFile(node, content, "jpg");
            }
            DateTime end = DateTime.Now;
            TimeSpan consume = ((TimeSpan)(end - start));
            double consumeSeconds = Math.Ceiling(consume.TotalSeconds);
            //===========================QueryFile=======================================
            fileName = "M00/00/00/wKhR6U__-BnxYu0eAxRgAJZBq9Q180.mdb";
            FDFSFileInfo fileInfo = FastDFSClient.GetFileInfo(node, fileName);
            Console.WriteLine(string.Format("FileName:{0}", fileName));
            Console.WriteLine(string.Format("FileSize:{0}", fileInfo.FileSize));
            Console.WriteLine(string.Format("CreateTime:{0}", fileInfo.CreateTime));
            Console.WriteLine(string.Format("Crc32:{0}", fileInfo.Crc32));
            //==========================AppendFile=======================================
            FastDFSClient.AppendFile("group1", fileName, content);
            FastDFSClient.AppendFile("group1", fileName, content);


            //===========================RemoveFile=======================================
            FastDFSClient.RemoveFile("group1", fileName);

            //===========================Http测试，流读取=======================================
            string url = "http://img13.360buyimg.com/da/g5/M02/0D/16/rBEDik_nOJ0IAAAAAAA_cbJCY-UAACrRgMhVLEAAD-J352.jpg";
            System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
            System.Net.HttpWebResponse res = (System.Net.HttpWebResponse)req.GetResponse();
            Image myImage = Image.FromStream(res.GetResponseStream());
            myImage.Save("c:\\fast.jpg");//保存
            //===========================Http测试，直接下载=======================================
            WebClient web = new WebClient();
            web.DownloadFile("http://img13.360buyimg.com/da/g5/M02/0D/16/rBEDik_nOJ0IAAAAAAA_cbJCY-UAACrRgMhVLEAAD-J352.jpg", "C:\\abc.jpg");
            web.DownloadFile("http://192.168.81.233/gruop1/M00/00/00/wKhR6VADbNr5s7ODAAIOGO1_YmA574.jpg", "C:\\abc.jpg");

            Console.WriteLine("Complete");
            Console.Read();
        }
    }
}
