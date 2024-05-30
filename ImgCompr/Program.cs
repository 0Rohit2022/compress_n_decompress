// using System;
// using System.IO;
// using System.IO.Compression;

// class Program
// {
//     static void Main(string[] args)
//     {
//         // Specify the path to the image file
//         string imagePath = "./kalilinux.jpg";
//         Console.WriteLine("Original image size: " + new FileInfo(imagePath).Length + " bytes");

//         // Compress the image data
//         string compressedImagePath = CompressImage(imagePath);

//         if (!string.IsNullOrEmpty(compressedImagePath))
//         {
//             // Decompress the compressed image data
//             DecompressImage(compressedImagePath);
//         }
//     }

//     static string CompressImage(string imagePath)
//     {
//         string compressedImagePath = Path.ChangeExtension(imagePath, ".compressed.jpg");

//         try
//         {
//             using (FileStream input = File.OpenRead(imagePath))
//             using (FileStream output = File.Create(compressedImagePath))
//             using (DeflateStream deflateStream = new DeflateStream(output, CompressionMode.Compress))
//             {
//                 input.CopyTo(deflateStream);
//             }

//             Console.WriteLine("Compressed image saved as: " + compressedImagePath);
//             return compressedImagePath;
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine("Error compressing image: " + ex.Message);
//             return null;
//         }
//     }

//     static void DecompressImage(string compressedImagePath)
//     {
//         try
//         {
//             string decompressedImagePath = Path.ChangeExtension(compressedImagePath, ".decompressed.jpg");

//             using (FileStream input = File.OpenRead(compressedImagePath))
//             using (FileStream output = File.Create(decompressedImagePath))
//             using (DeflateStream deflateStream = new DeflateStream(input, CompressionMode.Decompress))
//             {
//                 deflateStream.CopyTo(output);
//             }

//             Console.WriteLine("Decompressed image saved as: " + decompressedImagePath);
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine("Error decompressing image: " + ex.Message);
//         }
//     }
// }





using System;
using System.IO;
using System.IO.Compression;

class Program
{
    static void Main(string[] args)
    {
        // Specify the path to the image file
        string imagePath = "./30mb.jpg";
        Console.WriteLine("Original image size: " + new FileInfo(imagePath).Length + " bytes");

        // Read the image file into a byte array
        byte[] imageData = File.ReadAllBytes(imagePath);

        // Compress the image data
        byte[] compressedImageData = CompressImage(imageData);

        // Save the compressed image data to a file
        string compressedImagePath = "./compressed_image.jpg";
        File.WriteAllBytes(compressedImagePath, compressedImageData);
        Console.WriteLine("Compressed image saved as: " + compressedImagePath);

        // Decompress the compressed image data
        byte[] decompressedImageData = DecompressImage(compressedImageData);

        // Write the decompressed image data to a file
        string decompressedImagePath = "./decompressed_image.jpg";
        File.WriteAllBytes(decompressedImagePath, decompressedImageData);
        Console.WriteLine("Decompressed image saved as: " + decompressedImagePath);
    }

    static byte[] CompressImage(byte[] imageData)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress))
            {
                deflateStream.Write(imageData, 0, imageData.Length);
            }

            return memoryStream.ToArray();
        }
    }

    static byte[] DecompressImage(byte[] compressedImageData)
    {
        using (MemoryStream memoryStream = new MemoryStream(compressedImageData))
        using (MemoryStream decompressedStream = new MemoryStream())
        using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress))
        {
            deflateStream.CopyTo(decompressedStream);
            return decompressedStream.ToArray();
        }
    }
}
