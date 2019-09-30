using System;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System.Security.Cryptography;
using System.IO;

namespace SQSSample {
	public class S3Sample {

		static AmazonS3Client _client;
		static AmazonS3Client Client {
			get {
				if (_client == null) {
					_client = new AmazonS3Client(
						Config.accessKeyId,
						Config.secretAccessKey,
						RegionEndpoint.USEast1);
				}
				return _client;
			}
		}

		
		public static void UploadObject() {

			PutObjectRequest putObjectRequest = new PutObjectRequest {
				BucketName = Config.s3BucketName
			};

			/*
			 * You can upload to s3 using a stream
			 */
			string filePath = "./testFile.txt";
			putObjectRequest.InputStream = new FileStream("./testFile.txt", FileMode.Open);

			/*
			 * Or you can upload to s3 using a file path
			 * putObjectRequest.FilePath = "./testFile.txt";
			 */


			/*
			 * Sets the file path/name in the s3 bucket
			 */
			putObjectRequest.Key = "theFile.txt";


			PutObjectResponse putObjectResponse = Client.PutObject(putObjectRequest);

			/*
			 * If you want to verify that the file was uploaded correctly,
			 * the ETag is the md5 of the file that was received by amazon.
			 */

			using (var md5 = MD5.Create()) {
				using (var stream = File.OpenRead(filePath)) {
					string eTag = putObjectResponse.ETag.Replace("\"", ""); //Strip the quotes off the ETag.
					string md5String = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower(); //Create md5 string from file
					Console.WriteLine("ETag: " + eTag);
					Console.WriteLine("MD5: " + md5String);
					Console.WriteLine("File MD5 == ETag: " + (eTag == md5String));

				}
			}
		}

		public static void DownloadObject() {

			/*
			 * Create the request using the object key that we set in upload
			 */

			GetObjectRequest getObjectRequest = new GetObjectRequest {
				BucketName = Config.s3BucketName,
				Key = "theFile.txt"
			};

			GetObjectResponse response = Client.GetObject(getObjectRequest);

			/*
			 * If you want to save the file, change the output file path below to the destination
			 */
			string outputFilePath = "<Change me>";
			response.WriteResponseStreamToFile(outputFilePath);

			/*
			 * The ETag of the downloaded file matches the md5 of the file that we uploaded.
			 */
			Console.WriteLine(response.ETag);

		}
	}
}
