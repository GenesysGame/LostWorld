/// <reference path="types-gtanetwork/index.d.ts" />

API.onUpdate.connect(function () {
    var player = API.getLocalPlayer();
    var position = API.getEntityPosition(player);
    var scrSize = API.getScreenResolution();
    var x = scrSize.Width / 3;
    var y = scrSize.Height;
    API.drawText(position.ToString(), x, y, 0.3, 255, 255, 255, 255, 0, 1, false, true, 0);

    API.drawMenu(devMenu); // Draw Dev menu
});

API.onServerEventTrigger.connect(function (name, args) {
    switch (name) {
        case "client:presentStartWindow":
            API.sendNotification("Добро пожаловать на Lost World Role Play!");
            API.sendNotification("Сервер находится в разработке. Оставайтесь с нами :-)");
            presentStartWindow();
            break;
        case "client:readyForPlay":
            startPlaying();
    }
});

function presentStartWindow() {
    let pos = new Vector3(-153, 6985, 196);
    let rot = new Vector3(0, 0, 190);

    let newCamera = API.createCamera(pos, rot);
    API.setActiveCamera(newCamera);

    API.showCursor(true);
    devMenu.Visible = true;
}

function startPlaying() {
    API.setActiveCamera(null);
}

// MARK: - Dev Menu

var devMenu = API.createMenu("Lost World [DEV]", "Выберите действие", 0, 0, 6);
devMenu.ResetKey(menuControl.Back);
devMenu.AddItem(API.createMenuItem("Играть", "Немедленно начать игру"));

devMenu.OnItemSelect.connect(function (sender, item, index) {
    API.showCursor(false);
    devMenu.Visible = false;

    API.triggerServerEvent("server:readyForPlay");
});