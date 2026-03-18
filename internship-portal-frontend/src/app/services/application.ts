import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '../config/api';

@Injectable({
  providedIn: 'root'
})
export class Application {

  constructor(private http: HttpClient) {}

  apply(internshipId: number) {

    const token = localStorage.getItem('token');

    return this.http.post(
      `${API_URL}/application/apply?internshipId=${internshipId}`,
      {},
      {
        headers: {
          Authorization: `Bearer ${token}`
        }
      }
    );

  }

}
