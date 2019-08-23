using System;
using System.Collections.Generic;
using System.Net;
using Amazon;
using Amazon.Runtime.CredentialManagement;
using Amazon.SQS;
using Amazon.SQS.Model;
using System.Linq;

namespace SQSSample {

	class MainClass {

		//Create the AmazonSQSClient using values from the Config.class
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


		public static void Main(string[] args) {

			//Put 50 messages in the queue
			int messageCount = 50;
			PushMessagesToQueue(" -- This is a message at " + DateTime.Now.ToShortTimeString() + " -- ", messageCount);

			//Read 1 message from the queue
			List<Message> messages = GetMessagesFromQueue(1);

			Console.WriteLine("\nReceived {0} messages\n", messages.Count);

			messages.ForEach(msg => { 
				Console.WriteLine("Message Id: {0}, Message body: {1}", msg.MessageId, msg.Body); 
			});

			//Delete message that was received (or don't)
			messages.ForEach(msg => {
				DeleteMessageFromQueue(msg.ReceiptHandle);
			});


			//Read the rest of the queue 10 at a time. 
			//After each message is pulled, it will be invisible to any other consumers for a pre-set amount of time called the 'visibility timeout'.
			//Visibility timeout is currently set to 10 seconds.
			//If you don't delete the message within the visibility timeout, it can be dequeued again.

			do {
				messages.Clear();
				messages = GetMessagesFromQueue(10);

				Console.WriteLine("\nReceived {0} messages\n", messages.Count);

				messages.ForEach(msg => {
					Console.WriteLine("Message Id: {0}, Message body: {1}", msg.MessageId, msg.Body);
					DeleteMessageFromQueue(msg.ReceiptHandle);
				});
			} while (messages.Any());
		}


		//There is a batch send message method that can be used instead of this.
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


		//Returns a list of messages in the queue. Does not delete the messages. Max quantity per request is 10.
		public static List<Message> GetMessagesFromQueue(int quantity) {

			ReceiveMessageRequest receiveMessageRequest = new ReceiveMessageRequest {
				QueueUrl = Config.queueUrl,
				MaxNumberOfMessages = quantity
			};

			ReceiveMessageResponse response = Client.ReceiveMessage(receiveMessageRequest);

			return response.Messages;
		}



		//Deletes a single message from queue.
		public static void DeleteMessageFromQueue(string receiptHandle) {

			DeleteMessageRequest deleteMessageRequest = new DeleteMessageRequest {
				QueueUrl = Config.queueUrl,
				ReceiptHandle = receiptHandle
			};

			DeleteMessageResponse response = Client.DeleteMessage(deleteMessageRequest);

			if (!response.HttpStatusCode.Equals(HttpStatusCode.OK)) {
				Console.WriteLine("Delete failed");
			}

		}
	}
}
