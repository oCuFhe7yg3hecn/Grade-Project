import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule, MatButtonModule, MatSidenavModule, MatIconModule, MatListModule, MatGridListModule, MatCardModule, MatMenuModule, MatTableModule, MatPaginatorModule, MatSortModule } from '@angular/material';

import { RouterModule, Routes } from '@angular/router';
import { TestTableComponent } from './test-table/test-table.component';
import { MaterialsImportModule } from './materials-import/materials-import.module';
import { ItemsDashboardComponent } from './items-dashboard/items-dashboard.component';
import { GamesCatalogComponent } from './games-catalog/games-catalog.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    TestTableComponent,
    ItemsDashboardComponent,
    GamesCatalogComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatGridListModule,
    MatCardModule,
    MatMenuModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MaterialsImportModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
