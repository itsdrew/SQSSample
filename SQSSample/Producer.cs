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

		static string payload = "{\"Last_Modified\":null," +
			"\"Client_Number\":\"2-3-30132\"," +
			"\"Account_ID\":\"99999\"" +
			"\"Item_Number\":\"2017-277\"," +
			"\"Client_Entity\":\"Sodexo, Inc.\"," +
			"\"Entity_ID\":\"77777\"," +
			"\"Title_of_Action\":\"Midland Funding LLC vs. Williams, Robin\"," +
			"\"Document_Served1\":\"Doc Title 1\\r\\nDoc Title 2\"," +
			"\"Document_Served2\":null," +
			"\"Court_Agency\":\"Cobb County State Court\"," +
			"\"State_Served\":null," +
			"\"Case_Num\":\"CN: 17-G-654\"," +
			"\"Case_Type\":\"CaseType: Garnishment\"," +
			"\"Method_of_Service\":\"USPS\"," +
			"\"Date_Received\":\"12/20/2018\"," +
			"\"Date_to_Client\":\"12/20/2018\"," +
			"\"Num_Answer_Due\":\"5\"," +
			"\"Answer_Due_Date\":\"12/25/2018\"," +
			"\"SOP_Sender\":\"SOPSender: Requester name\"," +
			"\"Tracking_Num\":\"T:39248\"," +
			"\"Handled_By\":\"HandledBy: 111\"," +
			"\"Notes\":\"My Notes\"," +
			"\"Image_Link\":\"http://africau.edu/images/default/sample.pdf\"," +
			"\"Image_MD5\":null}";




		public static void execute() {

			// Put 50 messages in the queue
			int messageCount = 50;
			PushMessagesToQueue(payload, messageCount);

		}




		// There is a batch send message method that can be used instead of this.
		public static void PushMessagesToQueue(string messageBody, int messageCount) {

			for (int i = 0; i < messageCount; i++) {

				SendMessageRequest sendMessageRequest = new SendMessageRequest {
					QueueUrl = Config.queueUrl,
					MessageBody = messageBody
				};


				SendMessageResponse sendMessageResponse = Client.SendMessage(sendMessageRequest);
				Console.WriteLine("Sent message {0} status: {1}", i, sendMessageResponse.HttpStatusCode);
			};
		}
	}
}
