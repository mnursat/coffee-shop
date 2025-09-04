import {Component, Input} from '@angular/core';

@Component({
  selector: 'svg[icon]',
  imports: [],
  template: '<svg:use [attr.href]="href"></svg:use>',
  styles: ''
})
export class SvgIcon {
  @Input() icon = '';

  get href() {
    return `/assets/svg/icons.svg#${this.icon}`
  }
}
