"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var AlpacaService = /** @class */ (function () {
    function AlpacaService(http) {
        this.http = http;
        this.alpacaApiAddress = "nexerTest/api/alpaca/";
    }
    AlpacaService.prototype.getAlpacaList = function () {
        return this.http.get(this.alpacaApiAddress + "list");
    };
    return AlpacaService;
}());
exports.AlpacaService = AlpacaService;
//# sourceMappingURL=alpaca.service.js.map