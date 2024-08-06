# Toxiq Blazor Webapp

### Technology
- [Blazor Server](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor)
- [Masa Blazor](https://docs.masastack.com/blazor/getting-started/installation#)
- [telegram-web-app.js](https://telegram.org/js/telegram-web-app.js)

## Bad Code Warning
this code is not meant to be for prod use. its just a show case of how to interact with the api and a proof of concept for a telegram based webapp for ```Toxiq```

- code for this is incredibly inefficent     
it auths the user in on every page load and there is 0 state management
realesticly the code should take advantage of [cloud_storage](https://docs.telegram-mini-apps.com/packages/telegram-apps-sdk/components/cloud-storage) feature which is provided by [telegram-web-app.js](https://telegram.org/js/telegram-web-app.js) but this becomes a mess when a user loads the link outside of telegram  
so for the time being i decided the best way to make the webapp work would be to do a login on each page load. 



### How Telegram Auth Works
there is a few hoops jumped through to get tele to work with blazor  
most of the vanilla js code that we call is stored on [telegramInterop.js](/UnSocial.WebApp/wwwroot/js/telegramInterop.js)  
the methods that are declared in  [telegramInterop.js](/UnSocial.WebApp/wwwroot/js/telegramInterop.js) is called by C# code in  [ITelegramJsInterop.cs](/UnSocial.WebApp/Services/ITelegramJsInterop.cs)

#### [example code](https://github.com/FishieDotCom/Toxiq-Blazor-WebApp/blob/39dabb7909e6a690035e886cdaf2094566d49a51/UnSocial.WebApp/Pages/Profile.razor#L127)
``` C#
var initData = await telegram.InitializeTelegramWebApp();
var login = await ApiService.AuthService.Login(new Toxiq.Mobile.Dto.LoginDto { PhoneNumber = "str", OTP = initData });
if (login.token != "NA")
    IsAuth = true;

myProfile = await ApiService.UserService.GetUser(username);
```

#### Telegram initData explained
[AuthService.cs](/UnSocial.WebApp/Services/OnlineDataService/AuthService.cs) needs initData from [ITelegramJsInterop.cs](/UnSocial.WebApp/Services/ITelegramJsInterop.cs) which is a wrapper around [telegramInterop.js](/UnSocial.WebApp/wwwroot/js/telegramInterop.js) which is a wrapper around [telegram-web-app.js](https://telegram.org/js/telegram-web-app.js)

this data is then sent to the server which first [verifies the hash](https://core.telegram.org/bots/webapps#validating-data-received-via-the-mini-app) of the data  
once verified the server then makes sure that the [telegram_user_id](https://github.com/FishieDotCom/Toxiq-Legal/blob/main/Privacy%20Policy.md#information-we-collect-via-telegram-bot) matches whats in the user db  
if a match is found then the server gens a [JTW Token](https://github.com/FishieDotCom/Toxiq-API-Docs?tab=readme-ov-file#auth-schema) and returns


[More Details](https://github.com/FishieDotCom/Toxiq-API-Docs/blob/main/Endpoints/Login.md#login-via-telegram)



### Build from source
the following code cannot be built from source as its only open sourced as an example for how to use Toxiq Api.  
this code is a very stripped down version made as a Proof of concept only meant to be used as a placeholder while WebDev team finishes working on [Prod Build](https://github.com/FishieDotCom/toixiq-webapp)