import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { VenueComponent } from './venue.component';
import { VenueService } from './venue.service';

@NgModule({
  declarations: [
    VenueComponent
  ],
  imports: [
    HttpModule,
    CommonModule,
    FormsModule
  ],
  providers: [VenueService],
  exports: [VenueComponent]
})
export class VenueModule { }
