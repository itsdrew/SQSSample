using System;
using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace SQSSample {
	public class SNSSample {

		static AmazonSimpleNotificationServiceClient _client;
		static AmazonSimpleNotificationServiceClient Client {
			get {
				if (_client == null) {
					_client = new AmazonSimpleNotificationServiceClient(
						Config.accessKeyId,
						Config.secretAccessKey,
						RegionEndpoint.USEast1);
				}
				return _client;
			}
		}

		public static void SendMessage(String message) {
			PublishRequest publishRequest = new PublishRequest {
				TopicArn = Config.topicArn,
				Message = message
			};
			Client.Publish(publishRequest);
		}

		/*
		 * 
		 * If you catch an error, send a message to the topic with the body of the message from the queue
		 * and api type / region.
		 * After successfully posting the message to the topic, delete the message from the queue.
		 * Anyone who is subscribed to the topic will receive an email	 
		 * 
		 */
	}
}
