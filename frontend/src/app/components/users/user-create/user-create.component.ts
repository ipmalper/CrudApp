import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UsersService, CreateUserDto } from 'src/app/services/users.service';


@Component({
  selector: 'app-user-create',
  templateUrl: './user-create.component.html',
  styleUrls: ['./user-create.component.css']
})
export class UserCreateComponent {
  user: CreateUserDto = {
    nombres: '',
    apellidoPaterno: '',
    apellidoMaterno: '',
    telefono: '',
    email: '',
    passwordHash: ''
  };

  constructor(private usersService: UsersService, public router: Router) {}

  submit(): void {
    this.usersService.createUser(this.user).subscribe({
      next: () => this.router.navigate(['/users']),
      error: err => console.error(err)
    });
  }
}
