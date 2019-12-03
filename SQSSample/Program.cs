using System;
namespace SQSSample {
	public class Program {

		public static void Main(String[] args) {

			//Producer.execute();
			//Consumer.execute();

			//S3Sample.UploadObject();
			//S3Sample.DownloadObject();

			SNSSample.SendMessage("Hello this is a message");
		
		}
	}
}
