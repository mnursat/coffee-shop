import { Injectable, signal } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AuthService {
  public isAuthenticated = signal(false);

  constructor() {
    this.checkAuth();
  }

  // Проверка наличия валидной jwt cookie через API
  public async checkAuth() {
    try {
      const res = await fetch('http://localhost:5001/users/check-auth', {
        method: 'GET',
        credentials: 'include',
      });
      this.isAuthenticated.set(res.ok);
    } catch {
      this.isAuthenticated.set(false);
    }
  }

  public loginSuccess() {
    this.isAuthenticated.set(true);
  }

  public async logout() {
    await fetch('http://localhost:5001/users/logout', {
      method: 'POST',
      credentials: 'include',
    });
    await this.checkAuth();
  }
}
