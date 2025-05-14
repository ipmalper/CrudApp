import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

export interface LoginDto {
  email: string;
  password: string;
  userName: string;
}

export interface RegisterDto {
  email: string;
  password: string;
  userName: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly apiUrl = 'http://localhost:5142/api/Auth';

  constructor(private http: HttpClient) { }

  login(data: LoginDto): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, data);
  }

  register(data: RegisterDto): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, data);
  }
}
