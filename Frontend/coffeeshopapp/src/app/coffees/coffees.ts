import { Component, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { toSignal } from '@angular/core/rxjs-interop';
import { SlicePipe } from '@angular/common';
@Component({
    selector: 'app-coffees',
    imports: [SlicePipe],
    templateUrl: './coffees.html',
    styles: [''],
})
export class Coffees {
    private http = inject(HttpClient);

    readonly coffees = toSignal(this.http.get<Coffee[]>('http://localhost:5001/coffees'), {
        initialValue: [],
    });
}

interface Coffee {
    id: string;
    name: string;
    description: string;
    price: number;
    imageUrl: string;
    coffeeType: string;
}
