import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { LibraryService } from './library.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppMaterialModule } from './app.material.module';
import { RoutingModule } from './/routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AdminComponent } from './admin/admin.component';
import { TransactComponent } from './transact/transact.component';
import { AboutComponent } from './about/about.component';

@NgModule({
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    AppMaterialModule,
    RoutingModule
  ],
  declarations: [
    AppComponent,
    DashboardComponent,
    AdminComponent,
    TransactComponent,
    AboutComponent
  ],
  bootstrap: [AppComponent],
  providers: [
    { provide: 'libraryService', useClass: LibraryService },
    { provide: 'apiUrl', useValue: 'http://localhost:5000/api/' } ]
})
export class AppModule { }
