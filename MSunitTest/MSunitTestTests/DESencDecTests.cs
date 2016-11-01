// (c) Khaled A Alwan .
// All other rights reserved.
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass()]
    public class DESencDecTests
    {
        const string orPlain = "original.txt";//input plain text
        const string ExPlain = "expected.txt";//test plain output text should match orignal after decrypt is done.
        const string Encrypted = "Encrypted"; //the encryption results save here.
        public DESencDecTests()
        {

        }

        [TestMethod()]
        [Priority(0)]
        public void CleanPrevTest()
        {
            if (File.Exists(orPlain) == true)
                File.Delete(orPlain);
            if (File.Exists(ExPlain) == true)
                File.Delete(ExPlain);
            if (File.Exists(Encrypted) == true)
                File.Delete(Encrypted);
        }

        [TestMethod()]
        [Priority(1)]
        public void makePlainTextTestFile()
        {
            StreamWriter sw = new StreamWriter(orPlain);
            for (int i = 0; i < 10; i++)
            {
                sw.Write("testing, testing, {0}{1}{2}", i, i, i);
                sw.Write("yet another line");
            }
            sw.Close(); sw.Dispose();
        }

        [TestMethod()]
        [Priority(10)]
        public void EncryptTest()
        {
            Assert.IsTrue(File.Exists(orPlain));
            Assert.IsTrue(File.ReadAllBytes(orPlain).Length > 0);
            DESencDec test = new DESencDec();
            test.Encrypt(orPlain, Encrypted);
            Assert.IsTrue(File.Exists(Encrypted));
            Assert.IsTrue(File.ReadAllBytes(Encrypted).Length > 0);
        }

        [TestMethod()]
        [Priority(11)]
        public void DecryptTest()
        {
            Assert.IsTrue(File.Exists(orPlain));
            Assert.IsTrue(File.ReadAllBytes(orPlain).Length > 0);

            DESencDec test = new DESencDec();


            test.Decrypt(Encrypted, ExPlain);

            Assert.IsTrue(File.Exists(ExPlain));
            Assert.IsTrue(File.ReadAllBytes(ExPlain).Length > 0);

            StreamReader orignal = new StreamReader(orPlain);
            StreamReader expected = new StreamReader(ExPlain);

            string orignalStr = orignal.ReadToEnd(); orignal.Close(); orignal.Dispose();
            string expectedStr = expected.ReadToEnd(); expected.Close(); expected.Dispose();

            Assert.AreEqual(orignalStr, expectedStr);
        }

      


    }
}