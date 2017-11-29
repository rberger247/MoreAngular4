import { Component, OnInit } from '@angular/core';



import { VehicleService } from "../../Services/VehicleService";




@Component({
    selector: 'vehicleform',
    templateUrl: './vehicle-form.component.html',
    styleUrls: ['./vehicle-form.component.css'],
    providers: [VehicleService]
  
}

)
export class VehicleFormComponent implements OnInit {
   
    makes: any[];
    models: any[];
    features: any[];
    vehicle: any = {};

    constructor(private vehicleService: VehicleService) {}

    ngOnInit() {
        this.vehicleService.getMakes().subscribe(makes =>
            this.makes = makes);

        this.vehicleService.getFeatures().subscribe(features =>
            this.features = features);
    }

    onMakeChange() {
        var selectedMake = this.makes.find(m => m.id == this.vehicle.make);
        this.models = selectedMake ? selectedMake.models : [];
    }
}