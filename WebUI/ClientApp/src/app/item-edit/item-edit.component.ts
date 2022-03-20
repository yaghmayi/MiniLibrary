import { Component } from '@angular/core';
import { ItemService } from "../services/item.service";
import { IItem } from "../interfaces/item.interface";
import {Router} from '@angular/router';
import { ICategory } from "../interfaces/category.interface";

@Component({
  selector: 'item-edit',
  templateUrl: './item-edit.component.html',
})

export class ItemEditComponent {
  item: IItem = {};
  categories : ICategory[] = [];

  oldColorValue : string = null;

  pageTitle: string;
  saveButtonCaption : string;
  deleteButtonVisibility: boolean;

  constructor(private readonly service: ItemService, private readonly route:Router) {
  }

  ngOnInit() {
    this.loadFarms();
    let id = this.getIdParam();


    if (id === "-1") {
        this.pageTitle = "New Item";
        this.saveButtonCaption = "Save";
        this.deleteButtonVisibility = false;

        this.item = {
           id: -1,
           category: {}
        };

    } else {
        this.pageTitle = "Edit Item";
        this.saveButtonCaption = "Update";
        this.deleteButtonVisibility = true;

        this.loadItem(Number(id));
    }
  }

  onSave() {
    this.saveItem();
  }

  onCancel() {
    this.route.navigate(['/listitem']);
  }

  onDelete() {
    if (window.confirm("Are you sure to delete the Item?")) {
       this.service.deleteItem(this.item.id).subscribe(() => { this.route.navigate(['/listitem']); });
    }
  }

  private saveItem() {
    this.service.saveItem(this.item).subscribe(() => { this.route.navigate(['/listitems']); });
  }

  private loadItem(id: number) {
    this.service.getItem(id).subscribe(result => {
        this.item = result;
      }
    );
  }

  private loadFarms() {
    this.service.getCategoryList().subscribe(result =>
      this.categories = result
    );
  }

  getIdParam() : string {
      const parameters = new URLSearchParams(window.location.search);
      let idValue = parameters.get("id");

      return idValue;
  }

  isValidForm() : boolean {
    let isValid = true;

    if (this.isNullOrEmpty(this.item.name) || this.isNullOrEmpty(this.item.category.code)) {
      isValid = false;
    }

    return isValid;
  }

  isNullOrEmpty(val: number | string) : boolean {
    return val == null || val === '' || val.toString().trim() === '';
  }
}
