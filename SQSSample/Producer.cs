using System;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace SQSSample {
	public class Producer {

		// Create the AmazonSQSClient using values from the Config.class
		static AmazonSQSClient _client;
		static AmazonSQSClient Client {
			get {
				if (_client == null) {
					_client = new AmazonSQSClient(
						Config.accessKeyId,
						Config.secretAccessKey,
						RegionEndpoint.USEast1);
				}
				return _client;
			}
		}




		public static void execute() {

			// Put 50 messages in the queue
			int messageCount = 50;
			PushMessagesToQueue(" -- This is a message at " + DateTime.Now.ToShortTimeString() + " -- ", messageCount);

		}




		// There is a batch send message method that can be used instead of this.
		public static void PushMessagesToQueue(string messageBody, int messageCount) {

			for (int i = 0; i < messageCount; i++) {

				SendMessageRequest sendMessageRequest = new SendMessageRequest {
					QueueUrl = Config.queueUrl,
					MessageBody = messageBody + " | " + i
				};


				SendMessageResponse sendMessageResponse = Client.SendMessage(sendMessageRequest);
				Console.WriteLine("Sent message {0} status: {1}", i, sendMessageResponse.HttpStatusCode);
			};
		}
	}
}
