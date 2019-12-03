# SQS Sample
#### Uses the AWS SDK for .NET

*For demonstration purposes only*

Instructions  

1) `git clone git@github.com:itsdrew/SQSSample.git`
1) Open in an IDE
1) Replace the empty strings in Config.cs using the provided values
1) To generate sample messages, run Program.Main()
1) To read and acknowledge messages, in Program.Main() comment out the line with Producer.execute() and uncomment the line with Consumer.execute()

# S3 Sample

##### S3Sample.UploadObject()
Uploads the included testFile.txt to the S3 bucket (the name is set in the config file already).

##### S3Sample.DownloadObject()
Downloads the object and writes it to a file. For this to work, you need to change the file path in the DownloadObject() method.



