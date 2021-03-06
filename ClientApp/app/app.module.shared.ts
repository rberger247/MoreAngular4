import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { NgModule, ErrorHandler } from '@angular/core';
import { RouterModule } from '@angular/router';
import * as Raven  from 'raven-js';
import{ ToastyModule} from 'ng2-toasty'
//import { UniversalModule } from 'angular2-universal';

import { VehicleService } from './services/vehicleService';
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { VehicleFormComponent } from "./components/vehicleform/vehicleform.component";
import { AppErrorHandler } from "./app.error-handler";
import { VehicleListComponent } from "./components/vehicle-list/vehicle-list.component";
import { PaginationComponent } from "./components/shared/pagination.component";
import { ViewVehicleComponent } from "./components/view-vehicle/view-vehicle.component";
import { PhotoService } from "./Services/PhotoService";

Raven.config('https://fa7751a1fbec47a695a2db3339a196c9@sentry.io/255301')
  .install();


@NgModule({
    declarations: [
        AppComponent,
        PaginationComponent,
        NavMenuComponent,
        CounterComponent,
        ViewVehicleComponent,
        FetchDataComponent,
        HomeComponent,
        VehicleFormComponent,
        VehicleListComponent
    ],
    imports: [
        CommonModule,
      //  UniversalModule,
        ToastyModule.forRoot(),
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            //{ path: '', redirectTo: 'vehicles', pathMatch: 'full' },
            { path: 'vehicles/new', component: VehicleFormComponent },
            { path: 'vehicles/edit/:id', component: VehicleFormComponent },
            { path: 'vehicles/:id', component: ViewVehicleComponent },
            { path: 'vehicles', component: VehicleListComponent },
  
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
          
           { path: '**', redirectTo: 'vehicles' }
        ])

    ],
    providers: [
        { provide: ErrorHandler, useClass: AppErrorHandler },
        VehicleService,
        PhotoService
    ]
})
export class AppModuleShared {
}
