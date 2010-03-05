/*
DataMatrix.Net

DataMatrix.Net - .net library for decoding DataMatrix codes.
Copyright (C) 2009 Michael Faschinger

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public
License as published by the Free Software Foundation; either
version 3.0 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU General Public
License along with this library; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA

Contact: Michael Faschinger - michfasch@gmx.at
 
*/

using System;
using System.Collections.Generic;
using System.Text;
using DataMatrix.net;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace DataMatrixTest
{
    class Program
    {
        private static string testVal = "Hello World!";

        static void Main(string[] args)
        {
            TestMatrixEnDecoder();
            TestMosaicEnDecoder();
        }

        private static void TestMatrixEnDecoder()
        {
            string fileName = "encodedImg.png";
            DmtxImageEncoder encoder = new DmtxImageEncoder();
            DmtxImageEncoderOptions options = new DmtxImageEncoderOptions();
            options.ModuleSize = 8;
            options.MarginSize = 4;
            options.BackColor = Color.White;
            options.ForeColor = Color.Green;
            Bitmap encodedBitmap = encoder.EncodeImage(testVal);
            encodedBitmap.Save(fileName, ImageFormat.Png);

            DmtxImageDecoder decoder = new DmtxImageDecoder();
            List<string> codes = decoder.DecodeImage((Bitmap)Bitmap.FromFile(fileName), 1, new TimeSpan(0, 0, 3));
            foreach (string code in codes)
            {
                Console.WriteLine("Decoded:\n" + code);
            }

            string s = encoder.EncodeSvgImage("DataMatrix.net rocks!!one!eleven!!111!eins!!!!", 7, 7, Color.FromArgb(100, 255, 0, 0), Color.Turquoise);
            TextWriter tw = new StreamWriter("encodedImg.svg");
            tw.Write(s);
            tw.Flush();
            tw.Close();


            Console.Read();
        }


        private static void TestMosaicEnDecoder()
        {
            string fileName = "encodedMosaicImg.png";
            DmtxImageEncoder encoder = new DmtxImageEncoder();
            DmtxImageEncoderOptions options = new DmtxImageEncoderOptions();
            options.ModuleSize = 8;
            options.MarginSize = 4;
            Bitmap encodedBitmap = encoder.EncodeImageMosaic(testVal);
            encodedBitmap.Save(fileName, ImageFormat.Png);

            DmtxImageDecoder decoder = new DmtxImageDecoder();
            List<string> codes = decoder.DecodeImageMosaic((Bitmap)Bitmap.FromFile(fileName), 1, new TimeSpan(0, 0, 3));
            foreach (string code in codes)
            {
                Console.WriteLine("Decoded:\n" + code);
            }

            Console.Read();
        }

    }
}
