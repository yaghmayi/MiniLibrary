import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { IItem } from "../interfaces/item.interface";
import { ICategory } from "../interfaces/category.interface";
import { HttpHelpersService } from "./http-helpers.service";

@Injectable()
export class ItemService {

  private itemsApiAddress: string;

  constructor(private readonly http: HttpClient, private readonly httpHelpers: HttpHelpersService) {
    this.itemsApiAddress = this.httpHelpers.getApiBaseUrl() + "items/";
  }

  getItemList() {
    return this.http.get<IItem[]>(this.itemsApiAddress + "list");
  }

  getItem(id: number) {
    return this.http.get<IItem>(this.itemsApiAddress + "getById/" + id, this.httpHelpers.getHttpOptions());
  }

  saveItem(item : IItem) {
    return this.http.post(this.itemsApiAddress + "save", JSON.stringify(item), this.httpHelpers.getHttpOptions());
  }

  deleteItem(id: number) {
    return this.http.delete(this.itemsApiAddress + "delete/" + id, this.httpHelpers.getHttpOptions());
  }

  getCategoryList() {
    return this.http.get<ICategory[]>(this.itemsApiAddress + "categories");
  }

  
}
