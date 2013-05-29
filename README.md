# Sendy.Net
=========
Sendy.Net is a .Net library that can be used to interact with the Sendy API. Subscribe, Unsubscribe, access subscriber status, lists, create and list campaigns.

It has been built to interact version 1.1.6

## When to use it?
If you have recently decided to use [Sendy](http://sendy.co/) over an existing email campaign/newsletter service, then you can simply call the Sendy.Net API from within your code.

It is especially useful if you are creating your own user/client database, so are saving details prior to subscribing them to the email campaign/newsletter service.

## What is Sendy
![Sendy Logo](http://sendy.co/images/sendy-logo.jpg)
> [Sendy](http://sendy.co/) is a self hosted email newsletter application that lets you send trackable emails via [Amazon Simple Email Service SES](http://aws.amazon.com/ses/). This makes it possible for you to send authenticated bulk emails at an insanely low price without sacrificing deliverability.

## The Sendy API
> [Sendy's API](http://sendy.co/api) is based on simple HTTP POST. Use the API to integrate Sendy programmatically with your website or application. We're working to include more APIs.

##Campaign API
The default Sendy API does not contain any functionality for creating Campaigns. You can download a small zip file which contains a Campaign API from [here](http://forum.sendy.co/discussion/768/added-some-api-functionality/p1)

## Helpful Resources

If you would like some help with getting set up, or you are thinking of setting up Sendi on Azure, then take a look at Scott Hanselman's Blog article. 

[Hanselman on Sendy](http://www.hanselman.com/blog/InstallingSendyAPHPAppOnWindowsAzureToSendInexpensiveNewsletterEmailViaAmazonSES.aspx)

If you would like to download the Campaign API, check it out here.

[Campaign API](http://forum.sendy.co/discussion/768/added-some-api-functionality/p1)

## Issues I encountered

###Sendy 1.1.6
My installation of Sendy was on a Windows Server with Windows Server 2008 R2 Standard 64-Bit OS installed.
When setting up the bounces and complaints handling, Amazon AWS would sit at "Pending Confirmation". I delved into the php code, which Amazon AWS contacts when trying to certify your SNS Notification settings, and noticed that there is a line of code like this;
    
    $server_path_array = explode('includes/campaigns/bounces.php', $_SERVER['SCRIPT_FILENAME']);

This looked harmful enough, until I realised that `$_SERVER['SCRIPT_FILENAME'])` was returning the path with backward slashes '\' instead of the expected forward slash '/'. As a result, the `certs/cacert.pem` file was not found and not certified.

[Leonardo Cortez](http://wishfulcoding.com/) came up with an elegant solution to solve this problem by updating the php code to replace the slashes. So wherever you see a reference to `$_SERVER['SCRIPT_FILENAME']`, replace the code with;

    strtr($_SERVER['SCRIPT_FILENAME'], '\\', '/')    

###Campaign API
The Campaign API would fail if your administrator user had set up a brand using the same email address. The Campaign API would attempt to get the app from the DB based on the email, but would not find it. This would basically break the whole api.

## Questions?
Catch me in [JabbR](https://jabbr.net/#/rooms/general-chat) if you can.