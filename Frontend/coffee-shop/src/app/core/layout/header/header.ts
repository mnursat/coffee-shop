import { Component } from '@angular/core';
import {RouterLink} from '@angular/router';
import {SvgIcon} from '../../../shared/components/svg-icon/svg-icon';

@Component({
  selector: 'app-header',
  imports: [
    RouterLink,
  ],
  templateUrl: './header.html',
  styleUrl: './header.scss'
})
export class Header {

}
