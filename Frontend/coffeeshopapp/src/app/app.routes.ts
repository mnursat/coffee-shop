import { Routes } from '@angular/router';
import { Home } from './home/home';
import { About } from './about/about';
import { Coffees } from './coffees/coffees';

export const routes: Routes = [
    { path: '', component:  Home},
    { path: 'home', component:  Home},
    { path: 'about', component:  About},
    { path: 'coffees', component:  Coffees},
];
