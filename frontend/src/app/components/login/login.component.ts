import { Component } from '@angular/core';
import { AuthService, LoginDto } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginData: LoginDto = {
    email: '',
    password: '',
    userName: ''
  };

  constructor(private authService: AuthService, private router: Router) {}

  onLogin() {
    this.authService.login(this.loginData).subscribe({
      next: (response) => {
        console.log('Login exitoso', response);
        localStorage.setItem('token', response.token);
        // Aquí puedes redirigir al dashboard
         this.router.navigate(['/users']);
      },
      error: (err) => {
        console.error('Error de login', err);
        // Muestra un mensaje de error al usuario
      }
    });
    }

}
