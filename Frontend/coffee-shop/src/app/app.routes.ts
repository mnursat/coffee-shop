import { Routes } from '@angular/router';
import {PageLayout} from './core/layout/page-layout/page-layout';
import {Home} from './core/home/home';

export const routes: Routes = [
  {path: '', component: PageLayout, children: [
      {path: '', component: Home}
    ]}
];
