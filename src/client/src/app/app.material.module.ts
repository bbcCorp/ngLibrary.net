import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  MatListModule, MatButtonModule, MatToolbarModule, MatInputModule,
  MatProgressSpinnerModule, MatCardModule,
  MatMenuModule, MatSidenavModule,
  MatIconModule
} from '@angular/material';

@NgModule({
  imports: [
    MatListModule, MatButtonModule, MatToolbarModule, MatInputModule, MatProgressSpinnerModule, MatCardModule,
    MatIconModule, MatSidenavModule, MatMenuModule
  ],
  exports: [
    MatListModule, MatButtonModule, MatToolbarModule, MatInputModule, MatProgressSpinnerModule, MatCardModule,
    MatIconModule, MatSidenavModule, MatMenuModule
  ]
})

export class AppMaterialModule {}
