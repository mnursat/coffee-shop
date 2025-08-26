import { Component, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-header',
  imports: [RouterLink],
  templateUrl: './header.html',
  styles: []
})
export class Header {
  readonly isMenuOpen = signal(false);

  constructor(public auth: AuthService, private router: Router) {
    this.auth.checkAuth();
  }

  openMenu() {
    this.isMenuOpen.set(true);
  }

  closeMenu() {
    this.isMenuOpen.set(false);
  }

  async logout() {
    await this.auth.logout();
    this.router.navigate(['/login']);
  }
}
