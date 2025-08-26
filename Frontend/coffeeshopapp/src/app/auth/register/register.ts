import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ReactiveFormsModule, FormBuilder, Validators, ValidationErrors, AbstractControl } from '@angular/forms';

@Component({
    selector: 'app-register',
    imports: [RouterLink, ReactiveFormsModule],
    templateUrl: './register.html',
    styles: [],
})
export class Register {
    public showPassword = false;
    public showConfirmPassword = false;

    public passwordError = '';

    public form;

    constructor(private fb: FormBuilder, private router: Router) {
        this.form = this.fb.group({
            username: ['', Validators.required],
            email: ['', [Validators.required, Validators.email]],
            password: ['', [Validators.required, Validators.minLength(4)]],
            confirm: ['', [Validators.required, Validators.minLength(4)]],
        }, { validators: Register.passwordsMatchValidator });
    }

    public static passwordsMatchValidator(form: AbstractControl): ValidationErrors | null {
        const password = form.get('password')?.value;
        const confirm = form.get('confirm')?.value;
        return password === confirm ? null : { mismatch: true };
    }

    public isPasswordVisible() {
        return this.showPassword;
    }

    public isConfirmPasswordVisible() {
        return this.showConfirmPassword;
    }

    public togglePassword() {
        this.showPassword = !this.showPassword;
    }

    public toggleConfirmPassword() {
        this.showConfirmPassword = !this.showConfirmPassword;
    }

    public onSubmit() {
        if (this.form.invalid) {
            if (this.form.errors?.['mismatch']) {
                this.passwordError = 'Пароли не совпадают';
            } else {
                this.passwordError = 'Проверьте правильность заполнения формы';
            }
            return;
        }

        const { username, email, password } = this.form.value;

        fetch('http://localhost:5001/users/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ username, email, password }),
        })
            .then(async res => {
                if (!res.ok) {
                    const data = await res.json().catch(() => null);
                    this.passwordError = data?.message || 'Ошибка регистрации';
                    return;
                }
                this.passwordError = '';
                this.router.navigate(['/login']);
            })
            .catch(() => {
                this.passwordError = 'Ошибка соединения с сервером';
            });
    }
}
