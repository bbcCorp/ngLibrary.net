// Access public modules from other javascript files (ref:ES6 Modules)
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { SharedModule } from '@app/shared';
import { CoreModule } from '@app/core';

import { SettingsModule } from './settings';
import { StaticModule } from './static';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

// Services
import { LibraryService } from './library.service';

// Custom components
import { DashboardComponent } from './dashboard/dashboard.component';
import { AdminComponent } from './admin/admin.component';
import { TransactComponent } from './transact/transact.component';
import { AboutComponent } from './about/about.component';


// NgModule is a decorator function that takes a single metadata object whose properties describe the module.
@NgModule({
  // other modules whose exported classes are needed by component templates declared in this module.
  imports: [
    // angular
    BrowserAnimationsModule,
    BrowserModule,
    HttpClientModule,
    FormsModule,

    // core & shared
    CoreModule,
    SharedModule,

    // features
    StaticModule,
    SettingsModule,

    // app
    AppRoutingModule
  ],

  // declarations contain the view classes that belong to this module
  declarations: [
    AppComponent,
    DashboardComponent,
    AdminComponent,
    TransactComponent,
    AboutComponent
  ],

  // The main application view, called the root component, that hosts all other app views.
  // Only the root module should set this bootstrap property.
  bootstrap: [AppComponent],

  // providers contain the creators of services that this module contributes to the global collection of services;
  // they become accessible in all parts of the app.
  providers: [
    { provide: 'libraryService', useClass: LibraryService },
    { provide: 'apiUrl', useValue: 'http://localhost:5000/api/' } ],

  // exports contain the subset of declarations that should be visible and usable in the component templates of other modules.
  // A root module has no reason to export anything because other components don't need to import the root module.
  exports: [],

  // Since we have custom HTML tags, we need to specify schemas
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ]
})

// AppModule is exposed to other javascript files (ref:ES6 Modules)
export class AppModule { }
