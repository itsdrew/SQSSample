using System;
namespace SQSSample {

	/*
	 * For demonstration purposes only.
	 * Normally these would be externalized.
	 */ 

	public class Config {
	
		public static string queueName = "";
		public static string queueUrl = "";

		public static string accessKeyId = "";
		public static string secretAccessKey = "";

		public static string s3BucketName = "";

		public static string topicArn = "arn:aws:sns:us-east-1:023391462456:test-Computershare-API-dead-letter-topic";
	}
}
