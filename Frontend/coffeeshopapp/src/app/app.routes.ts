import { Routes } from '@angular/router';
import { Home } from './home/home';
import { About } from './about/about';
import { Coffees } from './coffees/coffees';
import { Register } from './auth/register/register';
import { Login } from './auth/login/login';

export const routes: Routes = [
    { path: '', component:  Home},
    { path: 'home', component:  Home},
    { path: 'about', component:  About},
    { path: 'coffees', component:  Coffees},
    { path: 'register', component:  Register},
    { path: 'login', component:  Login},
];
