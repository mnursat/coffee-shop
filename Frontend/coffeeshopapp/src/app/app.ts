import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Header } from './shared/header/header';
import { Footer } from './shared/footer/footer';

@Component({
    selector: 'app-root',
    imports: [Header, Footer, RouterOutlet],
    template: `
        <app-header />
        <router-outlet />
        <app-footer />
    `,
    styles: [],
})
export class App {
    protected readonly title = signal('coffeeshopapp');
}
