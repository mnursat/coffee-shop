import { Component } from '@angular/core';
import {Header} from '../header/header';
import {Footer} from '../footer/footer';
import {RouterOutlet} from '@angular/router';

@Component({
  selector: 'app-page-layout',
  imports: [
    Header,
    Footer,
    RouterOutlet
  ],
  templateUrl: './page-layout.html',
  styleUrl: './page-layout.scss'
})
export class PageLayout {

}
