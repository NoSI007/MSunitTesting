// (c) Khaled A Alwan .
// All other rights reserved.
using System.IO;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// A sample encrypt / decrypt functions, for unit testing purposes.
/// </summary>

public class DESencDec
{
    string key = "!$%^*@X0";
    /// <summary>
    /// Encrypt the Given file using the DES algo
    /// output the result into the given filename.
    /// </summary>
    /// <param name="InFile">The plain text file name.</param>
    /// <param name="OutFile">encrypted output file name.</param>
    public void Encrypt(string InFile, string OutFile)
    {
        //Create a reader stream
        FileStream inpStream = new FileStream(InFile, FileMode.Open, FileAccess.Read);
        //Create Encryptor Stream output.
        FileStream EncryptedStream = new FileStream(OutFile, FileMode.Create, FileAccess.Write);
        // initialize the Encryptor to transform.
        ICryptoTransform desencrypt = getED(true);
        //Set-up the Encryptor Stream Transformer.
        CryptoStream cryptostream = new CryptoStream(EncryptedStream, desencrypt, CryptoStreamMode.Write);
        //Perform the transformation.
        byte[] bytearrayinput = new byte[inpStream.Length];
        inpStream.Read(bytearrayinput, 0, bytearrayinput.Length);
        cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
        //Close all streams.
        cryptostream.Close(); cryptostream.Dispose();
        inpStream.Close(); inpStream.Dispose();
        EncryptedStream.Close(); EncryptedStream.Dispose();
        desencrypt.Dispose();
    }
    /// <summary>
    /// Decrypt the Given file using the DES algo
    /// output the plain text into the given filename.
    /// </summary>
    /// <param name="sInputFilename">the encrypted file name.</param>
    /// <param name="sOutputFilename">plain text output file name.</param>
    public void Decrypt(string sInputFilename, string sOutputFilename)
    {
        // initialize the Decryptor to transform.
        ICryptoTransform desdecrypt = getED(false);

        //Create a file stream to read the encrypted file back.
        FileStream fsread = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read);
        //Set-up the Decryptor to start work.
        CryptoStream cryptostreamDecr = new CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read);
        //init the output stream.
        StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
        //write the contents of the decrypted file.
        fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
        fsDecrypted.Flush();
        fsDecrypted.Close();fsDecrypted.Dispose();
        fsread.Close();fsread.Dispose();
        desdecrypt.Dispose();
    }
    /// <summary>
    /// initialize an Encryptor / Decryptor class
    /// </summary>
    /// <param name="enc">true/false to Encrypt/Decrypt</param>
    /// <returns>Return the Encryptor / Decryptor transform</returns>
    private ICryptoTransform getED(bool enc)
    {
        DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
        DES.Key = ASCIIEncoding.ASCII.GetBytes(key);
        DES.IV = ASCIIEncoding.ASCII.GetBytes(key);
        ICryptoTransform desencrypt = null;
        if (enc == true)
            desencrypt = DES.CreateEncryptor();
        else
            desencrypt = DES.CreateDecryptor();
        return desencrypt;
    }
}