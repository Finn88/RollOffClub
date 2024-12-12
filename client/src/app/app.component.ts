import { AsyncPipe, NgFor } from '@angular/common';
import { Component, OnInit, } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { of, from, Observable, tap } from 'rxjs';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgFor, AsyncPipe, FormsModule, ReactiveFormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  continentSelect = new FormControl();
  countrySelect = new FormControl();
  continents$: Observable<Continent[]> | undefined;
  continent$: Observable<Continent> | undefined;
  countries: Array<Country> | undefined;

  constructor(private fb: FormBuilder) {
  }

  ngOnInit(): void {
    this.continents$ = of([
      {
        id: 0,
        name: 'Africa',
        countries: [
          { id: 1, name: 'Nigeria' },
          { id: 2, name: 'Egypt' },
        ],
      },
      {
        id: 1,
        name: 'Asia',
        countries: [
          { id: 3, name: 'China' },
          { id: 4, name: 'India' },
        ],
      },
      {
        id: 2,
        name: 'Europe',
        countries: [
          { id: 5, name: 'Germany' },
          { id: 6, name: 'France' },
        ],
      },
    ]);

  }  
}


interface Country {
  id: number;
  name: string;
}

interface Continent extends Country {  
  id: number;
  name: string;
  countries: Country[];
}
 
