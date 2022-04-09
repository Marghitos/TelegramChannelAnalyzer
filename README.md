# Telegram channel analyzer

This application enables fetching messages from a telegram public channel and it's based on .NET 6.0. The structure of the solution is based on [this approach](https://github.com/rallets/console-app-net5)
Before starting the application you need to follow a few steps:

## Create a telegram application

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

## Analyzing data with PowerBi
Data obtained by this application can be parsed by the PowerBi report provided inside the same github repository. 

In this case I analyzed this telegram channel, which reports news about the Ukraine - Russia war which broke on the 24th February 2022
![enter image description here](https://raw.githubusercontent.com/Marghitos/TelegramChannelAnalyzer/main/TelegramChannelRetriever/Images/TheKyivIndipendentLogo.png)
### Reaction count grouped by positive, negative and neutral by date
![enter image description here](https://raw.githubusercontent.com/Marghitos/TelegramChannelAnalyzer/main/TelegramChannelRetriever/Images/ReactionCount.png)
### Number of messages posted by date
![enter image description here](https://raw.githubusercontent.com/Marghitos/TelegramChannelAnalyzer/main/TelegramChannelRetriever/Images/MessageCountByDate.png)
### Word cloud

![enter image description here](https://raw.githubusercontent.com/Marghitos/TelegramChannelAnalyzer/main/TelegramChannelRetriever/Images/WordCloud.png)
