import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UsersService, UpdateUserDto, UserDto } from 'src/app/services/users.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit{
userId!: number;
  user!: UpdateUserDto;

  constructor(
    private route: ActivatedRoute,
    private usersService: UsersService,
    public router: Router
  ) {}

  ngOnInit(): void {
    this.userId = Number(this.route.snapshot.paramMap.get('id'));
    this.usersService.getUserById(this.userId).subscribe({
      next: (data: UserDto) => {
        this.user = {
          userId: data.userId,
          nombres: data.nombres,
          apellidoPaterno: data.apellidoPaterno,
          apellidoMaterno: data.apellidoMaterno,
          telefono: data.telefono,
          email: data.email,
          activo: data.activo,
          passwordHash: data.passwordHash
        };
      },
      error: err => console.error(err)
    });
  }

  submit(): void {
    this.usersService.updateUser(this.userId, this.user).subscribe(() => {
      this.router.navigate(['/users']);
    });
  }
}
