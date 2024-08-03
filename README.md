# Toxiq Blazor Webapp

### Technology
- [Blazor Server](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor)
- [Masa Blazor](https://docs.masastack.com/blazor/getting-started/installation#)
- [telegram-web-app.js](https://telegram.org/js/telegram-web-app.js)

### How Telegram Auth Works
there is a few hoops jumped through to get tele to work with blazor  
most of the vanilla js code that we call is stored on [telegramInterop.js](/UnSocial.WebApp/wwwroot/js/telegramInterop.js)  
the methods that are declared in  [telegramInterop.js](/UnSocial.WebApp/wwwroot/js/telegramInterop.js) is called by C# code in  [ITelegramJsInterop](/UnSocial.WebApp/Services/ITelegramJsInterop.cs)

#### [example code](https://github.com/FishieDotCom/Toxiq-Blazor-WebApp/blob/39dabb7909e6a690035e886cdaf2094566d49a51/UnSocial.WebApp/Pages/Profile.razor#L127)
``` C#
var initData = await telegram.InitializeTelegramWebApp();
var login = await ApiService.AuthService.Login(new Toxiq.Mobile.Dto.LoginDto { PhoneNumber = "str", OTP = initData });
if (login.token != "NA")
    IsAuth = true;

myProfile = await ApiService.UserService.GetUser(username);
```

[More Details](https://github.com/FishieDotCom/Toxiq-API-Docs/blob/main/Endpoints/Login.md#login-via-telegram)

### Build from source
the following code cannot be built from source as its only open sourced as an example for how to use Toxiq Api.