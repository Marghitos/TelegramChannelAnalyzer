# Telegram channel analyzer

This application enables fetching messages from a telegram public channel and it's based on .NET 6.0. The structure of the solution is based on [this approach](https://github.com/rallets/console-app-net5)
Before starting the application you need to follow a few steps:

# Create a telegram application

Navigate to https://my.telegram.org/ to create an application.
After creating it take note of the API id and API hash.

## Configuring the appsettings.json

Before starting the application you need to configure the appsettings.json. Below you can find the explanation of the settings:
**Telegram**
 - ApiId : API id of the telegram application 
 - ApiHash: API hash of the telegram application
 - PhoneNumber: Phone number associated with the telegram application
  
**Channel**
 - Id: Id of the channel you want to retrieve messages
 
**StopWords**
 - CultureName: Specify the name of the culture you want the stop words functionality
 
**OutputFile**
 - Path: Set the path for the output file
 - Name: Set the name for the output file


