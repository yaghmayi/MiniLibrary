import { Component } from '@angular/core';
import { ItemService } from "../services/item.service";
import { IItem } from "../interfaces/item.interface";

@Component({
  selector: 'item-list',
  templateUrl: './item-list.component.html',
})

export class ItemListComponent {
  items: IItem[] = [];

  constructor(private readonly service: ItemService) {
  }

  ngOnInit() {
    this.loadItems();
  }

  private loadItems() {
    this.service.getItemList().subscribe(result => { this.items = result; });
  }
}
