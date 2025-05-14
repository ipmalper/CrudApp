import { Component } from '@angular/core';
import { AuthService, RegisterDto } from 'src/app/services/auth.service';
import { Router } from '@angular/router'; 
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerData: RegisterDto = {
    email: '',
    password: '',
    userName: ''
  };

  constructor(private authService: AuthService, private router: Router) {}

  onRegister() {
    this.authService.register(this.registerData).subscribe({
      next: () => {
        alert('Registro exitoso. Ahora puedes iniciar sesión.');
        this.router.navigate(['/login']); // O redirige a donde tú prefieras
      },
      error: (err) => {
        console.error('Error al registrar', err);
        alert(err.error); // Muestra mensaje del backend
      }
    });
  }

}
