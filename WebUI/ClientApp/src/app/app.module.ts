import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { ItemListComponent } from './item-list/item-list.component';
import { ItemEditComponent } from './item-edit/item-edit.component';
import { ItemService } from './services/item.service';
import { HttpHelpersService } from "./services/http-helpers.service";

@NgModule({
  declarations: [
    AppComponent,
    ItemListComponent,
    ItemEditComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: ItemListComponent },
      { path: 'listitems', component: ItemListComponent },
      { path: 'edititem', component: ItemEditComponent }
    ])
  ],
  providers: [
      HttpHelpersService,
      ItemService
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
