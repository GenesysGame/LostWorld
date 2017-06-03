/// <reference path="types-gtanetwork/index.d.ts" />
"use strict";
API.onUpdate.connect(function () {
    var player = API.getLocalPlayer();
    var position = API.getEntityPosition(player);
    var scrSize = API.getScreenResolution();
    var x = scrSize.Width / 3;
    var y = scrSize.Height;
    API.drawText(position.ToString(), x, y, 0.3, 255, 255, 255, 255, 0, 1, false, true, 0);
});
