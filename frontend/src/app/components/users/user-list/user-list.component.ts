import { Component, OnInit } from '@angular/core';
import { UsersService, UserDto } from 'src/app/services/users.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit{
  users: UserDto[] = [];

  constructor(private usersService: UsersService, private router: Router) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.usersService.getAllUsers().subscribe({
      next: (data) => this.users = data,
      error: (err) => console.error(err)
    });
  }

  goToCreate(): void {
    this.router.navigate(['/users/create']);
  }

  goToEdit(id: number): void {
    this.router.navigate([`/users/edit/${id}`]);
  }

  deactivate(id: number): void {
    this.usersService.deactivateUser(id).subscribe(() => this.loadUsers());
  }
}
