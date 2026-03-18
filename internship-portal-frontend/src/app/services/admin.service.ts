import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '../config/api';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) {}

  getStats() {

    const token = localStorage.getItem('token');

    return this.http.get(`${API_URL}/dashboard/stats`, {
      headers: {
        Authorization: `Bearer ${token}`
      }
    });

  }
  getApplicationsByDepartment() {

  const token = localStorage.getItem('token');

  return this.http.get(`${API_URL}/dashboard/applications-by-department`, {
    headers: {
      Authorization: `Bearer ${token}`
    }
  });

}

}
