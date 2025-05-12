window.initializeTelegramWebApp = () => {
    Telegram.WebApp.headerColor = "#1f1f1f"
    Telegram.WebApp.backgroundColor = "#1f1f1f"
    Telegram.WebApp.ready();
    Telegram.WebApp.expand();
    return Telegram.WebApp.initData;
};

window.shareToTele = (url) => {
    const array = ['https://t.me/share?url=',url];
    Telegram.WebApp.openTelegramLink(array.join(''));
};

window.showButton = () => {
    const mainButton = Telegram.WebApp.MainButton;
    mainButton.show();
};

window.hideButton = () => {
    const mainButton = Telegram.WebApp.MainButton;
    mainButton.hide();
};

window.close = () => {
    Telegram.WebApp.close();
};

window.saveToCloud = (key, value) => {
    Telegram.WebApp.storage.set(key, value).then(() => {
        console.log(`Saved to cloud storage: ${key} = ${value}`);
    }).catch((error) => {
        console.error(`Error saving to cloud storage: ${error}`);
    });
};

window.loadFromCloud = (key) => {
    return Telegram.WebApp.storage.get(key).then((value) => {
        document.getElementById('cloud-data').innerText = `Cloud data: ${value}`;
        return value;
    }).catch((error) => {
        console.error(`Error loading from cloud storage: ${error}`);
    });
};

window.showAlert = (msg) => {
    Telegram.WebApp.showAlert(msg);
};