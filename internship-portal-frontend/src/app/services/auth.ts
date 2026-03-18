// import { Injectable } from '@angular/core';

// @Injectable({
//   providedIn: 'root',
// })
// export class Auth {}
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '../config/api';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Auth {

  constructor(private http: HttpClient) {}

  login(data: any): Observable<any> {
    return this.http.post(`${API_URL}/auth/login`, data);
  }

  registerStudent(data: any) {

  return this.http.post(`${API_URL}/auth/register-student`, data);

}

registerCompany(data: any) {

  return this.http.post(`${API_URL}/auth/register-company`, data);

}
}
