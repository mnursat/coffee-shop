import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
    selector: 'app-login',
    imports: [RouterLink, ReactiveFormsModule],
    templateUrl: './login.html',
    styles: [],
})
export class Login {
    public form: any;
    public showPassword = false;

    public validationError = '';

    public isPasswordVisible() {
        return this.showPassword;
    }

    public togglePassword() {
        this.showPassword = !this.showPassword;
    }

    constructor(private fb: FormBuilder, private router: Router, private auth: AuthService) {
        this.form = this.fb.group({
            email: ['', [Validators.required, Validators.email]],
            password: ['', [Validators.required, Validators.minLength(4)]],
        });
    }

    public onSubmit() {
        if (this.form.invalid) {
            this.validationError = 'Неправильный пароль или имя пользователя';
            return;
        }

        const { email, password } = this.form.value;

        fetch('http://localhost:5001/users/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ email, password }),
            credentials: 'include'
        })
            .then(async (res) => {
                if (!res.ok) {
                    const data = await res.json().catch(() => null);
                    this.validationError = data?.message || 'Ошибка входа';
                    return;
                }
                this.validationError = '';
                await this.auth.loginSuccess();
                this.router.navigate(['/']);
            })
            .catch(() => {
                this.validationError = 'Ошибка соединения с сервером';
            });
    }
}
